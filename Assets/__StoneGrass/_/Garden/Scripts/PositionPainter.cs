using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

namespace PT.Garden
{
    public class PositionPainter : MonoBehaviour
    {
        [SerializeField] public Brush _removeBrush;
        //public InkCanvas _bendCanvas, _removeCanvas;
        public List<InkCanvas> _removeCanvas;
        public bool _isPainting = false;
        public LayerMask lm;

        public static PositionPainter Ins;
        private void Awake()
        {
            Ins = this;
        }
        public void ChangerScale(float scale)
        {
            _removeBrush.brushScale = scale;
        }
        private void FixedUpdate()
        {
            if (_isPainting && Time.frameCount % 2 == 0)
            {
                for (int i = 0; i < _removeCanvas.Count; i++)
                {
                    if (_removeCanvas[i] != null)
                    {
                        _removeCanvas[i].Paint(
                            _removeBrush,
                            new Vector3(
                                transform.position.x,
                                _removeCanvas[i].transform.position.y,
                                transform.position.z
                            )
                        );
                    }
                }
            }
        }
    }
}