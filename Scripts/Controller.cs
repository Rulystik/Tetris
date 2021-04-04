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
            InitModel();
            StartPlay();
        }

        
        private void StartPlay()
        {
            var newFigure = _figures.GetRandomFigure();
            _tetramino.Init(newFigure);
            
            foreach (Cell cell in _tetramino.Cell)
                _model.SetFieldValue(cell);
        }

        void InitModel()
        {
            _model.Field = new bool[10, 20];
            // _model.SetFieldValue(new Cell() {X = 8, Y = 5, init = true});
        }
        
        
       
    }
