using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Executes a callback function after the given time
// Usage: Yield.instance.Dispatch(3f, () => { Debug.Log("I ran after 3 seconds!"); });

// Requires a world instance to work!
public class Yield : MonoBehaviour
{
  public static Yield instance = null;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }

  public void Dispatch(float waitTime, Action callback)
  {
    StartCoroutine(
      WaitAndExecute(waitTime, callback)
    );
  }

  IEnumerator WaitAndExecute(float waitTime, Action callback)
  {
    yield return new WaitForSeconds(waitTime);
    callback();
  }
}
