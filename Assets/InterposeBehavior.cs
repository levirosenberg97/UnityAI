using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterposeBehavior : MonoBehaviour
{
    public Transform thing1;
    public Transform thing2;
    public float speed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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


    private void Update()
    {
        Vector3 midPoint = (thing1.position + thing2.position) /2;
        float midPointTime = Vector3.Distance(midPoint, transform.position) / speed;

        Vector3 aPos = thing1.position + thing1.GetComponent<Rigidbody>().velocity * midPointTime;
        Vector3 bPos = thing2.position + thing2.GetComponent<Rigidbody>().velocity * midPointTime;

        Vector3 desiredMidPoint = (aPos + bPos) / 2;

        arrive(desiredMidPoint);
        Debug.DrawLine(transform.position, transform.position + desiredMidPoint);
    }
}
