using System;
using Infastracture;
using Infastracture.Services;
using Infastracture.Utils;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FieldView : MonoBehaviour
{[SerializeField] private GUIStyle _guiStyle;

  public Action<ShiftDirection> Input;

  [Space] [SerializeField] private GridLayoutGroup _grid;
  [SerializeField] private IModelView _model;

  [SerializeField]private ILevelManager _levelManager;
  [SerializeField] private TetrominoView[,] _fieldView;
  [SerializeField] private MyButton _rotateButton;
  [SerializeField] private MyButton _leftButton;
  [SerializeField] private MyButton _rightButton;
  [SerializeField] private MyButton _downButton;
  [SerializeField] public Cooldown cooldownDown;
  [SerializeField] public Cooldown cooldownSide;

  private void OnEnable()
  {
    _rotateButton.Down += Rotate;
      
    _leftButton.Hold += LeftShift;
    _rightButton.Hold += RightShift;
    _downButton.Hold += DownShift;

    _downButton.Down += DownDown;
    _rightButton.Down += RightDown;
    _leftButton.Down += LightDown;

    _downButton.Up += DownUp;
    _rightButton.Up += SideUp;
    _leftButton.Up += SideUp;
  }

  private void Start()
  {
      
    var rec = (_grid.transform as RectTransform).rect.size;
    _grid.cellSize = new Vector2((rec.x / 10 - _grid.spacing.x), ((rec.y / 20 - _grid.spacing.y)));
    cooldownDown = new Cooldown(0.03f, 0.17f);
    cooldownSide = new Cooldown(0.07f, 0.17f);
  }

  private void OnDisable()
  {
    _rotateButton.Down -= Rotate;
    _leftButton.Hold -= LeftShift;
    _rightButton.Hold -= RightShift;
    _downButton.Hold -= DownShift;
  }

  private void LightDown()
  {
    Input?.Invoke(ShiftDirection.left);
  }

  private void RightDown()
  {
    Input?.Invoke(ShiftDirection.right);
  }

  private void DownDown()
  {
    Input?.Invoke(ShiftDirection.down);
  }

  private void SideUp()
  {
    cooldownSide.Reset();
  }


  private void DownShift()
  {
    if (cooldownDown.isDone())
      Input?.Invoke(ShiftDirection.down);
  }

  private void DownUp()
  {
    cooldownDown.Reset();
  }

  private void RightShift()
  {
    if (cooldownSide.isDone())
      Input?.Invoke(ShiftDirection.right);
  }

  private void LeftShift()
  {
    if (cooldownSide.isDone())
      Input?.Invoke(ShiftDirection.left);
  }

  private void Rotate()
  {
    Input?.Invoke(ShiftDirection.rotate);
  }

  public void Init(IModelView model,  ILevelManager levelManager)
  {
    _levelManager = levelManager;
      
    _model = model;
    _model.FieldCreate += OnFieldCreate;
    _model.FieldChanged += OnFieldChange;
    _model.CellChange += OnCellChange;
  }

  private void OnCellChange(Cell cell)
  {
    _fieldView[cell.X, cell.Y].IsActive = cell.Value;
  }

  private void OnFieldCreate(bool[,] field)
  {
    InitField(field);
  }

  private void OnFieldChange(int x, int y, bool value)
  {
    if (_fieldView != null && _fieldView[x, y].IsActive != value)
    {
      _fieldView[x, y].IsActive = value;
    }
  }

  private void InitField(bool[,] field)
  {
    if (_fieldView != null)
      return;

    var rec = (_grid.transform as RectTransform).rect.size;
    _grid.cellSize = new Vector2((rec.x / Model.XCount - _grid.spacing.x), (rec.y / Model.YCount - _grid.spacing.y));
    _fieldView = new TetrominoView[Model.XCount, Model.YCount];

    for (int y = 0; y < Model.YCount; y++)
    for (int x = 0; x < Model.XCount; x++)
    {
      var c = _levelManager.Create<TetrominoView>(true);
     
      // var c = Instantiate(tetrominoViewPrefab, _grid.transform);
      c.transform.SetParent(_grid.transform);
      c.name = $"[{x},{y}]";
      c.IsActive = field[x, y];
      _fieldView[x, y] = c;
    }
  }

  private void OnDrawGizmos()
  {
    if (_fieldView != null)
    {
      Handles.color = Color.cyan;
      foreach (var tetraminoView in _fieldView)
      {
        if (tetraminoView.IsActive)
        {
          Handles.Label(tetraminoView.transform.position, tetraminoView.name, _guiStyle);
        }
      }
    }
  }
}