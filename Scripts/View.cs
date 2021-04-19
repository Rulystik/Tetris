using System;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class View : MonoBehaviour
    {
        [SerializeField] private GUIStyle _guiStyle;
        public Action<ShiftDirection> Input;
        // public Action LeftAction;
        // public Action RightAction;
        
        [SerializeField] private TetraminoView tetraminoViewPrefab;
       
        [Space]
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private IModelView _model;

        [SerializeField] private TetraminoView[,] _fieldView;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _downButton;
        private void Start()
        {
            var rec = (_grid.transform as RectTransform).rect.size;
            _grid.cellSize = new Vector2((rec.x / 10 - _grid.spacing.x),( (rec.y / 20 - _grid.spacing.y)));
            _rotateButton.onClick.AddListener(Rotate);
            _leftButton.onClick.AddListener(LeftShift);
            _rightButton.onClick.AddListener(RightShift);
            _downButton.onClick.AddListener(DownShift);
        }

        private void DownShift()
        {
            Input?.Invoke(ShiftDirection.down);
        }

        private void RightShift()
        {
            Input?.Invoke(ShiftDirection.right);
        }

        private void LeftShift()
        {
            Input?.Invoke(ShiftDirection.left);
        }
        
        private void Rotate()
        {
            Input?.Invoke(ShiftDirection.rotate);
        }

        public void Init(IModelView model)
        {
            _model = model;
            _model.FieldCreate += OnFieldCreate;
            _model.FieldChanged += OnFieldChange;
            _model.CellChange += OnCellChange;
        }

        private void OnCellChange(Cell cell)
        {
            _fieldView[cell.X, cell.Y].IsActive = cell.init ;
        }

        private void OnFieldCreate(bool[,] field)
        {
            InitField(field);
        }
        
        private void OnFieldChange(int x, int y, bool value)
        {
            if (_fieldView != null && _fieldView[x,y].IsActive != value)
            {
                _fieldView[x, y].IsActive = value ;
            }
        }

        private void InitField(bool[,] field)
        {
            if (_fieldView != null)
                return;
            
            var rec = (_grid.transform as RectTransform).rect.size;
            _grid.cellSize = new Vector2((rec.x / Model.XCount - _grid.spacing.x),( (rec.y / Model.YCount - _grid.spacing.y)));
            _fieldView = new TetraminoView[Model.XCount, Model.YCount];

            for (int y = 0; y < Model.YCount; y++)
            for (int x = 0; x < Model.XCount; x++)
            {
                var c = Instantiate(tetraminoViewPrefab, _grid.transform);
                c.name = $"[{x},{y}]";
                c.IsActive = field[x,y];
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
                        Handles.Label(tetraminoView.transform.position, tetraminoView.name,_guiStyle);
                    }
                }
            }
            
        }
    }
}