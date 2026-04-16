using UnityEngine;
using UnityEngine.AI;


public class GuardChase : MonoBehaviour
{

    public NavMeshAgent guard;
    public GuardController guardController;




    public float chaseSpeedMultiplier = 1.5f;



    public float viewDistance = 10f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartChasing()
    {
        Debug.Log("Guard starts chasing the fox!");

        guard.speed = guardController.move.speed * chaseSpeedMultiplier;
    }


    void Awake()
    {
        guard = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    public void UpdateChasing()
    {
        guard.SetDestination(guardController.fox.position);

    }



}