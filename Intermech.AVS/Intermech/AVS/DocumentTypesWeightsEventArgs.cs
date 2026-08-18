// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Аргументы для событий от редактора "весов" типов объектов-документов
/// </summary>
public class DocumentTypesWeightsEventArgs : EventArgs
{
  /// <summary>Коллекция типов объектов-документов</summary>
  public DocumentTypeWeightCollection Items;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Коллекция типов объектов-документов</param>
  public DocumentTypesWeightsEventArgs(DocumentTypeWeightCollection items) => this.Items = items;
}
