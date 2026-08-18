
// Type: Intermech.Client.DBPatches.PatchRunner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.DBPatches;
using System;


namespace Intermech.Client.DBPatches;

public sealed class PatchRunner : AbstractPatchRunner
{
  private IOutputView outputView;

  public PatchRunner(IOutputView outputView = null) => this.outputView = outputView;

  protected override void LogPatchException(
    AbstractPatch patch,
    Exception exception,
    string errorMessage,
    string errorType,
    string errorStackTrace)
  {
    if (this.outputView == null)
      return;
    this.outputView.WriteString("Ошибки", errorMessage);
    this.outputView.WriteString("Ошибки", errorType);
    this.outputView.WriteString("Ошибки", errorStackTrace);
  }
}
