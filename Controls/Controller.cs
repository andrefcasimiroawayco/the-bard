using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class Controller : MonoBehaviour
{
    // Inputs
    InputHandler IWalk;

    [Header("Movement Properties")]
    public float speed = 10f;
    public float maxSpeed = 10f;
    public float currentX;
    public float currentZ;

    private void Start()
    {
      // Create keys for walking
      List<string> walkKeys = new List<string>();
      walkKeys.Add("Horizontal");
      walkKeys.Add("Vertical");

      Walk walk = new Walk(this.gameObject, new WalkProps(speed, maxSpeed));
      // Bind an input to our Walk class
      IWalk = new InputHandler(walkKeys, walk);
    }

    private void Update() {}

    private void FixedUpdate() {
      IWalk.Listen();

      ManageAnimations();
    }

    private void ManageAnimations() {

      float x = Input.GetAxis("Horizontal") != 0 ? currentX : 0f;
      float z = Input.GetAxis("Vertical") != 0 ?currentZ : 0f;

      GetComponent<Animator>().SetFloat("Vertical", x, 0.1f, Time.deltaTime);
      GetComponent<Animator>().SetFloat("Horizontal", z, 0.1f, Time.deltaTime);
    }

    public void SetCurrentX(float x) {
      this.currentX = x;
    }
    public void SetCurrentZ(float z) {
      this.currentZ = z;
    }
}
