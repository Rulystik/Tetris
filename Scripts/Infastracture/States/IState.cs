namespace Infastracture.States
{
  public interface IState
  {
    void OnEnter();
    void OnExit();
  }
}