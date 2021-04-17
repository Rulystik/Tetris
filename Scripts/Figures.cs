using System.Collections.Generic;

namespace DefaultNamespace
{
    public enum ShiftDirection
    {
        down,
        left,
        right,
        rotate
    }

    public class Figures
    {
        public Figures()
        {
            figureList.Add(O);
            figureList.Add(L);
            figureList.Add(J);
            figureList.Add(S);
            figureList.Add(Z);
            figureList.Add(E);
            figureList.Add(I);
        }

        public bool[,] GetRandomFigure()
        {
            int index = UnityEngine.Random.Range(0, figureList.Count);
            return figureList[index];
        }

        
        private List<bool[,]> figureList = new List<bool[,]>();

        private bool[,] O =
        {
            {false, false, false, false},
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false},
        };

        private bool[,] L =
        {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, true, false},
            {false, false, false, false},
        };

        private bool[,] J =
        {
            {false, false, true, false},
            {false, false, true, false},
            {false, true, true, false},
            {false, false, false, false},
        };

        private bool[,] S =
        {
            {false, false, false, false},
            {false, false, true, true},
            {false, true, true, false},
            {false, false, false, false},
        };

        private bool[,] Z =
        {
            {false, false, false, false},
            {true, true, false, false},
            {false, true, true, false},
            {false, false, false, false},
        };

        private bool[,] E =
        {
            {false, false, false, false},
            {true, true, true, false},
            {false, true, false, false},
            {false, false, false, false},
        };

        private bool[,] I =
        {
            {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false},
        };
    }
}
        
