using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MoveUnit
{
    protected override void Awake()
    {
        base.Awake();
        jumpForce = 60f;
        slowDown = 5f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
        if (isGrounded == false)
        {
            SlowDown();
        }

       // Debug.Log(agent.velocity);
       //Debug.Log(rb.velocity);

    }

    private void OnCollisionEnter(Collision other)
    {
       // return;
        
        if (!isGrounded)
        {
            rb.isKinematic = true;
            agent.enabled = true;
            isGrounded = true;
            rb.isKinematic = false;
        }
    }

    public void SlowDown()
    {
       // rb.AddRelativeForce(transform.forward * slowDown, ForceMode.Impulse);
       
       // rb.AddForce(-transform.forward * slowDown, ForceMode.Force);

    }
    
    public override void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            agent.enabled = false;
            rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
            // agent.velocity = rb.velocity;
        }

    }


}
