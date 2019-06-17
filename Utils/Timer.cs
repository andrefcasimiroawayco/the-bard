using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Instantiates a timer
public class Timer
{
  float timer = 0f;
  float timerLimit;

  public Timer(float tMax)
  {
    this.timerLimit = tMax;
  }

  public void Run()
  {
    timer += Time.deltaTime;
  }

  public bool HasFinished()
  {
    return timer >= timerLimit;
  }
}
