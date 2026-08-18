// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.ImbaseCommonParams
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces.Imbase.Params.CommonParams;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Общие/админские параметры</summary>
[Serializable]
public class ImbaseCommonParams
{
  public ImbaseCommonParams()
  {
    this.FolderApplicabilityIcons = new FolderApplicabilityStatusIcons();
    this.ImbaseSyncParams = new ImbaseSyncParams();
    this.SkipAttributes = new List<AttributeForObjectTypeInfo>();
    this.NotExpandableAttributes = new List<AttributeForObjectTypeInfo>();
  }

  /// <summary>
  /// Учитывать права видимости записей при просмотре таблиц
  /// </summary>
  public bool AnalizeHiddenRecords { get; set; }

  /// <summary>
  /// Использовать расширенную проверку прав доступа для индексов
  /// </summary>
  public bool UseExtendedSecurityCheckForIndexes { get; set; }

  /// <summary>
  ///  Запретить создание нескольких ярлыков на одну таблицу
  /// </summary>
  public bool DenyFewLinksForSameTable { get; set; }

  /// <summary>Режим удаления записи</summary>
  public DeleteRecordMode DeleteRecordMode { get; set; }

  /// <summary>
  /// Учитывать Применяемость Imbase при формировании состава
  /// </summary>
  public bool CheckApplicabilityBeforeCreateComposition { get; set; }

  /// <summary>
  /// Изображения для папок в зависимости от атрибута применяемость
  /// </summary>
  public FolderApplicabilityStatusIcons FolderApplicabilityIcons { get; set; }

  /// <summary>Параметры синхронизации со старым Imbase</summary>
  public ImbaseSyncParams ImbaseSyncParams { get; set; }

  /// <summary>Пропускаемые при синхронизации атрибуты</summary>
  public List<AttributeForObjectTypeInfo> SkipAttributes { get; set; }

  /// <summary>Нераскрываемые при синхронизации атрибуты</summary>
  public List<AttributeForObjectTypeInfo> NotExpandableAttributes { get; set; }
}
