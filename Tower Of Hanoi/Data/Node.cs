using System.Collections;

namespace Tower_Of_Hanoi.Data;

public class Node
{
    public List<Stack<int>> State { get; set; }
    public string HashedState;
    private int DiskCount { get; set; }
    public Node? Parent { get; set; }
    public List<Node> Successors { get; set; } = new();
    private Move? Move { get; set; }
    public int Huristic => CalcHuristic();
    public int Cost;
    public int TotalCost => CalcHuristic() + Cost;

    public Node(List<Stack<int>> state, Node? parent, Move? move)
    {
        State = state;
        Parent = parent;
        Move = move;
        foreach (var tower in State)
        {
            DiskCount += tower.Count;
        }

        HashedState = GetHash(State);
    }

    int CalcHuristic()
    {
        var cost = 0;
        for (var i = 0; i < State.Count; i++)
        {
            if (i == 0) cost -= State[i].Sum();
            else cost += State[i].Sum();
        }

        return cost;
    }

    public void GenerateSuccessors()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                //  if (i == j) continue;
                //  if (Move?.From == j && Move?.To == i) continue;
                // if (State[i].Any() || (!State[j].Any() && State[j].Peek() <= State[i].Peek())) continue;

                var newState = new List<Stack<int>>();
                foreach (var stack in State)
                {
                    var newStack = new Stack<int>();
                    foreach (var num in stack.Reverse())
                    {
                        newStack.Push(num);
                    }

                    newState.Add(newStack);
                }

                var res = Transfer(newState, new Move(i, j));
                if (res is null) continue;
                var newNode = new Node(newState, this, new Move(i, j));
                Successors.Add(newNode);
            }
        }
    }

    private static List<Stack<int>>? Transfer(List<Stack<int>> state, Move action)
    {
        if (state[action.From].Count == 0) return null;
        if (state[action.To].Count == 0 || state[action.To].Peek() > state[action.From].Peek())
        {
            state[action.To].Push(state[action.From].Pop());
            return state;
        }

        return null;
    }

    public Stack<Move> GeneratePath()
    {
        var curr = this;
        Stack<Move> path = new();
        while (curr.Parent is not null)
        {
            path.Push(curr.Move!);
            curr = curr.Parent;
        }

        return path;
    }

    public static bool IsGoal(List<Stack<int>> state)
    {
        var disks = state[0].Count + state[1].Count + state[2].Count;

        return state[1]?.Count == disks || state?[2].Count == disks;
    }
    static string GetHash(List<Stack<int>> state)
    {
        var s = "";
        foreach (var tower in state)
        {
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    s += tower.ElementAt(i);
                }
                catch (Exception)
                {
                    s += "0";
                }
            }
        }
        return s;
    }
}