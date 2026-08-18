// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.OccurenceRef
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Описывает ссылку на исполнение сборочной единицы в обменном файле.
/// </summary>
public class OccurenceRef : ICloneable
{
  /// <summary>
  /// Значение поля Ind, обозначающее, что деталь или подсборка входит в базовое исполнение
  /// сборочной единицы.
  /// </summary>
  public const string BasicProject = "<BasicProject>";
  /// <summary>
  /// Значение поля Ind, обозначающее, что деталь или подсборка входит во все исполнения
  /// сборочной единицы.
  /// </summary>
  public const string AllProjects = "<AllProjects>";
  private string ind;
  private MeasuredValue count;
  private string designation;

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон</returns>
  public OccurenceRef Clone()
  {
    return new OccurenceRef()
    {
      ind = this.ind,
      count = (MeasuredValue) this.count.Clone(),
      designation = this.designation
    };
  }

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>
  /// Возвращает или задает идентификатор исполнения сборочной единицы из обменного файла.
  /// Поле может содержать следующие значения (см. AVS_CADM.doc):
  ///   - обозначение сборочной единицы;
  ///   - специальное значение BasicProject или AllProjects;
  ///   - суффикс исполнения, относительно обозначения основного исполнения.
  /// </summary>
  public string Ind
  {
    get => this.ind;
    set => this.ind = value;
  }

  /// <summary>
  /// Возвращает количество компонентов в составе исполнения.
  /// </summary>
  public MeasuredValue Count
  {
    get => this.count;
    set => this.count = value;
  }

  /// <summary>
  /// Возвращает или задает обозначение исполнения сборочной единицы. Этого поля нет в
  /// обменном файле, оно заполняется в процессе работы анализатора. Если значение поля
  /// Ind == AllProjects, то это поле будет пустым.
  /// </summary>
  public string Designation
  {
    get => this.designation;
    set => this.designation = value;
  }
}
