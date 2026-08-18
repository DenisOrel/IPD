// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.CadmechHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Files;
using Intermech.Imbase.Selection;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Pdm;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ArticlesList;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.API;

internal static class CadmechHelper
{
  private static Options _options = new Options();
  private static List<string> _dataList = new List<string>();
  private static List<string> _outList = new List<string>();
  private static List<string> _resultList = new List<string>();
  private static ICatalogInfo _catalog = (ICatalogInfo) null;
  private static string _key;
  private static string _cadmechPath;
  private static IImbaseServer _server = (IImbaseServer) null;
  internal const int CADMECH = 32768 /*0x8000*/;
  internal const int CADMECH_T = 16384 /*0x4000*/;
  internal const int AVS = 8192 /*0x2000*/;
  internal const int SEARCH = 4096 /*0x1000*/;
  internal const char MINUS_CHAR = '\u007F';
  private const int mofExcludeParamkey = 1;
  private const int mofListMode = 2;
  private const int mofExcludeLongNames = 4;
  private const int mofNoShortNamesChange = 8;
  private const int mofAddCatalogRecord = 16 /*0x10*/;
  private const string ERROR_STR = "****";
  private static int _pmMode = 0;
  private static string _dia;
  private static string _baseErrorString = string.Empty;

  internal static IImbaseServer GetServer(IUserSession session)
  {
    return session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
  }

  internal static int Execute(string command, string[] fileData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      CadmechHelper._server = CadmechHelper.GetServer(session);
      CadmechHelper._options.Clear();
      CadmechHelper._options.sleepOut = false;
      CadmechHelper._options.tableMode = false;
      CadmechHelper._options.progMode = MaterialMode.DIALOG;
      CadmechHelper._options.CMTmode = 0;
      CadmechHelper._options.fieldName = LocalizationHolder.rm.GetString("Imbase.Client_1");
      CadmechHelper.parseCommandString(command);
      CadmechHelper._dataList.Clear();
      CadmechHelper._outList.Clear();
      CadmechHelper._resultList.Clear();
      if (CadmechHelper._options.progMode != MaterialMode.RETURN)
      {
        CadmechHelper._catalog = CadmechHelper.LoadCatalog(CadmechHelper._options.catalogName, session);
        if (CadmechHelper._catalog == null)
        {
          CadmechHelper.MakeError();
          return -1;
        }
        switch (CadmechHelper._options.progMode)
        {
          case MaterialMode.DIALOG:
            CadmechHelper._resultList.Add("****");
            CadmechHelper.Dialog();
            break;
          case MaterialMode.CMTMODE:
            if (CadmechHelper._options.tableMode)
            {
              if (CadmechHelper._options.tableName[0] == '@')
              {
                CadmechHelper._options.fieldName = CadmechHelper._options.tableName.Substring(1).ToUpper();
                CadmechHelper._options.CMTmode = 2;
                goto case MaterialMode.DIALOG;
              }
              CadmechHelper._dataList.Add(CadmechHelper._options.tableName);
              CadmechHelper.CMTEmulate(session, CadmechHelper._dataList, CadmechHelper._outList, 1);
            }
            else
            {
              CadmechHelper._dataList.Clear();
              CadmechHelper._dataList.AddRange((IEnumerable<string>) fileData);
              if ((CadmechHelper._options.Flags & 2) != 0)
                CadmechHelper.CMTEmulateEx(session, CadmechHelper._dataList, CadmechHelper._outList, false);
              else
                CadmechHelper.CMTEmulate(session, CadmechHelper._dataList, CadmechHelper._outList, 0);
            }
            CadmechHelper._resultList.Clear();
            CadmechHelper._resultList.AddRange((IEnumerable<string>) CadmechHelper._outList);
            break;
          case MaterialMode.GETINFO:
            if (CadmechHelper._key.Length > 0)
            {
              CadmechHelper.ShowKeysInfo(CadmechHelper._key);
              break;
            }
            break;
          case MaterialMode.MANUAL:
            if (CadmechHelper._options.fieldName == LocalizationHolder.rm.GetString("Imbase.Client_2"))
            {
              CadmechHelper._options.fieldName = string.Empty;
              goto case MaterialMode.DIALOG;
            }
            goto case MaterialMode.DIALOG;
          case MaterialMode.SHOWTMODE:
            CadmechHelper.DoShowtMode((object) CadmechHelper._options.tableName);
            break;
          case MaterialMode.CMTINFO:
            CadmechHelper.CmtInfo(CadmechHelper._options.fieldName, session);
            break;
          case MaterialMode.SELECTTABLE:
            CadmechHelper.DoSelectTable();
            break;
        }
      }
    }
    CadmechHelper._server = (IImbaseServer) null;
    return 1;
  }

  internal static int CreateObject(long recordId, long linkId, ref string objectGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long objectID = (session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).CreateObject(session.SessionGUID, -1L, linkId, recordId, true, -1);
      IDBObject dbObject = session.GetObject(objectID);
      objectGuid = dbObject.GUID.ToString();
      return 1;
    }
  }

  internal static int CreateObjectFromTempKey(string tempKey, ref string objectGuid)
  {
    objectGuid = string.Empty;
    if (tempKey == null || tempKey.Length < 2)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_6"));
    if (char.ToUpper(tempKey[0]) == 'I')
    {
      if (char.ToUpper(tempKey[1]) == 'K')
      {
        int num1 = tempKey.IndexOf('.');
        if (num1 != -1)
        {
          string s1 = tempKey.Substring(2, num1 - 2);
          string s2 = tempKey.Substring(num1 + 1);
          long num2 = long.Parse(s1);
          long recordId = long.Parse(s2);
          string empty = string.Empty;
          long linkId = num2;
          ref string local = ref empty;
          int objectFromTempKey = CadmechHelper.CreateObject(recordId, linkId, ref local);
          if (objectFromTempKey != 1)
            return objectFromTempKey;
          objectGuid = "IG" + empty;
          return objectFromTempKey;
        }
      }
      else if (char.ToUpper(tempKey[1]) == 'G' && tempKey.Length == 38)
      {
        objectGuid = tempKey;
        return 1;
      }
    }
    throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_7"));
  }

  internal static int GetKeyInfo(
    string imbaseKey,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    int keyInfo1 = 1;
    tableRecord = string.Empty;
    catalogRecord = string.Empty;
    keysList = string.Empty;
    string str1 = "Размеры и параметры=";
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0038c-306c-11d8-b4e9-00304f19f545");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      CadmechHelper._server = CadmechHelper.GetServer(session);
      long linkId;
      long recordId;
      if (CadmechHelper.IsImbaseKey(imbaseKey, out linkId, out recordId, CadmechHelper._server, session))
      {
        AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
        DataTable recordsTable = (DataTable) null;
        string filter = $"[-2]={recordId.ToString()}";
        ImbaseKeyInfo keyInfo2 = new ImbaseKeyInfo(-1L);
        CadmechHelper._server.LoadRecords(session.SessionGUID, linkId, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo2);
        StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
        if (recordsTable.Rows.Count > 0)
        {
          List<string> list = CadmechHelper.CreateList(recordsTable.Rows[0], columnsAttributes, false, false);
          int count = list.Count;
          bool flag = true;
          for (int index = 0; index < count; ++index)
          {
            string str2 = list[index];
            if (flag && str2.StartsWith(str1, StringComparison.InvariantCultureIgnoreCase))
              flag = false;
            stringBuilder.AppendLine(list[index]);
          }
          if (flag)
            stringBuilder.AppendLine(str1);
        }
        tableRecord = stringBuilder.ToString();
        stringBuilder.Length = 0;
        AttributeValues[] attributesValues = session.GetObject(linkId).GetAttributesValues(GetAttributeValuesModes.IncludeName);
        int length = attributesValues.Length;
        for (int index = 0; index < length; ++index)
        {
          AttributeValues attributeValues = attributesValues[index];
          if (attributeValues.AttributeID == attributeTypeId)
          {
            object obj = attributeValues.Values[0];
            if (!DBNull.Value.Equals(obj))
            {
              long int64 = Convert.ToInt64(attributeValues.Values[0]);
              IDBObject dbObject = session.GetObject(int64, false);
              if (dbObject != null)
                stringBuilder.AppendLine($"{attributeValues.AttributeName}=IG{dbObject.GUID.ToString()}");
            }
          }
          else
            stringBuilder.AppendLine($"{attributeValues.AttributeName}={attributeValues.Values[0].ToString()}");
        }
        catalogRecord = stringBuilder.ToString();
        stringBuilder.Length = 0;
        stringBuilder.AppendLine("CtlId=" + keyInfo2.CatalogId.ToString());
        stringBuilder.AppendLine("TblId=" + keyInfo2.TableId.ToString());
        stringBuilder.AppendLine("CtlKey=" + linkId.ToString());
        stringBuilder.AppendLine("TblKey=" + recordId.ToString());
        stringBuilder.AppendLine("TblName=" + keyInfo2.TableName);
        stringBuilder.AppendLine("CtlName=" + keyInfo2.CatalogName);
        stringBuilder.AppendLine("ImKey=" + imbaseKey);
        if (!string.IsNullOrEmpty(imbaseKey) && imbaseKey.Length == 38 && char.ToUpper(imbaseKey[0]) == 'I' && char.ToUpper(imbaseKey[1]) == 'G')
        {
          string g = imbaseKey.Substring(2);
          IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(new Guid(g), "cad005aa-306c-11d8-b4e9-00304f19f545", true);
          stringBuilder.AppendLine("ImVersionKey=" + CadmechHelper.GetImbaseVersionKeyFromObject(objectByVersionsRule));
          string caption = objectByVersionsRule.Caption;
        }
        keysList = stringBuilder.ToString();
        keyInfo1 = 0;
      }
      else if (!string.IsNullOrEmpty(imbaseKey))
      {
        if (imbaseKey.Length == 38)
        {
          if (char.ToUpper(imbaseKey[0]) == 'I')
          {
            if (char.ToUpper(imbaseKey[1]) != 'G')
            {
              if (char.ToUpper(imbaseKey[1]) != 'V')
                goto label_38;
            }
            string g = imbaseKey.Substring(2);
            IDBObject dbObject1 = char.ToUpper(imbaseKey[1]) != 'V' ? session.GetObjectByVersionsRule(new Guid(g), "cad005aa-306c-11d8-b4e9-00304f19f545", true) : session.GetObject(new Guid(g), true);
            AttributeValues[] attributesValues = dbObject1.GetAttributesValues(GetAttributeValuesModes.IncludeName);
            int length = attributesValues.Length;
            StringBuilder stringBuilder1 = new StringBuilder();
            Guid guid;
            for (int index = 0; index < length; ++index)
            {
              AttributeValues attributeValues = attributesValues[index];
              if (attributeValues.AttributeID == attributeTypeId)
              {
                object obj = attributeValues.Values[0];
                if (!DBNull.Value.Equals(obj))
                {
                  long int64 = Convert.ToInt64(attributeValues.Values[0]);
                  IDBObject dbObject2 = session.GetObject(int64, false);
                  if (dbObject2 != null)
                  {
                    StringBuilder stringBuilder2 = stringBuilder1;
                    string attributeName = attributeValues.AttributeName;
                    guid = dbObject2.GUID;
                    string str3 = guid.ToString();
                    string str4 = $"{attributeName}=IG{str3}";
                    stringBuilder2.AppendLine(str4);
                  }
                }
              }
              else
                stringBuilder1.AppendLine($"{attributeValues.AttributeName}={attributeValues.Values[0].ToString()}");
            }
            stringBuilder1.AppendLine("F_CAPTION=" + dbObject1.Caption);
            stringBuilder1.AppendLine(str1);
            catalogRecord = stringBuilder1.ToString();
            ref string local = ref keysList;
            guid = dbObject1.GUID;
            string str5 = "ImKey=IG" + guid.ToString();
            local = str5;
            keysList = $"{keysList}{Environment.NewLine}ImVersionKey={CadmechHelper.GetImbaseVersionKeyFromObject(dbObject1)}";
            keyInfo1 = 0;
          }
        }
      }
    }
label_38:
    CadmechHelper._server = (IImbaseServer) null;
    return keyInfo1;
  }

  internal static List<string> ResultList => CadmechHelper._resultList;

  private static void CmtInfo(string key, IUserSession session)
  {
    long linkId = -1;
    long recordId = -1;
    CadmechHelper._resultList.Clear();
    if (!CadmechHelper.IsImbaseKey(key, out linkId, out recordId, CadmechHelper._server, session))
      return;
    AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
    DataTable recordsTable = (DataTable) null;
    string filter = $"[-2]={recordId.ToString()}";
    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
    CadmechHelper._server.LoadRecords(session.SessionGUID, linkId, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
    if ((CadmechHelper._options.Flags & 2) != 0)
    {
      List<string> list = CadmechHelper.CreateList(recordsTable.Rows[0], columnsAttributes, true, true);
      CadmechHelper._resultList.Add(CadmechHelper.CreateDoubleList(list, key));
      int num = CadmechHelper._options.Flags & 16 /*0x10*/;
    }
    else
    {
      int length = columnsAttributes.Length;
      DataRow row = recordsTable.Rows[0];
      StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
      for (int index = 0; index < length; ++index)
      {
        DataColumn column = row.Table.Columns[columnsAttributes[index].AttributeID.ToString()];
        if (column != null && (columnsAttributes[index].Options & (AttributeOptions) CadmechHelper._options.tableOut) != AttributeOptions.None)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append('#');
          stringBuilder.Append(row[column].ToString());
        }
      }
      stringBuilder.Append('#');
      stringBuilder.Append(key);
      stringBuilder.Append('!');
      CadmechHelper._resultList.Add(stringBuilder.ToString());
    }
  }

  internal static List<string> CreateList(
    DataRow dataRow,
    AttributeTypeProperties[] atts,
    bool shortNames,
    bool quotes)
  {
    int length = atts.Length;
    List<string> list = new List<string>(length);
    for (int index = 0; index < length; ++index)
    {
      DataColumn column = dataRow.Table.Columns[atts[index].AttributeID.ToString()];
      string name;
      string str;
      if (column != null && CadmechHelper.GetNameValue(atts[index], dataRow[column], out name, out str, shortNames, quotes))
        list.Add($"{name}={str}");
    }
    return list;
  }

  private static bool GetNameValue(
    AttributeTypeProperties attType,
    object attValue,
    out string name,
    out string value,
    bool shortName,
    bool quotes)
  {
    name = attType.Name;
    if (shortName && attType.ShortName.Length > 0)
      name = attType.ShortName;
    bool flag = true;
    switch (attType.FieldType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftDouble:
      case FieldTypes.ftMeasured:
      case FieldTypes.ftAutoInc:
        flag = false;
        break;
    }
    if (TableLoadHelper.IsNull(attValue))
    {
      value = !flag ? "0" : (!quotes ? string.Empty : "\"\"");
      return true;
    }
    value = attValue.ToString();
    if (attType.FieldType == FieldTypes.ftMeasured && attValue is MeasuredValue mValue)
    {
      if (MeasureHelper.FindDescriptor(mValue.MeasureID).PhysicalQuantityID == Intermech.Imbase.Consts.MeasureLengthID)
        mValue = MeasureHelper.ConvertToMeasuredValue(mValue, Intermech.Imbase.Consts.mmUnitID);
      value = mValue.MeasureID != 0L ? mValue.Value.ToString() : mValue.Caption;
    }
    if (flag)
    {
      if (quotes)
        value = $"\"{value}\"";
    }
    else
      value = value.Replace(',', '.');
    return true;
  }

  private static void ShowKeysInfo(string keys)
  {
    ImbaseAPIRemImplementation._APIImplementation.ShowPropertyWindow(keys);
  }

  private static void DoSelectTable()
  {
    throw new Exception("The method or operation is not implemented.");
  }

  private static void DoShowtMode(object p)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  private static ICatalogInfo LoadCatalog(string catalogDef, IUserSession session)
  {
    CadmechHelper._catalog = CadmechHelper._server.GetCatalogInfo(session.SessionGUID, catalogDef);
    return CadmechHelper._catalog;
  }

  private static void CMTEmulateEx(
    IUserSession session,
    List<string> dataList,
    List<string> outputList,
    bool paramechMode)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    List<string> stringList3 = new List<string>();
    List<string> tableFields = new List<string>();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    Base b = (Base) null;
    List<int> showFieldList = new List<int>();
    List<int> intList = new List<int>();
    int outFieldCount = 0;
    int showFieldCount = 0;
    string empty3 = string.Empty;
    CadmechHelper._dia = string.Empty;
    long tableKey = -1;
    int num1;
    if (paramechMode)
    {
      num1 = 1;
      CadmechHelper._options.CMT_BaseName = dataList[0].ToUpper();
      stringList1.Add(CadmechHelper._options.CMT_BaseName);
      CadmechHelper._dia = string.Empty;
    }
    else
    {
      num1 = 0;
      int count = dataList.Count;
      for (int index = 0; index < count; ++index)
      {
        string data = dataList[index];
        if (data.Length > 0)
        {
          if (data[0] != '(')
          {
            if (data[0] == '@')
            {
              stringList2.Add(data.Substring(1));
              continue;
            }
          }
          else
            continue;
        }
        if (data.Length != 0)
        {
          stringList1.Add(data.ToUpper());
          ++num1;
        }
        else
          break;
      }
      if (num1 == 0)
      {
        CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfEmpty));
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      string str = stringList1[0];
      int length = str.IndexOf(' ');
      if (length != -1)
      {
        CadmechHelper._dia = str.Substring(length + 1);
        str = str.Substring(0, length);
        stringList1[0] = str;
      }
      if (str[0] == '*')
      {
        CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfBad));
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      CadmechHelper._options.CMT_BaseName = str.ToUpper();
    }
    long catalogKey = -1;
label_19:
    if (CadmechHelper.IsImbaseKey(stringList1[0]))
    {
      CadmechHelper._dia = "0";
      CadmechHelper._options.sleepOut = true;
    }
    if (!CadmechHelper._options.sleepOut && !CadmechHelper._options.dynamicMode)
    {
      CadmechHelper.GetMessage(ErrorMessage.fnBase);
      if (stringList1[0].IndexOf('.') == -1)
        CadmechHelper.GetMessage(ErrorMessage.fnStd);
      stringList1[0] = CadmechHelper.CheckCatalogName(stringList1[0], session);
      if ((tableKey = CadmechHelper.CheckIfImbaseKey(stringList1[0], session, tableFields, ref catalogKey, ref empty1, true)) == -1L)
        tableKey = CadmechHelper.ShowCMTForm(stringList1[0], CadmechHelper._dia, CadmechHelper.ResultList, tableFields, stringList2, ref catalogKey, ref empty1, ref b);
      if (tableKey == -1L)
      {
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      if (paramechMode)
      {
        CadmechHelper.CreateParamechOutput(CadmechHelper._catalog.Id, catalogKey, tableKey, b, tableFields, outputList);
        CadmechHelper.CloseBase(ref b);
        return;
      }
      b.GetShortList(stringList2, (CadmechHelper._options.Flags & 4) != 0);
      outputList.Add(CadmechHelper.CreateDoubleList(CadmechHelper._catalog.Id, catalogKey, tableKey, stringList2));
    }
    if ((CadmechHelper._options.sleepOut || CadmechHelper._options.dynamicMode) && CadmechHelper._dia.Length == 0)
    {
      CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfBad));
      CadmechHelper.MakeError();
      outputList.Clear();
      outputList.Add("****");
    }
    else
    {
      int num2 = 1;
      if (CadmechHelper._options.dynamicMode)
        num2 = 0;
      string str1 = string.Empty;
      int length1 = CadmechHelper._dia.IndexOf('=');
      string str2;
      if (length1 != -1)
      {
        string str3 = CadmechHelper._dia.Substring(0, length1);
        CadmechHelper._dia = CadmechHelper._dia.Substring(length1 + 1);
        str2 = str3;
      }
      else
        str2 = string.Empty;
      if (CadmechHelper._options.sleepOut)
        --num2;
      if (num2 < 0)
        num2 = 0;
      for (int index = num2; index < num1; ++index)
      {
        string objectDef = CadmechHelper.CheckCatalogName(stringList1[index], session);
        int length2 = objectDef.IndexOf(' ');
        string shortName;
        if (length2 != -1)
        {
          shortName = objectDef.Substring(length2 + 1).ToUpper();
          objectDef = objectDef.Substring(0, length2);
        }
        else
          shortName = str2;
        if (objectDef[0] == '*')
        {
          outputList.Add("****");
        }
        else
        {
          if (!CadmechHelper._options.sleepOut)
          {
            int num3 = CadmechHelper._options.dynamicMode ? 1 : 0;
          }
          catalogKey = CadmechHelper.GetCatalogKey(objectDef, session, ref empty1, ref tableKey);
          if (catalogKey == -1L)
          {
            CadmechHelper.DisplayError(string.Format(CadmechHelper.GetMessage(ErrorMessage.bmNotRegistered), (object) objectDef));
            outputList.Add("****");
          }
          else
          {
            b = CadmechHelper.LoadBase(empty1, catalogKey);
            if (b == null)
            {
              CadmechHelper.DisplayError(CadmechHelper._baseErrorString);
              outputList.Add("****");
            }
            else
            {
              if (CadmechHelper.FormIOArrays(b, ref showFieldCount, ref outFieldCount, showFieldList, intList, stringList1[index], 1) == -1)
                return;
              int num4 = b.records();
              int length3 = CadmechHelper._dia.Length;
              int num5 = -1;
              int num6 = -1;
              if (showFieldCount > 0 || shortName.Length > 0)
              {
                if (shortName.Length > 0)
                  num6 = b.GetFieldByShortName(shortName);
                if (showFieldCount > 0 && num6 == -1)
                  num6 = showFieldList[0];
                int num7;
                RecordItem recordItem;
                if (tableKey != -1L)
                {
                  for (num7 = 0; num7 < num4; ++num7)
                  {
                    recordItem = b.Record(num7, num6);
                    num5 = num7;
                    if (b.CurrentKey == tableKey)
                    {
                      if (index == 0)
                      {
                        CadmechHelper._dia = recordItem.cptr;
                        break;
                      }
                      break;
                    }
                  }
                  b.GetShortList(stringList2, (CadmechHelper._options.Flags & 4) != 0);
                  tableKey = b.CurrentKey;
                  outputList.Add(CadmechHelper.CreateDoubleList(CadmechHelper._catalog.Id, catalogKey, tableKey, stringList2));
                  if (index == 0 && CadmechHelper._options.dynamicMode)
                    str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, num7, outFieldCount, intList, -num6);
                }
                else
                {
                  b.SortBy(num6);
                  for (num7 = 0; num7 < num4; ++num7)
                  {
                    recordItem = b.Record(num7, num6);
                    if (recordItem.len > 0 && recordItem.len == length3 && CadmechHelper._dia.Equals(recordItem.cptr))
                    {
                      num5 = num7;
                      b.GetShortList(stringList2, (CadmechHelper._options.Flags & 4) != 0);
                      tableKey = b.CurrentKey;
                      outputList.Add(CadmechHelper.CreateDoubleList(CadmechHelper._catalog.Id, catalogKey, tableKey, stringList2));
                      if (index == 0 && CadmechHelper._options.dynamicMode)
                      {
                        str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, num7, outFieldCount, intList, -num6);
                        break;
                      }
                      break;
                    }
                  }
                }
                if (num5 == -1)
                {
                  if (CadmechHelper._options.dynamicMode && index == 0)
                  {
                    CadmechHelper._options.dynamicMode = false;
                    CadmechHelper._options.FastDrag = 0;
                    goto label_19;
                  }
                  if (CadmechHelper._options.sleepOut && index == 0)
                  {
                    double result1 = 0.0;
                    double.TryParse(CadmechHelper._dia, out result1);
                    double num8 = -10000.0;
                    double num9 = 10000.0;
                    int num10 = 0;
                    int num11 = 0;
                    int recNo = 0;
                    for (; num7 < num4; ++num7)
                    {
                      recordItem = b.Record(recNo, num6);
                      if (recordItem.len > 0)
                      {
                        double result2;
                        double.TryParse(recordItem.cptr, out result2);
                        double num12 = result2 - result1;
                        if (num12 > 0.0 && num9 > num12)
                        {
                          num9 = num12;
                          num11 = 1;
                          break;
                        }
                        if (num12 < 0.0 && num8 < num12)
                        {
                          num10 = 1;
                          num8 = num12;
                        }
                      }
                    }
                    double num13 = num8;
                    if (Math.Abs(num13) > num9)
                      num13 = num9;
                    string str4 = $"**** {result1 + num13}";
                    if (num10 != 0)
                      str4 = $"{str4} {$"{result1 + num8}"}";
                    if (num11 != 0)
                      str4 = $"{str4} {$"{result1 + num9}"}";
                    outputList.Clear();
                    string str5 = str4.Replace(',', '.');
                    outputList.Add(str5);
                    return;
                  }
                  outputList.Add("****");
                }
              }
              else
              {
                outputList.Add(CadmechHelper.CreateOutputRecord(catalogKey, b, 0, 0, intList));
                if (index == 0 && CadmechHelper._options.dynamicMode)
                  str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, 0, 0, intList, num6);
              }
              CadmechHelper.CloseBase(ref b);
            }
          }
        }
      }
      if (!CadmechHelper._options.dynamicMode || str1.Length <= 0)
        return;
      outputList.Add(str1);
      string empty4 = string.Empty;
    }
  }

  private static string CreateDoubleList(List<string> shortFields, string ImbaseKey)
  {
    string str1 = "((";
    string str2 = string.Empty;
    string str3 = " ";
    string str4 = "0";
    int count = shortFields.Count;
    for (int index = 0; index < count; ++index)
    {
      string shortField = shortFields[index];
      string str5 = $"\"{CadmechHelper.ExtractName(shortField)}\" ";
      string str6 = CadmechHelper.UpdateQuotes(CadmechHelper.ExtractValue(shortField));
      if (str6.Length == 0)
        str6 = str4;
      str1 += str5;
      str2 = str2 + str6 + str3;
    }
    if ((CadmechHelper._options.Flags & 1) == 0)
    {
      str1 += "\"IMKEY\"";
      str2 = $"{str2}\"{ImbaseKey}\"";
    }
    return $"{str1})({str2}))";
  }

  private static string UpdateQuotes(string value)
  {
    char[] charArray = value.ToCharArray();
    int length = charArray.Length;
    for (int index = 1; index < length - 1; ++index)
    {
      if (charArray[index] == '"' && charArray[index + 1] == '"')
        charArray[index] = '\\';
    }
    return new string(charArray);
  }

  private static string ExtractName(string s)
  {
    string name = s;
    int length = s.IndexOf('=');
    if (length != -1)
      name = name.Substring(0, length);
    return name;
  }

  private static string ExtractValue(string s)
  {
    string str = s;
    int num = s.IndexOf('=');
    if (num != -1)
      str = str.Substring(num + 1);
    return str;
  }

  private static string CreateDoubleList(
    long catalogId,
    long catalogKey,
    long tableKey,
    List<string> shortFields)
  {
    string ImbaseKey = ImbaseHelper.MakeInternalImbaseKey(catalogKey, tableKey);
    return CadmechHelper.CreateDoubleList(shortFields, ImbaseKey);
  }

  internal static long CheckIfImbaseKey(
    string imbaseKey,
    IUserSession session,
    List<string> tableFields,
    ref long catalogKey,
    ref string tableName,
    bool loadCatalog)
  {
    long recordId = -1;
    long linkId = -1;
    if (!CadmechHelper.IsImbaseKey(imbaseKey, out linkId, out recordId, CadmechHelper._server, session))
      return -1;
    AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
    DataTable recordsTable = (DataTable) null;
    string filter = $"[-2]={recordId.ToString()}";
    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
    CadmechHelper._server.LoadRecords(session.SessionGUID, linkId, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
    List<string> list = CadmechHelper.CreateList(recordsTable.Rows[0], columnsAttributes, true, true);
    tableFields.Clear();
    tableFields.AddRange((IEnumerable<string>) list);
    return recordId;
  }

  private static void CMTEmulate(
    IUserSession session,
    List<string> dataList,
    List<string> outputList,
    int paramechMode)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    string empty1 = string.Empty;
    CadmechHelper._pmMode = paramechMode;
    Base b = (Base) null;
    List<int> showFieldList = new List<int>();
    List<int> intList = new List<int>();
    int outFieldCount = 0;
    int showFieldCount = 0;
    long tableKey = -1;
    CadmechHelper._dia = string.Empty;
    int num1;
    if (paramechMode == 1)
    {
      num1 = 1;
      CadmechHelper._options.CMT_BaseName = dataList[0].ToUpper();
      stringList1.Add(CadmechHelper._options.CMT_BaseName);
      CadmechHelper._dia = string.Empty;
    }
    else
    {
      num1 = 0;
      int count = dataList.Count;
      for (int index = 0; index < count; ++index)
      {
        string data = dataList[index];
        if (data.Length > 0)
        {
          if (data[0] != '(')
          {
            if (data[0] == '@')
            {
              stringList2.Add(data.Substring(1));
              continue;
            }
          }
          else
            continue;
        }
        if (data.Length != 0)
        {
          stringList1.Add(data.ToUpper());
          ++num1;
        }
        else
          break;
      }
      if (num1 == 0)
      {
        CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfEmpty));
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      string str = stringList1[0];
      int length = str.IndexOf(' ');
      if (length != -1)
      {
        CadmechHelper._dia = str.Substring(length + 1);
        str = str.Substring(0, length);
        stringList1[0] = str;
      }
      if (str[0] == '*')
      {
        CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfBad));
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      CadmechHelper._options.CMT_BaseName = str.ToUpper();
    }
    long catalogKey = -1;
    List<string> stringList3 = new List<string>();
    List<string> stringList4 = new List<string>();
label_19:
    if (CadmechHelper.IsImbaseKey(stringList1[0]))
    {
      CadmechHelper._dia = "0";
      CadmechHelper._options.sleepOut = true;
    }
    if (!CadmechHelper._options.sleepOut && !CadmechHelper._options.dynamicMode)
    {
      CadmechHelper.GetMessage(ErrorMessage.fnBase);
      if (stringList1[0].IndexOf('.') == -1)
        CadmechHelper.GetMessage(ErrorMessage.fnStd);
      stringList1[0] = CadmechHelper.CheckCatalogName(stringList1[0], session);
      tableKey = CadmechHelper.ShowCMTForm(stringList1[0], CadmechHelper._dia, CadmechHelper.ResultList, stringList4, stringList2, ref catalogKey, ref empty1, ref b);
      if (tableKey == -1L)
      {
        CadmechHelper.MakeError();
        outputList.Clear();
        outputList.Add("****");
        return;
      }
      if (paramechMode == 1)
      {
        CadmechHelper.CreateParamechOutput(CadmechHelper._catalog.Id, catalogKey, tableKey, b, stringList4, outputList);
        CadmechHelper.CloseBase(ref b);
        return;
      }
      outputList.Add(CadmechHelper.CreateOutputRecord(CadmechHelper._catalog.Id, catalogKey, tableKey, CadmechHelper.ResultList, stringList4));
    }
    if ((CadmechHelper._options.sleepOut || CadmechHelper._options.dynamicMode) && CadmechHelper._dia.Length == 0)
    {
      CadmechHelper.DisplayError(CadmechHelper.GetMessage(ErrorMessage.emTransfBad));
      CadmechHelper.MakeError();
      outputList.Clear();
      outputList.Add("****");
    }
    else
    {
      int num2 = 1;
      if (CadmechHelper._options.dynamicMode)
        num2 = 0;
      string str1 = string.Empty;
      if (CadmechHelper._options.sleepOut)
        --num2;
      for (int index = num2; index < num1; ++index)
      {
        string objectDef = CadmechHelper.CheckCatalogName(stringList1[index], session);
        if (objectDef[0] == '*')
        {
          outputList.Add("****");
        }
        else
        {
          if (!CadmechHelper._options.sleepOut)
          {
            int num3 = CadmechHelper._options.dynamicMode ? 1 : 0;
          }
          catalogKey = CadmechHelper.GetCatalogKey(objectDef, session, ref empty1, ref tableKey);
          if (catalogKey == -1L)
          {
            CadmechHelper.DisplayError(string.Format(CadmechHelper.GetMessage(ErrorMessage.bmNotRegistered), (object) objectDef));
            outputList.Add("****");
          }
          b = CadmechHelper.LoadBase(empty1, catalogKey);
          if (b == null)
          {
            CadmechHelper.DisplayError(CadmechHelper._baseErrorString);
            outputList.Add("****");
          }
          else
          {
            if (CadmechHelper.FormIOArrays(b, ref showFieldCount, ref outFieldCount, showFieldList, intList, stringList1[index], 1) == -1)
              return;
            int num4 = b.records();
            int length = CadmechHelper._dia.Length;
            int num5 = -1;
            RecordItem recordItem;
            if (tableKey != -1L)
            {
              int num6;
              for (num6 = 0; num6 < num4; ++num6)
              {
                recordItem = b.Record(num6, showFieldList[0]);
                if (b.CurrentKey == tableKey)
                {
                  if (index == 0)
                  {
                    CadmechHelper._dia = recordItem.cptr;
                    break;
                  }
                  break;
                }
              }
              CadmechHelper.CreateTableLists(b, CadmechHelper._options.tableOut, CadmechHelper.ResultList, stringList4, stringList2);
              tableKey = b.CurrentKey;
              outputList.Add(CadmechHelper.CreateOutputRecord(CadmechHelper._catalog.Id, catalogKey, tableKey, CadmechHelper.ResultList, stringList4));
              if (index == 0 && CadmechHelper._options.dynamicMode)
                str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, num6, outFieldCount, intList, showFieldList[0]);
            }
            else if (showFieldCount > 0)
            {
              int num7;
              for (num7 = 0; num7 < num4; ++num7)
              {
                recordItem = b.Record(num7, showFieldList[0]);
                if (recordItem.len > 0 && recordItem.len == length && CadmechHelper._dia.Equals(recordItem.cptr))
                {
                  num5 = num7;
                  CadmechHelper.CreateTableLists(b, CadmechHelper._options.tableOut, CadmechHelper.ResultList, stringList4, stringList2);
                  tableKey = b.CurrentKey;
                  outputList.Add(CadmechHelper.CreateOutputRecord(CadmechHelper._catalog.Id, catalogKey, tableKey, CadmechHelper.ResultList, stringList4));
                  if (index == 0 && CadmechHelper._options.dynamicMode)
                  {
                    str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, num7, outFieldCount, intList, showFieldList[0]);
                    break;
                  }
                  break;
                }
              }
              if (num5 == -1)
              {
                if (CadmechHelper._options.dynamicMode && index == 0)
                {
                  CadmechHelper._options.dynamicMode = false;
                  CadmechHelper._options.FastDrag = 0;
                  goto label_19;
                }
                if (CadmechHelper._options.sleepOut && index == 0)
                {
                  double result1 = 0.0;
                  double.TryParse(CadmechHelper._dia, out result1);
                  double num8 = -10000.0;
                  double num9 = 10000.0;
                  int num10 = 0;
                  int num11 = 0;
                  int recNo = 0;
                  for (; num7 < num4; ++num7)
                  {
                    recordItem = b.Record(recNo, showFieldList[0]);
                    if (recordItem.len > 0)
                    {
                      string cptr = recordItem.cptr;
                      double result2;
                      double.TryParse(recordItem.cptr, out result2);
                      double num12 = result2 - result1;
                      if (num12 > 0.0 && num9 > num12)
                      {
                        num9 = num12;
                        num11 = 1;
                        break;
                      }
                      if (num12 < 0.0 && num8 < num12)
                      {
                        num10 = 1;
                        num8 = num12;
                      }
                    }
                  }
                  double num13 = num8;
                  if (Math.Abs(num13) > num9)
                    num13 = num9;
                  string str2 = $"**** {result1 + num13}";
                  if (num10 != 0)
                    str2 = $"{str2} {$"{result1 + num8}"}";
                  if (num11 != 0)
                    str2 = $"{str2} {$"{result1 + num9}"}";
                  outputList.Clear();
                  string str3 = str2.Replace(',', '.');
                  outputList.Add(str3);
                  return;
                }
                outputList.Add("****");
              }
            }
            else
            {
              outputList.Add(CadmechHelper.CreateOutputRecord(catalogKey, b, 0, 0, intList));
              if (index == 0 && CadmechHelper._options.dynamicMode)
                str1 = CadmechHelper.MakeDynamicOutput(catalogKey, b, 0, 0, intList, showFieldList[0]);
            }
            CadmechHelper.CloseBase(ref b);
          }
        }
      }
      if (!CadmechHelper._options.dynamicMode || str1.Length <= 0)
        return;
      outputList.Add(str1);
      string empty2 = string.Empty;
    }
  }

  private static string MakeDynamicOutput(
    long CatalogKey,
    Base b,
    int baseRec,
    int outCnt,
    List<int> outFields,
    int diaFieldNo)
  {
    StringBuilder stringBuilder = new StringBuilder(2048 /*0x0800*/);
    int num1 = b.records();
    string str1 = string.Empty;
    string str2 = string.Empty;
    List<string> stringList = new List<string>();
    int num2 = 3;
    int num3 = 1;
    if (CadmechHelper._options.DynamicFields.Length > 0)
    {
      stringList.AddRange((IEnumerable<string>) CadmechHelper._options.DynamicFields.Split(','));
      num2 = stringList.Count;
      outFields.Clear();
      for (int index = 0; index < num2; ++index)
        outFields.Add(b.GetFieldByShortName(stringList[index]));
      num3 = 0;
    }
    if (diaFieldNo < 0)
      diaFieldNo = -diaFieldNo;
    else if (num3 == 1)
      diaFieldNo = outFields[0];
    long currentKey = b.CurrentKey;
    if (!outFields.Contains(diaFieldNo))
      outFields.Insert(0, diaFieldNo);
    b.SortBy(outFields.ToArray());
    b.CurrentKey = currentKey;
    stringBuilder.Append('(');
    do
    {
      b.SetRow(baseRec);
      string str3 = ImbaseHelper.MakeInternalImbaseKey(CatalogKey, b.CurrentKey);
      RecordItem recordItem = b.Record(baseRec, diaFieldNo);
      if (str1.Length > 0)
      {
        if (str1 != recordItem.cptr)
          break;
      }
      else
        str1 = recordItem.cptr;
      stringBuilder.Append('(');
      for (int index = num3; index < num2; ++index)
      {
        int num4 = outFields[index];
        if (stringList.Count > 0)
          num4 = b.GetFieldByShortName(stringList[index - num3]);
        if (num4 != -1)
        {
          b.GetFieldInfo(num4);
          recordItem = b.Record(baseRec, num4);
        }
        if (recordItem.len == 0)
        {
          stringBuilder.Append(str2);
        }
        else
        {
          string str4 = recordItem.cptr;
          if (b.GetFieldInfo(num4).FieldType == FieldType.Float)
            str4 = str4.Replace(",", ".");
          stringBuilder.Append(str4);
        }
        if (recordItem.len > 0)
          str2 = recordItem.cptr;
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('"');
      stringBuilder.Append(str3);
      stringBuilder.Append('"');
      stringBuilder.Append(')');
      ++baseRec;
    }
    while (baseRec < num1);
    stringBuilder.Append(')');
    return stringBuilder.ToString();
  }

  private static void CreateTableLists(
    Base b,
    int mask,
    List<string> resultList,
    List<string> tableFields,
    List<string> shortFields)
  {
    if (tableFields != null)
      b.GetShortList(tableFields, false);
    if (shortFields != null)
      b.GetShortList(shortFields, true);
    if (resultList == null)
      return;
    resultList.Clear();
    for (int index = 0; index < b.FieldsFount; ++index)
    {
      FieldInfo fieldInfo = b.GetFieldInfo(index);
      if ((fieldInfo.Flags & CadmechHelper._options.tableOut) != 0)
      {
        string str1 = fieldInfo.LongName;
        if (str1.Length == 0)
          str1 = fieldInfo.ShortName;
        string str2 = str1.Replace(' ', '_');
        string str3 = b.ValueById(fieldInfo.AttributeId);
        if (str3.Length == 0)
          str3 = "?";
        if (fieldInfo.FieldType == FieldType.Float)
          str3 = str3.Replace(',', '.');
        resultList.Add($"{str2}={str3}");
      }
    }
  }

  private static int FormIOArrays(
    Base b,
    ref int showFieldCount,
    ref int outFieldCount,
    List<int> showFieldList,
    List<int> outFieldList,
    string name,
    int mode)
  {
    int fieldsFount = b.FieldsFount;
    for (int index = 0; index < fieldsFount; ++index)
    {
      FieldInfo fieldInfo = b.GetFieldInfo(index);
      if ((fieldInfo.Flags & CadmechHelper._options.tableShow) != 0)
      {
        ++showFieldCount;
        showFieldList.Add(index);
      }
      if ((fieldInfo.Flags & CadmechHelper._options.tableOut) != 0)
      {
        ++outFieldCount;
        outFieldList.Add(index);
      }
    }
    if (showFieldCount + outFieldCount != 0)
      return 1;
    CadmechHelper.DisplayError(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_3"), (object) name));
    if (mode <= 0)
      return 0;
    CadmechHelper.MakeError();
    return -1;
  }

  private static bool IsImbaseKey(string value)
  {
    if (value != null && value.Length > 0)
    {
      if (value.Length == 20 && char.ToUpper(value[0]) == 'I' && value[1] == '6')
        return true;
      if (char.ToUpper(value[0]) == 'I')
      {
        if (char.ToUpper(value[1]) == 'K')
          return value.IndexOf('.') != -1;
        if (char.ToUpper(value[1]) == 'G')
          return true;
      }
    }
    return false;
  }

  internal static bool IsImbaseKey(
    string value,
    out long linkId,
    out long recordId,
    IImbaseServer server,
    IUserSession session)
  {
    linkId = -1L;
    recordId = -1L;
    if (value != null && char.ToUpper(value[0]) == 'I')
    {
      if (char.ToUpper(value[1]) == 'K')
        return ImbaseHelper.TryParseRecordReference(session, value, out linkId, out recordId);
      if (char.ToUpper(value[1]) == 'G')
      {
        string g = value.Substring(2);
        return server.GetPrototypeDetails(session.SessionGUID, new Guid(g), ref linkId, ref recordId);
      }
      if (char.ToUpper(value[1]) == 'V')
      {
        string g = value.Substring(2);
        return server.GetPrototypeDetailsByVersion(session.SessionGUID, new Guid(g), ref linkId, ref recordId);
      }
    }
    return false;
  }

  private static long GetCatalogKey(
    string objectDef,
    IUserSession session,
    ref string tableName,
    ref long tableKey)
  {
    long linkId;
    return CadmechHelper.IsImbaseKey(objectDef, out linkId, out tableKey, CadmechHelper._server, session) ? linkId : TableFolders.Select(objectDef, "#" + CadmechHelper._catalog.Id.ToString(), ref tableName, ref tableKey);
  }

  private static string CreateOutputRecord(
    long CatalogId,
    long CatalogKey,
    long TableKey,
    List<string> OutData,
    List<string> TableData)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  private static string CreateOutputRecord(
    long CatalogKey,
    Base b,
    int recNo,
    int OutFieldCount,
    List<int> OutFieldList)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  private static void CloseBase(ref Base b)
  {
    if (b == null)
      return;
    b.Close();
    b = (Base) null;
  }

  private static void CreateParamechOutput(
    long catalogId,
    long catalogKey,
    long tableKey,
    Base b,
    List<string> tableFields,
    List<string> outputList)
  {
    outputList.Clear();
    if ((CadmechHelper._options.Flags & 1) == 0)
    {
      string str = ImbaseHelper.MakeInternalImbaseKey(catalogKey, tableKey);
      outputList.Add(";#" + str);
    }
    b.CurrentKey = tableKey;
    for (int index = 0; index < b.FieldsFount; ++index)
    {
      FieldInfo fieldInfo = b.GetFieldInfo(index);
      if ((fieldInfo.Flags & CadmechHelper._options.tableOut) != 0)
      {
        string str1 = fieldInfo.ShortName;
        if (str1.Length == 0)
          str1 = fieldInfo.LongName;
        string str2 = str1.Replace(' ', '_');
        string str3 = b.ValueById(fieldInfo.AttributeId);
        if (str3.Length == 0)
          str3 = "?";
        outputList.Add($"{str2}={str3}");
      }
    }
  }

  private static Base LoadBase(string tableName, long catalogKey)
  {
    using (TableViewForm tableViewForm = new TableViewForm())
    {
      tableViewForm.InitializeView(catalogKey);
      DataTable records = (DataTable) null;
      FieldInfo[] fields = (FieldInfo[]) null;
      ContextInfo context = new ContextInfo();
      long recordKey = -1;
      tableViewForm.GetData(ref records, ref fields, ref context, 0, ref recordKey);
      return new Base(records, fields, context);
    }
  }

  private static long ShowCMTForm(
    string objectDef,
    string _dia,
    List<string> resultList,
    List<string> tableFields,
    List<string> comments,
    ref long catalogKey,
    ref string tableName,
    ref Base b)
  {
    string empty = string.Empty;
    long recordKey = -1;
    if (comments.Count > 0)
    {
      int count = comments.Count;
      for (int index = 0; index < count; ++index)
      {
        if (empty.Length > 0)
          empty += Environment.NewLine;
        empty += comments[index];
      }
    }
    using (TableViewForm tableViewForm = new TableViewForm())
    {
      tableViewForm.InitializeView(objectDef, "#" + CadmechHelper._catalog.Id.ToString(), string.Empty, $"#{CadmechHelper._options.tableShow}", string.Empty, empty);
      IntPtr handle = tableViewForm.Handle;
      if (tableViewForm.ShowDialog() != DialogResult.OK)
        return -1;
      DataTable records = (DataTable) null;
      FieldInfo[] fields = (FieldInfo[]) null;
      ContextInfo context = new ContextInfo();
      tableViewForm.GetData(ref records, ref fields, ref context, 1, ref recordKey);
      catalogKey = context.LinkId;
      tableName = context.TableName;
      if (b != null)
        CadmechHelper.CloseBase(ref b);
      b = new Base(records, fields, context);
    }
    return recordKey;
  }

  private static string CheckCatalogName(string data, IUserSession session)
  {
    if (CadmechHelper.IsImbaseKey(data))
      return data;
    string str1 = data;
    int length1 = str1.IndexOf('@');
    if (length1 != -1)
    {
      string str2 = str1.Substring(length1 + 1);
      CadmechHelper._catalog = CadmechHelper.LoadCatalog(str1.Substring(0, length1), session);
      return str2;
    }
    int length2 = str1.IndexOf("-C");
    if (length2 == -1)
      length2 = str1.IndexOf("-c");
    if (length2 != -1)
    {
      string str3 = str1.Substring(0, length2);
      CadmechHelper._catalog = CadmechHelper.LoadCatalog(str1.Substring(length2 + 2).Trim(), session);
      return str3;
    }
    CadmechHelper._catalog = CadmechHelper.LoadCatalog(CadmechHelper._options.catalogName, session);
    return data;
  }

  private static string GetMessage(ErrorMessage errorMessage) => errorMessage.ToString();

  private static void DisplayError(string msg)
  {
    int num = (int) MessageBox.Show(msg, "CADMECH", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private static void Dialog()
  {
    ((IInvokeService) ServicesManager.GetService(typeof (IInvokeService))).InvokeAction(-1, new Action(CadmechHelper.SelectObject));
  }

  private static void SelectObject()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long[] catalogsList = CadmechHelper._server.GetCatalogsList(session.SessionGUID);
      if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service))
        throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_4"));
      long objectID = service.SelectFromCatalog(LocalizationHolder.rm.GetString("Imbase.Client_5"), string.Empty, (object) catalogsList, false, true, (int[]) null, -1);
      if (objectID == -1L)
      {
        CadmechHelper.MakeError();
      }
      else
      {
        IDBObject dbObject = session.GetObject(objectID);
        string imbaseKeyFromObject = CadmechHelper.GetImbaseKeyFromObject(dbObject);
        if (CadmechHelper._options.CMTmode != 0)
        {
          if (CadmechHelper._options.CMTmode == 2 && (CadmechHelper._options.Flags & 2) == 0)
            return;
          long linkId = -1;
          long recordId = -1;
          CadmechHelper._resultList.Clear();
          if (!CadmechHelper.IsImbaseKey(imbaseKeyFromObject, out linkId, out recordId, CadmechHelper._server, session))
            return;
          AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
          DataTable recordsTable = (DataTable) null;
          string filter = $"[-2]={recordId.ToString()}";
          ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
          CadmechHelper._server.LoadRecords(session.SessionGUID, linkId, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
          if ((CadmechHelper._options.Flags & 2) == 0)
            return;
          List<string> list = CadmechHelper.CreateList(recordsTable.Rows[0], columnsAttributes, true, true);
          CadmechHelper._resultList.Add(CadmechHelper.CreateDoubleList(list, imbaseKeyFromObject));
        }
        else
          CadmechHelper.CallV(imbaseKeyFromObject, dbObject);
      }
    }
  }

  private static void CallV(string ImbaseKey, IDBObject selectedObject)
  {
    string[] values = new string[4]
    {
      string.Empty,
      string.Empty,
      string.Empty,
      string.Empty
    };
    IDBAttribute attributeByGuid1 = selectedObject.GetAttributeByGuid(new Guid("cad008d8-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 != null)
      values[0] = attributeByGuid1.Value.ToString();
    IDBAttribute attributeByGuid2 = selectedObject.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid2 != null)
      values[1] = attributeByGuid2.Value.ToString();
    IDBAttribute attributeByGuid3 = selectedObject.GetAttributeByGuid(new Guid("cad003de-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid3 != null)
      values[2] = attributeByGuid3.Value.ToString();
    IDBAttribute attributeByGuid4 = selectedObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid4 != null)
      values[3] = attributeByGuid4.Value.ToString();
    CadmechHelper._resultList.Clear();
    if (CadmechHelper._options.progMode == MaterialMode.MANUAL)
    {
      if (!string.IsNullOrEmpty(values[1]))
        CadmechHelper._resultList.Add($"{ImbaseKey}#{values[1]}");
      else
        CadmechHelper._resultList.Add($"{ImbaseKey}#{values[0]} {values[1]} {values[2]}");
    }
    else
      CadmechHelper.Dials(values);
  }

  private static void Dials(string[] values)
  {
    int length = values[2].IndexOf('/');
    if (length == -1)
    {
      CadmechHelper._resultList.Add($"{values[0]} {values[1]}");
      CadmechHelper._resultList.Add(values[2]);
    }
    else
    {
      CadmechHelper._resultList.Add(values[0]);
      string str1 = values[2].Substring(0, length);
      string str2 = values[2].Substring(length + 1).Trim();
      CadmechHelper._resultList.Add($"{values[1]} {str1}");
      CadmechHelper._resultList.Add(str2);
    }
  }

  private static string GetImbaseKeyFromObject(IDBObject dbObject)
  {
    return "IG" + dbObject.GUID.ToString();
  }

  private static string GetImbaseVersionKeyFromObject(IDBObject dbObject)
  {
    return "IV" + dbObject.ObjectGUID.ToString();
  }

  private static void MakeError()
  {
    CadmechHelper._resultList.Clear();
    CadmechHelper._resultList.Add("****");
  }

  private static void parseCommandString(string command)
  {
    string[] strArray = CadmechHelper.ChangeMinus(command).Split('-');
    int length = strArray.Length;
    for (int index = 0; index < length; ++index)
      CadmechHelper.ParseCommand(strArray[index].Trim(' '));
  }

  private static string ChangeMinus(string command)
  {
    char[] charArray = command.ToCharArray();
    int length = charArray.Length;
    for (int index = 1; index < length; ++index)
    {
      if (charArray[index] == '-' && charArray[index - 1] != ' ')
        charArray[index] = '\u007F';
    }
    return new string(charArray);
  }

  private static string RestoreMinus(string command)
  {
    char[] charArray = command.ToCharArray();
    int length = charArray.Length;
    for (int index = 1; index < length; ++index)
    {
      if (charArray[index] == '\u007F')
        charArray[index] = '-';
    }
    return new string(charArray);
  }

  private static void ParseCommand(string comm)
  {
    if (comm.TrimEnd().Length == 0)
      return;
    string str = CadmechHelper.RestoreMinus(comm);
    string cmdData = str.Substring(1);
    char upper = char.ToUpper(str[0]);
    char ch = ' ';
    if (str.Length > 1)
      ch = char.ToUpper(str[1]);
    switch (upper)
    {
      case 'A':
        CadmechHelper._options.CMTmode = 1;
        break;
      case 'B':
        CadmechHelper._options.basePath = cmdData.ToUpper();
        break;
      case 'C':
        CadmechHelper._options.catalogName = cmdData.ToUpper();
        break;
      case 'D':
        CadmechHelper._options.fieldName = cmdData.ToUpper();
        break;
      case 'E':
        CadmechHelper._options.progMode = MaterialMode.RETURN;
        if (ch == 'T')
        {
          CadmechHelper._options.tempFileName = cmdData.Substring(1) + "$$$tmp.$$$";
          break;
        }
        if (ch != 'C')
          break;
        CadmechHelper.SetCadmechPath(cmdData.Substring(1));
        break;
      case 'F':
        CadmechHelper.ParseFlags(cmdData);
        break;
      case 'G':
        CadmechHelper._options.progMode = MaterialMode.SELECTTABLE;
        CadmechHelper._options.fieldName = cmdData;
        break;
      case 'I':
        CadmechHelper._options.progMode = MaterialMode.GETINFO;
        CadmechHelper._key = cmdData;
        break;
      case 'L':
        CadmechHelper._options.DynamicFields = cmdData.Replace(';', ',');
        CadmechHelper._options.Flags |= 2;
        break;
      case 'M':
        CadmechHelper._options.progMode = MaterialMode.MANUAL;
        if (ch != 'W')
          break;
        CadmechHelper._options.convertToDos = false;
        break;
      case 'N':
        CadmechHelper._options.progMode = MaterialMode.MANUAL;
        CadmechHelper._options.fieldName = cmdData.ToUpper();
        break;
      case 'P':
        CadmechHelper._options.tableMode = true;
        CadmechHelper._options.progMode = MaterialMode.CMTMODE;
        CadmechHelper._options.tableName = cmdData.ToLower();
        break;
      case 'S':
        CadmechHelper._options.progMode = MaterialMode.SHOWTMODE;
        CadmechHelper._options.tableName = cmdData.ToLower();
        break;
      case 'T':
        CadmechHelper._options.progMode = MaterialMode.CMTMODE;
        if (ch == 'S')
          CadmechHelper._options.sleepOut = true;
        if (ch == 'D')
          CadmechHelper._options.dynamicMode = true;
        if (ch != 'I')
          break;
        CadmechHelper._options.progMode = MaterialMode.CMTINFO;
        CadmechHelper._options.fieldName = cmdData.Substring(1);
        break;
      case 'X':
        switch (ch)
        {
          case 'C':
            CadmechHelper._options.Flags |= 16 /*0x10*/;
            return;
          case 'I':
            CadmechHelper._options.Flags |= 1;
            return;
          case 'L':
            CadmechHelper._options.Flags |= 4;
            return;
          case 'S':
            CadmechHelper._options.Flags |= 8;
            return;
          default:
            return;
        }
    }
  }

  private static void ParseFlags(string cmdData)
  {
    int length = cmdData.Length;
    bool flag = true;
    for (int index = 0; index < length; ++index)
    {
      char ch = cmdData[index];
      switch (ch)
      {
        case ',':
          continue;
        case '.':
        case ';':
          flag = false;
          continue;
        default:
          if (flag)
          {
            CadmechHelper.SetFlagValue(ch, ref CadmechHelper._options.tableShow);
            continue;
          }
          CadmechHelper.SetFlagValue(ch, ref CadmechHelper._options.tableOut);
          continue;
      }
    }
  }

  private static void SetFlagValue(char ch, ref int value)
  {
    switch (char.ToLower(ch))
    {
      case 'a':
        value |= 8192 /*0x2000*/;
        break;
      case 'c':
        value |= 32768 /*0x8000*/;
        break;
      case 's':
        value |= 4096 /*0x1000*/;
        break;
      case 't':
        value |= 16384 /*0x4000*/;
        break;
    }
  }

  private static void SetCadmechPath(string value) => CadmechHelper._cadmechPath = value;

  private static List<Article> GetArticles(long documentId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<Article> articles = new List<Article>();
      IArticleService service1 = (IArticleService) ServicesManager.GetService(typeof (IArticleService));
      IFiltrationService service2 = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
      long documentID = documentId;
      string filtrationServiceOwnerId = service2.FiltrationServiceOwnerID;
      IUserSession session = sessionKeeper.Session;
      List<QuickObjectInfo> listInstances = service1.FindListInstances(documentID, filtrationServiceOwnerId, (object) session);
      if (listInstances != null && listInstances.Count > 0)
      {
        for (int index = 0; index < listInstances.Count; ++index)
        {
          bool baseArticle = index == 0;
          articles.Add(new Article(listInstances[index].ObjectID, baseArticle, listInstances[index].ObjectTypeID, listInstances[index].Caption));
        }
      }
      return articles;
    }
  }

  internal static int ShowTables(
    int showFlags,
    string fieldNames,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    int num1 = 1;
    long contextObjsID = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IUserSession session = sessionKeeper.Session;
        CadmechHelper._server = CadmechHelper.GetServer(session);
        if (!string.IsNullOrEmpty(keysList))
        {
          string[] strArray = keysList.Split('|')[0].Split('=');
          if (strArray.Length > 1 && string.Equals(strArray[0], "ImKey", StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(strArray[1]))
          {
            IImbaseSelector service = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, false);
            try
            {
              contextObjsID = service != null ? service.GetObjectIdByImbaseKey(strArray[1], false) : -1L;
            }
            catch
            {
            }
          }
        }
        DescriptorCollection descriptorCollection = new DescriptorCollection();
        DescriptorCollection descriptors;
        if (showFlags == 249473877)
        {
          if (!string.IsNullOrEmpty(keysList))
          {
            if (keysList.IndexOf('|') != -1)
            {
              try
              {
                string fileName = keysList.Split('|')[1];
                IFileVault service = ServicesManager.GetService<IFileVault>(false);
                if (service != null)
                {
                  FileOrigin fileOrigin = service.WorkArea.GetFileOrigin(fileName, false);
                  if (fileOrigin != null)
                  {
                    if (fileOrigin.OriginType == FileOriginType.WorkFile)
                    {
                      long objectId = fileOrigin.WorkObject.ObjectId;
                      switch (objectId)
                      {
                        case -1:
                        case 0:
                          break;
                        default:
                          IDBObject dbObject = session.GetObject(objectId, false);
                          if (dbObject != null)
                          {
                            List<Article> articles = CadmechHelper.GetArticles(dbObject.ObjectID);
                            if (articles != null)
                            {
                              if (articles.Count > 0)
                              {
                                List<long> longList = articles.ConvertAll<long>((Converter<Article, long>) (article => article.ArticleID));
                                int articleType = articles[0].ArticleType;
                                IDescriptor descriptor = (IDescriptor) new ArticlesListDescriptor(new Dictionary<int, List<long>>()
                                {
                                  {
                                    articleType,
                                    longList
                                  }
                                }, articleType);
                                descriptorCollection.Add(descriptor);
                                break;
                              }
                              break;
                            }
                            break;
                          }
                          break;
                      }
                    }
                  }
                }
              }
              catch (InvalidOperationException ex)
              {
              }
            }
          }
          descriptors = new DescriptorCollection();
          foreach (IMSObjectType applicabilityChildObjectType in MetaDataHelper.GetApplicabilityChildObjectTypes(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"), Intermech.Imbase.Consts.IncludeByLinkRelGuid))
            descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(applicabilityChildObjectType.ObjectTypeID));
        }
        else
          descriptors = GetPossibleDescriptors.PossibleTypesDescriptors;
        if (descriptors != null && descriptors.Count > 0)
          descriptorCollection.Add((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Imbase.Client_110"), descriptors));
        IIMHSelector service1 = ServicesManager.GetService(typeof (IIMHSelector)) as IIMHSelector;
        UserRowSelector.Instance.Enabled = showFlags == 249473875 || showFlags == 249473875;
        long num2;
        if (showFlags == 249473875 && service1 != null)
        {
          if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service2))
            throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_4"));
          descriptorCollection.Add(service2.GetImbaseDescriptor(attributeId: MetaDataHelper.GetAttributeTypeID("cad0038c-306c-11d8-b4e9-00304f19f545")));
          num2 = service1.SelectMaterial(LocalizationHolder.rm.GetString("Imbase.Client_5"), string.Empty, (object) descriptorCollection, -1, contextObjsID);
        }
        else
        {
          if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service3))
            throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_4"));
          long[] catalogsList = CadmechHelper._server.GetCatalogsList(session.SessionGUID);
          List<int> intList = (List<int>) null;
          if (catalogsList != null && catalogsList.Length != 0 && descriptors != null && descriptors.Count > 0)
          {
            intList = new List<int>(descriptors.Count);
            for (int index = 0; index < descriptors.Count; ++index)
            {
              INodeID recordNodeId = descriptors[index].GetRecordNodeID();
              if (recordNodeId != null && !intList.Contains(recordNodeId.TypeID))
                intList.Add(recordNodeId.TypeID);
            }
          }
          descriptorCollection.Add(service3.GetImbaseDescriptor());
          num2 = service3.SelectFromCatalog(LocalizationHolder.rm.GetString("Imbase.Client_5"), string.Empty, (object) descriptorCollection, false, true, intList?.ToArray(), -1, contextObjsID);
        }
        if (!Intermech.Consts.IsUndefinedObjectId(num2))
        {
          num1 = 0;
          IDBObject dbObject = session.GetObject(num2);
          string imbaseKeyFromObject = CadmechHelper.GetImbaseKeyFromObject(dbObject);
          CadmechHelper.GetKeyInfo(imbaseKeyFromObject, ref tableRecord, ref catalogRecord, ref keysList);
          if (string.IsNullOrEmpty(keysList))
            keysList = "ImKey=" + imbaseKeyFromObject;
          if (keysList.IndexOf("ImVersionKey") == -1)
            keysList = $"{keysList}{Environment.NewLine}ImVersionKey={CadmechHelper.GetImbaseVersionKeyFromObject(dbObject)}";
        }
        else
          keysList = string.Empty;
      }
      finally
      {
        UserRowSelector.Instance.Enabled = false;
      }
    }
    CadmechHelper._server = (IImbaseServer) null;
    return num1;
  }
}
