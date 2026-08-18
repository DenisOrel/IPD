// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.EnterpriseArchiveCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class EnterpriseArchiveCommand : BackgroundCommandPresenter
{
  protected EnterpriseArchiveCommand(string commandName, bool infiniteProgressBar)
    : base(commandName, infiniteProgressBar)
  {
  }

  protected override bool IsCancelException(Exception x)
  {
    switch (x)
    {
      case CancelCommandException _:
        return true;
      case AbortException _:
        return true;
      default:
        return base.IsCancelException(x);
    }
  }

  protected override string GetCancelMessage(Exception x)
  {
    switch (x)
    {
      case CancelCommandException _:
        return x.Message;
      case AbortException _:
        return (string) null;
      default:
        return base.GetCancelMessage(x);
    }
  }

  protected void CheckAborted()
  {
    if (!this.IsAttachedToView)
      throw new AbortException();
  }
}
