using UnityEngine;

public class Bush : MonoBehaviour
{//make the bush a bit transparent when fox in

    Renderer bush;
    Material material;
    Color bushColor;
    public float fade = 0.2f;

    public AudioSource source;
    public AudioClip clip;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bush = GetComponent<Renderer>();
        material = bush.material; // cache instance


        bushColor = material.color; // URP/Lit supports this

    }


    void OnTriggerEnter(Collider other)
    {

        source = GameObject.Find("sfx").GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Audio/bush");
        source.clip = clip;
        source.Play();
        if (other.CompareTag("Fox"))
        {
            Debug.Log("Fox entered bush");
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
