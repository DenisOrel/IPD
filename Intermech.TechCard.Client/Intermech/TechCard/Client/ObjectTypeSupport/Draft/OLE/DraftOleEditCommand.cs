// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.DraftOleEditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE;

/// <summary>Конструктор</summary>
internal class DraftOleEditCommand(string commandName = "EditDocument") : DraftBaseEditCommand(commandName)
{
  /// <summary>Обработка объектов</summary>
  protected override void DoEditCommand(IDBObject dbObj)
  {
    if (dbObj == null)
      return;
    DraftOleClass draftOleClass = new DraftOleClass(dbObj.ObjectID);
    if (!draftOleClass.LoadData())
      return;
    Stream dataStream = draftOleClass.DataStream;
    if (!DraftOleEditDialog.ShowModal(ref dataStream, dbObj.Caption, true) || dataStream == null)
      return;
    draftOleClass.DataStream = dataStream;
    draftOleClass.SaveData();
    if (!(this.Items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChanged", (IList<long>) new long[1]
    {
      itemData.Value
    }, (IList<long>) new long[1]{ draftOleClass.ObjectId }));
  }
}
