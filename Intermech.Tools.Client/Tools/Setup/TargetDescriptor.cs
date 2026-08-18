// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.TargetDescriptor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class TargetDescriptor
{
  private ITarget target;
  private string displayName;
  private static readonly TargetDescriptor publicSettings = new TargetDescriptor((ITarget) AllUsersTarget.Value, LocalizationHolder.rm.GetString("Tools.Client_173"));

  public TargetDescriptor(ITarget target, string displayName)
  {
    if (target == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(displayName))
      throw new ArgumentException();
    this.target = target;
    this.displayName = displayName;
  }

  public ITarget Target => this.target;

  public string DisplayName => this.displayName;

  public override int GetHashCode() => this.target.GetHashCode();

  public override bool Equals(object obj)
  {
    return !(obj is TargetDescriptor targetDescriptor) ? base.Equals(obj) : targetDescriptor.target.Equals((object) this.target);
  }

  public override string ToString() => this.DisplayName;

  public static TargetDescriptor PublicSettings => TargetDescriptor.publicSettings;

  public static TargetDescriptor CurrentUser
  {
    get
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return new TargetDescriptor((ITarget) new UserTarget(service.UserID, service.UserGuid), service.UserName);
    }
  }
}
