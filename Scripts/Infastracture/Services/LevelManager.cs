using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture.Services
{
  public class LevelManager : ILevelManager
  {
    private readonly IGameFactory _factory;
    private Dictionary<Type, List<GameObject>> childsDictionary = new Dictionary<Type, List<GameObject>>();

    public LevelManager(IGameFactory factory)
    {
      _factory = factory;
    }

    public T GetOrCreate<T>() where T : Object
    {
      System.Type type = typeof(T);
      if (childsDictionary.ContainsKey(type))
      {
        for (int i = 0; i < childsDictionary[type].Count; i++)
        {
          if (childsDictionary[type][i] == null)
          {
            childsDictionary[type].Remove(childsDictionary[type][i]);
            i--;
          }
          else
          {
            return childsDictionary[type][i].GetComponentInChildren(typeof(T), true) as T;
          }
        }
        var go = CreateGameObject<T>();
        childsDictionary[type].Add(go);
      }
      else
      {
        var go = CreateGameObject<T>();
        childsDictionary.Add(type, new List<GameObject> {go});
      }

      return childsDictionary[type][0].GetComponentInChildren(typeof(T), true) as T;
    }

    public T Create<T>(bool includeInactive = false) where T : Object
    {
      Type type = typeof(T);
      var go = CreateGameObject<T>();

      if (childsDictionary.ContainsKey(type))
      {
        for (int i = 0; i < childsDictionary[type].Count; i++)
        {
          if (childsDictionary[type][i] == null)
          {
            childsDictionary[type].Remove(childsDictionary[type][i]); // clear null objects
            i--;
          }
        }

        childsDictionary[type].Add(go);
      }
      else
      {
        // childsDictionary.Add(type, new List<Type> {go});
      }

      return go.GetComponentInChildren(typeof(T), includeInactive) as T;
    }

    private GameObject CreateGameObject<T>() where T : Object
    {
      return _factory.Instantiate<T>();
    }

    public List<Component> GetAll<T>() where T : Object
    {
      var type = typeof(T);
      return childsDictionary.ContainsKey(type) ? childsDictionary[type].Select(i => i.GetComponentInChildren(typeof(T))).ToList() : null;
    }

    public void Remove(GameObject c)
    {
      foreach (var list in childsDictionary.Values)
      {
        if (list.Contains(c))
        {
          Object.Destroy(c);
          list.Remove(c);
          Debug.Log($"remove from LevelManager");
          return;
        }
      }
    }
  }
}