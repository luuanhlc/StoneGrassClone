using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SnapUnPlay : MonoBehaviour
    {
    private Vector3 gridSize = new Vector3(2, 0, 2);

    private void OnDrawGizmos()
    {
        unPlay();
    }
    private void unPlay()
        {
            var postion = new Vector3(
                Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
                transform.position.y,
                Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
                );
            transform.position = postion;
        }
    }
