namespace DefaultNamespace
{
  public struct Cell
  {
    public int X, Y;
    public bool Value;

    public bool EqualsPosition(Cell cell)
    {
      return X == cell.X && Y == cell.Y;
    }

    public bool EqualsPositionAndValue(Cell cell)
    {
      return EqualsPosition(cell) && Value == cell.Value;
    }
    public override string ToString()
    {
      return $"x{X}y{Y}:{Value}";
    }
  }
}