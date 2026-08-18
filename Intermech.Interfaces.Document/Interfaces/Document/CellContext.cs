// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellContext
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Контекст ячейки таблицы</summary>
public class CellContext
{
  /// <summary>Шаблон ячейки</summary>
  public RectangleElement Template;
  /// <summary>Кратный размер строки, null если не инициализирован</summary>
  public bool? IsFixedSizeRow;
  /// <summary>Базовый размер строки</summary>
  public float? RowSize;
  /// <summary>Требуется отрисовка сетки таблицы</summary>
  public bool DrawGrid = true;
  /// <summary>Поля ячеек</summary>
  public MarginsF Margins;

  /// <summary>Конструктор</summary>
  public CellContext()
  {
  }

  /// <summary>Конструктор копии контекста</summary>
  /// <param name="src">Оригинальный контекст</param>
  public CellContext(CellContext src)
  {
    if (src == null)
      return;
    this.Template = src.Template;
    this.IsFixedSizeRow = src.IsFixedSizeRow;
    this.RowSize = src.RowSize;
    if (src.Margins == null)
      return;
    this.Margins = src.Margins.Clone();
  }

  /// <summary>Кратный размер строки, не может быть null</summary>
  public bool IsFixedSizeRow_NN => this.IsFixedSizeRow.HasValue && this.IsFixedSizeRow.Value;

  /// <summary>Базовый размер строки. Non null - вместо null вернёт 0</summary>
  public float RowSize_NN => !this.RowSize.HasValue ? 0.0f : this.RowSize.Value;
}
