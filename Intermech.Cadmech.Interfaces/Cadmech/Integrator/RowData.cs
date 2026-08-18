// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.RowData
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Содержит расшифрованное описание строки в обменном файле.
/// </summary>
public class RowData
{
  private string zone;
  private string position;
  private PartData part;
  private string note;
  private Guid partGuid;
  private OccurenceFormat occurenceFormat;
  private List<OccurenceRef> refs;

  /// <summary>Создает объект.</summary>
  public RowData() => this.refs = new List<OccurenceRef>();

  /// <summary>
  /// Возвращает или задает зону расположения детали или подсборки на чертеже сборочной единицы.
  /// </summary>
  public string Zone
  {
    get => this.zone;
    set => this.zone = value;
  }

  /// <summary>
  /// Возвращает или задает позицию детали или подсборки в спецификации.
  /// </summary>
  public string Position
  {
    get => this.position;
    set => this.position = value;
  }

  /// <summary>
  /// Возвращает или задает описание объекта, входящего в сборочную единицу.
  /// </summary>
  public PartData Part
  {
    get => this.part;
    set => this.part = value;
  }

  /// <summary>Возвращает или задает примечание связи.</summary>
  public string Note
  {
    get => this.note;
    set => this.note = value;
  }

  /// <summary>
  /// Возвращает или задает произволное значение, которое можно использовать для поиска в базе данных
  /// компонента, входящего связью в исполнение сборочной единицы.
  /// </summary>
  public Guid PartGuid
  {
    get => this.partGuid;
    set => this.partGuid = value;
  }

  /// <summary>
  /// Возвращает формат ссылок на исполнения сборочной единицы.
  /// </summary>
  public OccurenceFormat OccurenceFormat
  {
    get => this.occurenceFormat;
    set => this.occurenceFormat = value;
  }

  /// <summary>
  /// Возвращает список ссылок на исполнения сборочной единицы.
  /// </summary>
  public List<OccurenceRef> Refs => this.refs;
}
