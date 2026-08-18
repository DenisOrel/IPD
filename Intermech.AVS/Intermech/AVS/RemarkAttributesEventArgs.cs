// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RemarkAttributesEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Аргументы для событий от редактора списка атрибутов</summary>
public class RemarkAttributesEventArgs : EventArgs
{
  /// <summary>Параметры, с которыми произошли изменения</summary>
  public AttributesListFormParams FormParams;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Коллекция типов объектов-документов</param>
  public RemarkAttributesEventArgs(AttributesListFormParams formParams)
  {
    this.FormParams = formParams;
  }
}
