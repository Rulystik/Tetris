using Infastracture.Services;
using Infastracture.Utils;

namespace Infastracture.States
{
  public class BootState : BaseState
  {
    private readonly AllServices _services;
    private readonly ICoroutineRunner _coroutineRunner;
    private Model _model;

    public BootState(GameStateMachine stateMachine, AllServices services, ICoroutineRunner coroutineRunner, Model model) : base(stateMachine)
    {
      _services = services;
      _coroutineRunner = coroutineRunner;
      _model = model;
    }

    public override void OnEnter()
    {
      RegisterServices();
      _stateMachine.SetState<LoadInitLevelState>();
    }

    private void RegisterServices()
    {
      // Model model = new Model();
      _services.RegisterSingle(_model);
      _services.RegisterSingle<IModelView>(_services.GetSingle<Model>());
      _services.RegisterSingle<IValidation>(new Validation(_services.GetSingle<Model>()));
      _services.RegisterSingle<ISceneLoader>(new SceneLoader(_coroutineRunner));
      _services.RegisterSingle<IAssets>(new AssetProvider());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.GetSingle<IAssets>()));
      _services.RegisterSingle<ILevelManager>(new LevelManager(_services.GetSingle<IGameFactory>()));
    }

    public override void OnExit()
    {
    }
  }

  internal class Validation : IValidation
  {
    private readonly Model _model;

    public Validation(Model model)
    {
      _model = model;
    }
  }

  internal interface IValidation : IService
  {
  }
}