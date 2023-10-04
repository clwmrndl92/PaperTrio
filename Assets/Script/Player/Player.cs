using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("States")]
    public float runVeclocity;
    public float airVeclocity;
    public float jumpPower;

    public StateMachine stateMachine { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }

    public PlayerGround ground {get; private set;}
    public Rigidbody2D rigid {get; private set;}
    [HideInInspector] public InputData input;


    [HideInInspector] public bool isJumping = false;

    void Awake()
    {
        input = new InputData();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        ground = GetComponent<PlayerGround>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        stateMachine = new StateMachine(StateName.Stand, new StandState(this));
        InitStateMachine();
    }

    void Update()
    {
        PlayerInputCustom.Instance.GetInput(out input);
        stateMachine?.UpdateState();
        stateMachine?.FixedUpdateState();
    }

    void FixedUpdate()
    {
        //stateMachine?.FixedUpdateState();
    }

    public void OnUpdateStat(float moveSpeed, int dashCount)
    {

    }

    private void InitStateMachine()
    {
        stateMachine.AddState(StateName.Run, new RunState(this));
        stateMachine.AddState(StateName.Jump, new JumpState(this));
        stateMachine.AddState(StateName.Air, new AirState(this));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Dead") )
        {
            GameManager.Instance._pageManager.RestartPage();
        }
        else if (other.gameObject.CompareTag("Laser") ){
            Time.timeScale = 0f;
            GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Reset());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dead") )
        {
            GameManager.Instance._pageManager.RestartPage();
        }
        if (other.gameObject.CompareTag("Laser") ){
            Time.timeScale = 0f;
            GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Reset());

        }
    }

    IEnumerator Reset(){
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.Instance._pageManager.RestartPage();

    }
}