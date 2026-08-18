
// Type: Intermech.Client.Core.ProcessFileService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;


namespace Intermech.Client.Core;

internal sealed class ProcessFileService : IProcessFileService
{
  public event FileProcessEventHandler FileProcessEvent;

  public void FireFileProcessEvent(FileProcessEventArgs eventArgs)
  {
    FileProcessEventHandler fileProcessEvent = this.FileProcessEvent;
    if (fileProcessEvent == null)
      return;
    fileProcessEvent((object) this, eventArgs);
    if (!eventArgs.IsHandled || eventArgs.LaunchType != LaunchType.Edit)
      return;
    FileAttribute4ObjectChangedEventArgs e = new FileAttribute4ObjectChangedEventArgs(eventArgs.AttributeId, eventArgs.ObjectId);
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) e);
  }
}
