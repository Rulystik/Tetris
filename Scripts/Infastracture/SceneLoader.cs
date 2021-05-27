using System;
using System.Collections;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infastracture
{
  public interface ISceneLoader : IService
  {
    void Load(string name, Action onLoaded = null);
  }

  internal class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null) => _coroutineRunner.StartCoroutine(LoadSceneAsync(name, onLoaded));

    private IEnumerator LoadSceneAsync(string name, Action onLoaded)
    {
      if (SceneManager.GetActiveScene().name == name)
      {
        onLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waiter = SceneManager.LoadSceneAsync(name);

      while (waiter.isDone == false)
      {
        yield return null;
      }
      onLoaded?.Invoke();
    }
  }
}