using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IModelView
    {
        event Action <bool[,]> FieldCreate ;
        event Action <int,int,bool> FieldChanged ;
    }
    
    
    
    
    [Serializable]
    public class Model : IModelView
    {
        [SerializeField]
        private bool[,] _activeObj;
        
        public Action <bool[,]> ActiveObjChange ;

        public bool[,] ActiveObj
        {
            get => _activeObj;
            set
            {
                if (value != _activeObj)
                {
                    _activeObj = value;
                    ActiveObjChange?.Invoke(_activeObj);
                }
            }
        }
        
        [SerializeField]
        private bool[,] _field ;
        
        public event Action <bool[,]> FieldCreate ;
        public event Action <int,int,bool> FieldChanged ;

        public bool[,] Field
        {
            set
            {
                if (value != _field)
                {
                    _field = value;
                    FieldCreate?.Invoke(_field);
                }
            }
        }

        public void SetFieldValue(int x, int y, bool value)
        {
            if (_field[x, y] != value)
            {
                _field[x, y] = value;
                FieldChanged?.Invoke(x, y, value);
            }
        }

    }

   
}