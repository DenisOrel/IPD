
// Type: AxInventorViewControlLib.AxInventorViewControlEventMulticaster
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using InventorViewControlLib;
using System.Runtime.InteropServices;


namespace AxInventorViewControlLib;

[ClassInterface(ClassInterfaceType.None)]
public class AxInventorViewControlEventMulticaster : _DInventorViewControlEvents
{
  private AxInventorViewControl parent;

  public AxInventorViewControlEventMulticaster(AxInventorViewControl parent)
  {
    this.parent = parent;
  }
}
