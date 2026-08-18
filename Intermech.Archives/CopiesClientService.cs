// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.CopiesClientService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Archives.Copies;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives;

internal class CopiesClientService : ICopiesClientService
{
  /// <summary>
  /// Копировать лист рассылки.
  /// !!! Атрибут Примечание пока не копируем. Предполагается, что он должен быть уникальным. Обсуждение ББ 1714591
  /// </summary>
  /// <param name="copiedDeliveryListID">ИД копируемого листа рассылки</param>
  /// <param name="docsDeliveryLists">Список листов рассылки, в которые будут скопированы абоненты</param>
  public void CopyDeliveryList(long copiedDeliveryListID, List<long> docsDeliveryLists)
  {
    using (CopyDeliveryListModeChoiceForm listModeChoiceForm = new CopyDeliveryListModeChoiceForm())
    {
      int num = (int) listModeChoiceForm.ShowDialog();
      switch (listModeChoiceForm.DialogResult)
      {
        case DialogResult.Yes:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
              throw new KernelException("Не найден ICopiesService");
            customService.AddSubcribersToDeliveryLists(sessionKeeper.Session.SessionGUID, copiedDeliveryListID, docsDeliveryLists);
            INotificationService service = ApplicationServices.Container.GetService<INotificationService>();
            if (service == null)
              break;
            List<int> objectTypeIDs = new List<int>(docsDeliveryLists.Count);
            for (int index = 0; index < docsDeliveryLists.Count; ++index)
              objectTypeIDs.Add(ConstsHolder.DeliveryListID);
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) docsDeliveryLists, (IList<int>) objectTypeIDs));
            break;
          }
        case DialogResult.No:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
              throw new KernelException("Не найден ICopiesService");
            customService.ReplaceSubscribersInDeliveryLists(sessionKeeper.Session.SessionGUID, copiedDeliveryListID, docsDeliveryLists);
            INotificationService service = ApplicationServices.Container.GetService<INotificationService>();
            if (service == null)
              break;
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) docsDeliveryLists, (IList<int>) new List<int>()
            {
              ConstsHolder.DeliveryListID
            }));
            break;
          }
      }
    }
  }
}
