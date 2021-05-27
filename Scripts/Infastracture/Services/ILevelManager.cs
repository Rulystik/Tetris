using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public interface ILevelManager : IService
  {
    T GetOrCreate<T>() where T : Object;
    List<Component> GetAll<T>() where T : Object;
    T Create<T>(bool includeInactive = false) where T : Object;
  }
}