using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private Collectable Prefab;
    public GameObject target;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private LineRenderer Path;
    [SerializeField]
    private float PathHeightOffset = .5f;
    [SerializeField]
    private float SpawnHeightOffset = .5f;
    [SerializeField]
    private float PathUpdateSpeed =0f;

    private Collectable ActiveInstance;
    private NavMeshTriangulation Triangulation;
    private Coroutine DrawPathCoroutine;

    private void Awake()
    {
        Triangulation = NavMesh.CalculateTriangulation();
    }

    private void Start()
    {
        SpawnNewObject();
    }

    public void SpawnNewObject()
    {
        ActiveInstance = Instantiate(Prefab,
            target.transform.position,
            Quaternion.Euler(90, 0, 0)
        );
        ActiveInstance.Spawner = this;

        if (DrawPathCoroutine != null)
        {
            StopCoroutine(DrawPathCoroutine);
        }

        DrawPathCoroutine = StartCoroutine(DrawPathToCollectable());
    }

    private IEnumerator DrawPathToCollectable()
    {
        WaitForSeconds Wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (ActiveInstance != null)
        {
            if (NavMesh.CalculatePath(Player.position, ActiveInstance.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {Player.position} and {ActiveInstance.transform.position}!");
            }

            yield return Wait;
        }
    }
}
