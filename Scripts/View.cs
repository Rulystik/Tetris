using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class View : MonoBehaviour
    {
        public Action RotareAction;
        
        [SerializeField] private TetraminoView tetraminoViewPrefab;
       
        [Space]
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private IModelView _model;

        [SerializeField] private TetraminoView[,] _fieldView;
        [SerializeField] private Button _rotateButton;
        private void Start()
        {
            var rec = (_grid.transform as RectTransform).rect.size;
            _grid.cellSize = new Vector2((rec.x / 10 - _grid.spacing.x),( (rec.y / 20 - _grid.spacing.y)));
            _rotateButton.onClick.AddListener(Rotate);
        }

        private void Rotate()
        {
            RotareAction?.Invoke();
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

        public void Update()
        {
            
        }
    }
}