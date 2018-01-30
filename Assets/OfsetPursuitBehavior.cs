using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfsetPursuitBehavior : MonoBehaviour
{
    public Transform leader;
    public float speed;

    Rigidbody rb;
    Vector3 leaderOffset;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        leaderOffset = transform.position - leader.transform.position;
	}

    void arrive(Vector3 targetPos)
    {
        Vector3 targetOnY = targetPos;
        targetOnY.y = transform.position.y;
        Vector3 targetOffset = targetPos - transform.position;
        float dist = Vector3.Distance(transform.position, targetPos);
        float rampedSpeed = speed * (targetOffset.magnitude / dist);
        float clippedSpeed = Mathf.Min(rampedSpeed, speed);
        Vector3 desiredVelocity = (clippedSpeed / targetOffset.magnitude) * targetOffset;

        rb.AddForce(desiredVelocity - rb.velocity);
    }


    // Update is called once per frame
    void Update ()
    {
        
        arrive(leader.transform.position + leaderOffset);

	}
}
