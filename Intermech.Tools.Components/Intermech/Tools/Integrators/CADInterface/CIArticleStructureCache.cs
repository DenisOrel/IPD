// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleStructureCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Секция данных, используемая для кэширования структуры изделия, полученной с помощью
/// CAD-интерфейса. Используется при обновлении проектных связей между изделиями,
/// выпускаемыми по документам CAD-системы.
/// </summary>
internal sealed class CIArticleStructureCache
{
  private readonly AssemblyStructureManagerProxy structureManager;
  private List<AssemblyStructureRecord> structure;

  /// <summary>Создает объект.</summary>
  /// <param name="structureManager">Менеджер для работы со структурой изделия, полученной с помощью CAD-интерфейса</param>
  public CIArticleStructureCache(AssemblyStructureManagerProxy structureManager)
  {
    this.structureManager = structureManager != null ? structureManager : throw new ArgumentNullException(nameof (structureManager));
  }

  /// <summary>
  /// Возвращает менеджер для работы со структурой изделия, полученной с помощью CAD-интерфейса.
  /// </summary>
  public AssemblyStructureManagerProxy StructureManager => this.structureManager;

  /// <summary>
  /// Структура изделия в виде записей, полученная с помощью CAD-интерфейса.
  /// </summary>
  public List<AssemblyStructureRecord> Structure
  {
    get => this.structure;
    set => this.structure = value;
  }
}
