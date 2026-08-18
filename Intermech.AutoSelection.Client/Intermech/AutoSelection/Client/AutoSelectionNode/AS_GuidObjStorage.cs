// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AS_GuidObjStorage
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AS_GuidObjStorage : AS_Guid
{
  private string _name;

  public AS_GuidObjStorage(AS_Guid data)
    : base(data.Value)
  {
  }

  public AS_GuidObjStorage(AS_Guid data, string name)
    : base(data.Value)
  {
    this._name = name;
  }

  public string Name
  {
    get
    {
      if (this._name != null)
        return this._name;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._name = sessionKeeper.Session.GetObjectInfo(this.Value).Caption;
      return this._name;
    }
  }

  public override string ToString() => this.Name;
}
