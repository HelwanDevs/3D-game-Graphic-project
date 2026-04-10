using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GuardView : MonoBehaviour
{
    //character 
    public Transform fox;



    //settings
    public bool foxSeen = false;
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public LayerMask obstacleMask;
    public float alertTime = 0.1f;//time for the guard to notice the fox

    //for cone view thingy
    public LineRenderer lineRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawCone();

        if (foxSeen) { return; }


        if (IsInView())
        {

            SeenFox();

        }


    }


    void OnDrawGizmos() //appear in game view only
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
        Debug.Log("The Fox was seen");

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
                    Debug.LogWarning("Guard sees the fox!");

                    return true;

                }

            }

        }

        return false;



    }








    public void DrawCone()
    {
        Vector3 origin = transform.position; // lift above ground

        Vector3 left = Quaternion.Euler(0f, -viewAngle / 2f, 0f) * transform.forward;
        Vector3 right = Quaternion.Euler(0f, viewAngle / 2f, 0f) * transform.forward;

        lineRenderer.positionCount = 4;

        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, origin + left * viewDistance);
        lineRenderer.SetPosition(2, origin);
        lineRenderer.SetPosition(3, origin + right * viewDistance);






    }



}
