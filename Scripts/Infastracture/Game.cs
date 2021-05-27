using Infastracture.Services;
using Infastracture.States;
using Infastracture.Utils;

namespace Infastracture
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      Model model = GetModel();
      StateMachine = new GameStateMachine(AllServices.Instance, coroutineRunner, model);
      StateMachine.SetState<BootState>();
    }

    private static Model GetModel()
    {
      return new Model();
    }
  }
}