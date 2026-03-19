using UnityEngine;
using UnityEngine.AI;


public class GuardChase : MonoBehaviour
{

    public Transform fox;
    public NavMeshAgent guard;
    public GuardMove guardMove;
    public GuardView guardView;




    public float chaseSpeed;
    public float loseFoxTime = 4f;
    private float loseFoxTimer = 0f;
    private bool chase = false;


    public float viewDistance = 10f;
    public float catchDistance = 1.5f;


    public bool gameover = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!chase) { return; }
        float distToFox = Vector3.Distance(transform.position, fox.position);

        if (distToFox <= catchDistance)
        {//if catch fox
            if (gameover)
            {
                GameOver();
            }
        }
        else if (!guardView.IsInView())
        {//if the guard lose sight of fox


            loseFoxTimer += Time.deltaTime;
            if (loseFoxTimer >= loseFoxTime)
            {
                StopChase();
            }
        }
        else
        {
            loseFoxTimer = 0;
            guard.SetDestination(fox.position);


        }

    }


    public void startChase()
    {
        chase = true;
        loseFoxTimer = 0f;
        guardMove.enabled = false;
        chaseSpeed = guardMove.speed * 1.5f;
        guard.speed = chaseSpeed;

    }

    public void StopChase()//and return to the last known position of the guard and move in pattern 
    {
        chase = false;
        guardView.foxSeen = false;
        guard.ResetPath();
        guard.speed = guardMove.speed;
        guardMove.enabled = true;
        guard.isStopped = false;
    }

    void GameOver()
    {
        guard.isStopped = true;
        guardMove.enabled = false;
    }
}