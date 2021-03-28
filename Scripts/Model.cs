using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Model
    {
        
        private List<int> _field;


        public List<int> Field
        {
            get => _field;
            set => _field = value;
        }
    }
}