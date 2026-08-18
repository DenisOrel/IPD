// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AddInProxy
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Win32;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class AddInProxy
{
  public AddInProxy(IIPSAddIn cadInterface) => this.AddIn = cadInterface;

  public void SwitchToApp()
  {
    ForegroundWindowHelper.Default.TrySetWindow(this.AddIn.GetMainWindowHandle());
  }

  public IIPSAddIn AddIn { get; }

  public void OpenObject(string fullPath) => ApiHelper.OpenObject(this.AddIn, fullPath);
}
