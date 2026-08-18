// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.CaptionsHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System.Collections;


namespace Intermech.Kernel;

public class CaptionsHelper : ICaptionsHelper
{
  private Hashtable ch;

  public CaptionsHelper() => this.ch = DataSetProcessor.ColumnCaptions;

  public string GetCaption(string id) => (this.ch[(object) id] ?? (object) id).ToString();

  public void AddCaption(string id, string caption) => this.ch[(object) id] = (object) caption;
}
