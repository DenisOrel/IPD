// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.IListExtension
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System.Collections;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Класс расширения IList</summary>
public static class IListExtension
{
  public static void SwapBefore(this IList list, int index)
  {
    object obj = list[index - 1];
    list[index - 1] = list[index];
    list[index] = obj;
  }

  public static void SwapAfter(this IList list, int index)
  {
    object obj = list[index + 1];
    list[index + 1] = list[index];
    list[index] = obj;
  }

  public static void MoveFirst(this IList list, int index)
  {
    object obj = list[index];
    list.RemoveAt(index);
    list.Insert(0, obj);
  }

  public static void MoveLast(this IList list, int index)
  {
    object obj = list[index];
    list.RemoveAt(index);
    list.Add(obj);
  }
}
