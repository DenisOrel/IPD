
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.DefaultViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class DefaultViewer : IViewer, IShellCommandLineSupport
{
  private ViewProcess _axExeViewer;
  private Control _owner;
  private string _shellCommandLine;

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
    this._axExeViewer.WorkingDirectory = Path.GetDirectoryName(fileItemInfo.FileFullName);
    ProcessStartInfo startInfo = new ProcessStartInfo(fileItemInfo.FileFullName)
    {
      Arguments = $"\"{(object) fileItemInfo}\" ",
      UseShellExecute = true,
      WindowStyle = ProcessWindowStyle.Normal,
      WorkingDirectory = Path.GetDirectoryName(fileItemInfo.FileFullName) ?? string.Empty
    };
    if (!string.IsNullOrEmpty(this._shellCommandLine))
    {
      string[] strArray = this._shellCommandLine.Split('¦');
      if (strArray.Length > 1)
        startInfo.Verb = strArray[1];
      string[] arguments = FileExtensionsInfo.ParseArguments(strArray[0]);
      startInfo.FileName = Environment.ExpandEnvironmentVariables(arguments[0]);
    }
    Process process = Process.Start(startInfo);
    Thread.Sleep(3000);
    this._axExeViewer.AttachProcess(process);
    this._axExeViewer.Visible = true;
  }

  public void SetCommandLine(string commandLine) => this._shellCommandLine = commandLine;
}
