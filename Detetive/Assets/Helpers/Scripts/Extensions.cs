using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    static public List<T> SortList<T>(this List<T> list)
    {
        return list.OrderBy(x=>Guid.NewGuid()).ToList();
    }
}
