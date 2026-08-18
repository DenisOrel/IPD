
// Type: Intermech.Client.Core.Show.Net.ShowNew.ExternFile.RefFileTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.ExternFile;

/// <summary>список отображаемых рисунков</summary>
[DebuggerDisplay("{Length}")]
internal class RefFileTable
{
  /// <summary>колекция</summary>
  private FileData[] _array;

  /// <summary>длинна списка блоков</summary>
  internal int Length => this._array.Length;

  /// <summary>получить по индексу сам</summary>
  /// <param name="index">индекс в таблице</param>
  /// <returns>блок</returns>
  internal FileData this[int index]
  {
    get
    {
      return index >= 0 && index < this.Length ? this._array[index] : throw new IndexOutOfRangeException();
    }
  }

  /// <summary>создать таблицу</summary>
  /// <param name="strings">массив имён</param>
  internal RefFileTable(string[] strings)
  {
    this._array = new FileData[strings.Length];
    int num = 0;
    while (num < strings.Length)
      ++num;
  }
}
