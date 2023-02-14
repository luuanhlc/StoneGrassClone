using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Destruction : MonoBehaviour
{
    [SerializeField] private GameObject perfabBox;
    [SerializeField] private GameObject DimondItems;
    [SerializeField] private GameObject NamCham;
    [SerializeField] private GameObject BiggerSaw;
    public Rigidbody _rb;
    public ParticleSystem _destroyFX;

    GameObject s;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (!this.gameObject.CompareTag("Box"))
        {
            Destroy(this.gameObject);
            return;
        }
        StartCoroutine(destroy(.6f));
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 5 != 0)
            return;

        if (_rb.velocity.y < -4f)
            this.gameObject.SetActive(false);
    }

    IEnumerator destroy(float time)
    {
        yield return Yielders.Get(time);
        Instantiate(perfabBox, transform.position, transform.rotation);
        int r = (int)Random.Range(0, 2);
       //ebug.Log(r);
        if (r>=1)
        {
            int i = (int)Random.RandomRange(0, 2);
            GameObject items = Instantiate(RandomItem(i), new Vector3(transform.position.x, transform.position.y + .8f, transform.position.z), Quaternion.Euler(0, 0, 0));
            items.name = RandomItem(i).name;
        }
        _destroyFX.Play();
        Destroy(this.gameObject);
    }

    private GameObject RandomItem(int i)
    {
        switch(i){
            case 0:
                s = DimondItems;
                break;
            case 1:
                s = NamCham;
                break;
            case 2:
                s = BiggerSaw;
                break;
            case 3:
                break;
        }
        return s;
    }
}
