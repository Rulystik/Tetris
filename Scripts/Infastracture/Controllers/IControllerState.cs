namespace Infastracture.Controllers
{
  public interface IControllerState
  {
    void Init();
    void Enter();
    void Exit();
    void Destroy();
  }
}