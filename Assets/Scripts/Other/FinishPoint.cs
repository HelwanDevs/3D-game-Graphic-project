using UnityEngine;

public class FinishPoint : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fox"))
        {
            Debug.Log("Fox reached the finish point!");
            GameManager.instance.ReachPoint();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fox"))
        {
            Debug.Log("Fox left the finish point!");
            GameManager.instance.LeavePoint();
        }
    }
}