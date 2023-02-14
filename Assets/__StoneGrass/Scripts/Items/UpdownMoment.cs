using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdownMoment : MonoBehaviour
{
    [SerializeField] float amp;
    [SerializeField] float freg;
    private Vector3 _transform;

    private void Start()
    {
        _transform = this.transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance._isIsland)
            return;
        waveMovement();
    }

    public void waveMovement()
    {
        transform.position = new Vector3(_transform.x, Mathf.Sin(Time.time * freg) * amp + _transform.y, _transform.z);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 60f);
    }
}
