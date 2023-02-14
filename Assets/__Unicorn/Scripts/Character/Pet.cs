using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


[RequireComponent(typeof(CharacterController))]
public class Pet: MonoBehaviour
{
    [SerializeField] private float minRestTime = 1;
    [SerializeField] private float maxRestTime = 3;
    [Range(0, 1)]
    [SerializeField] private float actionRatio = 0.3f;
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 360;
    [SerializeField] private float squaredStoppingDistance = 0.3f;
    [SerializeField] private float randomRadius = 25;
    [SerializeField] private Animator animator;
    private CharacterController controller;

    private Vector3 target;
    private Quaternion targetRotation;
    private static readonly int RunHashed = Animator.StringToHash("Run");

    private float nextMoveTime;

    private Vector3 ownerOffset;
    private float maxDistanceFromOwner;

    private float actionFinishTime;
    private static readonly int Action = Animator.StringToHash("Action");

    public bool CanMove { get; set; } = true;

    public Animator Animator => animator;

    public static Pet Spawn(GameObject character, Vector3 ownerOffset, float maxDistanceFromOwner, int petId = -1)
    {
        return null;
    }

    public static Pet Spawn(int petId = -1)
    {
        var prefabs = GameManager.Instance.PlayerDataManager.DataTextureSkin.pets;
        if (petId == -1)
        {
            petId = Random.Range(0, prefabs.Length);
        }
        var prefab = prefabs[petId];
        var pet = Instantiate(prefab);
        
        SceneManager.MoveGameObjectToScene(pet.gameObject, LevelManager.Instance.gameObject.scene);
        return pet;
    }
    
    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
        controller = GetComponent<CharacterController>();
        target = transform.position;
    }

    private void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();
        controller.center = renderer.bounds.size.y / 2 * Vector3.up;
        controller.height = renderer.bounds.size.y;
        if (controller.stepOffset > controller.height)
        {
            controller.stepOffset = controller.height * transform.lossyScale.magnitude;
        }
    }

    public void RegisterOwner(Vector3 ownerOffset, float maxDistanceFromOwner)
    {
        this.ownerOffset = ownerOffset;
        this.maxDistanceFromOwner = maxDistanceFromOwner;
    }
    
    
    private void Update()
    {
    }

    private void UpdateSpeed()
    {
    }
    
    private void StopMoving()
    {
        animator.SetBool(RunHashed, false);
    }

    private void DoAction()
    {
        if (actionFinishTime > Time.time) return;

        var rand = Random.value;
        if (rand > actionRatio)
        {
            actionFinishTime = Time.time + 0.3f;
            return;
        }
        
        animator.SetTrigger(Action);
        actionFinishTime = Time.time + 4;

        nextMoveTime = Mathf.Max(Time.time + 1f, nextMoveTime);
    }
    
    private void FindNewPosition()
    {
    }

    private void FindRandomPosition()
    {
        var direction = Random.insideUnitCircle.ToVectorXZ().normalized;
        direction *= Random.Range(0, 2) == 0 ? -1 : 1;
        
        target = transform.position + direction * Random.Range(2, randomRadius);
        targetRotation = Quaternion.LookRotation(target - transform.position);
    }

    private void FindPositionAroundOwner()
    {

    }
    
    
    private void MoveToTarget()
    {
        animator.SetBool(RunHashed, true);

        var newPosition = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        var motion = newPosition - transform.position;
        
        motion += Physics.gravity * Time.deltaTime;
        controller.Move(motion);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Pet")) return;
        if (hit.point.y < transform.position.y) return;
        target = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, randomRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target, 1);
    }
}
