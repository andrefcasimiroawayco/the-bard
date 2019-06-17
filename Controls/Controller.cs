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
      GetComponent<Animator>().SetFloat("Vertical", Input.GetAxis("Vertical"), 0.1f, Time.deltaTime);
      GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxis("Horizontal"), 0.1f, Time.deltaTime);
    }
}
