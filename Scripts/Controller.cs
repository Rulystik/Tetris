using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DefaultNamespace;
using UnityEngine;

public class Controller
{
  private Model _model;
  private readonly View _view;
  private TetraminoData _tetramino = new TetraminoData();

  public Controller(Model model, View view)
  {
    _model = model;
    _view = view;
    _view.Input += OnInput;
    _tetramino.NewPosition += OnTetraminoChange;
    _tetramino.NewFigure += OnTetraminoCreate;
    InitModel();
    StartPlay();
  }

  private void OnInput(ShiftDirection direction)
  {
    _tetramino.Shift(direction);
  }

  private void OnTetraminoCreate()
  {
    List<Cell> changes = GetCellsChanges();

    foreach (Cell cell in changes)
      _model.SetFieldValue(cell);

    if (IsStepOnCollizionCell())
    {
      Debug.Log($"Game Over");
      return;
    }
  }

  private void OnTetraminoChange()
  {
    ErrorType error = IsValide();

    if (error != ErrorType.Success) 
    {
      if (!_tetramino.TryStepBack())
      {
        // TODO Set ToField CreatedFigure
        Debug.Log($"Game Over");
        return;
      }

      if (_tetramino.Direction == ShiftDirection.down)
      {
        Debug.Log($"OnGround => TODO Create NEw");
       // TODO delete on field win lines 

        CreateNewFigure();
      }
      else
        Debug.Log($"error: {error}  you cannot do: {_tetramino.Direction}");

      return;
    }

    List<Cell> changes = GetCellsChanges();

    foreach (Cell cell in changes)
      _model.SetFieldValue(cell);
  }

  private ErrorType IsValide()
  {
    IEnumerable<Cell> cells = _tetramino.Cell.Cast<Cell>();
    ShiftDirection dir = _tetramino.Direction;
    if ((dir == ShiftDirection.left || dir == ShiftDirection.rotate) && IsOutOfBorderLeft())
    {
      return ErrorType.Outleft;
    }

    if ((dir == ShiftDirection.right || dir == ShiftDirection.rotate) && IsOutOfBorderRight())
    {
      return ErrorType.OutRight;
    }

    if ((dir == ShiftDirection.down || dir == ShiftDirection.rotate) && IsOutOfBorderDown())
    {
      return ErrorType.OutDown;
    }

    if (IsStepOnCollizionCell())
    {
      return ErrorType.ColizionCell;
    }

    return ErrorType.Success;


    bool IsOutOfBorderLeft()
    {
      return cells.Any(cell => cell.Value && cell.X < 0);
    }

    bool IsOutOfBorderRight()
    {
      return cells.Any(cell => cell.Value && cell.X >= Model.XCount);
    }

    bool IsOutOfBorderDown()
    {
      return cells.Any(cell => cell.Value && cell.Y < 0);
    }
  }

  private bool IsStepOnCollizionCell()
  {
    var newCells = _tetramino.GetNewExtractCache();

    for (int x = 0; x < _model.Field.GetLength(0); x++)
    for (int y = 0; y < _model.Field.GetLength(1); y++)
    {
      if (_model.Field[x, y] == true)
      {
        for (int i = 0; i < newCells.Count; i++)
        {
          if (newCells[i].X == x && newCells[i].Y == y)
          {
            return true;
          }
        }
      }
    }

    return false;
  }

  private List<Cell> GetCellsChanges()
  {
    var newCells = _tetramino.Cell.Cast<Cell>().Where(i => i.Value);
    if (_tetramino.CellCache == null)
    {
      return newCells.Where(i => i.Value == true).Select(c => new Cell() {X = c.X, Y = c.Y, Value = c.Value}).ToList();
    }

    var cells = _tetramino.CellCache.Cast<Cell>()
      .Where(i => i.Value == true)
      .Where(o => newCells.Any(o.EqualsPosition) == false)
      .Select(i => new Cell() {X = i.X, Y = i.Y, Value = false})
      .Union(newCells);

    return cells.ToList();
  }

  private void StartPlay()
  {
    CreateNewFigure();
  }

  private void CreateNewFigure()
  {
    var newFigure = _model.FiguresBlank.GetRandomFigure();
    _tetramino.CreateNew(newFigure);
  }

  private void InitModel()
  {
    _model.Field = new bool[10, 20];
  }
}