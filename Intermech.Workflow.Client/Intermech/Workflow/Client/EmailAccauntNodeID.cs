// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailAccauntNodeID
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.Interfaces;
using System.Diagnostics;

#nullable disable
namespace Intermech.Workflow.Client;

internal class EmailAccauntNodeID : INodeID
{
  private string _accauntEmail = string.Empty;
  private object _cookie;

  public EmailAccauntNodeID(string accauntEmail) => this._accauntEmail = accauntEmail;

  public int CategoryID
  {
    [DebuggerStepThrough] get => EmailConsts.CategoryEmail;
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => 0;
  }

  public object Cookie
  {
    [DebuggerStepThrough] get => this._cookie;
    [DebuggerStepThrough] set => this._cookie = value;
  }

  public override bool Equals(object obj)
  {
    return obj == null || obj.GetType() != typeof (EmailAccauntNodeID) ? base.Equals(obj) : this._accauntEmail == ((EmailAccauntNodeID) obj)._accauntEmail;
  }

  public override int GetHashCode() => this._accauntEmail.GetHashCode();
}
