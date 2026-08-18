
// Type: Intermech.Client.Core.FormDesigner.BeforeFormObjectCheckinEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;


namespace Intermech.Client.Core.FormDesigner;

public class BeforeFormObjectCheckinEventArgs : NotificationEventArgs
{
  public static readonly string BeforeFormObjectCheckinEvent = "BeforeFormObjectCheckin";

  public BeforeFormObjectCheckinEventArgs(long formObjectId)
    : base(BeforeFormObjectCheckinEventArgs.BeforeFormObjectCheckinEvent)
  {
    this.FormObjectId = formObjectId;
  }

  public long FormObjectId { get; }
}
