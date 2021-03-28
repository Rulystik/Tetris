using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Tetramino tetraminoPrefab;
       
        [Space]
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private IModelView _model;
        [SerializeField] private Tetramino tetramino;

        [SerializeField] private Tetramino[,] _fieldView;

        private void Start()
        {
            var rec = (_grid.transform as RectTransform).rect.size;
            _grid.cellSize = new Vector2((rec.x / 10 - _grid.spacing.x),( (rec.y / 20 - _grid.spacing.y)));
        }

        public void Init(IModelView model)
        {
            _model = model;
            _model.FieldCreate += OnFieldCreate;
            _model.FieldChanged += OnFieldChange;
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
            _fieldView = new Tetramino[Model.XCount, Model.YCount];

            for (int x = 0; x < Model.XCount; x++)
            for (int y = 0; y < Model.YCount; y++)
            {
                var c = Instantiate(tetraminoPrefab, _grid.transform);
                c.name = $"[{x},{y}]";
                c.IsActive = field[x,y];
                _fieldView[x, y] = c;
            }
            
            
        }
    }
}