// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextCellEnumerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Enumerator для перемещения по Текстовым ячейкам в сложной строке</summary>
public class TextCellEnumerator : 
  IEnumerator<TextData>,
  IDisposable,
  IEnumerator,
  IEnumerable<TextData>,
  IEnumerable
{
  /// <summary>Таблица с текстовыми ячейками</summary>
  private TableData row;
  /// <summary>Индекс текущего элемента</summary>
  private int currInternalIndex;
  private int index;
  private TextCellEnumerator innerEnumerator;

  /// <summary>Подтаблица содержащая текстовые ячейки</summary>
  /// <param name="table"></param>
  public TextCellEnumerator(TableData table)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    this.Reset();
    this.row = table;
  }

  /// <summary>Текущий элемент</summary>
  public TextData Current { get; set; }

  /// <summary>Сквозной индекс текущего элемента в этом уровне энумератора</summary>
  public int Index => this.index;

  /// <summary>Энумератор для подтаблиц</summary>
  private TextCellEnumerator InnerEnumerator
  {
    get => this.innerEnumerator;
    set
    {
      if (this.innerEnumerator == value)
        return;
      if (this.innerEnumerator != null)
        this.innerEnumerator.Dispose();
      this.innerEnumerator = value;
    }
  }

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    this.Reset();
    this.row = (TableData) null;
    this.Current = (TextData) null;
  }

  /// <summary>Текущий элемент для интерфейса IEnumerator</summary>
  object IEnumerator.Current => (object) this.Current;

  /// <summary>Перейти к следующему элементу</summary>
  /// <returns>Возвращает true, если переход успешен и false, если текущий элемент был последним</returns>
  public bool MoveNext()
  {
    ++this.index;
    if (this.innerEnumerator != null && this.innerEnumerator.MoveNext())
    {
      this.Current = this.innerEnumerator.Current;
      return true;
    }
    for (int index = this.currInternalIndex + 1; index < this.row.NodesCount; ++index)
    {
      if (this.row.Nodes[index] is TextData node1)
      {
        this.currInternalIndex = index;
        this.Current = node1;
        this.InnerEnumerator = (TextCellEnumerator) null;
        return true;
      }
      if (this.row.Nodes[index] is TableData node2)
      {
        TextCellEnumerator textCellEnumerator = new TextCellEnumerator(node2);
        if (textCellEnumerator.MoveNext())
        {
          this.InnerEnumerator = textCellEnumerator;
          this.currInternalIndex = index;
          this.Current = this.InnerEnumerator.Current;
          return true;
        }
        textCellEnumerator.Dispose();
      }
    }
    return false;
  }

  /// <summary>Сбросить энумератор в начальное состояние "перед" первым элементом</summary>
  public void Reset()
  {
    this.InnerEnumerator = (TextCellEnumerator) null;
    this.currInternalIndex = -1;
    this.index = -1;
    this.Current = (TextData) null;
  }

  /// <summary>Возвращает энумератором самого себя.
  /// Используется для подстановки самого энумератора в foreach вместо TableData, так как у него уже есть другой энумератор</summary>
  /// <returns></returns>
  IEnumerator<TextData> IEnumerable<TextData>.GetEnumerator() => (IEnumerator<TextData>) this;

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this;
}
