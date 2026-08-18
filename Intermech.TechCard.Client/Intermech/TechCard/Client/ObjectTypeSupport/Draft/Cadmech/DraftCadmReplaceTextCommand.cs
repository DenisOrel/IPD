// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.DraftCadmReplaceTextCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Controls;
using Intermech.TechAcad.Connector;
using Intermech.TechAcad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech;

/// <summary>
/// Реализация команды "Передать параметры в эскиз" для эскиза Cadmech-T
/// </summary>
/// <summary>Конструктор</summary>
internal class DraftCadmReplaceTextCommand(string commandName = "ReplaceDimText") : 
  DraftBaseEditCommand(commandName)
{
  /// <summary>Обработка объектов</summary>
  protected override void DoEditCommand(IDBObject dbObj)
  {
    IDBRelationID itemData = this.Items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBObjectID parentData = this.Items.GetParentData(0, typeof (IDBObjectID)) as IDBObjectID;
    if (dbObj == null || itemData == null || parentData == null)
      return;
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    foreach (AttributeValues attributesValue in dbObj.GetAttributesValues(GetAttributeValuesModes.CheckVisibility))
    {
      IMSAttributeType attrType = MetaDataHelper.GetAttributeType(attributesValue.AttributeID);
      if (attrType != null && attrType.Alias != string.Empty && !tupleList.Any<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (a => a.Item1 == attrType.Alias)))
        tupleList.Add(new Tuple<string, string>(attrType.Alias, attributesValue.Value.ToString()));
    }
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
    IDraftObject draftObject = sketchObject1?.DraftObject;
    if (draftObject == null)
      return;
    ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
    if (!service.LoadAcad(TechAcadLoadMode.Normal))
      return;
    string dwgName = draftObject.Extract(0);
    service.OpenPicture(draftObject.DraftID);
    string sketchId = sketchObject1.SketchID;
    string caption = parentData.Caption;
    Intermech.TechAcad.Connector.TechAcad.ShowOper(dwgName, sketchId, caption);
    Intermech.TechAcad.Connector.TechAcad.ReplaceDimText(tupleList);
  }
}
