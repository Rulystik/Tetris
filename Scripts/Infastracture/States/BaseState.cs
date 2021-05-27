namespace Infastracture.States
{
  public abstract class BaseState : IState
  {
    protected readonly GameStateMachine _stateMachine;

    protected BaseState(GameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    public abstract void OnEnter();
    public abstract void OnExit();
  }
}