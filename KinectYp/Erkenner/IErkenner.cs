using Microsoft.Kinect;

namespace KinectYp.Erkenner
{

    public interface IErkenner
    {
        ErkennerStatus Pruefe(Skeleton[] history);
        string GetDebugName();

    }
}
