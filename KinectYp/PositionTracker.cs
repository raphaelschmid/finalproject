using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KinectYp.Erkenner;
using KinectYp.Erkenner.Bewegungen;
using KinectYp.Erkenner.SpezialAngriffe;
using KinectYp.Erkenner.SpezialAngriffe.Ryu;
using KinectYp.Erkenner.StandardAngriffe;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace KinectYp {
    public class PositionTracker {
        private KinectSensor _sensor;
        private SpeechRecognitionEngine _speechEngine;
        private const int SkeletonCount = 6;
        public delegate void PunchEventHandler(object sender, string message);
        public delegate void StayEventHandler(object sender);
        public delegate void PositionChangedEventHandler(object sender, Skeleton p);
        public event PunchEventHandler Punched;
        public event StayEventHandler Stay;
        public event PositionChangedEventHandler PositionChanged;
        public List<IErkenner> Erkenners;
        public bool Normal = true;

        private const int HistorySize = 30;
        Skeleton[] _historySkeletons = new Skeleton[HistorySize];

        private class KinectNotConnectedException : Exception
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
            Erkenners = new List<IErkenner>
            {
                new RechtsLaufen(),
                new LinksLaufen(),
                new Kick(),
                new Punch(),
                new Ducken(),
                new Jump(),
                new RyuHadouken(),
                new RyuShoryuken()
            };

            DiscoverSensor();

            if (_sensor == null) {
                throw new KinectNotConnectedException();
            }




            // Alle Funktionen aktivieren
            _sensor.SkeletonStream.Enable();
            _sensor.AllFramesReady += KinectAllFramesReady;

            try {
                _sensor.Start();
            }
            catch (IOException) {
                // Kinect wird bereits von einer anderen Anwendung verwendet
                
            }
            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                _speechEngine = new SpeechRecognitionEngine(ri.Id);

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
                    _speechEngine.LoadGrammar(g);
                }

                _speechEngine.SpeechRecognized += SpeechRecognized;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                _speechEngine.SetInputToAudioStream(
                    _sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                _speechEngine.RecognizeAsync(RecognizeMode.Multiple);
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
            //Gibt ein Koerper
            Skeleton first = GetFirstSkeleton(e);

            UpdateHistory(first);


            if (first == null)
            {
                return;
            }

            string display = "";

            if (_historySkeletons.Last() != null)
            {

                foreach (var erkenner in Erkenners)
                {
                    try
                    {
                        display += erkenner.GetDebugName().PadRight(20) + erkenner.Pruefe(_historySkeletons) + "\n";
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
                Skeleton[] latestSkeletons = new Skeleton[SkeletonCount];

                skeletonFrameData.CopySkeletonDataTo(latestSkeletons);
                Skeleton first = latestSkeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                UpdateHistory(first);

                return first;
            }
        }

        private void UpdateHistory(Skeleton latest)
        {

            Skeleton[] tempHistorySkeletons = new Skeleton[HistorySize];

            for (int i = 1; i < _historySkeletons.Length; i++)
            {
                if (_historySkeletons[i - 1] != null)
                {
                    tempHistorySkeletons[i] = _historySkeletons[i - 1];
                }
                
            }

            tempHistorySkeletons[0] = latest;

            _historySkeletons = tempHistorySkeletons;


        }


        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double confidenceThreshold = 0.3;


            if (e.Result.Confidence >= confidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "SWITCH":
                        if (Normal)
                        {
                            Normal = false;
                            Program.Form1.setlblNormal("False");
                        }
                        else
                        {
                            Normal = true;
                            Program.Form1.setlblNormal("True");
                        }
                        break;

                   
                }
            }
        }
    }

}
