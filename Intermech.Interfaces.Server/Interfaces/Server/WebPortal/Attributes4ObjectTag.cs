// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.Attributes4ObjectTag
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal;

[Serializable]
public class Attributes4ObjectTag : Attributes4Tag
{
  public PublishObjectRootType RootType;
  public string LinkedGuid;

  public Attributes4ObjectTag(PublishObjectRootType rootType, string linkedGuid)
  {
    this.LinkedGuid = linkedGuid;
    this.RootType = rootType;
  }
}
