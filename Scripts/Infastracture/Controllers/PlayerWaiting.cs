using System.Collections.Generic;
using System.Linq;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.Controllers
{
  public class PlayerWaiting : ControllerStateBase
  {
    private readonly AutoProperty<ILevelManager> _levelManager; 
    private FieldView _view;
    private Model _model;
    private TetraminoData _tetramino => _model.Tetromino;

    public PlayerWaiting(ControllerStateMachine controllerStateMachine, IAllServices services) : base(controllerStateMachine, services)
    {
      _levelManager = new AutoProperty<ILevelManager>(_services.GetSingle<ILevelManager>);
      _model = _services.GetSingle<Model>();
      
    }

    public override void Init()
    {
    }

    public override void Enter()
    {
      Debug.Log($"{this.GetType().Name}");
      _view = _levelManager.Get().GetOrCreate<FieldView>();
      _tetramino.NewPosition += OnTetraminoChange;
      _tetramino.NewFigure += OnTetraminoCreate;
      _view.Input += OnInput;
    }

    public override void Exit()
    {
      _tetramino.NewPosition -= OnTetraminoChange;
      _tetramino.NewFigure -= OnTetraminoCreate;
      _view.Input -= OnInput;
    }

    public override void Destroy()
    {
    }
    private void OnTetraminoCreate()
    {
      List<Cell> changes = GetCellsChanges();

      if (IsStepOnCollizionCell())
      {
       _controllerStateMachine.SetState<GameOverState>();
      }

      foreach (Cell cell in changes)
        _model.SetFieldValue(cell);
    }
    
    private void OnTetraminoChange()
    {
      ErrorType error = IsValide();

      if (error != ErrorType.Success) 
      {
        if (! _tetramino.TryStepBack())
        {
          // TODO Set ToField CreatedFigure
          Debug.Log($"Game Over");
          _controllerStateMachine.SetState<GameOverState>();
          return;
        }

        if ( _tetramino.Direction == ShiftDirection.down)
        {
          Debug.Log($"OnGround => TODO Create NEw");
          // TODO delete on field win lines 
         // _controllerStateMachine.SetState<CheckFullLinesState>();
          if (TryGetFullLinesForDelete(out List<int> lines))
          {
            Debug.Log($"delete list {string.Join(" ", lines)}");
            // TODO remove lines
          }
          else
          {
            
          }

          CreateNewFigure();
        }
        else
          Debug.Log($"error: {error}  you cannot do: { _tetramino.Direction}");

        return;
      }

      List<Cell> changes = GetCellsChanges();

      foreach (Cell cell in changes)
        _model.SetFieldValue(cell);
    }
    
    private bool TryGetFullLinesForDelete(out List<int> lines)
    {
      List<int> YList = new List<int>();
      for (int x = 0; x < _tetramino.Cell.GetLength(0); x++)
      for (int y = 0; y < _tetramino.Cell.GetLength(1); y++)
      {
        if (_tetramino.Cell[x, y].Value == true && !YList.Contains(_tetramino.Cell[x, y].Y))
        {
          YList.Add(_tetramino.Cell[x, y].Y);
        }
      }
    
      for (int y = 0; y < YList.Count; y++)
      for (int x = 0; x < Model.XCount; x++)
      {
        if (_model.Field[x, YList[y]] == false)
        {
          YList.RemoveAt(y);
          y--;
          break;
        }
      }

      if (YList.Any())
      {
        
      }

      lines = YList;
      return lines.Any();
      
    }
    
    private void CreateNewFigure()
    {
      var newFigure = _model.FiguresBlank.GetRandomFigure();
      _tetramino.CreateNew(newFigure);
    }
    
    private void OnInput(ShiftDirection direction)
    {
      _tetramino.Shift(direction);
    }
    
     private List<Cell> GetCellsChanges()
      {
      var newCells =  _tetramino.Cell.Cast<Cell>().Where(i => i.Value);
        if ( _tetramino.CellCache == null)
      {
        return newCells.Where(i => i.Value == true).Select(c => new Cell() {X = c.X, Y = c.Y, Value = c.Value}).ToList();
      }
    
      var cells =  _tetramino.CellCache.Cast<Cell>()
          .Where(i => i.Value == true)
          .Where(o => newCells.Any(o.EqualsPosition) == false)
          .Select(i => new Cell() {X = i.X, Y = i.Y, Value = false})
          .Union(newCells);
    
        return cells.ToList();
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
     
  }

  internal class CreateNewFigureState : ControllerStateBase
  {
    private Model _model;
    private TetraminoData _tetramino => _model.Tetromino;

    public CreateNewFigureState(ControllerStateMachine controllerStateMachine, IAllServices services) : base(controllerStateMachine, services)
    {
      _model = _services.GetSingle<Model>();
    }
    
    

    public override void Init()
    {
      
    }

    public override void Enter()
    {
      CreateNewFigure();
      
    }

    public override void Exit()
    {
    }

    public override void Destroy()
    {
    }
    
    private void CreateNewFigure()
    {
      var newFigure = _model.FiguresBlank.GetRandomFigure();
      _tetramino.CreateNew(newFigure);
    }
  }
}