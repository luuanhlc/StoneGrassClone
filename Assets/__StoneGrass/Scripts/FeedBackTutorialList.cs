using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class FeedBackTutorialList : MonoBehaviour
{
    public CubeGrid[] _gridCubeFeedBacks;

    public List<MMFeedbacks> feedBack;

    #region Singleton
    public static FeedBackTutorialList Ins;

    private void Awake()
    {
        Ins = this;
    }
    #endregion

    public void RunGridFeedBack()
    {
        for(int i =0; i < _gridCubeFeedBacks.Length; i++)
        {
            //_gridCubeFeedBacks[i].PlayFeedbacks();
            _gridCubeFeedBacks[i].feedBack.PlayFeedbacks();
        }
    }

    public void actionFeedBacks(int crrStep)
    {
        if (feedBack.Count > crrStep)
            feedBack[crrStep].PlayFeedbacks();
    }
}
