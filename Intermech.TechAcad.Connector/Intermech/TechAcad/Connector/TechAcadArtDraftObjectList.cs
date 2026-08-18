// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadArtDraftObjectList
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadArtDraftObjectList(ITPObject tpObject) : TechAcadDraftObjectList(tpObject)
{
  public override int ReadOnly => 1;

  public override IDraftObject Add() => (IDraftObject) null;

  public override void Remove(int index)
  {
  }
}
