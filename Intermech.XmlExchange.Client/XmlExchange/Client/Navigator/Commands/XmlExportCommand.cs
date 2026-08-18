// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Navigator.Commands.XmlExportCommand
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.XmlExchange;
using Intermech.Interfaces.XmlExchange.XmlScripts;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.XmlExchange.Client.Kernel.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.Client.Navigator.Commands;

/// <summary>
/// 
/// </summary>
internal static class XmlExportCommand
{
  /// <summary>Получение информации об объекте и его типе</summary>
  /// <param name="items"></param>
  /// <param name="objInfoList"></param>
  /// <param name="needCheckFromBase"></param>
  /// <returns></returns>
  internal static bool GetSelectedItemsInfo(
    ISelectedItems items,
    out List<ObjInfoItem> objInfoList,
    bool needCheckFromBase)
  {
    objInfoList = new List<ObjInfoItem>();
    if (items == null || items.Count == 0)
      return false;
    List<ObjInfoItem> source = new List<ObjInfoItem>();
    for (int index = 0; index < items.Count; ++index)
    {
      long objectId = 0;
      int objTypeId = -1;
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
      {
        objectId = itemData2.ObjectID;
        objTypeId = itemData2.ObjectType;
      }
      else if (items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData1)
      {
        objectId = itemData1.Value;
        if (items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData)
          objTypeId = itemData.Value;
      }
      if (objectId != 0L)
        source.Add(new ObjInfoItem(objectId, objTypeId));
    }
    objInfoList.AddRange(source.Where<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => item.ObjTypeID != -1)));
    ObjInfoItem[] array = source.Where<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => item.ObjTypeID == -1)).ToArray<ObjInfoItem>();
    if (((IEnumerable<ObjInfoItem>) array).Any<ObjInfoItem>() & needCheckFromBase)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) array, sessionKeeper.Session);
    }
    objInfoList.AddRange((IEnumerable<ObjInfoItem>) array);
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoList);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  internal static void Execute(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray1 = XmlExchangeProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray1.Length];
    int appId = XmlExchangeProtectionKey.appId;
    byte[] queryData = numArray1;
    byte[] response = inArray;
    int num1 = service1.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(XmlExchangeProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_2"), (object) LocalizationHolder.rm.GetString("XmlExchange.Client_1"), (object) num1));
    List<ObjInfoItem> objInfoList;
    if (items == null || items.Count == 0 || viewServices == null || !XmlExportCommand.GetSelectedItemsInfo(items, out objInfoList, true) || objInfoList.Count == 0)
      return;
    long num2 = 0;
    string caption;
    XmlExchangeExportSettings settings;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> aList = (List<long>) null;
      foreach (int objectType in ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList))
      {
        List<long> exportXmlConfigs = XmlExportScriptCommand.GetExportXmlConfigs(objectType, true, sessionKeeper.Session);
        if (exportXmlConfigs.Count == 0)
        {
          int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_10"), (object) MetaDataHelper.GetObjectTypeName(objectType), (object) objectType), LocalizationHolder.rm.GetString("XmlExchange.Client_11"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
          return;
        }
        if (aList != null)
        {
          List<long> resultData;
          GenericListHelper.GetDifference<long>((IList<long>) aList, (IList<long>) exportXmlConfigs, GenericListHelper.SearchMode.smExistInBoth, out resultData);
          aList = resultData;
        }
        else
          aList = exportXmlConfigs;
      }
      // ISSUE: explicit non-virtual call
      switch (aList != null ? __nonvirtual (aList.Count) : 0)
      {
        case 0:
          int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("XmlExchange.Client_28"), LocalizationHolder.rm.GetString("XmlExchange.Client_11"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
          return;
        case 1:
          num2 = aList[0];
          break;
        default:
          long[] numArray2 = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("XmlExchange.Client_12"), string.Empty, (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ExportSettObjTypeGuid), LocalizationHolder.rm.GetString("XmlExchange.Client_13"), (IList) aList.ToArray()), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree);
          if (numArray2 != null && numArray2.Length != 0)
          {
            num2 = numArray2[0];
            break;
          }
          break;
      }
      if (num2 == 0L)
        return;
      caption = sessionKeeper.Session.GetObjectInfo(num2).Caption;
      if (!XmlExchangeExportHelper.LoadSettings(num2, sessionKeeper.Session, out settings))
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num2);
        int num5 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_14"), (object) objectInfo.Caption, (object) num2), LocalizationHolder.rm.GetString("XmlExchange.Client_11"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
    }
    string path = string.Empty;
    IConfigurationManager service2 = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service2 != null)
    {
      IConfiguration configuration = service2.Open("XmlExchange.Client");
      if (configuration != null)
        path = configuration.GetProperty("ExportDir");
    }
    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
    {
      Description = LocalizationHolder.rm.GetString("XmlExchange.Client_9"),
      ShowNewFolderButton = true
    };
    if (path != string.Empty && Directory.Exists(path))
      folderBrowserDialog.SelectedPath = path;
    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
      return;
    string selectedPath = folderBrowserDialog.SelectedPath;
    service2?.Create("XmlExchange.Client")?.SetProperty("ExportDir", selectedPath);
    if (settings.ObjVerRule == string.Empty)
    {
      VersionsRule currentRule = ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, true).Filtration?.CurrentRule;
      settings.ObjVerRule = currentRule != null ? currentRule.RuleObjectGuid : DataHelper.Consts.cnt_def_filtrationRule;
    }
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoList);
    List<object> list = ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList).ConvertAll<object>((Converter<long, object>) (value => (object) value));
    List<List<object>> objectListList = new List<List<object>>();
    bool flag = settings.PacketChunkSize > 0 && settings.PacketChunkSize < list.Count;
    if (flag)
      objectListList.AddRange((IEnumerable<List<object>>) GenericListHelper.SplitByChanks<object>((IList<object>) list, settings.PacketChunkSize));
    else
      objectListList.Add(list);
    for (int index2 = 0; index2 < objectListList.Count; ++index2)
    {
      List<ExportAttribute> exportAttributeList = new List<ExportAttribute>(1);
      exportAttributeList.Add(new ExportAttribute(1, objectListList[index2].ToArray()));
      string str = flag ? $"{selectedPath}\\{string.Format(settings.PacketChunkDirFormat, (object) index2)}" : selectedPath;
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      XmlExchangeExportTask task = new XmlExchangeExportTask(exportAttributeList.ToArray(), str, new object[2]
      {
        (object) settings,
        (object) num2
      });
      task.Name = flag ? string.Format(task.Name + ": {0} ({1})", (object) caption, (object) index2) : string.Format(task.Name + ": {0}", (object) caption);
      XmlExchangeClientCache.Services.BackgroundTaskView.AddTask((IBackgroundTask) task);
    }
  }
}
