
using UnityEngine;


public class fillCauCanvas : MonoBehaviour
{
        
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
