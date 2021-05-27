using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public interface IAssets : IService
  {
    GameObject Get(string path);
    GameObject GetPrefab<T>() where T : Object;
  }
}