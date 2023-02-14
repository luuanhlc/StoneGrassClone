using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DotweenMoney : MonoBehaviour
{

    
    private void Start()
    {
        MoveOut();
    }

    public void MoveOut()
    {
        transform.DOMove(new Vector3(this.transform.position.x + Random.Range(-150, 150), this.transform.position.y + Random.Range(-150, 150), this.transform.position.z), Random.Range(.3f, .5f)).OnComplete(MoveIn);
    }

    public void MoveIn()
    {
        switch (this.gameObject.name)
        {
            case "imgMoney(Clone)":
                transform.DOLocalMove(InGame.Ins.moneyIcon.transform.localPosition, Random.Range(.1f, 2f)).OnComplete(AddGold);
                break;
            case "imgDimond(Clone)":
                transform.DOLocalMove(InGame.Ins.dimondIcon.transform.localPosition, Random.Range(.1f, 2f)).OnComplete(AddGold);
                break;
        }
    }


    public void AddGold()
    {
        switch (this.gameObject.name)
        {
            case "imgMoney(Clone)":
                GameManager.Instance.Profile.AddGold(Random.Range(400, 700));
                InGame.Ins.moneyIcon.transform.DOScale(new Vector3(1.5f, 1.5f, 0f), .3f).SetEase(Ease.InOutBack);
                InGame.Ins.moneyIcon.transform.DOScale(new Vector3(1, 1, 1), .2f).SetDelay(.3f).SetEase(Ease.InOutBack);

                UiTop.Ins.UpdateUi(0);
                break;
            case "imgDimond(Clone)":
                GameManager.Instance.Profile.AddDimond(1);
                InGame.Ins.dimondIcon.transform.DOScale(new Vector3(1.5f, 1.5f, 0f), .3f).SetEase(Ease.InOutBack);
                InGame.Ins.dimondIcon.transform.DOScale(new Vector3(1, 1, 1), .2f).SetDelay(.3f).SetEase(Ease.InOutBack);

                UiTop.Ins.UpdateUi(1);
                break;
        }
        Destroy(this.gameObject);
    }

}
