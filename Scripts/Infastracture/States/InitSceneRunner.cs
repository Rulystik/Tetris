using Infastracture.Services;
using Views;

namespace Infastracture.States
{
  public class InitSceneRunner : BaseState
  {
    private readonly IAllServices _services;

    public InitSceneRunner(GameStateMachine stateMachine, IAllServices services) : base(stateMachine)
    {
      _services = services;
    }

    public override void OnEnter()
    {
      var curtain = _services.GetSingle<ILevelManager>().GetOrCreate<LoadingCurtain>();
      curtain.Show();
      _stateMachine.SetState<LoadLevel01State>();
    }

    public override void OnExit()
    {
      
    }
  }
}