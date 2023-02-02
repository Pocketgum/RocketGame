using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float Delaytime;
    [SerializeField] AudioClip Sucessland;
    [SerializeField] AudioClip Crashing;

    AudioSource audiosound;

    bool isTransitioning = false;

    private void Start()
    {
        audiosound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;                       
            case "Finish":
                StartSuccessSeq();
                break;
            default:
                Startcrashsequence();
                break;
        }
    }

    void StartSuccessSeq()
    {
        isTransitioning = true;
        audiosound.Stop();
        audiosound.PlayOneShot(Sucessland); 
        GetComponent<Movement>().enabled = false;
        Invoke("Nextlevel", Delaytime);
    }
    
    void Startcrashsequence()
    {
        audiosound.Stop();
        isTransitioning = true;
        audiosound.PlayOneShot(Crashing);
        GetComponent<Movement>().enabled= false;
        Invoke ("Reloadlevel", Delaytime);
    }
    void Nextlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void Reloadlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

