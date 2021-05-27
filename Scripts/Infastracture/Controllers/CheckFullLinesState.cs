using System.Collections.Generic;
using System.Linq;
using Infastracture.Services;
using Infastracture.Utils;
using UnityEngine;

namespace Infastracture.Controllers
{
  internal class CheckFullLinesState : ControllerStateBase
  {
    private readonly AutoProperty<ILevelManager> _levelManager; 
    private FieldView _view;
    private Model _model;
    private TetraminoData _tetramino => _model.Tetromino;

    public CheckFullLinesState(ControllerStateMachine controllerStateMachine, IAllServices services) : base(controllerStateMachine, services)
    {
      _levelManager = new AutoProperty<ILevelManager>(_services.GetSingle<ILevelManager>);
      _model = _services.GetSingle<Model>();
      
    }

    public override void Init()
    {
    }

    public override void Enter()
    {
      if (TryGetFullLinesForDelete(out List<int> lines))
      {
        Debug.Log($"delete list {string.Join(" ", lines)}");
        // TODO remove lines
      }
      else
      {
        _controllerStateMachine.SetState<CreateNewFigureState>(); 
      }

    }

    public override void Exit()
    {
    }

    public override void Destroy()
    {
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
  }
}