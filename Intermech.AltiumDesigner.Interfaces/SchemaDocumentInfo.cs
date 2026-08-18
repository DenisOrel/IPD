// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.SchemaDocumentInfo
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Информация о схеме</summary>
[Serializable]
public class SchemaDocumentInfo : DocumentInfo
{
  /// <summary>Создать объект</summary>
  /// <param name="fullPath">Путь и имя файла</param>
  /// <param name="obligatoryParameters">Обязательные атрибуты документа</param>
  public SchemaDocumentInfo(string fullPath, Parameter[] obligatoryParameters)
    : base(fullPath, "SCH")
  {
    this.ObligatoryParameters = obligatoryParameters;
  }

  /// <summary>Обязательные атрибуты документа</summary>
  public Parameter[] ObligatoryParameters { get; private set; }
}
