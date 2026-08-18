// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.CloneClassId
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Идентификатор типа клона</summary>
public enum CloneClassId
{
  /// <summary>Неизвестный тип</summary>
  UNKNOWN_CLONE,
  /// <summary>Базовый тип клона</summary>
  BASE_CLONE,
  /// <summary>Группа</summary>
  GROUP_CLONE,
  /// <summary>текст</summary>
  TEXT_CLONE,
  /// <summary>Рисунок</summary>
  PICT_CLONE,
  /// <summary>Таблица</summary>
  TABLE_CLONE,
  /// <summary>контейнер</summary>
  CONT_CLONE,
  /// <summary>Рабочая область</summary>
  AREA_CLONE,
  /// <summary>страница</summary>
  BLIST_CLONE,
  /// <summary>OLE контейнер</summary>
  OLE_CLONE,
  /// <summary>Документ</summary>
  DOCUMENT,
}
