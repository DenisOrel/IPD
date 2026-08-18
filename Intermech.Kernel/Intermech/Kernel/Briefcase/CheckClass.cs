// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckClass
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckClass : LoggedСheck
{
  protected UserSession session;
  protected CheckOptions options;

  protected bool noneSynhronizingError => !this.isSynhronizing || this.isErrorAlways;

  protected bool synhronizingError => this.isSynhronizing || this.isErrorAlways;

  protected bool isSynhronizing
  {
    get => (this.options & CheckOptions.IsSynhronizing) == CheckOptions.IsSynhronizing;
  }

  public CheckClass(UserSession session, int category, CheckOptions options)
    : base(category, (options & CheckOptions.IsErrorAlways) == CheckOptions.IsErrorAlways)
  {
    this.InitializeData(session, options);
  }

  public CheckClass(UserSession session, string categoryCaption, CheckOptions options)
    : base(categoryCaption, (options & CheckOptions.IsErrorAlways) == CheckOptions.IsErrorAlways)
  {
    this.InitializeData(session, options);
  }

  private void InitializeData(UserSession session, CheckOptions options)
  {
    this.session = session;
    this.options = options;
  }
}
