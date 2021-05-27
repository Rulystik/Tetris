using Infastracture.Constants;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.States
{
  public class LoadLevel01State : BaseState
  {
    private readonly IAllServices _services;
    private ISceneLoader _sLoader;
    private AutoProperty<ISceneLoader> sceneLoader;
    public LoadLevel01State(GameStateMachine gameStateMachine, IAllServices services) : base(gameStateMachine)
    {
      _services = services;
      sceneLoader = new AutoProperty<ISceneLoader>(_services.GetSingle<ISceneLoader>);
    }

    public override void OnEnter()
    {
      sceneLoader.Get().Load(AssetPath.Level01, OnLoaded);
    }

    private void OnLoaded()
    {
      Debug.Log($"Load Level01");
      _stateMachine.SetState<GameRunner>();
    }

    public override void OnExit()
    {
    }
  }
}