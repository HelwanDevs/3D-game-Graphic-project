using System;
using UnityEngine;
using UnityEngine.AI;


public class GuardMove : MonoBehaviour
{

    //refrences
    public NavMeshAgent guard;
    public float speed = 5f;
    //increase guard speed
    public int movesToIncreaseSpeed = 3;
    public float speedIncreaseAmount = 1f;
    private int moveCount = 0;
    //for the guard to move in random

    //points
    public Transform[] points;
    public int curPoint = 0;
    //time to wait at each point
    public float waitTime = 2f;
    private float timer = 0f;
    public bool isWaiting = false;

    public bool rotation = true;
    public float rotationSpeed = 90f;
    public float rotationtimer = 0f;
    public float rotationWaitTime = 1f;









    public void StartMoving()
    {
        guard.speed = speed;
        isWaiting = false;

        if (points.Length > 0)
        {
            NextRandomPoint();
        }
    }

    public void UpdateMoving()
    {


        if (guard.remainingDistance <= guard.stoppingDistance && !isWaiting)
        {
            isWaiting = true;

            rotationtimer = timer = 0f;
            rotation = true;
        }

        if (isWaiting)
        {
            timer += Time.deltaTime;
            rotationtimer += Time.deltaTime;

            //to rotate the guard to the right then left while waiting
            transform.Rotate(0f, (rotation ? 1 : -1) * rotationSpeed * Time.deltaTime, 0f);

            if (rotationtimer >= rotationWaitTime)
            {
                rotation = !rotation;
                rotationtimer = 0f;
            }

            if (timer >= waitTime)
            {
                isWaiting = false;
                NextRandomPoint();
            }
        }
    }


    void NextRandomPoint()
    {
        if (points.Length == 0)
        { return; }

        //to randomly select a point from the array of points
        curPoint = UnityEngine.Random.Range(0, points.Length);
        guard.SetDestination(points[curPoint].position);
        moveCount++;
        if (moveCount >= movesToIncreaseSpeed)
        {

            guard.speed += speedIncreaseAmount;
            speed = guard.speed;
            moveCount = 0;
        }



    }
}
