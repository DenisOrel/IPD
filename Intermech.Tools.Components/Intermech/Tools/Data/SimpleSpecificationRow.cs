// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SimpleSpecificationRow
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Data;

public sealed class SimpleSpecificationRow
{
  private readonly long objectId;
  private readonly string designation;
  private readonly string okpCode;
  private readonly string name;
  private readonly string imbaseKey;
  private readonly string sectionName;
  private readonly Guid occurenceGuid;
  private readonly string position;
  private readonly string note;
  private readonly string zone;
  private readonly MeasuredValue count;
  private readonly MeasuredValue mass;
  private readonly string material;
  private LinkedList<string> projectDesignations;

  internal SimpleSpecificationRow(
    long objectId,
    string designation,
    string okpCode,
    string name,
    string imbaseKey,
    string sectionName,
    Guid occurenceGuid,
    string position,
    string note,
    string zone,
    MeasuredValue count,
    MeasuredValue mass,
    string material)
  {
    this.objectId = objectId;
    this.designation = designation;
    this.okpCode = okpCode;
    this.name = name;
    this.imbaseKey = imbaseKey;
    this.sectionName = sectionName;
    this.occurenceGuid = occurenceGuid;
    this.position = position;
    this.note = note;
    this.zone = zone;
    this.count = count;
    this.material = material;
    this.mass = mass;
    this.projectDesignations = new LinkedList<string>();
  }

  public string GetProjectDesignationsList()
  {
    switch (this.projectDesignations.Count)
    {
      case 0:
        return "";
      case 1:
        return this.projectDesignations.First.Value;
      default:
        return string.Join(", ", (IEnumerable<string>) this.projectDesignations);
    }
  }

  public long ObjectId => this.objectId;

  public string Designation => this.designation;

  public string OKPCode => this.okpCode;

  public string Name => this.name;

  public string ImbaseKey => this.imbaseKey;

  public string SectionName => this.sectionName;

  /// <summary>
  /// Возвращает глобальный идентификатор входимости. Может быть пуст, если это позиция из перекачанной базы Search.
  /// </summary>
  public Guid OccurenceGuid => this.occurenceGuid;

  public string Position => this.position;

  public string Note => this.note;

  public string Zone => this.zone;

  public MeasuredValue Count => this.count;

  /// <summary>
  /// Возвращает массу изделия. Значение свойства может быть не задано, если масса изделия не заполнена.
  /// </summary>
  public MeasuredValue Mass
  {
    [DebuggerStepThrough] get => this.mass;
  }

  public string Material => this.material;

  public ICollection<string> ProjectDesignations => (ICollection<string>) this.projectDesignations;
}
