// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ArticleAttributesSyncSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Класс для хранения настроек синхронизации атрибутов конструкторских документов и изделий
/// </summary>
[Serializable]
public class ArticleAttributesSyncSettings
{
  /// <summary>Идентификаторы синхронизируемых атрибутов</summary>
  public int[] SyncAttributes { get; private set; }

  /// <summary>
  /// Типы объектов, являющиеся главными конструкторскими документами
  /// </summary>
  public int[] MainDocumentsTypes { get; private set; }

  public ArticleAttributesSyncSettings(int[] syncAttributes, int[] mainDocumentsTypes)
  {
    this.SyncAttributes = syncAttributes;
    this.MainDocumentsTypes = mainDocumentsTypes;
  }
}
