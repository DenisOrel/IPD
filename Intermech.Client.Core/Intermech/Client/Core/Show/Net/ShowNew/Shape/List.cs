
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.List
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

internal static class List
{
  /// <summary>высвобождения распределенных ресурсов в списке</summary>
  /// <typeparam name="T">Тип элементов в списке.</typeparam>
  /// <param name="set">типизированный список объектов</param>
  internal static void Dispose<T>(this List<T> set)
  {
    foreach (T obj in set)
    {
      if (obj is IDisposable disposable)
        disposable.Dispose();
    }
    set.Clear();
  }
}
