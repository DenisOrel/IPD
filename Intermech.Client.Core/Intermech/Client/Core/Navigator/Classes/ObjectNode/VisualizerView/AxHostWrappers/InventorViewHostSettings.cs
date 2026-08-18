
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.AxHostWrappers.InventorViewHostSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.AxHostWrappers;

/// <summary>
/// 
/// </summary>
internal class InventorViewHostSettings
{
  public bool Initialized { get; private set; }

  public InventorViewHostSettings() => this.Initialized = this.Configure();

  private bool Configure()
  {
    string attributeValue = typeof (InventorViewControlHost).GetAttributeValue<AxHost.ClsidAttribute, string>((Func<AxHost.ClsidAttribute, string>) (cl => cl.Value), true);
    if (attributeValue == string.Empty)
      return false;
    using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID\\" + attributeValue, false))
    {
      if (registryKey1 == null)
        return false;
      using (RegistryKey registryKey2 = registryKey1.OpenSubKey("InprocServer32", false))
      {
        if (registryKey2 == null)
          return false;
        string str = (string) registryKey2.GetValue(string.Empty);
        if (!File.Exists(str))
          return false;
        string directoryName = new FileInfo(str).DirectoryName;
        string environmentVariable = Environment.GetEnvironmentVariable("PATH");
        if ((environmentVariable + ";").IndexOf(directoryName + ";", StringComparison.CurrentCultureIgnoreCase) != -1)
          return true;
        Environment.SetEnvironmentVariable("PATH", $"{directoryName};{environmentVariable}");
      }
    }
    return true;
  }
}
