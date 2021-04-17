using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
    public interface IModelView
    {
        event Action <bool[,]> FieldCreate ;
        event Action <Cell> CellChange ;
        event Action <int,int,bool> FieldChanged ;
    }

    [Serializable]
    public class Model : IModelView
    {
        public const int XCount = 10;
        public const int YCount = 20;
        public static readonly int[] startPos = {9,13};
        private Figures _figures;
        public Figures Figures => _figures ?? (_figures = new Figures()); 
        
        [SerializeField]
        private bool[,] _activeObj;
        
        
        public Action <bool[,]> ActiveObjChange ;
        public Action <Cell[,]> Tetromino ;

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
        public event Action<Cell> CellChange;
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

        public List<Cell> FieldDropperCell { get; private set; }

        public Model()
        {
            FieldDropperCell = new List<Cell>();
        }
        public void SetFieldValue(int x, int y, bool value)
        {
            if (_field[x, y] != value)
            {
                _field[x, y] = value;
                FieldChanged?.Invoke(x, y, value);
            }
        } 
        public void SetFieldValue(Cell cell)
        {
            if (_field[cell.X, cell.Y]  != cell.init)
            {
                _field[cell.X, cell.Y] = cell.init;
                CellChange?.Invoke(cell);
            }
        }

    }

    public struct Cell
    {
        public int X, Y;
        public bool init;

        public bool EqualsPosition(Cell cell)
        {
            return X == cell.X && Y == cell.Y;
        }

        public override string ToString()
        {
            return $"x{X}y{Y}:{init}";
        }
    }
   
}