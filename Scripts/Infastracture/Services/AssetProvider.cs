using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public class AssetProvider : IAssets
  {
    private PrefabProvider _prefabProvider;

    private Dictionary<Type, GameObject> _prefabs;
    private Dictionary<Type, GameObject> _prefabs2;
    public AssetProvider()
    {
      _prefabProvider = Resources.Load<PrefabProvider>(nameof(PrefabProvider));
      _prefabProvider.Init();
    }

   

    public GameObject GetPrefab<T>() where T : Object
    {
      return _prefabProvider.GetPrefab<T>();
    }
     public T GetPrefab2<T>() where T : Object
    {
      return _prefabProvider.GetPrefab<T>() as T;
    }
    
    public GameObject Get(string path)
    {
      var result = Resources.Load<GameObject>(path);
      if (result == null)
      {
        Debug.Log($" cannot load {path}");
        return null;
      }
      return result;
    }
    
  }

}