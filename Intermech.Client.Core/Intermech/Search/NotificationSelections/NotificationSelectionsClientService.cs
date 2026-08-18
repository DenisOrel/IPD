
// Type: Intermech.Search.NotificationSelections.NotificationSelectionsClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.NotifySamples;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;


namespace Intermech.Search.NotificationSelections;

public sealed class NotificationSelectionsClientService : INotificationSelectionsClientService
{
  private static readonly TimeSpan SynchronizationInterval = new TimeSpan(0, 0, 30);
  private DateTime _nextSynchronizationTime;

  public void StartNotificationSelections()
  {
    ServiceLocator.Get<INotificationService>().Subscribe(new NotificationEventHandler(this.NotificationEventFired));
    new Thread(new System.Threading.ThreadStart(this.ThreadStart))
    {
      IsBackground = true,
      Priority = ThreadPriority.Lowest
    }.Start();
  }

  private void ThreadStart()
  {
    try
    {
      while (true)
      {
        Thread.Sleep(100);
        if (this._nextSynchronizationTime <= DateTime.Now)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            INotifySamplesProcessor samplesProcessor = sessionKeeper.Session.GetNotifySamplesProcessor();
            NSResult nsResult = samplesProcessor.ProcessSamples();
            if (nsResult != null)
            {
              if (nsResult.Samples != null && nsResult.Samples.Count > 0 && ServicesManager.GetService(typeof (IUINotificationService)) is IUINotificationService service)
              {
                UINotificationBuilder notificationBuilder = new UINotificationBuilder();
                foreach (NSDifferences sample in nsResult.Samples)
                {
                  NSDifferences nsDifferences = sample;
                  StringBuilder stringBuilder = new StringBuilder();
                  if (nsDifferences.IncludedObjects != null && nsDifferences.IncludedObjects.Length != 0)
                    stringBuilder.AppendFormat("В выборке {0} появилось {1} новых объектов. ", (object) nsDifferences.SampleName, (object) nsDifferences.IncludedObjects.Length);
                  if (nsDifferences.ExcludedObjects != null && nsDifferences.ExcludedObjects.Length != 0)
                    stringBuilder.AppendFormat("Условиям выборки {0} перестали соответствовать или были удалены {1} объектов.", (object) nsDifferences.SampleName, (object) nsDifferences.ExcludedObjects.Length);
                  string message = stringBuilder.ToString();
                  notificationBuilder.Message = message;
                  notificationBuilder.Icon = UINotificationIcon.Info;
                  notificationBuilder.SetContentAction((Action) (() => this.UINotificationAction(nsDifferences, message)));
                  service.ShowNotification(notificationBuilder.Build());
                }
                try
                {
                  samplesProcessor.SaveSamplesState();
                }
                catch (Exception ex)
                {
                  notificationBuilder.FillFromException(ex);
                  service.ShowNotification(notificationBuilder.Build());
                }
              }
              this._nextSynchronizationTime = nsResult.NextProcessTime;
            }
            else
              this._nextSynchronizationTime = DateTime.Now + NotificationSelectionsClientService.SynchronizationInterval;
          }
        }
        Thread.Sleep(NotificationSelectionsClientService.SynchronizationInterval);
      }
    }
    catch (Exception ex)
    {
      if (!(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("Ошибки", ex.Message);
    }
  }

  private void UINotificationAction(NSDifferences nsDifferences, string caption)
  {
    ObjectsSelectionOptionsHolder selectionOptionsHolder = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications | ObjectsSelectionOptions.LocalTypesMode);
    ServiceContainer viewServices = new ServiceContainer((IServiceProvider) ServicesManager.ServiceContainer);
    Utils.OpenNewWindow((IDescriptor) new NSDifferencesDescriptor(nsDifferences, caption), (IServiceProvider) viewServices);
  }

  private void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e.EventName == "ObjectsChanged") && !(e.EventName == "ObjectsCreated") && !(e.EventName == "ObjectsRemoved") || !(e is DBObjectsEventArgs))
      return;
    this._nextSynchronizationTime = DateTime.Now;
  }
}
