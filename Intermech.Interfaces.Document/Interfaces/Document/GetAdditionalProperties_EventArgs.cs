// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GetAdditionalProperties_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

public class GetAdditionalProperties_EventArgs : EventArgs
{
  private ImDocumentData doc;
  private DocumentTreeNode node;
  private List<PropertyDescriptor> properties = new List<PropertyDescriptor>();

  public List<PropertyDescriptor> Properties => this.properties;

  public ImDocumentData Document
  {
    get => this.doc;
    set => this.doc = value;
  }

  public DocumentTreeNode Node
  {
    get => this.node;
    set => this.node = value;
  }
}
