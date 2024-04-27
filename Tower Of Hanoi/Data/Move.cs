namespace Tower_Of_Hanoi.Data;

public class Move(int from, int to)
{
    public int From { get; set; } = from;
    public int To { get; set; } = to;
}