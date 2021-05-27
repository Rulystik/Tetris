using System;
using System.Collections.Generic;
using System.Linq;
using Infastracture.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastracture
{
  [CreateAssetMenu]
  public class PrefabProvider : ScriptableObject
  {
    [SerializeField] List<ObjectGameObject> _prefabs;
    [SerializeField] private Dictionary<Type,GameObject> _dictionary;
    
    public GameObject GetPrefab<T>() where T: Object
    {
      // if (_dictionary == null)
      // {
      //   Init();
      // }
      //
      // if (_dictionary.ContainsKey(typeof(T)) == false)
      // {
      //   Debug.Log($"{name} cannot find prefab: {typeof(T).Name}");
      //   return null;
      // }

      return _prefabs.FirstOrDefault(i => i._id is T  )?._item;
      // return _dictionary[typeof(T)];
    }
    // public T GetPrefab2<T>() where T: Type
    // {
    //   
    //   var t = _prefabs.FirstOrDefault(i => i._id as T  )._id;
    //   
    //   // return _dictionary[typeof(T)];
    // }

    public void Init()
    {
      _dictionary = new Dictionary<Type, GameObject>(_prefabs.Count);
      foreach (ObjectGameObject o in _prefabs)
      {
        _dictionary.Add(o._id.GetType(), o._item);
      }
    }
    
    [Serializable]
    private class ObjectGameObject
    {
      [SerializeField] public Object _id;

      [SerializeField] public GameObject _item;
    }

  }
}