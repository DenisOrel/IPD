// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VersionAttributesEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Аргументы для событий от редактора списка атрибутов</summary>
public class VersionAttributesEventArgs : EventArgs
{
  /// <summary>Параметры, с которыми произошли изменения</summary>
  public VersionAttributesListFormParams FormParams;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Коллекция типов объектов-документов</param>
  public VersionAttributesEventArgs(VersionAttributesListFormParams formParams)
  {
    this.FormParams = formParams;
  }
}
