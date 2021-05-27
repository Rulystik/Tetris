using System;
using System.Collections.Generic;
using Infastracture.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public interface IModelView : IService
{
    event Action NewFigure; 
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

    public event Action <bool[,]> FieldCreate ;
    public event Action <int,int,bool> FieldChanged ;
    public event Action<Cell> CellChange;
    public event Action NewFigure;
    private FiguresBlank _figuresBlank;
    public FiguresBlank FiguresBlank => _figuresBlank ?? (_figuresBlank = new FiguresBlank());

    private TetraminoData _tetromino = new TetraminoData();


    [SerializeField]
    private bool[,] _field ;

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

    public TetraminoData Tetromino
    {
        get => _tetromino;
        set => _tetromino = value;
    }

    public Model()
    {
        Tetromino.NewFigure = NewFigure; 
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