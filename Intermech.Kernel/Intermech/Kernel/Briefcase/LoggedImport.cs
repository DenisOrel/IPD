// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.LoggedImport
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal abstract class LoggedImport : ILogged<string>
{
  protected void AddToLog(string message) => this.Log.Add(message);

  public List<string> Log { get; } = new List<string>();
}
