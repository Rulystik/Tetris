    using System;
    using DefaultNamespace;

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
            _view.RotareAction += OnRotareAction;
            InitModel();
            _tetramino.TetraminoAction += OnTetraminoAction;
            StartPlay();
        }

        private void OnRotareAction(ShiftDirection direction)
        {
            switch (direction)
            {
                case ShiftDirection.down:
                    _tetramino.ShiftDown();
                    break;
                case ShiftDirection.left:
                    _tetramino.ShiftLeft();
                    break;
                case ShiftDirection.right:
                    _tetramino.ShiftRight();
                    break;
                case ShiftDirection.rotate:
                    _tetramino.Rotate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void OnTetraminoAction(Cell[,] obj)
        {
            foreach (Cell cell in _tetramino.Cell)
                _model.SetFieldValue(cell);
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
