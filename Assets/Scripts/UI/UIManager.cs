using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class UIManager : MonoBehaviour
{

    public GameObject levels;
    public GameObject htp;
    public GameObject options;

    public AudioSource source;
    public AudioClip clip;




    public void RestartGame()
    {
        PlayWithSound(() =>
             {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             });


    }
    public void ReturnToMainMenu()
    {
        PlayWithSound(() =>
             {
                 SceneManager.LoadScene(0);
             });

    }
    public void NextLevel()
    {
        PlayWithSound(() =>
             {
                 int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                 if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                 {
                     SceneManager.LoadScene(nextSceneIndex);
                 }
                 else
                 {
                     SceneManager.LoadScene(0);
                 }


             });

    }

    public void SelectLevel(int level)
    {
        PlayWithSound(() =>
             {
                 switch (level)
                 {
                     case 1:
                         SceneManager.LoadScene(1);
                         break;
                     case 2:
                         SceneManager.LoadScene(2);
                         break;
                     case 3:
                         SceneManager.LoadScene(3);
                         break;

                     default:
                         SceneManager.LoadScene(0);
                         break;
                 }



             });


    }

    public void OpenLevels()
    {

        PlayWithSound(() =>
             {
                 levels.SetActive(true);
                 options.SetActive(false);



             });




    }

    public void OpenHTP()
    {
        PlayWithSound(() =>
             {
                 htp.SetActive(true);
                 options.SetActive(false);
             });



    }

    public void OpenOptions()
    {
        PlayWithSound(() =>
             {
                 options.SetActive(true);
                 htp.SetActive(false);
                 levels.SetActive(false);
             });


    }

    public void ExitGame()
    {
        PlayWithSound(() =>
        {

            Application.Quit();
        });

    }

    void PlayWithSound(System.Action action)
    {
        StartCoroutine(PlaySoundAndExecute(action));
    }

    IEnumerator PlaySoundAndExecute(System.Action action)
    {
        source.clip = clip;
        source.pitch = 6;

        source.Play();

        yield return new WaitForSeconds(clip.length / source.pitch);

        action.Invoke();
    }




}