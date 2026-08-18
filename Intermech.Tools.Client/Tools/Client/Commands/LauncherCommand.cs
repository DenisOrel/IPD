// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.LauncherCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class LauncherCommand : ObjectCommand
{
  private LaunchType launchType;
  private bool autoCheckout;

  public LauncherCommand(string name, LaunchType launchType, bool autoCheckout)
    : base(name)
  {
    this.launchType = launchType;
    this.autoCheckout = autoCheckout;
  }

  protected override void DoExecute()
  {
    VersionsRulePackage versionsRule = this.launchType == LaunchType.Edit ? VersionsRuleSources.GetEditorRule() : VersionsRuleSources.GetCurrentWindowRule();
    ClientContext.LaunchActions.Launch(new LaunchParams(this.launchType, this.ObjectId, DBHelper.GetObjectType(this.ObjectId), versionsRule, this.autoCheckout));
  }
}
