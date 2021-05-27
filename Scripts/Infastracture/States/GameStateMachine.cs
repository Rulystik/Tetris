using System;
using System.Collections.Generic;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.States
{
  public class GameStateMachine
  {
    IState _currentState = null;
    private Dictionary<Type, IState> _states;
    public GameStateMachine(AllServices services, ICoroutineRunner coroutineRunner, Model model)
    {
      _states = new Dictionary<Type, IState>()
      {
        [typeof(BootState)] = new BootState(this, services, coroutineRunner, model), 
        [typeof(LoadInitLevelState)] = new LoadInitLevelState(this, services), 
        [typeof(InitSceneRunner)] = new InitSceneRunner(this, services), 
        [typeof(LoadLevel01State)] = new LoadLevel01State(this, services), 
        [typeof(GameRunner)] = new GameRunner(this, services, coroutineRunner, model), 
      };
    }
    public void SetState<TState>() where TState : IState
    {
      _currentState?.OnExit();
      
      _currentState = _states[typeof(TState)];
      Debug.Log($"state: {typeof(TState).Name}");
      _currentState.OnEnter();
    }
  }
}