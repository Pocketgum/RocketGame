using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    //parameters - for tuning
    [SerializeField] float Mainthrust;
    [SerializeField] float Rotationforce;
    [SerializeField] AudioClip Mainengine;

    [SerializeField] ParticleSystem RSideThrusters;
    [SerializeField] ParticleSystem LSideThrusters;
    [SerializeField] ParticleSystem Mainthrusters;



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
            StartThrusting();
        }
        else
        {
            Stoppingthrust();
        }

    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }

    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Mainthrust * Time.deltaTime);

        if (!audiosound.isPlaying)
        {
            audiosound.PlayOneShot(Mainengine);
        }
        if (!Mainthrusters.isPlaying)
        {
            Mainthrusters.Play();
        }
    }
    private void Stoppingthrust()
    {
        Mainthrusters.Stop();
        audiosound.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-Rotationforce);
        if (!RSideThrusters.isPlaying)
        {
            RSideThrusters.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(Rotationforce);
        if (!LSideThrusters.isPlaying)
        {
            LSideThrusters.Play();
        }
    }
    private void StopRotating()
    {
        LSideThrusters.Stop();
        RSideThrusters.Stop();
    }  
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing roation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing roation
    }
}
