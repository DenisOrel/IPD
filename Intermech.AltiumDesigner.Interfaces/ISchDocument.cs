// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.ISchDocument
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Электрическая схема</summary>
public interface ISchDocument : 
  IParametrable,
  IValueBagContainer,
  IIdentification,
  IFileDocument,
  IDisposable
{
  /// <summary>
  /// Получить следующий компонент схемы, null если следующий компонент не найден.
  /// </summary>
  /// <returns></returns>
  ISchComponent GetNextComponent();

  /// <summary>
  /// Получить следующую функциональную группу в схеме, null если не найдено.
  /// </summary>
  /// <returns></returns>
  ISchSheetSymbol GetNextSheetSymbol();

  /// <summary>
  /// Обязательные параметры схемы (номер листа, обозначение...) и их строковые значения
  /// </summary>
  Parameter[] ObligatoryParameters { get; }

  /// <summary>Проект, в состав которого входит схема</summary>
  IADProject Project { get; }
}
