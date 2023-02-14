using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitonPos : MonoBehaviour
{
    private Canvas _canvas;
    private Image _image;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        
    }

    private void Draw()
    {
        var CanvasRect = _canvas.GetComponent<RectTransform>().rect;
        var canvasWidth = CanvasRect.width;
        var canvasHeight = CanvasRect.height;

        _image.rectTransform.sizeDelta = new Vector2(canvasWidth, canvasHeight);
    }
}
