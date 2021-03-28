using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Tetromino _tetrominoPrefab;
        
        [Space]
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private IModelView _model;
        [SerializeField] private Tetromino _tetromino;

        [SerializeField] private Tetromino[,] _fieldView;

        public void Init(IModelView model)
        {
            _model = model;
            _model.FieldCreate += OnFieldCreate;
            _model.FieldChanged += OnFieldChange;
        }

        private void OnFieldCreate(bool[,] field)
        {
            if (_fieldView == null)
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
            var xLence = field.GetLength(0);
            var yLence = field.GetLength(1);
            var rec = (_grid.transform as RectTransform).rect.size;
            _grid.cellSize = new Vector2((rec.x / xLence - _grid.spacing.x),( (rec.y / yLence - _grid.spacing.y)));
            _fieldView = new Tetromino[field.GetLength(0),field.GetLength(1)];

            for (int x = 0; x < field.GetLength(0); x++)
            for (int y = 0; y < field.GetLength(1); y++)
            {
                var c = Instantiate(_tetrominoPrefab, _grid.transform);
                c.name = $"[{x},{y}]";
                c.IsActive = field[x, y];
                _fieldView[x, y] = c;
            }
            
            
        }
    }
}