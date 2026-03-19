using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class GuardView : MonoBehaviour
{

    public Transform fox;
    public NavMeshAgent guard;
    public bool foxSeen = false;


    public GuardChase guardChase;

    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public LayerMask obstacleMask;

    public float alertTime = 3f;//time for the guard to notice the fox
    private float alertTimer = 0f;//time seeing fox
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (foxSeen) { return; }


        if (IsInView())
        {

            alertTimer += Time.deltaTime;
            if (alertTimer >= alertTime)
            {
                SeenFox();
            }
        }
        else
        {

            alertTimer = 0f;
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0f, -viewAngle / 2f, 0f) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0f, viewAngle / 2f, 0f) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
    }





    void SeenFox()
    {
        foxSeen = true;

        guardChase.startChase();

    }


    public bool IsInView()
    {

        Vector3 direcToFox = fox.position - transform.position;
        float distToFox = Vector3.Distance(transform.position, fox.position);

        if (distToFox < viewDistance)
        {
            float angleToFox = Vector3.Angle(transform.forward, direcToFox);
            if (angleToFox < viewAngle / 2f)
            {
                if (!Physics.Raycast(transform.position, direcToFox.normalized, distToFox, obstacleMask))//first parameter is start of ray, second is direction, third is length, fourth is the layer mask to ignore
                {
                    return true;
                }

            }

        }
        return false;



    }
}
