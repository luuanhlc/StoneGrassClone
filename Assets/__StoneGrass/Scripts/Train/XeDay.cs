using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XeDay : MonoBehaviour
{
    public Transform[] target;

    public bool _isActive = false;

    [SerializeField] private float speed;
    [SerializeField] private int currentPos;
    [SerializeField] public bool _isMoving;

    private Vector3 originPos;

    public bool _isSellAble = true;

    vanOfTrain _trainVan;
    public bool go = false;

    #region Singleton
    public static XeDay Isn;

    private void Awake()
    {
        originPos = transform.position;
        Isn = this;
    }
    #endregion

    private void Start()
    {
        _trainVan = vanOfTrain.Isn;
        updateSpeed();
    }

    public void updateSpeed()
    {
        speed = PlayerDataManager.GetTrainSpeed();
    }

    private void FixedUpdate()
    {
        if (!go)
            return;
        move();
    }
    public void move()
    {
        _isSellAble = false;
       if (transform.position != target[currentPos].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target[currentPos].position, speed * Time.deltaTime);
            transform.LookAt(target[currentPos].position);
        }
        else
       {
           if (currentPos == target.Length - 1)
           {
               go = false;
               transform.position = originPos;
               transform.localRotation = Quaternion.Euler(Vector3.zero);
               currentPos = 0;
               _isSellAble = true;
               _trainVan.resetScale();
           }
              currentPos = (currentPos + 1);
       }
    }
}
