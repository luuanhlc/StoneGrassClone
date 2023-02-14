using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
public class PopUpReturnHome : UICanvas
{
    private Button ReturnHome;
    public GameObject tutorialCircle;
    private void Start()
    {
        ReturnHome = this.gameObject.GetComponent<Button>();
        ReturnHome.onClick.AddListener(OnClickReturn);
        if (FillCau.Isn != null)
            SetEnabled(!FillCau.Isn.need);
        else
            SetEnabled(true);
    }

    public void SetEnabled(bool _active = false)
    {
        ReturnHome.enabled = _active;
    }
    private void OnClickReturn()
    {
        FollowPath.Ins.go = true;
        FollowPath.Ins.currentPos = FollowPath.Ins.search();
        PlayerController.Ins._isReturnHome = true;
        PlayerController._isMove = true;
        CinemachineBrain _cineBrain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
        if(tutorialCircle != null)
            tutorialCircle.GetComponent<Image>().DOFade(0, .5f).OnComplete(() => Destroy(tutorialCircle));
        _cineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
        _cineBrain.m_BlendUpdateMethod = CinemachineBrain.BrainUpdateMethod.LateUpdate;
        
    }
}
