using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class TetraminoData
    {
        private enum ShiftDirection
        {
            down = -1,
            left = -1,
            right = 1
        }
        public Action<Cell[,]> TetraminoAction;
        
        private Cell[,] cell = new Cell[4, 4];
        private Cell[,] cellCache = new Cell[4, 4];

        public void Init(bool[,] array)
        {
            int a = 3;
            int b = 16;
            for (int x = 0; x < cell.GetLongLength(0); x++)
            for (int y = 0; y < cell.GetLongLength(1); y++)
            {
                cell[x,y] = new Cell{X = a+x, Y = b+y , init = array[x,y]};
            }
            cellCache = cell;
        }

        public Cell[,] Cell => cell;
        public void Rotate()
        {
            cellCache = cell;
            cell = RotateCell(cell);
            TetraminoAction?.Invoke(cell);
        }
        
      
        
        public void ShiftDown()
        {
            Shift(ShiftDirection.down);
        }

        public void ShiftLeft()
        {
            Shift(ShiftDirection.left);
        }

        public void ShiftRight()
        {
            Shift(ShiftDirection.right);
        }
        
        public void StepBack()
        {
            cell = cellCache;
            TetraminoAction?.Invoke(cell);
        }
        
        private Cell[,] RotateCell(Cell[,] target)
        {
            bool[,] figure = new bool[4,4];
            
            for (int x = 0; x < target.GetLongLength(0); x++)
            for (int y = 0; y < target.GetLongLength(1); y++)
            {
                figure[x, y] = target[x, y].init;
            }

            figure = RotateMatrix(figure);
            
            for (int x = 0; x < target.GetLongLength(0); x++)
            for (int y = 0; y < target.GetLongLength(1); y++)
            {
                target[x, y].init = figure[x, y];
            }

            return target;
        }

        private void Shift(ShiftDirection dir)
        {
            cellCache = cell;
            for (int x = 0; x < cell.GetLongLength(0); x++)
            for (int y = 0; y < cell.GetLongLength(1); y++)
            {
                if(dir == ShiftDirection.down)
                  cell[x, y].Y -= 1;
                else
                    cell[x, y].X += (int) dir;
            }
            TetraminoAction?.Invoke(cell);
        }

        private bool[,] RotateMatrix(bool[,] matrix) {
            bool[,] ret = new bool[4,4];

            for (int i = 0; i < 4; ++i) {
                for (int j = 0; j <4; ++j) {
                    ret[i, j] = matrix[4 - j - 1, i];
                }
            }

            return ret;
        }
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
        
