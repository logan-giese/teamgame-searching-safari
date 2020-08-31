using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script to increase an object's mass when it impacts something
public class HeavyOnImpact : MonoBehaviour
{
    // The multiplier by which to change the object's mass
    public float massMultiplier = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().mass *= massMultiplier;
    }
}
