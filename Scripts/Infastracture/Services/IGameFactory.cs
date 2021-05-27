using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public interface IGameFactory : IService
  {
    GameObject Instantiate<T>() where T: Object;
  }
}