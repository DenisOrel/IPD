
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.AxHostWrappers.InventorViewControlHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using AxInventorViewControlLib;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;
using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.AxHostWrappers;

/// <summary>
/// 
/// </summary>
internal class InventorViewControlHost : AxInventorViewControl, IAxHost, IOpenClose
{
  private static readonly Lazy<InventorViewHostSettings> Settings = new Lazy<InventorViewHostSettings>();

  public InventorViewControlHost(string clsid)
  {
    if (!InventorViewControlHost.Settings.Value.Initialized)
      throw new Exception(LocalizationHolder.rm.GetString("InventorViewInitError"));
  }

  public bool Open(string fileName)
  {
    this.FileName = fileName;
    return true;
  }

  public void Close()
  {
  }

  public Control AxControl => (Control) this;

  public AxHost AxHost => (AxHost) this;
}
