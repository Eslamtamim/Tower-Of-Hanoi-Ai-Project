namespace Tower_Of_Hanoi.Data;

public class HanoiTower
{
    public HanoiTower(int? diskCount = 5, List<Stack<int>>? towers = null)
    {
        DiskCount = diskCount ?? 5;
        MovesCount = 0;
        MoveState = "You haven't moved any disks yet.";
        MoveStateColor = null;
        Towers = towers ?? new List<Stack<int>> { new(Enumerable.Range(1, DiskCount).Reverse()), new(), new() };
        MovesStack = new Stack<Move>();
        TempMove = new Move(-1, -1);
    }

    public int DiskCount { get; set; }
    public int MovesCount { get; set; }
    public string MoveState { get; set; }
    public bool? MoveStateColor { get; set; }
    public List<Stack<int>> Towers { get; set; }
    public Stack<Move> MovesStack { get; set; }
    public Move TempMove { get; set; }

    public readonly List<string> Colors = new()
    {
        "#f0ece2", "#dfd3c3", "#c7b198", "#596e79", "#0e2e3b", "#f0ece2", "#dfd3c3", "#c7b198", "#596e79", "#0e2e3b"
    };


    public void TowerClick(int index)
    {
        if (TempMove.From == -1)
        {
            if (Towers[index].Count == 0)
            {
                MoveState = "There are no disks to move from this tower.";
                MoveStateColor = false;
                return;
            }

            TempMove.From = index;
        }
        else
        {
            TempMove.To = index;
            if (TempMove.From != TempMove.To)
            {
                MoveDiskIfPossible(TempMove.From, TempMove.To);
            }

            TempMove.From = -1;
            TempMove.To = -1;
        }
    }


    public void StepBack()
    {
        if (MovesStack.Count <= 0) return;
        var move = MovesStack.Pop();
        MoveDiskIfPossible(move.To - 1, move.From - 1);
        TempMove = new Move(-1, -1);
        MovesStack.Pop();
    }


    public bool MoveDiskIfPossible(int from, int to)
    {
        if (Towers[from].Count == 0)
        {
            MoveState = "There are no disks to move from this tower.";
            MoveStateColor = false;
            return false;
        }

        if (Towers[to].Count == 0 || Towers[to].Peek() > Towers[from].Peek())
        {
            Towers[to].Push(Towers[from].Pop());
            MoveState = "You moved a disk from tower " + (from + 1) + " to tower " + (to + 1) + ".";
            MovesCount++;
            MoveStateColor = true;
            MovesStack.Push(new Move(from + 1, to + 1));
            if (Node.IsGoal(Towers))
            {
                MoveState = "Solved.";
            }

            return true;
        }

        MoveState = "You cannot move a larger disk on top of a smaller disk.";
        MoveStateColor = false;
        return false;
    }


    public void MoveToGoal(Stack<Move> moves)
    {
    }
}