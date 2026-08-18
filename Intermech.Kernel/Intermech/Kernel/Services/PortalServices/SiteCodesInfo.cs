// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SiteCodesInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Services.PortalServices;

public class SiteCodesInfo
{
  public long ObjectID { get; private set; }

  public int ObjectType { get; private set; }

  public char Creator { get; private set; }

  public char? Owner { get; private set; }

  public char? CompositionOwner { get; private set; }

  public SiteCodesInfo(
    long objectId,
    int objectType,
    char creator,
    char? owner,
    char? compositionOwner)
  {
    this.ObjectID = objectId;
    this.ObjectType = objectType;
    this.Creator = creator;
    this.Owner = owner;
    this.CompositionOwner = compositionOwner;
  }
}
