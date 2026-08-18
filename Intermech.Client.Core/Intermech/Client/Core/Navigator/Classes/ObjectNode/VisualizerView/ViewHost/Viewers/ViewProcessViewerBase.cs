
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ViewProcessViewerBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using System;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal abstract class ViewProcessViewerBase : IViewer
{
  protected ViewProcess _axExeViewer;
  protected Control _owner;

  public void Init(Control owner)
  {
    this._owner = owner;
    this._axExeViewer = new ViewProcess();
    this._owner.SuspendLayout();
    this._owner.Controls.Add((Control) this._axExeViewer);
    this._owner.Resize += new EventHandler(this._owner_Resize);
    this._owner.ResumeLayout(false);
    this.OnResize();
  }

  private void OnResize()
  {
    this._axExeViewer.Width = this._axExeViewer.Parent.Width;
    this._axExeViewer.Height = this._axExeViewer.Parent.Height;
  }

  private void _owner_Resize(object sender, EventArgs e) => this.OnResize();

  public void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider)
  {
    this.SetFileName(fileItemInfo.FileFullName);
    this._axExeViewer.FileName = this.GetFileName();
    this._axExeViewer.Arguments = this.GetArguments();
    this._axExeViewer.WorkingDirectory = Path.GetDirectoryName(fileItemInfo.FileFullName);
    this._axExeViewer.LoadProcess();
    this._axExeViewer.Visible = true;
  }

  public void Close()
  {
    this._axExeViewer.Visible = false;
    this._axExeViewer.ReleaseProcess();
  }

  public void Clear()
  {
    if (this._axExeViewer == null)
      return;
    this._owner.Resize -= new EventHandler(this._owner_Resize);
    if (this._owner != null && this._owner.Controls.Contains(this._owner))
      this._owner.Controls.Remove((Control) this._axExeViewer);
    this._axExeViewer.Dispose();
  }

  protected virtual string GetFileName() => string.Empty;

  protected virtual string GetArguments() => string.Empty;

  protected virtual void SetFileName(string fileName)
  {
  }
}
