// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FormSearch
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Document.Model;

public class FormSearch
{
  public string Name;
  public DocumentTreeNode node;
  public DocumentSection owner;

  public FormSearch(string aName, Page aCode)
  {
    this.Name = aName.ToUpper();
    this.node = (DocumentTreeNode) aCode;
    this.owner = (DocumentSection) null;
  }

  public FormSearch(string aName, DocumentTreeNode aCode, DocumentSection aOwner)
  {
    this.Name = aName.ToUpper();
    this.node = aCode;
    this.owner = aOwner;
  }

  public override int GetHashCode() => this.Name.GetHashCode();

  public override bool Equals(object obj)
  {
    return obj != null && obj is FormSearch formSearch && this.Name == formSearch.Name && this.node == formSearch.node && this.owner == formSearch.owner;
  }
}
