using UnityEngine;

namespace Infastracture.Utils
{
  public class Cooldown
  {
    private float _interval;
    private readonly float _firsttime;
    private bool isfisttime = true;
    public float nextTime;

    public Cooldown(float interval, float firsttime)
    {
      nextTime = Time.time + firsttime;
      _interval = interval;
      _firsttime = firsttime;
    }

    public void Reset()
    {
      isfisttime = true;
      nextTime = Time.time + _firsttime;
    }

    public void SetInterval(float interval)
    {
      _interval = interval;
    }

    public bool isDone()
    {
      if (isfisttime)
      {
        nextTime = Time.time + _firsttime;
        isfisttime = false;
      }
      
      if (Time.time >= nextTime)
      {
        nextTime = Time.time + _interval;
        return true;
      }

      return false;
    }
  }
}