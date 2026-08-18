// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADIntegratorSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Базовые настройки электрических интеграторов</summary>
public class ECADIntegratorSettings : ISettingsObject
{
  /// <summary>Таблица атрибутов сборочной единицы</summary>
  public List<Tuple<StringKey, StringKey, bool>> AssemblyAttributesTable { get; set; }

  /// <summary>Таблица атрибутов детали</summary>
  public List<Tuple<StringKey, StringKey, bool>> PartAttributesTable { get; set; }

  /// <summary>Таблица атрибутов связей с деталями</summary>
  public List<Tuple<StringKey, StringKey, bool>> RelationPartAttributesTable { get; set; }

  /// <summary>Таблица атрибутов документа</summary>
  public List<Tuple<StringKey, StringKey, bool>> DocumentAttributesTable { get; set; }

  /// <summary>
  /// Список папок проекта, не импортируемых в систему (различные темповые  и бэкап папки)
  /// </summary>
  public List<string> NotImportingDir { get; set; }

  /// <summary>Проводить синхронизацию</summary>
  public bool ImbaseSync { get; set; }

  /// <summary>Проверка применяемости</summary>
  public bool ImbaseSyncCheckApplicability { get; set; }

  /// <summary>Атрибут для синхронизации</summary>
  public GlobalId<int> ImbaseSyncAttribute { get; set; }

  /// <summary>
  /// Наименование параметра компонента, в котором приведены предельные значения для выполнения подбора
  /// </summary>
  public string NominalsParameter { get; set; }

  /// <summary>
  /// Параметр компонента схемы и его значение, при котором компонент в IPS идентифицируется как основной для подбора
  /// </summary>
  public List<Tuple<StringKey, StringKey>> TuningParameters { get; set; }

  /// <summary>
  /// Параметр компонента схемы и его значение, при котором компонент в IPS идентифицируется как имеющий доп.замены
  /// </summary>
  public List<Tuple<StringKey, StringKey>> ReplaceParameters { get; set; }

  /// <summary>
  /// Наименование параметра штампа в котором указано наименование функциональной группы
  /// </summary>
  public string FGName { get; set; }

  /// <summary>
  /// Наименование параметра штампа в котором указано обозначение функциональной группы
  /// </summary>
  public string FGDesignation { get; set; }

  /// <summary>
  /// Наименование параметра в котором указано Позиционное обозначение ДС
  /// </summary>
  public string ASPosDesignation { get; set; }
}
