using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialModifier : MonoBehaviour
{
    public bool isRandom = true;
    public Color mainColor = Color.HSVToRGB(0, 25, 87);

    [Sirenix.OdinInspector.Button]
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!isRandom)
        {
            var propertyBlock = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(SkinChanger.MAIN_COLOR_ID, mainColor);
            meshRenderer.SetPropertyBlock(propertyBlock);
        } else
        {
            var newColor = Random.ColorHSV(
                0, 1, 0.25f, 0.25f, 0.87f, 0.87f);
            var propertyBlock = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(SkinChanger.MAIN_COLOR_ID, newColor);
            meshRenderer.SetPropertyBlock(propertyBlock);

        }
    }
}
