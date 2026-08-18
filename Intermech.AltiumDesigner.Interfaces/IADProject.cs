// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.IADProject
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Проект Altium Designer</summary>
public interface IADProject : 
  IParametrable,
  IValueBagContainer,
  IIdentification,
  IFileDocument,
  IDisposable
{
  /// <summary>
  /// Список путей и имен файлов документов, входящих в проект
  /// </summary>
  /// <param name="leaveDocsOpen">Оставить документы открытыми в редакторе</param>
  List<DocumentInfo> GetDocuments(bool leaveDocsOpen);

  /// <summary>
  /// Список путей и имен файлов документов, входящих в проект
  /// </summary>
  List<DocumentInfo> GeneratedDocuments { get; }

  /// <summary>Количество вариантов для схемы</summary>
  int VariantsCount { get; }

  /// <summary>Получить вариант по индексу</summary>
  /// <param name="index"></param>
  /// <returns></returns>
  IVariant GetVariant(int index);
}
