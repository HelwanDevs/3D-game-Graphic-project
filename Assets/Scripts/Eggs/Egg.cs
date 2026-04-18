using UnityEngine;

public class Egg : MonoBehaviour
{

    public AudioSource source;
    public AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        source = GameObject.Find("sfx").GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Audio/click");
        source.clip = clip;
        source.Play();


        if (other.CompareTag("Fox"))
        {
            Debug.Log("Fox touched egg");
            GameManager.instance.CollectEgg();

            Destroy(gameObject);
        }
    }
}
