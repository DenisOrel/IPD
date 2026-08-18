// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlScripts.XmlExportScriptCommand
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using ICSharpCode.SharpZipLib.Zip;
using Intermech.Expert;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.XmlExchange.Settings.Export.Common;
using Intermech.Kernel.Search;
using Intermech.Localization.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.XmlScripts;

/// <summary>
/// Класс - скрипт экспорта объектов в XML в фоновых задачах (маршрутизаторе, планировщике задач)
/// </summary>
public sealed class XmlExportScriptCommand
{
  /// <summary>
  /// Получение списка конфигураций экспорта/импорта XML для типа объекта
  /// </summary>
  /// <param name="objTypeId">Тип объекта (для экспорта)</param>
  /// <param name="needCommonConfigs">Признак добавления "общих" конфигураций</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public static List<long> GetExportXmlConfigs(
    int objTypeId,
    bool needCommonConfigs,
    IUserSession session)
  {
    List<long> exportXmlConfigs = new List<long>();
    if (objTypeId == -1)
      return exportXmlConfigs;
    List<Guid> objectTypeParentsGuid = MetaDataHelper.GetObjectTypeParentsGuid(objTypeId);
    objectTypeParentsGuid.Insert(0, MetaDataHelper.GetObjectTypeGuid(objTypeId));
    List<string> stringList = objectTypeParentsGuid.ConvertAll<string>((Converter<Guid, string>) (item => item.ToString()));
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545");
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) attributeTypeId1, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) attributeTypeId2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    };
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(3)
    {
      new ConditionStructure(attributeTypeId1, RelationalOperators.In, (object) objectTypeParentsGuid.ToArray(), LogicalOperators.OR, 0, false),
      new ConditionStructure(attributeTypeId2, RelationalOperators.In, (object) objectTypeParentsGuid.ToArray(), LogicalOperators.OR, 0, false)
    };
    Guid conditionValue = new Guid("CAD00001-306C-11D8-B4E9-00304F19F545");
    conditionStructureList.Add(new ConditionStructure(attributeTypeId1, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, false));
    DataTable objectData = DataHelper.GetObjectData(MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ExportSettObjTypeGuid), session, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray());
    if (objectData == null || objectData.Rows.Count == 0)
      return exportXmlConfigs;
    Dictionary<long, List<string>> dictionary = new Dictionary<long, List<string>>();
    int columnIndex = 0;
    foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
    {
      long result;
      if (long.TryParse(row[columnIndex].ToString(), out result) && !dictionary.ContainsKey(result))
      {
        IDBObject dbObject = session.GetObject(result, false);
        if (dbObject != null)
        {
          List<string> list = new List<string>();
          dictionary.Add(result, list);
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(attributeTypeId1);
          if (attributeById1 != null && attributeById1.Value != DBNull.Value)
          {
            string text = attributeById1.Value.ToString();
            if (GuidHelper.IsGuid(text))
              list.Add(text);
          }
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(attributeTypeId2);
          if (attributeById2 != null && attributeById2.ValuesCount > 0)
          {
            foreach (object obj in attributeById2.Values)
            {
              if (obj != DBNull.Value)
              {
                string text = obj.ToString();
                if (GuidHelper.IsGuid(text))
                  list.Add(text);
              }
            }
          }
          GenericListHelper.MakeUnique<string>(list);
        }
      }
    }
    foreach (string str in stringList)
    {
      if (!(str == string.Empty))
      {
        foreach (KeyValuePair<long, List<string>> keyValuePair in dictionary)
        {
          if (keyValuePair.Value.BinarySearch(str) >= 0)
            exportXmlConfigs.Add(keyValuePair.Key);
        }
        if (exportXmlConfigs.Count > 0)
          break;
      }
    }
    if (needCommonConfigs)
    {
      string str = conditionValue.ToString();
      foreach (KeyValuePair<long, List<string>> keyValuePair in dictionary)
      {
        if (keyValuePair.Value.BinarySearch(str) >= 0)
          exportXmlConfigs.Add(keyValuePair.Key);
      }
    }
    return exportXmlConfigs;
  }

  /// <summary>Запуск задачи экспорта данных в XML</summary>
  /// <param name="obj2Export">Экспортируемые объекты</param>
  /// <param name="xmlConfigId">Ид. конфигурации для экспорта объектов</param>
  /// <param name="exportPath">Директория экспорта</param>
  /// <param name="session"></param>
  public static void Execute(
    List<ObjInfoItem> obj2Export,
    long xmlConfigId,
    string exportPath,
    IUserSession session)
  {
    if (obj2Export == null || obj2Export.Count == 0 || xmlConfigId == 0L)
      return;
    IXmlExchangeService service = ServiceUtils.GetService<IXmlExchangeService>((object) session, true);
    XmlExchangeExportSettings settings;
    if (!XmlExchangeExportHelper.LoadSettings(xmlConfigId, session, out settings))
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(xmlConfigId);
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_5"), (object) objectInfo.Caption, (object) xmlConfigId));
    }
    if (settings.ObjVerRule == string.Empty)
      settings.ObjVerRule = DataHelper.Consts.cnt_def_filtrationRule;
    GenericListHelper.MakeUnique<ObjInfoItem>(obj2Export);
    List<object> list = ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) obj2Export).ConvertAll<object>((Converter<long, object>) (value => (object) value));
    List<List<object>> objectListList = new List<List<object>>();
    bool flag1 = settings.PacketChunkSize > 0 && settings.PacketChunkSize < list.Count;
    if (flag1)
      objectListList.AddRange((IEnumerable<List<object>>) GenericListHelper.SplitByChanks<object>((IList<object>) list, settings.PacketChunkSize));
    else
      objectListList.Add(list);
    for (int index1 = 0; index1 < objectListList.Count; ++index1)
    {
      List<ExportAttribute> exportAttributeList = new List<ExportAttribute>(1);
      exportAttributeList.Add(new ExportAttribute(1, objectListList[index1].ToArray()));
      string str1 = flag1 ? $"{exportPath}\\{string.Format(settings.PacketChunkDirFormat, (object) index1)}" : exportPath;
      if (!Directory.Exists(str1))
        Directory.CreateDirectory(str1);
      IXmlExchangeExportTask exportTask = service.CreateExportTask(session.SessionGUID);
      try
      {
        string errorMsg;
        if (!exportTask.ExportData(exportAttributeList.ToArray(), new object[2]
        {
          (object) settings,
          (object) xmlConfigId
        }, out errorMsg))
          throw new Exception(errorMsg);
        string[] exportDataFiles;
        if (!exportTask.GetExportFiles(out exportDataFiles))
          break;
        foreach (string str2 in exportDataFiles)
        {
          string str3 = string.Empty;
          string[] strArray = str2.Split(Path.DirectorySeparatorChar);
          bool flag2 = false;
          for (int index2 = strArray.Length - 1; index2 >= 0; --index2)
          {
            if (GuidHelper.IsGuid(strArray[index2]))
            {
              flag2 = true;
              break;
            }
            str3 = str3 != string.Empty ? strArray[index2] + Path.DirectorySeparatorChar.ToString() + str3 : strArray[index2];
          }
          if (!flag2)
            str3 = Path.GetFileName(str2);
          string str4 = str1 + Path.DirectorySeparatorChar.ToString() + str3;
          if (File.Exists(str4))
            File.Delete(str4);
          string path = Path.GetDirectoryName(str4) ?? string.Empty;
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
          if (File.Exists(str2))
          {
            File.Move(str2, str4);
          }
          else
          {
            IBlobReader exportData = exportTask.GetExportData(str2);
            if (exportData != null)
            {
              try
              {
                exportData.OpenBlob(4194304 /*0x400000*/);
                using (FileStream fileStream = new FileStream(str4, FileMode.Create))
                {
                  try
                  {
                    while (true)
                    {
                      byte[] buffer = exportData.ReadDataBlock();
                      if (buffer.Length != 0)
                        fileStream.Write(buffer, 0, buffer.Length);
                      else
                        break;
                    }
                  }
                  finally
                  {
                    fileStream.Flush();
                    fileStream.Close();
                  }
                }
              }
              finally
              {
                exportData.CloseBlob();
              }
            }
            else
              continue;
          }
          if (settings.CompressMode == XmlExportCompressMode.None)
          {
            try
            {
              new FastZip().ExtractZip(str4, str1, (string) null);
              File.Delete(str4);
            }
            catch (Exception ex)
            {
              if (!(ex is ZipException))
                throw;
            }
          }
        }
      }
      finally
      {
        service.DisposeExportTask(exportTask.TaskGuid);
      }
    }
  }

  /// <summary>Запуск задачи экспорта данных в XML</summary>
  /// <param name="obj2Export">Экспортируемые объекты</param>
  /// <param name="exportPath">Директория экспорта</param>
  /// <param name="session"></param>
  public static void Execute(List<long> obj2Export, string exportPath, IUserSession session)
  {
    if (obj2Export == null)
      throw new ArgumentNullException(nameof (obj2Export));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (obj2Export.Count == 0)
      return;
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) obj2Export);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, session);
    List<long> aList = (List<long>) null;
    foreach (int objectType in ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objectInfoList))
    {
      List<long> exportXmlConfigs = XmlExportScriptCommand.GetExportXmlConfigs(objectType, false, session);
      if (exportXmlConfigs.Count == 0)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_6"), (object) MetaDataHelper.GetObjectTypeName(objectType), (object) objectType));
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
        throw new Exception(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_7"));
      case 1:
        long xmlConfigId = aList[0];
        if (xmlConfigId == 0L)
          break;
        XmlExportScriptCommand.Execute(objectInfoList, xmlConfigId, exportPath, session);
        break;
      default:
        throw new Exception(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_8"));
    }
  }
}
