// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AdditionalPropertiesManagerHelper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client;

internal class AdditionalPropertiesManagerHelper
{
  internal static void Instance_GetAdditionalProperties(
    object sender,
    GetAdditionalProperties_EventArgs e)
  {
    DocumentTreeNode node = e.Node;
    ImDocumentData document = e.Document;
    AdditionalPropertiesWrapper propertiesWrapper = (AdditionalPropertiesWrapper) null;
    if (document != null && node is TextBoxElement)
      propertiesWrapper = (AdditionalPropertiesWrapper) new TextBoxElementPropertiesWrapper(node);
    if (propertiesWrapper == null)
      return;
    e.Properties.AddRange((IEnumerable<PropertyDescriptor>) propertiesWrapper.GetProperties());
  }
}
