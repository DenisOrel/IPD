// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AdditionalPropertiesManager
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

public class AdditionalPropertiesManager
{
  private static AdditionalPropertiesManager instance;
  private GetAdditionalProperties_EventHandler getAdditionalProperties_EventHandler;

  public List<PropertyDescriptor> GetProperties(ImDocumentData doc, DocumentTreeNode node)
  {
    GetAdditionalProperties_EventArgs e = new GetAdditionalProperties_EventArgs();
    e.Document = doc;
    e.Node = node;
    if (this.getAdditionalProperties_EventHandler != null)
      this.getAdditionalProperties_EventHandler((object) this, e);
    return e.Properties;
  }

  /// <summary>Происходит когда произошли изменения</summary>
  public event GetAdditionalProperties_EventHandler GetAdditionalProperties
  {
    add => this.getAdditionalProperties_EventHandler += value;
    remove => this.getAdditionalProperties_EventHandler -= value;
  }

  public static AdditionalPropertiesManager Instance
  {
    get
    {
      if (AdditionalPropertiesManager.instance == null)
        AdditionalPropertiesManager.instance = new AdditionalPropertiesManager();
      return AdditionalPropertiesManager.instance;
    }
  }
}
