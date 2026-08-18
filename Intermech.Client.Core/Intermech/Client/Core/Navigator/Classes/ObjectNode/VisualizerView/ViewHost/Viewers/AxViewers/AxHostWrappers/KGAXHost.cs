
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.KGAXHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using AxKGAXLib;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal sealed class KGAXHost : AxKGAX, IAxHost, IOpenClose
{
  public KGAXHost(string clsid)
  {
  }

  public bool Open(string fileName)
  {
    this.CloseAll();
    this.ActivateDocument((object) this.AddDocument(fileName));
    return true;
  }

  public void Close()
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      try
      {
        this.CloseAll();
        // ISSUE: reference to a compiler-generated method
        this.GetKompasObject()?.Quit();
      }
      catch
      {
      }
    }
    base.Dispose(disposing);
  }

  public Control AxControl => (Control) this;

  public AxHost AxHost => (AxHost) this;
}
