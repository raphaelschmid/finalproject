using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{
    class SkeletonHistory
    {
        public Skeleton[] History;
        public HashSet<HashSet<double>> Kennzahlen; 

        private long _addedSkeletonCount;
        public SkeletonHistory(int historySize)
        {
            History = new Skeleton[historySize];
        }

        public void Push(Skeleton skeleton)
        {
            for (int i = 0; i < History.Length - 1; i++)
            {
                History[i] = History[i + 1];
            }
            History[0] = skeleton;
            _addedSkeletonCount++;
        }

        public bool IsReady()
        {
            return _addedSkeletonCount > History.Length;
        }
    }
}
