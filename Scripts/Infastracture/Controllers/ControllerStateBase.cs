using Infastracture.Services;

namespace Infastracture.Controllers
{
  public abstract class ControllerStateBase : IControllerState
  {
    protected  ControllerStateMachine _controllerStateMachine;
    protected readonly IAllServices _services;

    protected ControllerStateBase(ControllerStateMachine controllerStateMachine, IAllServices services)
    {
      _controllerStateMachine = controllerStateMachine;
      _services = services;
    }
    public abstract void Init();
    

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Destroy();
  }
}