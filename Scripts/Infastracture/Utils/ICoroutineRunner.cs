using System.Collections;
using UnityEngine;

namespace Infastracture.Utils
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}