// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AdditionalPropertiesWrapper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client;

internal class AdditionalPropertiesWrapper
{
  protected DocumentTreeNode owner;

  public AdditionalPropertiesWrapper(DocumentTreeNode node) => this.owner = node;

  public List<PropertyDescriptor> GetProperties()
  {
    List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) this))
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((PropertyDescriptor) new AdditionalPropertiesDescriptor(this, property));
      properties.Add((PropertyDescriptor) propertyDescriptor);
    }
    return properties;
  }
}
