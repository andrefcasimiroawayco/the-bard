using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We make the walk properties a struct, similar to how types work in
// ES6. This way we can create a object and pass it into the Walk constructor
// with the properties we want our entity to walk with (speed, gravity, etc.)
public struct RunProps
{
    public float speed;

    public RunProps(float speed = 5f)
    {
        this.speed = speed;
    }
}

public class Run : Movement
{
    GameObject entity;
    Animator animator => entity.GetComponent<Animator>();
    Rigidbody rigidbody => entity.GetComponent<Rigidbody>();
    CapsuleCollider capsuleCollider => entity.GetComponent<CapsuleCollider>();
    Controller controller => entity.GetComponent<Controller>();

    // Internal values
    RunProps props;
    private float z, x;


    public Run (GameObject entity, RunProps props) {
      this.entity = entity;
      this.props = props;
    }

    public override void Dispatch()
    {
      RigidbodyUtils.ClampNegativeVelocity(rigidbody);

      bool pressedX = Input.GetAxis("Horizontal") != 0;
      bool pressedZ = Input.GetAxis("Vertical") != 0;

      x = pressedX ? Input.GetAxis("Horizontal") * 1f : 0f;
      z = pressedZ ? Input.GetAxis("Vertical") * 1f : 0f;

      controller.SetCurrentX(x);
      controller.SetCurrentZ(z);

      float m_speed = pressedX && pressedZ ? props.speed * 1f : props.speed;
      RigidbodyUtils.ApplyDirection(
        x, z, entity.transform, rigidbody, m_speed
      );
    }

    public override void OnFinished() {}
}
