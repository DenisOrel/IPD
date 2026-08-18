// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.FontFlags
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>General flags</summary>
[Flags]
[Serializable]
public enum FontFlags
{
  /// <summary>None</summary>
  fNone = 0,
  /// <summary>used in text fields for font, in polylines and tables</summary>
  fBold = 1,
  /// <summary>used in tables</summary>
  fSerif = 2,
  /// <summary>font flags for text fields</summary>
  fItalic = 4,
  /// <summary>font flags for text fields</summary>
  fUnderline = 8,
  /// <summary>font flags for text fields</summary>
  fSuperscript = 16, // 0x00000010
  /// <summary>used in tables to draw ellipses instead</summary>
  fEllipse = 32, // 0x00000020
  /// <summary>Search</summary>
  fSearch = 64, // 0x00000040
}
