// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.GenAlignment
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Общее выравнивание</summary>
[Serializable]
public enum GenAlignment
{
  /// <summary>По умолчанию</summary>
  gaNone,
  /// <summary>Влево</summary>
  gaLeft,
  /// <summary>Вверх</summary>
  gaTop,
  /// <summary>Горизонтально по центру</summary>
  gaHCenter,
  /// <summary>Вертикально по центру</summary>
  gaVCenter,
  /// <summary>Вправо</summary>
  gaRight,
  /// <summary>Вниз</summary>
  gaBottom,
}
