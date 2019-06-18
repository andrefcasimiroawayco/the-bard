using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_INPUT_TYPE {
  CONTINUOUS, // If key is down, run for as long as this happens
  ONCE // If key is down, run only once
}

public class InputHandler
{
    // Keyboard / gamepad input key
    private string key;
    private List<string> keys = new List<string>();
    private Movement movement;
    private ENUM_INPUT_TYPE type;

    public InputHandler(string key, Movement movement) {
        this.key = key;
        this.movement = movement;
    }

    public InputHandler(List<string> keys, Movement movement) {
        this.keys = keys;
        this.movement = movement;
    }

    public InputHandler(List<string> keys, Movement movement, ENUM_INPUT_TYPE type) {
        this.keys = keys;
        this.movement = movement;
        this.type = type;
    }

    public void Listen()
    {
      // Listen for key presses
      if (type == ENUM_INPUT_TYPE.ONCE)
      {
        KeyDown();
      }
      else
      {
        KeyPressed();
      }

      // Listen for key ups
      CheckForKeyUps();
    }

    void KeyPressed()
    {
      // For one single key
      if (key != null)
      {
        if (Input.GetButton(key)) { OnKeyPressed(); }
      }

      // For a list of keys
      if (keys.Count > 0)
      {
        foreach (string _key in keys)
        {
          if (Input.GetButton(_key)) { OnKeyPressed(); }
        }
      }
    }

    void KeyDown()
    {
      // For one single key
      if (key != null)
      {
        if (Input.GetButton(key)) { OnKeyPressed(); }
      }

      // For a list of keys
      if (keys.Count > 0)
      {
        foreach (string _key in keys)
        {
          if (Input.GetButtonDown(_key)) { OnKeyPressed(); }
        }
      }
    }

    void CheckForKeyUps()
    {
      // For one single key
      if (key != null)
      {
        if (Input.GetButtonUp(key)) { OnKeyUp(); }
      }

      // For a list of keys
      if (keys.Count > 0) {
        foreach (string _key in keys)
        {
          if (Input.GetButtonUp(_key)) { OnKeyUp(); }
        }
      }
    }

    public void OnKeyPressed() {
      movement.Dispatch();
    }
    public void OnKeyUp() {
      movement.OnFinished();
    }
}
