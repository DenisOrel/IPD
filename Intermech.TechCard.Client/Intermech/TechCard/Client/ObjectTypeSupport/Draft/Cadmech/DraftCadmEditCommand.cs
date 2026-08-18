// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.DraftCadmEditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.TechAcad.Connector;
using Intermech.TechAcad.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech;

/// <summary>
/// Обработка команды "Редактировать" для эскиза Cadmech-T
/// </summary>
/// <summary>Конструктор</summary>
internal class DraftCadmEditCommand(string commandName = "EditDocument") : DraftBaseEditCommand(commandName)
{
  /// <summary>Обработка объектов</summary>
  protected override void DoEditCommand(IDBObject dbObj)
  {
    if (dbObj == null)
      return;
    IDBRelationID itemData = this.Items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBObjectID parentData = this.Items.GetParentData(0, typeof (IDBObjectID)) as IDBObjectID;
    if (itemData == null || parentData == null)
    {
      ObjectCommands.EditCommand(this.Items, this.ContextServices, this.AdditionalInfo);
    }
    else
    {
      NavWindow activeDockControl = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, true).ActiveDockControl as NavWindow;
      ITPObject tpObject = TechAcadApplication.GetTpObject(new ObjInfoItem(itemData.ProjID), activeDockControl);
      if (tpObject.SketchCollection.Count == 0)
        return;
      ISketchObject sketchObject1 = (ISketchObject) null;
      for (int Index = 0; Index < tpObject.SketchCollection.Count; ++Index)
      {
        ISketchObject sketchObject2 = tpObject.SketchCollection.get_Item(Index);
        if (sketchObject2.OrderID == itemData.Sorting)
          sketchObject1 = sketchObject2;
      }
      if (sketchObject1 == null)
        sketchObject1 = tpObject.SketchCollection.get_Item(0);
      IDraftObject draftObject = sketchObject1?.DraftObject;
      if (draftObject == null && tpObject.DraftCollection.ItemCount != 0)
        draftObject = tpObject.DraftCollection.get_Item(0);
      if (draftObject == null || sketchObject1 == null)
        return;
      ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
      if (!service.LoadAcad(TechAcadLoadMode.Normal))
        return;
      string dwgName = draftObject.Extract(0);
      service.OpenPicture(draftObject.DraftID);
      string sketchId = sketchObject1.SketchID;
      string caption = parentData.Caption;
      Intermech.TechAcad.Connector.TechAcad.ShowOper(dwgName, sketchId, caption);
    }
  }
}
