using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SkinChanger))]
[RequireComponent(typeof(AudioSource))]
public class Character : MonoBehaviour
{
    
    [SerializeField] private float rotationSpeed;

    private SkinChanger skinCharacter;
    private Animator animator;
    private AudioSource audioSource;

    private bool canMove;
    private bool canKill = true;

    public event Action<Character> OnDie;

    public SkinChanger SkinCharacter
    {
        get => skinCharacter;
        protected set => skinCharacter = value;
    }

    public Animator Animator => animator;
    public bool IsDead { get; protected set; }
    public bool IsPlayer { get; protected internal set; }

    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    public AudioSource AudioSource
    {
        get => audioSource;
        protected set => audioSource = value;
    }

    protected virtual void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        skinCharacter = GetComponent<SkinChanger>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        SkinCharacter.Init(this);
    }
}