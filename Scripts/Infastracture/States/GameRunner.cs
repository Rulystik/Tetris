using System;
using Infastracture.Controllers;
using Infastracture.Services;
using Infastracture.Utils;
using Views;

namespace Infastracture.States
{
  public class GameRunner : BaseState
  {
    private readonly IAllServices _services;
    private readonly ICoroutineRunner _coroutineRunner;
    private FieldView _view;
    private Model _model;
    private ControllerStateMachine _controllerStateMachine;

    private ILevelManager _levelManager;

    public GameRunner(GameStateMachine stateMachine, IAllServices services, ICoroutineRunner coroutineRunner, Model model) : base(stateMachine)
    {
      _services = services;
      _coroutineRunner = coroutineRunner;
      _model = model;
    }

    public override void OnEnter()
    {
      _levelManager = _services.GetSingle<ILevelManager>();
      _view = _levelManager.GetOrCreate<FieldView>();
      _view.Init(_model, _levelManager);

      _controllerStateMachine = new ControllerStateMachine(_services, _coroutineRunner);
      _controllerStateMachine.SetState<CreateField>();

      var certain = _levelManager.GetOrCreate<LoadingCurtain>();
      certain.Hide();
    }


    private void Init()
    {
    }

    public override void OnExit()
    {
    }
  }
}