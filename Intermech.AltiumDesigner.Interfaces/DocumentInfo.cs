// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.DocumentInfo
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Информация о документе</summary>
[Serializable]
public class DocumentInfo
{
  /// <summary>Создать объект</summary>
  /// <param name="fullPath">Путь и имя файла</param>
  /// <param name="kind">Тип документа</param>
  public DocumentInfo(string fullPath, string kind)
  {
    this.FullPath = fullPath;
    this.Kind = kind;
  }

  /// <summary>Путь и имя файла</summary>
  public string FullPath { get; private set; }

  /// <summary>Тип документа</summary>
  public string Kind { get; private set; }
}
