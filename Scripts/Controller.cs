    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DefaultNamespace;
    using UnityEngine;

    public class Controller
    {
        private Model _model;
        private View _view;
        private Figures _figures = new Figures();
        private TetraminoData _tetramino = new TetraminoData();

        public Controller(Model model, View view)
        {
            _model = model;
            _view = view;
            _view.Input += OnInput;
            _tetramino.NewPosition += OnTetraminoChange;
            InitModel();
            StartPlay();
        }

        private void OnInput(ShiftDirection direction)
        {
            _tetramino.Shift(direction);
        }

        private void OnTetraminoChange()
        {
            if (!IsValide())// todo add check if licvide
            {
                _tetramino.StepBack();
                
                if (_tetramino.Direction == ShiftDirection.down)
                    Debug.Log($"OnGround => TODO");
                else
                    Debug.Log($"You cannot do: {_tetramino.Direction}");
                
                return;
            }
            
            List<Cell> changes = GetCellsChanges();
            foreach (Cell cell in changes)
                _model.SetFieldValue(cell);
        }

        private List<Cell> GetCellsChanges()
        {
            var newCells = _tetramino.Cell.Cast<Cell>().Where(i => i.init);
            var cells = _tetramino.CellCache.Cast<Cell>()
                .Where(i => i.init == true)
                .Where(o => newCells.Any(o.EqualsPosition) == false)
                .Select(i => new Cell() {X = i.X, Y = i.Y, init = false})
                .Union(newCells);
                
            // cells.Union(newCells);
            return cells.ToList();
        }

        private bool IsValide()
        {
            IEnumerable<Cell> cells = _tetramino.Cell.Cast<Cell>();
            ShiftDirection dir = _tetramino.Direction;
             if ((dir == ShiftDirection.left ||dir == ShiftDirection.rotate) && IsOutOfBorderLeft())
             {
                 return false;
             }

             if ((dir == ShiftDirection.right ||dir == ShiftDirection.rotate) && IsOutOfBorderRight())
             {
                 return false;
             }
             if ((dir == ShiftDirection.down ||dir == ShiftDirection.rotate) && IsOutOfBorderDown())
             {
                 return false;
             }

             if (IsStepOnDroppedCell())
             {
                 return false;
             }

             return true;
             
             
             bool IsOutOfBorderLeft()
             {
                 return cells.Any(cell => cell.init && cell.X < 0);
             }
             bool IsOutOfBorderRight()
             {
                 return cells.Any(cell => cell.init && cell.X >= Model.XCount);
             }
            bool IsOutOfBorderDown()
             {
                 return cells.Any(cell => cell.init && cell.Y < 0);
             }

             bool IsStepOnDroppedCell()
             {
                return _model.FieldDropperCell.Any(i=> cells.Any(i.EqualsPosition)); 
             }
            
        }

        // private void OnRotareAction()
        // {
        //     _tetramino.Rotate();
        // }


        private void StartPlay()
        {
            var newFigure = _figures.GetRandomFigure();
            _tetramino.Init(newFigure);
        }

        void InitModel()
        {
            _model.Field = new bool[10, 20];
            // _model.SetFieldValue(new Cell() {X = 8, Y = 5, init = true});
        }
        
        
       
    }
