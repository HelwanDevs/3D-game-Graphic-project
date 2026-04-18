
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(GuardMove))]
[RequireComponent(typeof(GuardView))]
[RequireComponent(typeof(GuardChase))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuardController : MonoBehaviour
{

    //characters and scripts
    public Transform fox;
    Animator ani;

    public NavMeshAgent guard;
    public GuardMove move;
    public GuardView view;
    public GuardChase chase;
    public Transform[] points;
    public LayerMask obstacleMask;
    public LineRenderer lineRenderer;

    //settings
    public enum GuardState
    {
        Patrol,
        Chase
    }

    public GuardState currentState = GuardState.Patrol;
    public float loseTime = 4f;
    public float loseTimer = 0f;
    public bool isGameOver = false;
    public bool isLooking = false;



    public AudioSource source;
    public AudioClip chaseClip;
    public AudioClip loseClip;





    void Start()//set the guard to patrol at the start of the game
    {
        SetState(GuardState.Patrol);



    }


    void Awake()//get the scripts and characters when the game start
    {
        move = GetComponent<GuardMove>();
        view = GetComponent<GuardView>();
        chase = GetComponent<GuardChase>();
        guard = GetComponent<NavMeshAgent>();
        fox = GameObject.FindGameObjectWithTag("Fox").transform;
        obstacleMask = LayerMask.GetMask("obstacle");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        ani = guard.GetComponent<Animator>();

        source = GameObject.Find("sfx").GetComponent<AudioSource>();
        loseClip = Resources.Load<AudioClip>("Audio/lose");
        chaseClip = Resources.Load<AudioClip>("Audio/chase");



        Debug.Log("Fox: " + fox);
        Debug.Log("Head: " + fox.Find("head IK"));


        // GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("point");
        // points = new Transform[pointObjects.Length];
        // for (int i = 0; i < pointObjects.Length; i++)
        // {
        //     points[i] = pointObjects[i].transform;
        // }
        // System.Array.Sort(points, (a, b) => a.name.CompareTo(b.name));


        setupReferences();

    }



    void Update()
    {

        if (isGameOver) { return; }

        UpdateAnimations();

        if (currentState == GuardState.Patrol)
        {
            move.UpdateMoving();

            if (view.IsInView())
            {
                source.clip = chaseClip;
                source.Play();
                SetState(GuardState.Chase);
            }
        }
        else if (currentState == GuardState.Chase)
        {
            float distanceToFox = Vector3.Distance(transform.position, fox.position);
            float distancehead = Vector3.Distance(transform.position, view.foxhead.position);
            float distancetail = Vector3.Distance(transform.position, view.foxtail.position);

            if (distanceToFox <= 1f || distancehead <= 1f || distancetail <= 1f)
            {

                GameOver();

            }

            chase.UpdateChasing();
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            if (!view.IsInView() || distanceToFox > view.viewDistance)
            {
                loseTimer += Time.deltaTime;
                if (loseTimer >= loseTime)
                {
                    lineRenderer.startColor = Color.yellow;
                    lineRenderer.endColor = Color.yellow;
                    Debug.Log("Guard lost the fox, returning to patrol.");
                    SetState(GuardState.Patrol);
                }
            }
            else
            {
                loseTimer = 0f;
            }

        }


    }


    public void SetState(GuardState newState)
    {
        currentState = newState;
        loseTimer = 0f;

        move.enabled = (newState == GuardState.Patrol);
        chase.enabled = (newState == GuardState.Chase);

    }

    void UpdateAnimations()
    {
        ani.speed = 2f;
        if (currentState == GuardState.Patrol)
        {
            if (move.isWaiting)
            {
                ani.Play("look");
            }
            else
            {
                ani.Play("Walking");
            }
        }
        else if (currentState == GuardState.Chase)
        {
            ani.Play("run");
        }
    }


    public void GameOver()
    {
        // isGameOver = true;
        // move.enabled = false;
        // view.enabled = false;
        // chase.enabled = false;
        // guard.isStopped = true;

        source.clip = loseClip;
        source.Play();

        ani.enabled = false;
        GameManager.instance.GameOver();
        Debug.Log("Game Over :3");
    }


    public void setupReferences()
    {
        move.guard = guard;
        move.points = points;

        view.fox = fox;
        view.obstacleMask = obstacleMask;
        view.lineRenderer = lineRenderer;



        chase.guard = guard;
        chase.guardController = this;
    }
}
