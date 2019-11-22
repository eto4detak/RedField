﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MoveUnit
{
    protected override void Awake()
    {
        base.Awake();
        jumpForce = 60f;
        slowDown = 5f;
        health = 100f;
        maxHealth = 200f;
        mana = 100f;
        maxMana = 100f;
        aggression = 0f;
        faction = Faction.Hero;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
        if (isGrounded == false)
        {
            SlowDown();
        }

    }

    private void OnCollisionEnter(Collision other)
    {
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
