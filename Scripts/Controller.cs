    using DefaultNamespace;

    public class Controller
    {
        private Model _model;
        private View _view;

        public Controller(Model model, View view)
        {
            _model = model;
            _view = view;
         InitModel();   
        }
        void InitModel()
        {
            _model.Field = new bool[10,20];
            _model.SetFieldValue(8,5,true);
        }

    }
