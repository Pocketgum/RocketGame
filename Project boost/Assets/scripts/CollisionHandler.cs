using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float Delaytime;
    [SerializeField] AudioClip Sucessland;
    [SerializeField] AudioClip Crashing;

    [SerializeField] ParticleSystem SucessParticle;
    [SerializeField] ParticleSystem deathParticle;


    AudioSource audiosound;

    bool isTransitioning = false;
    bool collisiondisabled = false;

    private void Start()
    {
        audiosound = GetComponent<AudioSource>();
    }
    void Update()
    {
        Respondtodebugkeys();
    }

     void Respondtodebugkeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Nextlevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) 
        {
            collisiondisabled = !collisiondisabled; // toggle collision
            Debug.Log("Collision disable is " + collisiondisabled);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisiondisabled) { return; }
        
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
        SucessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Nextlevel", Delaytime);
    }
    
    void Startcrashsequence()
    {
        audiosound.Stop();
        isTransitioning = true;
        audiosound.PlayOneShot(Crashing);
        deathParticle.Play();
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

