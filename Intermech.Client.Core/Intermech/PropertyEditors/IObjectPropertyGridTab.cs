
// Type: Intermech.PropertyEditors.IObjectPropertyGridTab
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Интерфейс страниц ObjectPropertyGridTab, используемых в ObjectPropertyGrid для фильтрации списка атрибутов объекта/связи
/// </summary>
public interface IObjectPropertyGridTab
{
  Guid TabGuid { get; }

  GetAttributeValuesModes TabAttributeValuesModes { get; }

  void InitTab(GetAttributeValuesModes avm);

  PropertyDescriptorCollection PropDescriptorCollection(object component);
}
