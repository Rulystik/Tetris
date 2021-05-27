using Infastracture.Services;

namespace Infastracture.Controllers
{
  public class CreateField : ControllerStateBase
  {
    public CreateField(ControllerStateMachine controllerStateMachine, IAllServices services) :
      base(controllerStateMachine, services)
    {
    }

    public override void Init()
    {
      
    }

    public override void Enter()
    {
      var model = _services.GetSingle<Model>();
      model.Field = new bool[Model.XCount, Model.YCount];
      _controllerStateMachine.SetState<CreateTetromino>();
    }

    public override void Exit()
    {
      
    }

    public override void Destroy()
    {
      
    }
  }
}