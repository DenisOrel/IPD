// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECO_AnyCommands
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.ECO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECO_AnyCommands
{
  public static void CreateLinkedCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long objectID = (items.GetItemData(sc_6342.ssp_eco_6354(1774303346), typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute byId = (sessionKeeper.Session.GetObject(objectID) as IDBEditingContextsObject).Attributes.FindByID(RevHelper.idLinkedContNumber);
      ECOPlugin.RevObjectCreator.linkedNumber = byId == null || byId.Value == DBNull.Value ? objectID : Convert.ToInt64(byId.Value);
    }
    long ecoObjectId;
    using (SelIzvType selIzvType = new SelIzvType())
    {
      if (selIzvType.ShowDialog() != DialogResult.OK)
        return;
      ecoObjectId = selIzvType.EcoObjectID;
    }
    if (ecoObjectId == -1L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBEditingContextsObject editingContextsObject = sessionKeeper.Session.GetObject(ecoObjectId) as IDBEditingContextsObject;
      (editingContextsObject.GetAttributeByID(RevHelper.idAttrObjectLink) ?? editingContextsObject.Attributes.AddAttribute(RevHelper.idAttrObjectLink, false)).AsInteger = objectID;
    }
    ECOEditorForm openedEcoEditor = plugin.GetOpenedECOEditor(ecoObjectId, false);
    if (openedEcoEditor == null || ECOPlugin.RevObjectCreator.linkedNumber == -1L)
      return;
    openedEcoEditor.ECO.linkedContextNo = ECOPlugin.RevObjectCreator.linkedNumber;
  }

  public static void LinkToOtherCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (ECOPlugin.FindPlugin() == null)
      return;
    List<int> contextTopObjectsIds = MetaDataHelper.GetEditingContextTopObjectsIDs();
    DescriptorCollection descriptors = new DescriptorCollection();
    for (int index = 0; index < contextTopObjectsIds.Count; ++index)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(contextTopObjectsIds[index]));
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_248"), LocalizationHolder.rm.GetString("ECO.Client_249"), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("ECO.Client_32"), descriptors), SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectID1 = numArray[0];
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectID1, false);
      if (!(dbObject1 is IDBEditingContextsObject))
        return;
      IDBEditingContextsObject editingContextsObject = dbObject1 as IDBEditingContextsObject;
      if (editingContextsObject.LinkedContextNumber == 0L)
        editingContextsObject.LinkedContextNumber = Math.Abs(dbObject1.ObjectID);
      long linkedContextNumber1 = editingContextsObject.LinkedContextNumber;
      List<long> revList = new List<long>();
      for (int index = 0; index < items.Count; ++index)
      {
        long objectID2 = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectID2, false);
        if (dbObject2 is IDBEditingContextsObject)
        {
          long linkedContextNumber2 = (dbObject2 as IDBEditingContextsObject).LinkedContextNumber;
          if (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService)
          {
            foreach (long linkedContext in customService.GetLinkedContexts((object) sessionKeeper.Session.SessionGUID, linkedContextNumber2))
              revList.Add(linkedContext);
          }
        }
      }
      (sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer).LinkRevisionsToOther(sessionKeeper.Session.SessionGUID, (IEnumerable<long>) revList, linkedContextNumber1);
    }
  }

  public static void UnLinkToOtherCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (ECOPlugin.FindPlugin() == null)
      return;
    long num1 = (items.GetItemData(sc_6342.ssp_eco_6355(1158215385), typeof (IDBObjectID)) as IDBObjectID).Value;
    long linkedContextNumber = 0;
    StringBuilder stringBuilder = (StringBuilder) null;
    List<long> longList = (List<long>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(num1, false);
      if (dbObject != null)
      {
        if (dbObject is IDBEditingContextsObject)
        {
          linkedContextNumber = (dbObject as IDBEditingContextsObject).LinkedContextNumber;
          if (linkedContextNumber != 0L)
          {
            longList = (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetLinkedContexts((object) sessionKeeper.Session.SessionGUID, linkedContextNumber);
            if (longList != null)
            {
              if (!longList.Contains(linkedContextNumber) && !longList.Contains(-linkedContextNumber))
                longList.Add(linkedContextNumber);
              if (longList.Contains(num1))
                longList.Remove(num1);
              stringBuilder = new StringBuilder();
              stringBuilder.AppendLine(LocalizationHolder.rm.GetString("ECO.Client_256"));
              stringBuilder.AppendLine("\n\r");
              foreach (long objectID in longList)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
                stringBuilder.AppendFormat("{0} [{1}] \n\r", (object) objectInfo.Caption, (object) objectInfo.ObjectID);
              }
              stringBuilder.AppendLine("\n\r");
              stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_257"));
            }
          }
        }
      }
    }
    if (linkedContextNumber == 0L || longList == null || longList.Count == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_247"), LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK);
    }
    else
    {
      if (MessageBox.Show(stringBuilder.ToString(), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        (sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer).UnlinkToOther(sessionKeeper.Session.SessionGUID, num1);
    }
  }

  internal static long UnhideAllowed(HashSet<long> objIDs)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) RevHelper.idAttrHiding
      });
      long num = 0;
      foreach (long objId in objIDs)
      {
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, objId);
        if (dataTable == null || dataTable.Rows.Count == 0 || Convert.ToInt32(dataTable.Rows[0][1]) != 2)
          return 0;
        long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
        if (num == 0L)
          num = int64;
        else if (num != int64)
          return 0;
      }
      return num;
    }
  }

  public static void UnhideHiddenCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    HashSet<long> longSet = new HashSet<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
      {
        long num = itemData.Value;
        if (!longSet.Contains(num))
          longSet.Add(num);
      }
    }
    long objectID = ECO_AnyCommands.UnhideAllowed(longSet);
    if (objectID == 0L)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_404"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
    }
    else
      ECOPlugin.plugin.OpenECOEditorForObject(objectID, false, true, true, false)?.UnhideObjects(longSet);
  }

  public static void CreateDI(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long II_ID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    plugin.IssueDI(II_ID);
  }

  public static void CreateDPI(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long PI_ID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    plugin.IssueDPI(PI_ID);
  }
}
