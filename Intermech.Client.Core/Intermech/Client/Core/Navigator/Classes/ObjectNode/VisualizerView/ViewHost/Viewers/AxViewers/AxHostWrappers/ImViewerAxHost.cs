
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.ImViewerAxHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

/// <summary>Класс обертка над AxHost</summary>
[AxHost.Clsid("{43C7E03D-DC17-40EA-A97A-E7C09696DEE3}")]
internal sealed class ImViewerAxHost : 
  UserControl,
  IOpenClose,
  IAxHost,
  IFileStateSupport,
  IOpenConfiguration
{
  private AxImViewerControl _axImViewerControl;
  private ImViewerFileSateInfo _imViewerFileSateInfoControl;
  private bool? _isActual;
  private int _fileStateControlHeight = 30;

  public ImViewerAxHost(string clsid)
  {
    this.SuspendLayout();
    AxImViewerControl axImViewerControl = new AxImViewerControl();
    axImViewerControl.TabIndex = 0;
    axImViewerControl.Left = 0;
    this._axImViewerControl = axImViewerControl;
    ImViewerFileSateInfo viewerFileSateInfo = new ImViewerFileSateInfo();
    viewerFileSateInfo.Top = 0;
    viewerFileSateInfo.Left = 0;
    viewerFileSateInfo.Height = this._fileStateControlHeight;
    viewerFileSateInfo.Visible = false;
    viewerFileSateInfo.TabIndex = 1;
    viewerFileSateInfo.ShowText = "Файлы IMViewer, необходимые для просмотра, содержат неактуальные или отсутствующие элементы";
    this._imViewerFileSateInfoControl = viewerFileSateInfo;
    this.Controls.AddRange(new Control[2]
    {
      (Control) this._axImViewerControl,
      (Control) this._imViewerFileSateInfoControl
    });
    this.Resize += new EventHandler(this.ImViewerAxHost_Resize);
    this.VisibleChanged += new EventHandler(this.ImViewerAxHost_VisibleChanged);
    this.ResumeLayout(false);
    this.OnResize_();
  }

  private void ImViewerAxHost_VisibleChanged(object sender, EventArgs e)
  {
    if (this._isActual.HasValue)
      this._imViewerFileSateInfoControl.Visible = !this._isActual.Value;
    this.OnResize_();
  }

  private void OnResize_()
  {
    this._imViewerFileSateInfoControl.Width = this._axImViewerControl.Width = this.Width;
    if (this._imViewerFileSateInfoControl.Visible)
    {
      this._axImViewerControl.Top = this._imViewerFileSateInfoControl.Bottom;
      this._axImViewerControl.Height = this.Height - this._imViewerFileSateInfoControl.Height;
    }
    else
    {
      this._axImViewerControl.Top = 0;
      this._axImViewerControl.Height = this.Height;
    }
  }

  private void ImViewerAxHost_Resize(object sender, EventArgs e) => this.OnResize_();

  public bool Open(string fileName) => this._axImViewerControl.Open(fileName);

  public void Close() => this._imViewerFileSateInfoControl.Visible = false;

  public Control AxControl => (Control) this;

  public AxHost AxHost => (AxHost) this._axImViewerControl;

  public void SetState(bool? isActual)
  {
    this._isActual = isActual;
    if (this._isActual.HasValue)
      this._imViewerFileSateInfoControl.Visible = !this._isActual.Value;
    this.OnResize_();
  }

  public bool OpenConfiguration(string fileName, string configName)
  {
    return this._axImViewerControl.OpenConfig(fileName, configName);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.Resize -= new EventHandler(this.ImViewerAxHost_Resize);
      this.VisibleChanged -= new EventHandler(this.ImViewerAxHost_VisibleChanged);
    }
    base.Dispose(disposing);
  }
}
