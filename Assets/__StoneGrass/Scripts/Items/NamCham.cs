/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NamCham : Items
{

    public NamCham(string name, Vector3 _transform) : base(name, _transform)
    {
        name = this.gameObject.name;
        _transform = this.transform.position;
    }

    private void Start()
    {
        base.name = this.gameObject.name;
        base._transform = this.transform.position;
    }

    void Update()
    {
        base.waveMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        base.UseItem(this.gameObject.name);
    }
}
*/