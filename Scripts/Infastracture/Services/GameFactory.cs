using System;
using Infastracture.Constants;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject Instantiate<T>() where T : Object
    {
      return Object.Instantiate(_assets.GetPrefab<T>());
    }
  }
}
