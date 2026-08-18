// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TypeSet
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Набор допустимых примитивов</summary>
[Flags]
[Serializable]
public enum TypeSet
{
  /// <summary>Неизвестный</summary>
  ptUnknown = 1,
  /// <summary>автотекст</summary>
  ptAutoText = 2,
  /// <summary>Текстовое поле</summary>
  ptTextField = 4,
  /// <summary>Полилиния</summary>
  ptPolyLine = 8,
  /// <summary>Таблица</summary>
  ptTable = 16, // 0x00000010
  /// <summary>Рисунок</summary>
  ptPicture = 32, // 0x00000020
  /// <summary>Контейнер примитива</summary>
  ptContainer = 64, // 0x00000040
  /// <summary>Рабочая область</summary>
  ptArea = 128, // 0x00000080
  /// <summary>Страница бланка</summary>
  ptBlankList = 256, // 0x00000100
  /// <summary>OLE контейнер</summary>
  ptOLEContainer = 512, // 0x00000200
  /// <summary>Пользовательский примитив</summary>
  ptUserPrimitive = 1024, // 0x00000400
}
