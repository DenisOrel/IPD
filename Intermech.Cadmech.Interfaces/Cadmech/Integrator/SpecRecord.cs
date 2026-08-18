// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SpecRecord
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>Реализует представление записи в спецификации.</summary>
public class SpecRecord
{
  private string projDesignation;
  private string zone;
  private string position;
  private PartData part;
  private MeasuredValue count;
  private string note;
  private List<SpecRelation> relations;

  /// <summary>Создает объект.</summary>
  public SpecRecord() => this.relations = new List<SpecRelation>();

  /// <summary>
  /// 
  /// </summary>
  public string ProjectDesignation
  {
    get => this.projDesignation;
    set => this.projDesignation = value;
  }

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

  /// <summary>
  /// Возвращает количество компонентов в составе исполнения.
  /// </summary>
  public MeasuredValue Count
  {
    get => this.count;
    set => this.count = value;
  }

  /// <summary>Возвращает или задает примечание связи.</summary>
  public string Note
  {
    get => this.note;
    set => this.note = value;
  }

  /// <summary>
  /// Возвращает список идентификаторов связей в базе данных, соответствующих этой строке спецификации.
  /// Если этот список содержит более 1-й связи, то запись находится в общей части спецификации.
  /// </summary>
  public List<SpecRelation> Relations => this.relations;
}
