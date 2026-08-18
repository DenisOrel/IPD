// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.CheckPublishCompositionEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class CheckPublishCompositionEventArgs
{
  public IUserSession Session { get; private set; }

  public PublishComposition Composition { get; private set; }

  public ExtendedPublishOptions Options { get; private set; }

  public CheckPublishCompositionEventArgs(
    IUserSession session,
    PublishComposition composition,
    ExtendedPublishOptions options)
  {
    this.Session = session;
    this.Composition = composition;
    this.Options = options;
  }
}
