using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class product : MonoBehaviour
{
    XepProduct set;

    Rigidbody _rb;

    Transform _truckPos;
    private bool done = false;

    private bool taked = false;
    private GameObject _van;
    private bool doneup = false;

    [SerializeField] private float speed = 2f;
    [SerializeField] private bool xep = false;

    [Header("Kich thuoc thung")]
    [SerializeField] public float ktX = 2f;
    [SerializeField] public float ktZ = 4f;

    private float maxX;
    private float maxZ;

    [SerializeField] private bool _isFull = false;

    [SerializeField] private float stayTimetoTake = 3f;

    

    public bool selling;
    public string sellType;

    //public static Transform sellPos;

    #region Singleton
    public static product Isn;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        Isn = this;
    }
    #endregion

    private void Start()
    {
        set = XepProduct.Isn;

        maxX = set.fristPos.x + transform.localScale.x * ktX;
        maxZ = set.fristPos.z - transform.localScale.z * ktZ;
    }

    private void Update()
    {
        if (_rb.velocity.y <= -10)
            this.gameObject.SetActive(false);
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        touchTime += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (set.Succhua <= set.numberofProduct)
            _isFull = true;
        else
            _isFull = false;

        if (other.CompareTag("Player") && !taked && touchTime >= 1 && !_isFull || other.CompareTag("Player") && Items.Ins._isMagnet)
            taked = true;
    }
    //float t = 0;



    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") && _isFull)
            return;
    }*/

    /*IEnumerator staytime(float time)
    {
        Debug.Log("Taking");
        yield return Yielders.Get(time);
        taked = true;
    }*/

    /*void bechild()
    {
        transform.SetParent(_van.transform);
        move();
    }*/

    /*void goUp()
    {
        if (set.numberofProduct == set.Succhua)
        {
            transform.SetParent(null);
            _rb.useGravity = true;
            _rb.isKinematic = false;
            return;
        }
        if (!doneup && !done)
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, set.LocalNextPos.y + 5f, transform.localPosition.z), Random.Range(.4f, 1f));
            if (transform.localPosition.y >= set.LocalNextPos.y + 4f)
                doneup = true;
        }
        else if (doneup)
            bechild();
    }*/
    public void Nay()
    {
        _rb.AddForce(transform.up * 70f);
        transform.rotation = Random.rotation;
    }
/*
    void move()
    {
        if (done)
            return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, set.LocalNextPos, Random.Range(.4f, 1f));
        transform.rotation = _truckPos.rotation;

        _rb.useGravity = false;
        _rb.isKinematic = true;
        stopmove();
    }

    void stopmove()
    {
        if (transform.localPosition == set.LocalNextPos)
        {
            Nextpos();
            //this.gameObject.SetActive(false);
        }
    }*/

    

    public void sell()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _rb.AddForce(transform.up * 70f);
        StartCoroutine(ToSellPos(.4f));
    }

    IEnumerator ToSellPos(float time)
    {
        yield return Yielders.Get(time);
        _rb.useGravity = false;
        _rb.isKinematic = true;
        //transform.position = Vector3.MoveTowards(transform.position, sellPos.position, .5f);
        //if (transform.position == sellPos.position)
            this.gameObject.SetActive(false);
    }
}