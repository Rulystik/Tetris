using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IModelView
    {
        event Action <bool[,]> FieldCreate ;
        event Action <int,int,bool> FieldChanged ;
    }

    public class Figures
    {
        private bool[,] O = {{true,true},{true,true}};
        private bool[,] L = {{true,true},{true,true}};
        private bool[,] J = {{true,true},{true,true}};
        private bool[,] S = {{true,true},{true,true}};
        private bool[,] Z = {{true,true},{true,true}};
        private bool[,] E = {{true,true},{true,true}};
    }
    
    
    
    [Serializable]
    public class Model : IModelView
    {
        public const int XCount = 10;
        public const int YCount = 20;
        
        
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