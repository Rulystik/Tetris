using System.Collections.Generic;
using System.Linq;
using Infastracture.Services;
using UnityEngine;

namespace Infastracture.Controllers
{
  public class CreateTetromino : ControllerStateBase
  {
    public CreateTetromino(ControllerStateMachine controllerStateMachine, IAllServices services) : base(controllerStateMachine, services)
    {
    }

    private Model _model;
    private Model Model => _model ?? (_model = _services.GetSingle<Model>());
    public override void Init()
    {
    }

    public override void Enter()
    {
      CreateNewFigure();
    }
    private void CreateNewFigure()
    {
      var newFigure = Model.FiguresBlank.GetRandomFigure();
      Model.Tetromino.CreateNew(newFigure);
      ToNextState();
      
    }
    
    public override void Exit()
    {
    }

    public override void Destroy()
    {
    }
    
    private void ToNextState()
    {
      List<Cell> changes = GetCellsChanges();
      
      if (IsStepOnCollizionCell())
      {
        _controllerStateMachine.SetState<GameOverState>();
        return;
      }

      foreach (Cell cell in changes)
        _model.SetFieldValue(cell);
      
      _controllerStateMachine.SetState<PlayerWaiting>();
    }
    
    private List<Cell> GetCellsChanges()
    {
      var newCells = _model.Tetromino.Cell.Cast<Cell>().Where(i => i.Value);
      if (_model.Tetromino.CellCache == null)
      {
        return newCells.Where(i => i.Value == true).Select(c => new Cell() {X = c.X, Y = c.Y, Value = c.Value}).ToList();
      }

      var cells = _model.Tetromino.CellCache.Cast<Cell>()
        .Where(i => i.Value == true)
        .Where(o => newCells.Any(o.EqualsPosition) == false)
        .Select(i => new Cell() {X = i.X, Y = i.Y, Value = false})
        .Union(newCells);

      return cells.ToList();
    }
    
    private bool IsStepOnCollizionCell()
    {
      var newCells = _model.Tetromino.GetNewExtractCache();

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
    
  }
}