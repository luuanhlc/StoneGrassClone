using UnityEngine;

[ExecuteInEditMode]
public class MobileFrost : MonoBehaviour
{
    [Range(0, 1)]
    public float Vignette = 1f;
    [Range(0, 1)]
    public float Transparency = 1f;

    public Texture2D frost;
    public Texture2D normal;

    static readonly int vignetteString = Shader.PropertyToID("_Vignette");
    static readonly int transparencyString = Shader.PropertyToID("_Transparency");
    static readonly int frostString = Shader.PropertyToID("_FrostTex");
    static readonly int normalString = Shader.PropertyToID("_BumpTex");

    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture(frostString, frost);
        material.SetTexture(normalString, normal);
        material.SetFloat(transparencyString, Transparency);
        material.SetFloat(vignetteString, Vignette * 2 - 1);
        Graphics.Blit(source, destination, material, 0);
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 5 != 0 || GameManager.Instance._isIsland || frost == null)
            return;

        if (PlayerController._isMove)
        {
            Vignette = Mathf.Max(.2f, Vignette - .005f);
            Transparency = Mathf.Min(1, Transparency + .005f);
            return;
        }
        else
        {
            Vignette = Mathf.Min(.4f, Vignette + .001f);
            Transparency = Mathf.Max(0, Transparency - .005f);
            return;
        }
    }
}
