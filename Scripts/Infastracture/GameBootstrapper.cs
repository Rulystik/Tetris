using Infastracture.States;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture
{
  public class GameBootstrapper : MonoBehaviour , ICoroutineRunner
  {
    private Game _game;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    private void Start()
    {
      _game = new Game(this);
      
    }
  }
}