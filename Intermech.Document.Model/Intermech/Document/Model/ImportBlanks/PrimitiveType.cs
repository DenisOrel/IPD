// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PrimitiveType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Тип примитива</summary>
[Serializable]
public enum PrimitiveType
{
  /// <summary>Неизвестный</summary>
  ptUnknown,
  /// <summary>автотекст</summary>
  ptAutoText,
  /// <summary>Текстовое поле</summary>
  ptTextField,
  /// <summary>полилиния</summary>
  ptPolyLine,
  /// <summary>Таблица</summary>
  ptTable,
  /// <summary>Рисунок</summary>
  ptPicture,
  /// <summary>Контейнер примитива</summary>
  ptContainer,
  /// <summary>Рабочая область</summary>
  ptArea,
  /// <summary>Страница бланка</summary>
  ptBlankList,
  /// <summary>OLE контейнер</summary>
  ptOLEContainer,
  /// <summary>Пользовательский примитив</summary>
  ptUserPrimitive,
}
