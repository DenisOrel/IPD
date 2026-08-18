// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureCommands
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using ImSSP;
using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.DatabaseConfigurator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>команды для работы со структурой архива</summary>
public class ArchiveStructureCommands
{
  /// <summary>удалить атрибут из структуры</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void DeleteAttributeType(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    ArchiveStructureNodeID itemData1 = items.GetItemData(0, typeof (ArchiveStructureNodeID)) as ArchiveStructureNodeID;
    List<long> longList = new List<long>();
    longList.Add(itemData1.ArchiveID);
    List<int> attrTypeIDs = new List<int>();
    string text = items.Count <= 1 ? sc_552.ssp_archives_554() : sc_552.ssp_archives_553();
    DialogResult dialogResult = !ArchiveStructureCommands.HasEntersIn(itemData1.ArchiveID, longList) ? MessageBox.Show(items.Count <= 1 ? sc_552.ssp_archives_556() : sc_552.ssp_archives_555(), ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) : MessageBox.Show(text, ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    if (dialogResult.Equals((object) DialogResult.Cancel))
      return;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (ArchiveStructureNodeID)) is ArchiveStructureNodeID itemData2)
        attrTypeIDs.Add(itemData2.TypeID);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IArchiveService customService = sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService;
      if (dialogResult != DialogResult.Yes)
      {
        if (dialogResult != DialogResult.No)
          return;
        customService.UpdateArchiveStructure(longList, attrTypeIDs, ArchiveStructureChangeAction.DeleteFromArchive, sessionKeeper.Session.SessionGUID);
      }
      else
        customService.UpdateArchiveStructure(longList, attrTypeIDs, ArchiveStructureChangeAction.DeleteFromArchiveAndDocs, sessionKeeper.Session.SessionGUID);
    }
  }

  /// <summary>добавить атрибут в структуру архива</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void AddAttributeType(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    long archiveID = items.Count <= 0 ? (items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID : (items.GetItemData(0, typeof (ArchiveStructureNodeID)) as ArchiveStructureNodeID).ArchiveID;
    List<long> longList = new List<long>();
    longList.Add(archiveID);
    List<int> attrTypeIDs = new List<int>();
    List<int> intList = new List<int>();
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true, new int[1]))
    {
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(ConstsHolder.DocTypeGuid);
      if (!attributesSelectDlg.ShowDialog().Equals((object) DialogResult.OK) || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      List<int> selectedAttributesId = attributesSelectDlg.SelectedAttributesID;
      string empty = string.Empty;
      DialogResult dialogResult;
      if (ArchiveStructureCommands.HasEntersIn(archiveID, longList))
      {
        dialogResult = MessageBox.Show(selectedAttributesId.Count <= 1 ? ServiceHolder.rm.GetString(sc_552.ssp_archives_558()) : ServiceHolder.rm.GetString(sc_552.ssp_archives_557()), ServiceHolder.rm.GetString(sc_552.ssp_archives_559()), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      }
      else
      {
        dialogResult = MessageBox.Show(selectedAttributesId.Count <= 1 ? ServiceHolder.rm.GetString(sc_552.ssp_archives_561()) : ServiceHolder.rm.GetString(sc_552.ssp_archives_560()), ServiceHolder.rm.GetString(sc_552.ssp_archives_562()), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult.Equals((object) DialogResult.Yes))
          dialogResult = DialogResult.No;
        else if (dialogResult.Equals((object) DialogResult.No))
          dialogResult = DialogResult.Cancel;
      }
      if (dialogResult.Equals((object) DialogResult.Cancel))
        return;
      bool flag = false;
      foreach (int num1 in selectedAttributesId)
      {
        if (num1 < 0)
        {
          if (!flag)
          {
            int num2 = (int) MessageBox.Show(ServiceHolder.rm.GetString(sc_552.ssp_archives_563()), ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            flag = true;
          }
        }
        else
          attrTypeIDs.Add(num1);
      }
      if (attrTypeIDs.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IArchiveService customService = sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService;
        if (dialogResult != DialogResult.Yes)
        {
          if (dialogResult != DialogResult.No)
            return;
          customService.UpdateArchiveStructure(new List<long>()
          {
            archiveID
          }, attrTypeIDs, ArchiveStructureChangeAction.AddToArchive, sessionKeeper.Session.SessionGUID);
        }
        else
          customService.UpdateArchiveStructure(longList, attrTypeIDs, ArchiveStructureChangeAction.AddToArchive, sessionKeeper.Session.SessionGUID);
      }
    }
  }

  /// <summary>
  /// созадть новый атрибут и добавить ег ов структуру архива
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CreateAttributeType(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    long archiveID = items.Count <= 0 ? (items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID : (items.GetItemData(0, typeof (ArchiveStructureNodeID)) as ArchiveStructureNodeID).ArchiveID;
    List<long> longList = new List<long>();
    longList.Add(archiveID);
    List<int> attrTypeIDs = new List<int>();
    if (!(ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service))
      return;
    int num1 = service.AddAttribute(ServiceHolder.rm.GetString(sc_552.ssp_archives_564()), (int[]) null);
    if (num1.Equals(0))
      return;
    DialogResult dialogResult = DialogResult.No;
    if (ArchiveStructureCommands.HasEntersIn(archiveID, longList))
    {
      dialogResult = MessageBox.Show(ServiceHolder.rm.GetString(sc_552.ssp_archives_565()), ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (dialogResult.Equals((object) DialogResult.Cancel))
        return;
    }
    if (num1 < 0)
    {
      int num2 = (int) MessageBox.Show(ServiceHolder.rm.GetString(sc_552.ssp_archives_566()), ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IArchiveService customService = sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService;
        attrTypeIDs.Add(num1);
        if (dialogResult != DialogResult.Yes)
        {
          if (dialogResult != DialogResult.No)
            return;
          customService.UpdateArchiveStructure(new List<long>()
          {
            archiveID
          }, attrTypeIDs, ArchiveStructureChangeAction.AddNewToArchive, sessionKeeper.Session.SessionGUID);
        }
        else
          customService.UpdateArchiveStructure(longList, attrTypeIDs, ArchiveStructureChangeAction.AddNewToArchive, sessionKeeper.Session.SessionGUID);
      }
    }
  }

  /// <summary>есть ли у данного архива дочерние?</summary>
  /// <param name="archiveID">id архива</param>
  /// <param name="listOfChildArchvesIDs"> список id-ков дочерних архивов </param>
  /// <returns></returns>
  private static bool HasEntersIn(long archiveID, List<long> listOfChildArchvesIDs)
  {
    ArchiveStructureCommands.GetEntersIn(listOfChildArchvesIDs, archiveID, false);
    return listOfChildArchvesIDs.Count > 1;
  }

  /// <summary>сформировать список id-ков дочерних объектов</summary>
  /// <param name="list"></param>
  /// <param name="parentID"></param>
  /// <param name="recursive"></param>
  private static void GetEntersIn(List<long> list, long parentID, bool recursive)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) parentID, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.ArcTypeID, dbRecordSetParams);
      if (dataTable == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int32 = (long) Convert.ToInt32(row[0]);
        if (!list.Contains(int32))
        {
          list.Add(int32);
          if (recursive)
            ArchiveStructureCommands.GetEntersIn(list, int32, recursive);
        }
      }
    }
  }
}
