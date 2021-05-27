using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  public event Action Down ;
  public event Action Up ;
  public event Action Hold;
    
  private bool preseed = false;
   
  public void OnPointerDown(PointerEventData eventData)
  {
    Down?.Invoke();
    preseed = true;
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    Up?.Invoke();
    preseed = false;
  }

  private void Update()
  {
    if (preseed)
      Hold?.Invoke();
  }
}