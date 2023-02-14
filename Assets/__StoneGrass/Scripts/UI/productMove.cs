using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;

public class productMove : MonoBehaviour
{
    public float t;
    [HideInInspector] public Transform g;
    private bool _canMove;
    Rigidbody _rb;

    public bool selling;
    public bool take;
    private int touchTime;

    float speed;


    private void Awake()
    {
        speed = Random.Range(.03f, .04f);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || selling)
            return; 
        if (touchTime < 1 && !Items.Ins._isMagnet)
            return;
        g = PlayerController.Ins.takePos.transform;
        take = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        touchTime += 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Plane") || collision.collider.CompareTag("GrassPlace"))
        {
            a = transform.position;
            _canMove = true;
        }
    }
    public void Nay(Vector3 pos)
    {
        transform.DORotateQuaternion(Random.rotation, 1);
        Vector3 dic = (pos - this.transform.position) + pos;
        dic.y = .1f;
        transform.DOJump(dic, 1f, 1, .75f);
    }
    private void Update()
    {
        if (!take || g == null)
        {
            t = 0;
            if (transform.position.y <= -3)
                gameObject.SetActive(false);
            return;
        }
        if (selling)
        {
            MoveToSell(g.transform);
            return;
        }

        if (XepProduct.Isn.Succhua == XepProduct.Isn.numberofProduct)
        {
            VanFlash.Ins.SetActive(true);
            take = false;
            return;
        }
        
        movetoPos(g.transform);
    }
    public Vector3 a;
    bool _done;

    private void MoveToSell(Transform other)
    {
        t = Mathf.Min(1, t += .15f);
        transform.position = QuadraticCurve(a, other.position, t);

        if (transform.position == other.position)
        {
            transform.localPosition = XepProduct.Isn.LocalNextPos;
            take = false;
            g = null;
            XepProduct.Isn._nextpos.transform.localPosition = XepProduct.Isn.LocalNextPos;
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins.moneySound, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
            this.gameObject.SetActive(false);
        }
    }

    private void movetoPos(Transform other)
    {
        if (a == Vector3.zero)
            a = transform.position;
        t = Mathf.Min(1, t += .09f);
        transform.position =  QuadraticCurve(a, other.position, t);
        if(transform.position == other.position)
        {
            transform.parent = PlayerController.Ins.van.transform;
            transform.localPosition = XepProduct.Isn.LocalNextPos;
            take = false;
            g = null;
            Nextpos();
            XepProduct.Isn._nextpos.transform.localPosition = XepProduct.Isn.LocalNextPos;
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins.takeSound, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            this.gameObject.SetActive(false);
        }
    }

    void Nextpos()
    {
        XepProduct.Isn.taked(this.gameObject);
        if (XepProduct.Isn.nz == 2)
        {
            if (XepProduct.Isn.nx == 4)
            {
                XepProduct.Isn.LocalNextPos = new Vector3(XepProduct.Isn.fristPos.x, XepProduct.Isn.LocalNextPos.y + .2f, XepProduct.Isn.fristPos.z);
                XepProduct.Isn.ny++;
                XepProduct.Isn.nz = XepProduct.Isn.nx = 0;
            }
            else
            {
                XepProduct.Isn.LocalNextPos = new Vector3(XepProduct.Isn.fristPos.x, XepProduct.Isn.LocalNextPos.y, XepProduct.Isn.LocalNextPos.z - .2f);
                XepProduct.Isn.nx++;
                XepProduct.Isn.nz = 0;
            }
        }
        else
        {
            XepProduct.Isn.LocalNextPos = new Vector3(XepProduct.Isn.LocalNextPos.x + .4f, XepProduct.Isn.LocalNextPos.y, XepProduct.Isn.LocalNextPos.z);
            XepProduct.Isn.nz++;
        }

        XepProduct.Isn.numberofProduct += 1;
        _done = true;
    }

    public Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;
    }

    public Vector3 QuadraticCurve(Vector3 a, Vector3 c, float t)
    {
         Vector3 mid = (a + c) / 2;
         mid.y += 5f;


        Vector3 p0 = Lerp(a, mid, t);
        Vector3 p1 = Lerp(mid, c, t);

        return Lerp(p0, p1, t);
    }
}
