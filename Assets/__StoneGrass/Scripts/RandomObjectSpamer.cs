using UnityEngine;

public class RandomObjectSpamer : MonoBehaviour
{

    public Transform center;
    public GameObject KeyPrefab;
    public int min, max;

        // Use this for initialization
    void Start()
    {
        for(int i = 0; i < (int)(Random.RandomRange(min, max)); i++)
        {
            SpawnKey();
        }
    }

    public void SpawnKey()
    {

        Vector3 pos = center.position + new Vector3(Random.Range(-center.localScale.x / 2, center.localScale.x / 2), Random.Range(-center.localScale.z / 2, center.localScale.z / 2), Random.Range(-center.localScale.z / 2, center.localScale.z / 2));

        Instantiate(KeyPrefab, pos, Quaternion.identity);

    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center.position, center.localScale);
    }
#endif
}
