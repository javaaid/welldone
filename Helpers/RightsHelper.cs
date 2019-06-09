using System.Collections.Generic;
using System.Linq;

namespace WelldonePOS.Helpers
{
    public static class RightsHelper
    {
        public static string ToVarchar(Dictionary<int, bool> rights)
        {
            IEnumerable<int> found = from pair in rights
                                     where (pair.Value == true)
                                     select pair.Key;

            List<int> rightList = found.ToList();

            return string.Join(",", rightList.ToArray());
        }

        public static Dictionary<int, bool> ToRights(string varchar)
        {
            Dictionary<int, bool> rights = new Dictionary<int, bool>()
            {
                {0, false}, 
                {1, false}, {2, false}, 
                {3, false}, {4, false}, 
                {5, false}, {6, false}, 
                {7, false}, {8, false}, 
                {9, false}, {10, false}, 
                {11, false}, 
                {12, false}, {13, false}, {14, false}, 
                {15, false}, {16, false}, {17, false}, 
                {18, false}, {19, false}, {20, false},
                {21, false}, {22, false}, {23, false},
                {24, false}, {25, false}, {26, false},
                {27, false}, 
                {28, false}, {29, false},
                {30, false}, {31, false}, {32, false}, {33, false}, 
                {34, false}, {35, false}, {36, false}, {37, false}, 
                {38, false}, {39, false}, {40, false}, {41, false},
                {42, false}, {43, false}, {44, false}, {45, false},
                {46, false}, 
                {47, false}, {48, false}, {49, false}, {50, false}, 
                {51, false}, {52, false}, {53, false}, {54, false},
                {55, false}, {56, false}
            };

            List<string> rightList = varchar.Split(',').ToList();

            for (int i = 0; i < rightList.Count; i++)
            {
                rights[int.Parse(rightList[i])] = true;
            }

            return rights;
        }
    }
}
