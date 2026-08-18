// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.VertAlignment
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Вертикальное выравнивание</summary>
[Serializable]
public enum VertAlignment
{
  /// <summary>По умолчанию</summary>
  vaNone,
  /// <summary>Вверх</summary>
  vaTop,
  /// <summary>По центру</summary>
  vaCenter,
  /// <summary>Вниз</summary>
  vaBottom,
}
