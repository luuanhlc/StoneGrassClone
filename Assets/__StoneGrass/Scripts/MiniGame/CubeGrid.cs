using UnityEngine;
using MoreMountains.Feedbacks;
public class CubeGrid : MonoBehaviour
{
    public MMFeedbacks feedBack;

    public static CubeGrid Ins;

    private void Awake()
    {
        Ins = this;
    }
}
