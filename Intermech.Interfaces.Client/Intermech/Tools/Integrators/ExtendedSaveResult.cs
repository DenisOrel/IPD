// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ExtendedSaveResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Содержит данные о результате выполнения расширенного сохранения документа.
/// </summary>
public sealed class ExtendedSaveResult
{
  /// <summary>Создает объект.</summary>
  public ExtendedSaveResult(
    bool isSuccessful,
    List<long> affectedObjectIds,
    List<string> errors,
    bool open)
  {
    this.IsSuccessful = isSuccessful;
    this.AffectedObjectIds = affectedObjectIds;
    this.Errors = errors;
    this.OpenObjects = open;
  }

  /// <summary>Создает объект.</summary>
  public ExtendedSaveResult(bool isSuccessful, List<long> affectedObjectIds, List<string> errors)
    : this(isSuccessful, affectedObjectIds, errors, false)
  {
  }

  /// <summary>Признак успеха выполнения сохранения изменений.</summary>
  public bool IsSuccessful { get; }

  /// <summary>Нужно ли открывать объекты в IPS.</summary>
  public bool OpenObjects { get; }

  /// <summary>Список идентификаторов объектов.</summary>
  public List<long> AffectedObjectIds { get; }

  /// <summary>Список ошибок.</summary>
  public List<string> Errors { get; }
}
