using System.Collections.Generic;
using UnityEngine;
using System;

public class Rope : MonoBehaviour
{
    public Transform player;

    public LineRenderer rope;
    public LayerMask collMask;
    public GameObject Targer;

    public Material _ropeMaterial;
    private readonly int _Time = Shader.PropertyToID("_Time");

    public List<Vector3> ropePositions { get; set; } = new List<Vector3>();

    //private void Awake() => AddPosToRope(Targer.transform.position);

    public void SetTarget()
    {
        if(ropePositions.Count >= 2)
        {
            rope.SetPosition(0, Targer.transform.position);
            return;
        }
        AddPosToRope(Targer.transform.position);
    }

    private void Update()
    {

        _ropeMaterial.SetFloat(_Time, Time.time);
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();

        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();
    }
        
    private void DetectCollisionEnter()
    {
        RaycastHit hit;
        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out hit, collMask))
        {
            Debug.Log("DetectCollisionEnter");
            ropePositions.RemoveAt(ropePositions.Count - 1);
            AddPosToRope(hit.point);
        }
    }

    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position); //Always the last pos must be the player
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos() => rope.SetPosition(rope.positionCount - 1, player.position);
}