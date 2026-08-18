// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailDescriptor
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

public class EmailDescriptor : HiveDescriptor
{
  private string _accauntEmail = string.Empty;
  private EmailNode _node;

  private EmailNode Node
  {
    get
    {
      if (this._node == null)
        this._node = new EmailNode(this._accauntEmail);
      return this._node;
    }
  }

  public EmailDescriptor(string accauntEmail)
    : base(EmailConsts.CategoryEmail, 0, accauntEmail)
  {
    this._accauntEmail = accauntEmail;
  }

  public override INode GetChild(INodeID nodeID) => (INode) this.Node;

  public override bool Equals(object obj)
  {
    return obj == null || obj.GetType() != typeof (EmailDescriptor) ? base.Equals(obj) : this._accauntEmail == ((EmailDescriptor) obj)._accauntEmail;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new EmailDescriptor(this._accauntEmail);
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    return dataFormat == typeof (IEmailNode) ? (object) this.Node : base.GetData(nodeID, dataFormat);
  }

  public override INodeID GetRecordNodeID() => (INodeID) new EmailAccauntNodeID(this._accauntEmail);
}
