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
    InputHandler IRun;

    [Header("Movement Properties")]
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float currentX;
    public float currentZ;

    private void Start()
    {
      // Create keys for walking
      List<string> walkKeys = new List<string>();
      walkKeys.Add("Horizontal");
      walkKeys.Add("Vertical");

      string runKey = "Run";

      Walk walk = new Walk(this.gameObject, new WalkProps(walkSpeed));
      // Bind an input to our Walk class
      IWalk = new InputHandler(walkKeys, walk);

      Run run = new Run(this.gameObject, new RunProps(runSpeed));
      IRun = new InputHandler(runKey, run);
    }

    private void Update() {}

    private void FixedUpdate() {
      IWalk.Listen();
      IRun.Listen();

      ManageAnimations();
    }

    private void ManageAnimations() {

      float z = Input.GetAxis("Vertical") != 0 ? currentZ : 0f;
      float x = Input.GetAxis("Horizontal") != 0 ? currentX : 0f;

      GetComponent<Animator>().SetFloat("Vertical", z, 0.1f, Time.deltaTime);
      GetComponent<Animator>().SetFloat("Horizontal", x, 0.1f, Time.deltaTime);
    }

    public void SetCurrentX(float x) {
      this.currentX = x;
    }
    public void SetCurrentZ(float z) {
      this.currentZ = z;
    }
}
