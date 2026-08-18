
// Type: Intermech.Client.Core.Show.Net.ShowNew.Block.BlockTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Show;
using System;
using System.Collections;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.Block;

/// <summary>список блоков </summary>
[DebuggerDisplay("{Length}")]
public class BlockTable : IBlockTable, IEnumerable
{
  /// <summary>создать таблицу блоков</summary>
  /// <param name="strings">массив имён блоков</param>
  /// <param name="work"></param>
  internal BlockTable(string[] strings, IShowDwgWork work)
  {
    this.Array = new IBlock[strings.Length];
    for (int index = 0; index < strings.Length; ++index)
      this.Array[index] = (IBlock) new BlockObject(index + 1, strings[index], work);
  }

  /// <summary>длинна списка блоков</summary>
  public int Length => this.Array.Length;

  /// <summary>список блоков</summary>
  public IBlock[] Array { get; }

  /// <summary>получить по индексу сам блок</summary>
  /// <param name="index">индекс в таблице</param>
  /// <returns>блок</returns>
  public IBlock this[int index]
  {
    get
    {
      return index >= 0 && index < this.Length ? this.Array[index] : throw new IndexOutOfRangeException();
    }
  }

  /// <summary>Вернуть ссылку на интерфейс IEnumerator</summary>
  /// <returns>Ссылка на интерфейс IEnumerator</returns>
  public IEnumerator GetEnumerator() => this.Array.GetEnumerator();
}
