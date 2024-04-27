// namespace Tower_Of_Hanoi.Shared;
//
// public static class ExtentionList
// {
//     public static string ListOfStacksOfIntsToString(this List<Stack<int> stacks)
//     {
//         if (stacks == null) throw new ArgumentNullException(nameof(stacks));
//         var str = "";
//         foreach (var stack in stacks)
//         {
//             str += string.Join(",", stack.Reverse());
//         }
//
//         return str;
//     }
// }