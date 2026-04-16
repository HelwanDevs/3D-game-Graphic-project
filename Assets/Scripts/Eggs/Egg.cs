using UnityEngine;

public class Egg : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fox"))
        {
            Debug.Log("Fox touched egg");
            GameManager.instance.CollectEgg();

            Destroy(gameObject);
        }
    }
}
