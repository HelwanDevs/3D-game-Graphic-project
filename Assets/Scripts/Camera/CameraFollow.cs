using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform fox;
    public Vector3 offset = new Vector3(0f, 5f, -10f);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fox = GameObject.FindGameObjectWithTag("Fox").transform;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = fox.position + offset;





    }




}
