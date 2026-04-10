
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
    public NavMeshAgent guard;
    public GuardMove move;
    public GuardView view;
    public GuardChase chase;
    public Transform[] points;
    public LayerMask obstacleMask;

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




        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("point");
        points = new Transform[pointObjects.Length];
        for (int i = 0; i < pointObjects.Length; i++)
        {
            points[i] = pointObjects[i].transform;
        }

        setupReferences();

    }



    void Update()
    {

        if (isGameOver) { return; }

        if (currentState == GuardState.Patrol)
        {
            move.UpdateMoving();

            if (view.IsInView())
            {
                SetState(GuardState.Chase);
            }
        }
        else if (currentState == GuardState.Chase)
        {

            float distanceToFox = Vector3.Distance(transform.position, fox.position);
            if (distanceToFox <= 1f)
            {
                GameOver();

            }

            chase.UpdateChasing();
            if (!view.IsInView() || distanceToFox > view.viewDistance)
            {
                loseTimer += Time.deltaTime;
                if (loseTimer >= loseTime)
                {
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


    public void GameOver()
    {
        // isGameOver = true;
        // move.enabled = false;
        // view.enabled = false;
        // chase.enabled = false;
        // guard.isStopped = true;
        Debug.Log("Game Over :3");
        Debug.LogError("Game Over!!");
    }


    public void setupReferences()
    {
        move.guard = guard;
        move.points = points;

        view.fox = fox;
        view.obstacleMask = obstacleMask;



        chase.guard = guard;
        chase.guardController = this;
    }
}
