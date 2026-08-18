// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.SignsGroup
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public class SignsGroup
{
  private BindingList<SignsDataItemChildren> _children;
  private int _groupId;
  private SignsDataItem _parent;

  public SignsGroup() => this._children = new BindingList<SignsDataItemChildren>();

  public SignsGroup(SignsDataItem parent)
    : this()
  {
    this._parent = parent;
  }

  public bool SignAnyGraph => this.Children.Count == 0;

  /// <summary>Идентификатор группы, нужен если задавали разные</summary>
  public int GroupID
  {
    get => this._groupId;
    set => this._groupId = value;
  }

  [XmlIgnore]
  public SignsDataItem Parent
  {
    get => this._parent;
    set
    {
      this._parent = value;
      foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) this.Children)
        child.Parent = value;
    }
  }

  public BindingList<SignsDataItemChildren> Children
  {
    get => this._children;
    set => this._children = value;
  }
}
