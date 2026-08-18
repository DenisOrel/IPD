
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.ActiveXViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers;

internal class ActiveXViewer : IViewer, IClsidSupport
{
  private Control _axControl;
  private Control _owner;
  private Guid _clsid;

  public void Close()
  {
    this._axControl.Visible = false;
    if (!(this._axControl is IOpenClose axControl))
      return;
    axControl.Close();
  }

  public void Init(Control owner)
  {
    this._owner = owner;
    IAxHost axHost1 = AxHostFactory.Instance.Create(this._clsid);
    this._axControl = axHost1.AxControl;
    AxHost axHost2;
    if ((axHost2 = axHost1.AxHost) != null)
      axHost2.BeginInit();
    this._owner.SuspendLayout();
    this._owner.Controls.Add(this._axControl);
    AxHost axHost3;
    if ((axHost3 = axHost1.AxHost) != null)
      axHost3.EndInit();
    this._owner.Resize += new EventHandler(this._owner_Resize);
    this._owner.ResumeLayout(false);
    this.OnResize();
  }

  private void OnResize()
  {
    this._axControl.Width = this._axControl.Parent.Width;
    this._axControl.Height = this._axControl.Parent.Height;
  }

  private void _owner_Resize(object sender, EventArgs e) => this.OnResize();

  public void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider)
  {
    if (this._axControl is IOpenConfiguration axControl2 && fileItemInfo.CadModelNameConfiguration != null)
      axControl2.OpenConfiguration(fileItemInfo.FileFullName, fileItemInfo.CadModelNameConfiguration);
    else if (this._axControl is IOpenClose axControl1)
      axControl1.Open(fileItemInfo.FileFullName);
    if (this._axControl is IFileStateSupport axControl3)
      axControl3.SetState(fileItemInfo.IsViewerFileActual);
    this._axControl.Visible = true;
  }

  public void Clear()
  {
    this._owner.Resize -= new EventHandler(this._owner_Resize);
    if (this._axControl == null || this._owner == null || !this._owner.Controls.Contains(this._axControl))
      return;
    this._owner.Controls.Remove(this._axControl);
    this._axControl.Dispose();
  }

  public void SetClsid(Guid clsid) => this._clsid = clsid;
}
