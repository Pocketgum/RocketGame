using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    //parameters - for tuning
    [SerializeField] float Mainthrust;
    [SerializeField] float Rotationforce;
    [SerializeField] AudioClip Mainengine;

    
    // Cache - refrences 
    Rigidbody rb;
    AudioSource audiosound;

    //State - memeber varioables like bools 

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Mainthrust * Time.deltaTime);
            
            if (!audiosound.isPlaying)
            {
                audiosound.PlayOneShot(Mainengine);       
            }
        }
        else
        {
            audiosound.Stop();
        }
       
    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(- Rotationforce);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(Rotationforce);
        }

    }

     void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing roation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing roation
    }
}
