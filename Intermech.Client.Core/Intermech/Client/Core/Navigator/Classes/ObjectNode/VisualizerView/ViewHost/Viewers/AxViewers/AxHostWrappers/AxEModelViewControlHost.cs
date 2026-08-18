
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AxEModelViewControlHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using AxEModelView;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal class AxEModelViewControlHost(string clsid) : AxEModelViewControl(clsid), IAxHost, IOpenClose
{
  public Control AxControl => (Control) this;

  public AxHost AxHost => (AxHost) this;

  public bool Open(string fileName) => this.OpenDoc(fileName, false, false, true, "");

  public void Close() => this.CloseActiveDoc(string.Empty);
}
