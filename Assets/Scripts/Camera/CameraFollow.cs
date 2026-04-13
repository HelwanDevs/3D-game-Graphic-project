using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform fox;
    public Vector3 offset = new Vector3(0f, 5f, -3f);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fox = GameObject.FindGameObjectWithTag("Fox").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (fox == null)
        {
            Debug.LogWarning("Fox is null in camera");
            return;
        }
        // transform.position = fox.position + fox.rotation * offset;
        // transform.LookAt(fox.position + Vector3.up * 1.5f);

        Vector3 rotatedOffset = fox.rotation * offset;

        // Desired position
        Vector3 desiredPosition = fox.position + rotatedOffset;

        // Smooth movement
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            10f * Time.deltaTime
        );

        Vector3 euler = fox.rotation.eulerAngles;

        // Lock X, keep Y from fox, set Z to 0
        transform.rotation = Quaternion.Euler(25f, euler.y, 0f);
    }




}
