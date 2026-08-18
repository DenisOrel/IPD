// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ExtendedImportedInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ExtendedImportedInfo : TypedImportedInfo
{
  public string ActionCaption { get; private set; }

  public ExtendedImportedInfo(TypedImportedInfo info, string actionCaption)
    : base(info.Guid, info.Id, info.ObjectId, info.Category, info.IsNew, info.SystemType, info.ObjectType)
  {
    this.IsLink = info.IsLink;
    this.ActionCaption = actionCaption;
  }
}
