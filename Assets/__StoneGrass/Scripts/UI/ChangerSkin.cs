using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerSkin : MonoBehaviour
{
    [SerializeField] private MeshFilter body;
    [SerializeField] private MeshRenderer bodyMaterial;
    [SerializeField] private MeshFilter Wheels;
    [SerializeField] private MeshRenderer wheelMaterial;
    
    #region SinggleTon
    public static ChangerSkin Ins;

    private void Awake()
    {
        Ins = this;
    }
    #endregion

    public void Change(int id, Mesh body, Vector3 bodyScale, Material[] mtr, Mesh Wheels, GameObject mt)
    {
        HumanController.Ins.SetDriveType((float)(id));
        this.body.mesh = body;
        this.bodyMaterial.materials = mtr;

        this.body.transform.localScale = bodyScale;
        ChangeWheel(Wheels, mt);
    }

    public void ChangeWheel(Mesh wheels, GameObject mt)
    {
        this.Wheels.mesh = wheels;
        this.wheelMaterial.materials = mt.GetComponent<MeshRenderer>().sharedMaterials;
    }
}
