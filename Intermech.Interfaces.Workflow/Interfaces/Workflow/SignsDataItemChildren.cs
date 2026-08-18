// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.SignsDataItemChildren
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.Workflow;

[Serializable]
public class SignsDataItemChildren
{
  private string _graphForType;
  private bool _strongControl;
  private SignsDataItem _parent;
  private int _groupId;

  [XmlIgnore]
  public SignsDataItem Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  /// <summary>Идентификатор группы, нужен если задавали разные</summary>
  public int GroupID
  {
    get => this._groupId;
    set => this._groupId = value;
  }

  /// <summary>Набор граф для подписи</summary>
  public string GraphForType
  {
    get => this._graphForType;
    set => this._graphForType = value;
  }

  /// <summary>Строгий контроль</summary>
  public bool StrongControl
  {
    get => this._strongControl;
    set => this._strongControl = value;
  }

  public SignsDataItemChildren()
  {
  }

  public SignsDataItemChildren(SignsDataItem parent) => this._parent = parent;
}
