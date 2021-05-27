using System;

namespace Infastracture.Utils
{
  public class AutoProperty<T>
  {
    private readonly Func<T> _func;
    private T _instance;

    public T Get()
    {
      return _instance != null ? _instance  : (_instance = _func.Invoke());
    }
    public AutoProperty(Func<T> func)
    {
      _func = func;
    }
  }
}