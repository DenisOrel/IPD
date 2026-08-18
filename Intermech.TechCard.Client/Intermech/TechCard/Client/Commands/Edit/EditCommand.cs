// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.EditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.TechCard.Client.Commands.Action;
using Intermech.TechCard.Client.UI.Controls;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>
/// Реализация команды "Изменить объект" для технологических объектов
/// </summary>
/// <summary>Конструктор</summary>
internal class EditCommand(string commandName = "editObjectNode") : BaseEditCommand(commandName)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
    ServiceUtils.GetService<IFormDesignerService>((object) session, false)?.ClearUserVersionCache(session.UserID);
  }

  /// <summary>Обработка объектов</summary>
  protected override void DoProceedItems()
  {
    if (!(this.Items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID) || !(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      if (service != null)
      {
        List<int> objTypeIds;
        if (service.GetCreationTypes(sessionKeeper.Session.SessionGUID, out objTypeIds))
        {
          if (objTypeIds != null)
          {
            if (objTypeIds.Contains(itemData.ObjectType))
              flag = true;
          }
        }
      }
    }
    EditCommandActionParam actionParam = new EditCommandActionParam(this.Items, this.ContextServices);
    IList<CategoryValue> modificationsList;
    if (!(flag ? (CommandAction) new EditCommandImbaseAction(actionParam) : (CommandAction) new EditCommandAction(actionParam)).Execute(out modificationsList) || modificationsList == null || !modificationsList.Any<CategoryValue>())
      return;
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents(modificationsList))
      this.Notifications.QueueEvent(notificationEvent);
  }
}
