using UnityEngine;

public class Bush : MonoBehaviour
{//make the bush a bit transparent when fox in

    Renderer bush;
    Material material;
    Color bushColor;
    public float fade = 0.2f;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bush = GetComponent<Renderer>();
        material = bush.material; // cache instance


        bushColor = material.color; // URP/Lit supports this

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fox"))
        {
            Debug.LogError("Fox entered bush");
            Color c = material.color;
            c.a = fade;
            material.color = c;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fox"))
        {
            material.color = bushColor;
        }
    }


}
