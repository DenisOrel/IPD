// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.BlankCodeAttrProxy
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class BlankCodeAttrProxy
{
  internal long ID;
  internal string Name = string.Empty;

  public BlankCodeAttrProxy(long id)
  {
    if (id == 0L)
      return;
    this.ID = id;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.Name = sessionKeeper.Session.GetObjectInfo(this.ID).Caption;
  }

  public override string ToString() => this.Name;
}
