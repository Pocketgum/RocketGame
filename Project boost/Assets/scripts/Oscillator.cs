using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)]  float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return;}
        float cylcles = Time.time / period; // Growing over time 
        const float tau = Mathf.PI * 2; // const val of 6.283
        float Rawsinwave = Mathf.Sin(cylcles * tau); // going from 1- to 1 

        movementFactor = (Rawsinwave + 1f) / 2f; // recalc to go from 0 to 1
        Vector3 Offset = movementVector * movementFactor;
        transform.position = startingPosition + Offset;
    }
}
