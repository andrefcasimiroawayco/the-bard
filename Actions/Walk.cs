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

    public WalkProps(float speed = 2f)
    {
        this.speed = speed;
    }
}

public class Walk : Movement
{
    GameObject entity;
    Animator animator => entity.GetComponent<Animator>();
    Rigidbody rigidbody => entity.GetComponent<Rigidbody>();
    CapsuleCollider capsuleCollider => entity.GetComponent<CapsuleCollider>();
    Controller controller => entity.GetComponent<Controller>();

    // Internal values
    WalkProps props;
    private float z, x;


    public Walk (GameObject entity, WalkProps props) {
      this.entity = entity;
      this.props = props;
    }

    public override void Dispatch()
    {
      RigidbodyUtils.ClampNegativeVelocity(rigidbody);

      bool pressedZ = Input.GetAxis("Vertical") != 0;
      bool pressedX = Input.GetAxis("Horizontal") != 0;

      z = pressedZ ? Input.GetAxis("Vertical") * 0.5f : 0f;
      x = pressedX ? Input.GetAxis("Horizontal") * 0.5f : 0f;

      controller.SetCurrentX(x);
      controller.SetCurrentZ(z);

      float m_speed = pressedX && pressedZ ? 1f : props.speed;
      RigidbodyUtils.ApplyDirection(
        x, z, entity.transform, rigidbody, m_speed
      );
    }

    public override void OnFinished() {}
}
