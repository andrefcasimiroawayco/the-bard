using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We make the walk properties a struct, similar to how types work in
// ES6. This way we can create a object and pass it into the Walk constructor
// with the properties we want our entity to walk with (speed, gravity, etc.)
public struct WalkProps
{
    public float speed;
    public float maxSpeed;

    public WalkProps(float speed = 2f, float maxSpeed = 3f)
    {
        this.speed = speed;
        this.maxSpeed = maxSpeed;
    }
}

public class Walk : Movement
{
    GameObject entity;
    Animator animator => entity.GetComponent<Animator>();
    Rigidbody rigidbody => entity.GetComponent<Rigidbody>();
    CapsuleCollider capsuleCollider => entity.GetComponent<CapsuleCollider>();

    // Internal values
    WalkProps props;
    private float z, x;


    public Walk(GameObject entity, WalkProps props) {
      this.entity = entity;
      this.props = props;
    }

    public override void Dispatch()
    {
      RigidbodyUtils.ClampNegativeVelocity(rigidbody);

      x = Input.GetAxis("Horizontal");
      z = Input.GetAxis("Vertical");

      RigidbodyUtils.ApplyDirection(
        x, z, entity.transform, rigidbody, props.speed, props.maxSpeed
      );
    }

    public override void OnFinished() {}
}
