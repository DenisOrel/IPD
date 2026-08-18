// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.FldType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Types of Text Fields - for default validation</summary>
[Serializable]
public enum FldType
{
  /// <summary>Любой текст</summary>
  ftyAnyText,
  /// <summary>Целое число</summary>
  ftyInteger,
  /// <summary>Вещественное число</summary>
  ftyFloat,
  /// <summary>Идентификатор</summary>
  ftyIdent,
  /// <summary>Дата</summary>
  ftyDate,
  /// <summary>Переключатель</summary>
  ftyToggle,
}
