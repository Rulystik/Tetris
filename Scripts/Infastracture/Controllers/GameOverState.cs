using Infastracture.Services;
using UnityEngine;

namespace Infastracture.Controllers
{
  public class GameOverState : ControllerStateBase
  {
    public GameOverState(ControllerStateMachine controllerStateMachine, IAllServices services) : base(controllerStateMachine, services)
    {
    }

    public override void Init()
    {
      
    }

    public override void Enter()
    {
      Debug.Log($"GameOver");
    }

    public override void Exit()
    {
    }

    public override void Destroy()
    {
    }
  }
}