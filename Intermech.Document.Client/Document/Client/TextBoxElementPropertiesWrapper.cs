// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.TextBoxElementPropertiesWrapper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Document.Client;

internal class TextBoxElementPropertiesWrapper(DocumentTreeNode node) : AdditionalPropertiesWrapper(node)
{
  [DisplayName("Составное обозначение")]
  [Category("Поля Документа")]
  [Description("Проверять что в поле находится составное обозначение с знаком '|'")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ComplexDesignation
  {
    get
    {
      string attributeValue = this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_ComplexDesignation, false);
      if (attributeValue != null)
      {
        bool result = false;
        if (bool.TryParse(attributeValue, out result))
          return result;
      }
      return false;
    }
    set
    {
      this.owner.SetAttributeValue(DocumentTreeNode.AttributeName_ComplexDesignation, value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
  }
}
