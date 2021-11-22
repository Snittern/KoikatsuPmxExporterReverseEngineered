// Decompiled with JetBrains decompiler
// Type: PmxLib.CP
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  internal static class CP
  {
    public static void Swap<T>(ref T v0, ref T v1) where T : struct
    {
      T obj = v0;
      v0 = v1;
      v1 = obj;
    }

    public static void Swap<T>(IList<T> list, int ix1, int ix2)
    {
      T obj = list[ix1];
      list[ix1] = list[ix2];
      list[ix2] = obj;
    }

    public static List<T> CloneList<T>(List<T> list) where T : ICloneable
    {
      List<T> objList = new List<T>();
      int count = list.Count;
      for (int index = 0; index < count; ++index)
        objList.Add((T) list[index].Clone());
      return objList;
    }

    public static List<T> CloneList_ValueType<T>(List<T> list) where T : struct => new List<T>((IEnumerable<T>) list.ToArray());

    public static T[] CloneArray<T>(T[] src) where T : ICloneable
    {
      T[] objArray = new T[src.Length];
      for (int index = 0; index < src.Length; ++index)
        objArray[index] = (T) src[index].Clone();
      return objArray;
    }

    public static T[] CloneArray_ValueType<T>(T[] src) where T : struct
    {
      T[] objArray = new T[src.Length];
      Array.Copy((Array) src, (Array) objArray, src.Length);
      return objArray;
    }

    public static Dictionary<Tv, int> ArrayToTable<Tl, Tv>(
      Tl[] arr,
      Func<int, Tv> objProc)
    {
      Dictionary<Tv, int> dictionary = new Dictionary<Tv, int>(arr.Length);
      for (int index = 0; index < arr.Length; ++index)
      {
        Tv key = objProc(index);
        if ((object) key != null && !dictionary.ContainsKey(key))
          dictionary.Add(key, index);
      }
      return dictionary;
    }

    public static Dictionary<T, int> ArrayToTable<T>(T[] arr)
    {
      Dictionary<T, int> dictionary = new Dictionary<T, int>(arr.Length);
      for (int index = 0; index < arr.Length; ++index)
      {
        T key = arr[index];
        if ((object) key != null && !dictionary.ContainsKey(key))
          dictionary.Add(key, index);
      }
      return dictionary;
    }

    public static Dictionary<Tv, int> ListToTable<Tl, Tv>(
      List<Tl> list,
      Func<int, Tv> objProc)
    {
      Dictionary<Tv, int> dictionary = new Dictionary<Tv, int>(list.Count);
      for (int index = 0; index < list.Count; ++index)
      {
        Tv key = objProc(index);
        if ((object) key != null && !dictionary.ContainsKey(key))
          dictionary.Add(key, index);
      }
      return dictionary;
    }

    public static Dictionary<T, int> ListToTable<T>(List<T> list)
    {
      Dictionary<T, int> dictionary = new Dictionary<T, int>(list.Count);
      for (int index = 0; index < list.Count; ++index)
      {
        T key = list[index];
        if ((object) key != null && !dictionary.ContainsKey(key))
          dictionary.Add(key, index);
      }
      return dictionary;
    }

    public static bool InRange<T>(T[] arr, int index) => 0 <= index && index < arr.Length;

    public static bool InRange<T>(List<T> list, int index) => 0 <= index && index < list.Count;

    public static bool InRange<T>(IList<T> list, int index) => 0 <= index && index < list.Count;

    public static bool InRange(int min, int max, int val) => min <= val && val <= max;

    public static T SafeGet<T>(T[] arr, int index) where T : class => arr == null || !CP.InRange<T>(arr, index) ? default (T) : arr[index];

    public static T SafeGet<T>(IList<T> arr, int index) where T : class => arr == null || !CP.InRange<T>(arr, index) ? default (T) : arr[index];

    public static T SafeGetV<T>(T[] arr, int index) where T : struct => arr == null || !CP.InRange<T>(arr, index) ? default (T) : arr[index];

    public static T SafeGetV<T>(T[] arr, int index, out bool flag) where T : struct
    {
      flag = false;
      T obj;
      if (arr != null && CP.InRange<T>(arr, index))
      {
        flag = true;
        obj = arr[index];
      }
      else
        obj = default (T);
      return obj;
    }

    public static T SafeGetV<T>(IList<T> arr, int index) where T : struct => arr == null || !CP.InRange<T>(arr, index) ? default (T) : arr[index];

    public static T SafeGetV<T>(IList<T> arr, int index, out bool flag) where T : struct
    {
      flag = false;
      T obj;
      if (arr != null && CP.InRange<T>(arr, index))
      {
        flag = true;
        obj = arr[index];
      }
      else
        obj = default (T);
      return obj;
    }

    public static int[] SortIndexForRemove(int[] ix)
    {
      List<int> intList = new List<int>((IEnumerable<int>) ix);
      intList.Sort((Comparison<int>) ((l, r) => r - l));
      return intList.ToArray();
    }

    public static void SSort<T>(List<T> list, Comparison<T> comp)
    {
      List<KeyValuePair<int, T>> keyValuePairList = new List<KeyValuePair<int, T>>(list.Count);
      for (int index = 0; index < list.Count; ++index)
        keyValuePairList.Add(new KeyValuePair<int, T>(index, list[index]));
      keyValuePairList.Sort((Comparison<KeyValuePair<int, T>>) ((x, y) =>
      {
        int num = comp(x.Value, y.Value);
        if (num == 0)
          num = x.Key.CompareTo(y.Key);
        return num;
      }));
      for (int index = 0; index < list.Count; ++index)
        list[index] = keyValuePairList[index].Value;
    }

    public static int[] ComposeIndices(int[] ix1, int[] ix2)
    {
      List<int> intList = new List<int>((IEnumerable<int>) ix1);
      Dictionary<int, int> dictionary = new Dictionary<int, int>(ix1.Length);
      for (int index = 0; index < ix1.Length; ++index)
        dictionary.Add(ix1[index], 0);
      for (int index = 0; index < ix2.Length; ++index)
      {
        if (!dictionary.ContainsKey(ix2[index]))
        {
          dictionary.Add(ix2[index], 0);
          intList.Add(ix2[index]);
        }
      }
      return intList.ToArray();
    }

    public static int[] RemoveIndices(int[] ix1, int[] ix2)
    {
      List<int> intList = new List<int>(ix1.Length);
      Dictionary<int, int> dictionary = new Dictionary<int, int>(ix2.Length);
      for (int index = 0; index < ix2.Length; ++index)
        dictionary.Add(ix2[index], 0);
      for (int index = 0; index < ix1.Length; ++index)
      {
        if (!dictionary.ContainsKey(ix1[index]))
        {
          dictionary.Add(ix1[index], 0);
          intList.Add(ix1[index]);
        }
      }
      return intList.ToArray();
    }

    public static bool IsSameIndices(int[] arr1, int[] arr2)
    {
      bool flag;
      if (arr1.Length != arr2.Length)
      {
        flag = false;
      }
      else
      {
        for (int index = 0; index < arr1.Length; ++index)
        {
          if (arr1[index] != arr2[index])
            return false;
        }
        flag = true;
      }
      return flag;
    }
  }
}
