using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectYp.Erkenner.Bewegungen;
using KinectYp.Erkenner.SpezialAngriffe;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.Drawing;
using Microsoft.Speech.Synthesis;

namespace KinectYp {
    public class PositionTracker {
        private KinectSensor _sensor;
        private SpeechRecognitionEngine speechEngine;
        bool _closing = false;
        private const int _skeletonCount = 6;
        public delegate void PunchEventHandler(object sender, string message);
        public delegate void StayEventHandler(object sender);
        public delegate void PositionChangedEventHandler(object sender, Skeleton p);
        public event PunchEventHandler Punched;
        public event StayEventHandler Stay;
        public event PositionChangedEventHandler PositionChanged;
        public List<IErkenner> erkenners;
        public bool normal = true;

        private const int HistorySize = 30;
        Skeleton[] HistorySkeletons = new Skeleton[HistorySize];

        public class KinectNotConnectedException : Exception
        {

            public override string Message
            {
                get
                {
                    return "Sorry something went wrong with your Kinect. Probably its not connected.";
                }
            }
        }

        public void Init() {

            //erkenner hizufügen
            erkenners = new List<IErkenner>();
            erkenners.Add(new RechtsLaufen());
            erkenners.Add(new LinksLaufen());
            erkenners.Add(new Kick());
            erkenners.Add(new Punch());
            erkenners.Add(new Ducken());
            erkenners.Add(new Jump());
            erkenners.Add(new RyuHadouken());
            erkenners.Add(new RyuShoryuken());
  
            DiscoverSensor();

            if (_sensor == null) {
                throw new KinectNotConnectedException();
            }



            //SmoothParameters
            var parameters = new TransformSmoothParameters {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };

            // Alle Funktionen aktivieren
            _sensor.SkeletonStream.Enable();
            _sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(KinectAllFramesReady);

            try {
                _sensor.Start();
            }
            catch (IOException) {
                // Kinect wird bereits von einer anderen Anwendung verwendet
                
            }
            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                speechEngine = new SpeechRecognitionEngine(ri.Id);

                /****************************************************************
                * 
                * Use this code to create grammar programmatically rather than from
                * a grammar file.
                * 
                * var directions = new Choices();
                * directions.Add(new SemanticResultValue("forward", "FORWARD"));
                * directions.Add(new SemanticResultValue("forwards", "FORWARD"));
                * directions.Add(new SemanticResultValue("straight", "FORWARD"));
                * directions.Add(new SemanticResultValue("backward", "BACKWARD"));
                * directions.Add(new SemanticResultValue("backwards", "BACKWARD"));
                * directions.Add(new SemanticResultValue("back", "BACKWARD"));
                * directions.Add(new SemanticResultValue("turn left", "LEFT"));
                * directions.Add(new SemanticResultValue("turn right", "RIGHT"));
                *
                * var gb = new GrammarBuilder { Culture = ri.Culture };
                * gb.Append(directions);
                *
                * var g = new Grammar(gb);
                * 
                ****************************************************************/

                // Create a grammar from grammar definition XML file.
                using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Resource1.SpeechGrammar)))
                {
                    var g = new Grammar(memoryStream);
                    speechEngine.LoadGrammar(g);
                }

                speechEngine.SpeechRecognized += SpeechRecognized;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                speechEngine.SetInputToAudioStream(
                    _sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }



        private void DiscoverSensor() {
           
                // Find first sensor that is connected.
                foreach (KinectSensor sensor in KinectSensor.KinectSensors) {
                    if (sensor.Status == KinectStatus.Connected) {
                        _sensor = sensor;
                        break;
                    }
                }   
        }

        private void KinectAllFramesReady(object sender, AllFramesReadyEventArgs e) {
            if (_closing) {
                return;
            }

            //Gibt ein Koerper
            Skeleton first = GetFirstSkeleton(e);

            UpdateHistory(first);


            if (first == null)
            {
                return;
            }

            string display = "";

            if (HistorySkeletons.Last() != null)
            {

                foreach (var erkenner in erkenners)
                {
                    try
                    {
                        display += erkenner.GetDebugName().PadRight(20) + erkenner.Pruefe(HistorySkeletons) + "\n";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    
                }
            }
            Punched(this, display);
            PositionChanged(this, first);
        }



        private Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame()) {
                if (skeletonFrameData == null) {
                    return null;
                }
                Skeleton[] latestSkeletons = new Skeleton[_skeletonCount];

                skeletonFrameData.CopySkeletonDataTo(latestSkeletons);
                Skeleton first = latestSkeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                UpdateHistory(first);

                return first;
            }
        }

        private void UpdateHistory(Skeleton latest)
        {

            Skeleton[] tempHistorySkeletons = new Skeleton[HistorySize];

            for (int i = 1; i < HistorySkeletons.Length; i++)
            {
                if (HistorySkeletons[i - 1] != null)
                {
                    tempHistorySkeletons[i] = HistorySkeletons[i - 1];
                }
                
            }

            tempHistorySkeletons[0] = latest;

            HistorySkeletons = tempHistorySkeletons;


        }

        /// <summary>
        /// Execute uninitialization tasks.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (null != this._sensor)
            {
                this._sensor.AudioSource.Stop();

                this._sensor.Stop();
                this._sensor = null;
            }

            if (null != this.speechEngine)
            {
                this.speechEngine.SpeechRecognized -= SpeechRecognized;
                this.speechEngine.RecognizeAsyncStop();
            }
        }


        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.3;


            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "CHANGE":
                        if (normal)
                        {
                            normal = false;
                            Program.form1.setlblNormal("False");
                        }
                        else
                        {
                            normal = true;
                            Program.form1.setlblNormal("True");
                        }
                        break;

                   
                }
            }
        }
    }

}
