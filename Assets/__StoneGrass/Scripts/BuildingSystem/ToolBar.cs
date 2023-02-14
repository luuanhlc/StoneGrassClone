using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using MoonAntonio.UI;

public class ToolBar : MonoBehaviour
    {
    public GameObject parent;
    
    public ParticleSystem smoke;

    public GameObject body;

    public MMFeedbacks PlaceFeedBack;

    public void RotateParent()
    {
        parent.GetComponent<ObjectToPlace>().Rotate();
    }

    public void Close()
    {
        PlayerDataManager.SetTotalStar(PlayerDataManager.GetTotalStar() + 5);
        UiController.Ins.UiTop._isBuild = false;
        UiController.Ins.UiTop.setButton();
        Destroy(parent.gameObject);
    }

    public void DatClick()
    {
        if (!BuildingSystem.Ins.CanBePlace(BuildingSystem.Ins.objectToPlace))
            return;
        Destroy(this.gameObject.GetComponent<Interactivo>());
        parent.GetComponent<BuildingDrag>().DatComplete();
        smoke.Play();
        PlaceFeedBack.PlayFeedbacks();
        BuildingSystem.Ins.Dat();
        UiTop.Ins.resetUI();
    }
}
