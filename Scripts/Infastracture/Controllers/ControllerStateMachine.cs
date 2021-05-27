using System;
using System.Collections.Generic;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.Controllers
{
  public class ControllerStateMachine
  {
    IControllerState _currentState = null;

    private Dictionary<Type, IControllerState> _states;
    public ControllerStateMachine(IAllServices services, ICoroutineRunner coroutineRunner)
    {
      _states = CreateStates(services);
      InitStates();
    }

    private Dictionary<Type, IControllerState> CreateStates(IAllServices services)
    {
      return new Dictionary<Type, IControllerState>
      {
        [typeof(CreateField)] = new CreateField(this,services), 
        [typeof(CreateTetromino)] = new CreateTetromino(this,services), 
        [typeof(PlayerWaiting)] = new PlayerWaiting(this,services), 
        [typeof(GameOverState)] = new GameOverState(this,services), 
        [typeof(CheckFullLinesState)] = new CheckFullLinesState(this,services), 
        [typeof(CreateNewFigureState)] = new CreateNewFigureState(this,services), 
      };
    }

    private void InitStates()
    {
      foreach (IControllerState controllerState in _states.Values)
      {
        controllerState.Init();
      }
    }

    public void SetState<T>() where T : IControllerState
    {
      _currentState?.Exit();
      _currentState = _states[typeof(T)];
      Debug.Log($"setController{typeof(T).Name}");
      _currentState.Enter();
    }
  }
}