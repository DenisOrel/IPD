
// Type: Intermech.Client.Core.BlobProcessorTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Threading;


namespace Intermech.Client.Core;

/// <summary>Summary description for BlobProcessorTask.</summary>
public class BlobProcessorTask : CustomBackgroundTask
{
  private string origName = string.Empty;

  public BlobProcessorTask(string name, BlobProcCustomClass blobProcessor)
  {
    this.origName = name;
    this._imageIndex = 25;
    this._name = name;
    this._state = BackgroundTaskState.Paused;
    blobProcessor.Progress += new BlobProcCustomClass.ProgressEventHandler(this.blobProcessor_Progress);
    blobProcessor.ThreadFinish += new BlobProcCustomClass.ThreadFinishEventHandler(this.blobProcessor_ThreadFinish);
  }

  private void blobProcessor_Progress(
    BlobProcCustomClass sender,
    BlobProcessorMode mode,
    int progress)
  {
    if (mode != BlobProcessorMode.Unknown)
      this.Name = string.Format(this.origName + " ({0})", (object) EnumTypeHelper.GetCaption((Enum) mode));
    else
      this.Name = this.origName;
    if (this._state == BackgroundTaskState.Paused)
    {
      this._state = BackgroundTaskState.Running;
      this.OnChanged(BackgroundTaskChangedType.State);
    }
    if (progress > this._maxValue)
      return;
    this._value = progress;
    this.OnChanged(BackgroundTaskChangedType.Value);
  }

  private void blobProcessor_ThreadFinish(
    BlobProcCustomClass sender,
    bool result,
    object message,
    Exception exception,
    BlobInformation bi)
  {
    if (this._state != BackgroundTaskState.Running)
      return;
    this.Result = message;
    if (!result)
    {
      this._state = BackgroundTaskState.Error;
      this.OnChanged(BackgroundTaskChangedType.State);
    }
    else
    {
      this._state = BackgroundTaskState.Terminated;
      Thread.Sleep(1000);
      this.OnChanged(BackgroundTaskChangedType.Dispose);
    }
  }
}
