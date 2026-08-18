// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.SimpleEditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>Редактирование объекта с открытием в новом окне</summary>
internal class SimpleEditCommand : BaseEditCommand
{
  public SimpleEditCommand(string commandName = "editObjectNode")
    : base(commandName)
  {
    this._checkProjLink = false;
  }

  protected override void DoProceedItems()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(itemData.Value);
          switch (dbObject1.ObjectModifyMode)
          {
            case ObjectModifyModes.Checkout:
            case ObjectModifyModes.CreateVersion:
              if (dbObject1.CheckoutBy == 0L)
              {
                dbObject1 = dbObject1.CheckOut();
                if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service && dbObject1 != null)
                {
                  DBObjectsCheckOutEventArgs e = new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new List<long>()
                  {
                    itemData.Value
                  }, (IList<long>) new List<long>()
                  {
                    dbObject1.ObjectID
                  });
                  service.FireEvent((object) null, (NotificationEventArgs) e);
                  break;
                }
                break;
              }
              if (dbObject1.CheckoutBy != sessionKeeper.Session.UserID)
              {
                IDBObject dbObject2 = sessionKeeper.Session.GetObject(dbObject1.CheckoutBy);
                if (dbObject2 == null)
                  return;
                int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_177"), new object[3]
                {
                  (object) dbObject1.Caption,
                  (object) dbObject1.ObjectID.ToString(),
                  (object) dbObject2.Caption
                }), LocalizationHolder.rm.GetString("TechCard.Client_178"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
              }
              break;
            case ObjectModifyModes.CantModify:
              int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_410"), (object) dbObject1.Caption, (object) dbObject1.ObjectID), LocalizationHolder.rm.GetString("TechCard.Client_178"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return;
          }
          if (dbObject1 != null)
          {
            switch (this.DoEditCommand(dbObject1, index))
            {
              case BaseCommandResult.Terminate:
                return;
              default:
                continue;
            }
          }
        }
      }
    }
  }

  protected virtual BaseCommandResult DoEditCommand(IDBObject dbObject, int index)
  {
    TechCardClientConst.OpenObjectInNewWindow(dbObject.ObjectID);
    return BaseCommandResult.OK;
  }
}
