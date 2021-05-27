using Infastracture.Constants;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.States
{
  public class LoadInitLevelState : BaseState
  {
    private AutoProperty<ISceneLoader> sceneLoader;
    private ISceneLoader _sLoader;
    // private ISceneLoader sceneLoader => _sLoader ?? (_sLoader = _services.GetSingle<ISceneLoader>());
    public LoadInitLevelState(GameStateMachine stateMachine, IAllServices services) : base(stateMachine)
    {
      sceneLoader = new AutoProperty<ISceneLoader>(services.GetSingle<ISceneLoader>);
    }

    public override void OnEnter()
    {
      sceneLoader.Get().Load(AssetPath.LoadingScene, OnLoaded);
    }

    private void OnLoaded()
    {
      Debug.Log($"Load {AssetPath.LoadingScene} level");
      _stateMachine.SetState<InitSceneRunner>();
    }

    public override void OnExit()
    {
    }
  }
}