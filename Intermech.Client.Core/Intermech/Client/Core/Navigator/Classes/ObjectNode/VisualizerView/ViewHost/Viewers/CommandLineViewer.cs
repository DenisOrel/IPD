
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.CommandLineViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class CommandLineViewer : IViewer, ICommandLineSupport
{
  private ViewProcess _axExeViewer;
  private Control _owner;
  private string _commandLine;

  public void Clear() => this._owner.Resize -= new EventHandler(this._owner_Resize);

  public void Close()
  {
    this._axExeViewer.Visible = false;
    this._axExeViewer.ReleaseProcess();
  }

  public void Init(Control owner)
  {
    this._owner = owner;
    this._axExeViewer = new ViewProcess();
    this._owner.SuspendLayout();
    owner.Controls.Add((Control) this._axExeViewer);
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
    string str = this._commandLine.Replace("\"", string.Empty).Trim();
    this._axExeViewer.AttachProcess(Process.Start(str.Substring(0, str.IndexOf("%1")).Trim(), str.Substring(str.IndexOf("%1"), str.Length - str.IndexOf("%1")).Trim().Replace("%1", fileItemInfo.FileFullName)));
    this._axExeViewer.Visible = true;
  }

  public void SetCommandLine(string commnadLine) => this._commandLine = commnadLine;
}
