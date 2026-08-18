
// Type: Intermech.Client.Core.Visualizers.PreviewExtender
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Collections.Generic;


namespace Intermech.Client.Core.Visualizers;

internal class PreviewExtender : IPreviewExtender
{
  public event ExtendEventHandler Extend;

  internal void GetObjects(
    int objectType,
    long baseObjectId,
    List<FileBlobItem> items,
    ref long preferedBlobID)
  {
    if (this.Extend == null)
      return;
    ExtendEventArgs eventArgs = new ExtendEventArgs(objectType, baseObjectId, items);
    foreach (ExtendEventHandler invocation in this.Extend.GetInvocationList())
    {
      try
      {
        invocation(eventArgs);
      }
      catch
      {
      }
    }
    preferedBlobID = eventArgs.PreferedBlobID;
  }
}
