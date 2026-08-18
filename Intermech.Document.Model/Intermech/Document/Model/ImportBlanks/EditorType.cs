// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.EditorType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Types of Text Editors</summary>
[Serializable]
public enum EditorType
{
  /// <summary>Однострочный</summary>
  etSingleLine,
  /// <summary>Многострочный</summary>
  etMultiLine,
  /// <summary>С форматирование</summary>
  etRichEdit,
}
