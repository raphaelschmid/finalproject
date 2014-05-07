using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Drawing;

namespace KinectYp {
    public class PositionTracker {
        private KinectSensor _sensor;
        bool _closing = false;
        private const int _skeletonCount = 6;
        Skeleton[] _allSkeletons = new Skeleton[_skeletonCount];
        public delegate void PunchEventHandler(object sender, string message);
        public delegate void StayEventHandler(object sender);
        public delegate void PositionChangedEventHandler(object sender, Skeleton p);
        public event PunchEventHandler Punched;
        public event StayEventHandler Stay;
        public event PositionChangedEventHandler PositionChanged;
        private List<IErkenner> erkenners;

        private const int HistorySize = 30;
        Skeleton[] HistorySkeletons = new Skeleton[HistorySize];
        private long historyFramesAdded = 0;

        public PositionTracker() {

        }

        public void Init() {

            //erkenner hizufügen
            erkenners = new List<IErkenner>();
            erkenners.Add(new RechtsLaufen());
            erkenners.Add(new LinksLaufen());
            erkenners.Add(new Kick());

  
            DiscoverSensor();

            if (_sensor == null) {
                // Todo: Throw an exception or something
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
            catch (System.IO.IOException) {
                // Kinect wird bereits von einer anderen Anwendung verwendet
                
            }
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


            if (HistorySkeletons.Last() != null)
            {
                foreach (var erkenner in erkenners)
                {
                    if (erkenner.Pruefe(HistorySkeletons))
                    {
                        Punched(this, erkenner.GetMessage());
                    }

                }
            }
            PositionChanged(this, first);
            //asadd
            
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
            historyFramesAdded++;


        }
    }
}
