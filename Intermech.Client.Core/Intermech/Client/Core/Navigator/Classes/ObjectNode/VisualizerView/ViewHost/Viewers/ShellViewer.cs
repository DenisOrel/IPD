
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ShellViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Interfaces;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class ShellViewer : ViewProcessViewerBase, IShellCommandLineSupport
{
  private string _commnadLine;
  private string[] _arguments;
  private string _fileName;

  protected override void SetFileName(string fileName) => this._fileName = fileName;

  protected override string GetFileName()
  {
    this._arguments = FileExtensionsInfo.ParseArguments(this._commnadLine.Replace("\"%1\"", $"\"{this._fileName}\""));
    return Environment.ExpandEnvironmentVariables(this._arguments[0]);
  }

  protected override string GetArguments() => this._arguments[1];

  public void SetCommandLine(string commandLine) => this._commnadLine = commandLine;
}
