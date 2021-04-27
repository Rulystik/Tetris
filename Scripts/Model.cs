using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
        
        public static readonly int[] startPos = {3,16};
        
        private FiguresBlank _figuresBlank;
        public FiguresBlank FiguresBlank => _figuresBlank ?? (_figuresBlank = new FiguresBlank()); 
        
        // private List<Cell> _dropperFigures;

        // public Action <List<Cell>> DroppedFigureAction ;
        public Action <Cell[,]> Tetromino ;

        // public List<Cell> DroppedFigure
        // {
        //     get => _dropperFigures;
        //     set
        //     {
        //         if (value != _dropperFigures)
        //         {
        //             _dropperFigures = value;
        //             DroppedFigureAction?.Invoke(_dropperFigures);
        //         }
        //     }
        // }
        
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
            get => _field;
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
            if (_field[cell.X, cell.Y]  != cell.Value)
            {
                _field[cell.X, cell.Y] = cell.Value;
                CellChange?.Invoke(cell);
            }
        }

    }
}