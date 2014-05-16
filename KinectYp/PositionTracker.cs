using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KinectYp.Erkenner;
using KinectYp.Erkenner.Bewegungen;
using KinectYp.Erkenner.SpezialAngriffe.Ryu;
using KinectYp.Erkenner.StandardAngriffe;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace KinectYp {
    /// <summary>
    /// Oberklasse, welche die Erkennung durhc eine Liste Erkenener verwaltet.
    /// </summary>
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
                    return "Sorry something went wrong with your Kinect. Probably it's not connected.";
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="KinectYp.PositionTracker.KinectNotConnectedException"></exception>
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

            _sensor.Start();

            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                _speechEngine = new SpeechRecognitionEngine(ri.Id);

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


        /// <summary>
        /// Gets the kinect recognizer.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Discovers the sensor.
        /// </summary>
        private void DiscoverSensor() {
                // Find first sensor that is connected.
                foreach (KinectSensor sensor in KinectSensor.KinectSensors) {
                    if (sensor.Status == KinectStatus.Connected) {
                        _sensor = sensor;
                        break;
                    }
                }   
        }


        /// <summary>
        /// Wird jedes mal ausgeführt, wenn alle Frames Ready sind.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AllFramesReadyEventArgs"/> instance containing the event data.</param>
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


        /// <summary>
        /// Gets the first skeleton.
        /// </summary>
        /// <param name="e">The <see cref="AllFramesReadyEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Updates the history.
        /// </summary>
        /// <param name="latest">The latest.</param>
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
                            Program.Form1.setlblNormal("Rechts");
                        }
                        else
                        {
                            Normal = true;
                            Program.Form1.setlblNormal("Links");
                        }
                        break;

                    case "UP":
                        MotionFunctions.SendAction(MotionFunctions.Up());
                        break;

                    case "DOWN":
                        MotionFunctions.SendAction(MotionFunctions.Down());
                        break;

                    case "LEFT":
                        MotionFunctions.SendAction(MotionFunctions.Left());
                        break;

                    case "RIGHT":
                        MotionFunctions.SendAction(MotionFunctions.Right());
                        break;

                    case "SELECT":
                        MotionFunctions.SendAction(MotionFunctions.LKick());
                        break;
                }
            }
        }
    }

}
