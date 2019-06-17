using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides a boolean flag
public class Flag
{
  public Flag() {}
  private bool m_Value = false;

  public void SetTrue()
  {
    m_Value = true;
  }
  public void SetFalse()
  {
    m_Value = false;
  }
  public void Toggle()
  {
    m_Value = !m_Value;
  }
  public bool Check()
  {
    return m_Value;
  }
}
