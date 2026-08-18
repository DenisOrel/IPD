// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertTableProcessor
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpertTableProcessor
{
  private static ExpertServer es = (ExpertServer) null;
  private static DataType curDataType = DataType.Unknown;

  static ExpertTableProcessor() => ExpertTableProcessor.es = ExpertServer.es;

  public static ExpertResult CalcTable(
    ExpertServer.ExpServTask ti,
    IUserSession session,
    long objectID,
    long tableID,
    out ResultExpertValue[] Result)
  {
    bool flag = false;
    ExpertResult expertResult = ExpertResult.OK;
    ArrayList list = new ArrayList();
    XmlNode curNode = ti.curNode;
    try
    {
      lock (ti)
      {
        if (ti.makeTrace)
        {
          if ((ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None)
          {
            XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_88"), ExpertServer.ExpertNamespace);
            XmlAttribute attribute1 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_89"), ExpertServer.ExpertNamespace);
            attribute1.Value = objectID.ToString();
            XmlAttribute attribute2 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_90"), ExpertServer.ExpertNamespace);
            attribute2.Value = tableID.ToString();
            ti.traceAddAttribute(element, "_OBJ_ID_", Convert.ToString(tableID));
            element.Attributes.Append(attribute1);
            element.Attributes.Append(attribute2);
            ti.curNode.AppendChild(element);
            ti.curNode = element;
          }
        }
      }
      if (session.GetObject(tableID) is IExpertTable expertTable)
      {
        expertTable.Load();
        eTableCollection eTableCollection = expertTable.LoadTableData();
        if (eTableCollection != null)
        {
          eTable[] tables = eTableCollection.Tables;
          eTable table = tables[0];
          switch (table.TableType)
          {
            case eTableType.NoEntry:
              ExpertTableProcessor.CalcNoEntry(ti, objectID, session, list, table);
              break;
            case eTableType.SingleEntry:
              ExpertTableProcessor.CalcSingleEntry(ti, objectID, session, list, table);
              break;
            case eTableType.DoubleEntry:
              ExpertTableProcessor.CalcDoubleEntry(ti, objectID, session, list, tables);
              break;
          }
        }
      }
      Result = list.ToArray(typeof (ResultExpertValue)) as ResultExpertValue[];
      flag = true;
    }
    catch (Exception ex)
    {
      lock (ti)
      {
        if (ti.makeTrace)
        {
          if ((ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None)
          {
            XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_91"), ExpertServer.ExpertNamespace);
            element.InnerText = ex.Message;
            ti.curNode.AppendChild(element);
          }
        }
      }
      Result = new ResultExpertValue[0];
      throw;
    }
    finally
    {
      lock (ti)
      {
        if ((ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None && flag)
        {
          XmlNode element1 = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_92"), ExpertServer.ExpertNamespace);
          foreach (ResultExpertValue resultExpertValue in list)
          {
            XmlNode element2 = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_93"), ExpertServer.ExpertNamespace);
            XmlAttribute attribute3 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_94"));
            attribute3.Value = resultExpertValue.ObjectTypeGuid.ToString();
            XmlAttribute attribute4 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_95"));
            attribute4.Value = resultExpertValue.AttributeTypeGuid.ToString();
            element2.Attributes.Append(attribute3);
            element2.Attributes.Append(attribute4);
            element2.InnerText = resultExpertValue.ToString();
            element1.AppendChild(element2);
          }
          ti.curNode.AppendChild(element1);
        }
        ti.traceSetNode(curNode);
      }
    }
    return expertResult;
  }

  public static ExpertResult CalcTable(
    ExpertServer.ExpServTask ti,
    IUserSession session,
    long objectID,
    eTableCollection tableCollection,
    long tableId,
    out ResultExpertValue[] Result)
  {
    XmlNode curNode = ti.curNode;
    bool flag = false;
    ExpertResult expertResult = ExpertResult.OK;
    ArrayList list = new ArrayList();
    try
    {
      lock (ti)
      {
        if (ti.makeTrace)
        {
          if ((ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None)
          {
            XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_96"));
            if (node != null)
            {
              ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_97"), objectID.ToString());
              ti.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(tableId));
            }
            ti.curNode = node;
          }
        }
      }
      if (tableCollection != null)
      {
        eTable[] tables = tableCollection.Tables;
        eTable table = tables[0];
        switch (table.TableType)
        {
          case eTableType.NoEntry:
            ExpertTableProcessor.CalcNoEntry(ti, objectID, session, list, table);
            break;
          case eTableType.SingleEntry:
            ExpertTableProcessor.CalcSingleEntry(ti, objectID, session, list, table);
            break;
          case eTableType.DoubleEntry:
            ExpertTableProcessor.CalcDoubleEntry(ti, objectID, session, list, tables);
            break;
        }
      }
      Result = list.ToArray(typeof (ResultExpertValue)) as ResultExpertValue[];
      flag = true;
    }
    catch (Exception ex)
    {
      lock (ti)
      {
        if ((ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None)
        {
          XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_98"), ExpertServer.ExpertNamespace);
          element.InnerText = ex.Message;
          ti.curNode.AppendChild(element);
        }
      }
      Result = new ResultExpertValue[0];
      throw;
    }
    finally
    {
      lock (ti)
      {
        if (ti.makeTrace && (ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None && flag)
        {
          XmlNode element1 = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_99"), ExpertServer.ExpertNamespace);
          foreach (ResultExpertValue resultExpertValue in list)
          {
            XmlNode element2 = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_100"), ExpertServer.ExpertNamespace);
            XmlAttribute attribute1 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_208"));
            attribute1.Value = resultExpertValue.ObjectTypeGuid != Guid.Empty ? MetaDataHelper.GetObjectType(resultExpertValue.ObjectTypeGuid).ObjectTypeName : LocalizationHolder.rm.GetString("Expert.Server_33");
            XmlAttribute attribute2 = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_209"));
            attribute2.Value = MetaDataHelper.GetAttributeTypeName(resultExpertValue.AttributeTypeGuid);
            element2.Attributes.Append(attribute1);
            element2.Attributes.Append(attribute2);
            element2.InnerText = resultExpertValue.ToString();
            element1.AppendChild(element2);
          }
          ti.curNode.AppendChild(element1);
        }
        ti.traceSetNode(curNode);
      }
    }
    return expertResult;
  }

  private static void CalcNoEntry(
    ExpertServer.ExpServTask ti,
    long objectID,
    IUserSession session,
    ArrayList list,
    eTable table)
  {
    Hashtable cache = new Hashtable();
    eRow fixedRow = table.FixedRows[0];
    IDBAttributable attributable = ExpertServer.GetAttributable(session, objectID);
    for (int index = 0; index < fixedRow.ColumnsCount; ++index)
    {
      eCell headerCell = fixedRow[index];
      ExpertTableProcessor.Trace(ti, headerCell);
      if (headerCell.CommonType != null)
      {
        CommonTypeHolder commonType = headerCell.CommonType;
        int objTypeID = -1;
        if (!commonType.ObjectType.Guid.Equals(Guid.Empty))
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(commonType.ObjectType.Guid);
          if (objectType != null)
          {
            objTypeID = objectType.ObjectTypeID;
          }
          else
          {
            IMSRelationType relationType = MetaDataHelper.GetRelationType(commonType.ObjectType.Guid);
            if (relationType != null)
              objTypeID = relationType.RelationTypeID;
          }
        }
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(commonType.AttributeType.Guid);
        AttribPair key = new AttribPair(attributeTypeId, objTypeID);
        object obj = (object) null;
        bool DisableTrace = (ti.traceFlags & ExpertTraceFlags.TraceTables) == ExpertTraceFlags.None;
        try
        {
          int orCalc = (int) ExpertTableProcessor.es.GetOrCalc(ti.taskId, objTypeID, attributeTypeId, objectID, DisableTrace, out obj);
        }
        catch (ExpertServerException ex)
        {
          if (fixedRow[index].CellDestination != eCellDestination.Result)
            throw;
        }
        if (obj != null)
          cache.Add((object) key, obj);
      }
    }
    int row1 = -1;
    int num1 = -1;
    for (int row2 = 0; row2 < table.ValuesTable.RowsCount; ++row2)
    {
      eRow row3 = table.ValuesTable.GetRow(row2);
      bool flag1 = false;
      int num2 = 0;
      for (int index = 0; index < table.ValuesTable.ColumnsCount; ++index)
      {
        eCell cellValue = row3[index];
        eCellSymbol cellSymbol = cellValue.CellSymbol;
        if (cellSymbol == eCellSymbol.None)
          cellSymbol = fixedRow[index].CellSymbol;
        object attrValue = (object) null;
        bool flag2 = ExpertTableProcessor.HasValue(cellValue, cache, session);
        if (fixedRow[index].CellDestination != eCellDestination.Result || !flag2)
        {
          if (!ExpertTableProcessor.ValueAccepted(cellSymbol, cellValue, ti, ref attrValue, cache, attributable, session, row2, index, true))
          {
            flag1 = true;
            num2 = -1;
            break;
          }
        }
        else if (fixedRow[index].CellDestination == eCellDestination.Result & flag2)
        {
          if (ExpertTableProcessor.ValueAccepted(cellSymbol, cellValue, ti, ref attrValue, cache, attributable, session, row2, index, true))
            ++num2;
          else
            flag1 = true;
        }
      }
      if (!flag1)
      {
        row1 = row2;
        break;
      }
      if (num2 > num1)
      {
        row1 = row2;
        num1 = num2;
      }
    }
    if (row1 < 0)
      return;
    eRow row4 = table.ValuesTable.GetRow(row1);
    for (int index = 0; index < fixedRow.ColumnsCount; ++index)
    {
      eCell eCell = fixedRow[index];
      if (eCell.CellDestination.Equals((object) eCellDestination.Result) && eCell.CommonType != null)
      {
        CommonTypeHolder commonType = eCell.CommonType;
        ResultExpertValue resultExpertValue = new ResultExpertValue(commonType.ObjectType.Guid, commonType.AttributeType.Guid, row4[index].CellValue);
        list.Add((object) resultExpertValue);
      }
    }
  }

  private static void CalcSingleEntry(
    ExpertServer.ExpServTask ti,
    long objectID,
    IUserSession session,
    ArrayList list,
    eTable table)
  {
    Hashtable cache = new Hashtable();
    int row1 = -1;
    IDBAttributable attributable = ExpertServer.GetAttributable(session, objectID);
    int num = -1;
    for (int index1 = 0; index1 < table.ValuesTable.RowsCount; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < table.FixedColumns.Count; ++index2)
      {
        eColumn fixedColumn = table.FixedColumns[index2];
        eCell header = fixedColumn.Header;
        ExpertTableProcessor.Trace(ti, header);
        if (header.CellDestination.Equals((object) eCellDestination.Header))
        {
          CommonTypeHolder commonType = header.CommonType;
        }
        eCell cellValue = fixedColumn[index1];
        eCellSymbol cellSymbol = cellValue.CellSymbol;
        if (cellSymbol == eCellSymbol.None)
          cellSymbol = header.CellSymbol;
        if (cellValue.isEmpty)
        {
          if (index2 > num)
          {
            flag = true;
            break;
          }
        }
        else
        {
          object attrValue = (object) null;
          if (!ExpertTableProcessor.ValueAccepted(cellSymbol, cellValue, ti, ref attrValue, cache, attributable, session, index1, index2))
          {
            flag = true;
            if (num >= index2)
            {
              num = index2 - 1;
              break;
            }
            break;
          }
          if (num < index2)
            num = index2;
        }
      }
      if (!flag)
      {
        row1 = index1;
        break;
      }
    }
    if (row1 < 0)
      return;
    eRow row2 = table.ValuesTable.GetRow(row1);
    for (int index = 0; index < row2.ColumnsCount; ++index)
    {
      eCell eCell = row2[index];
      ResultExpertValue resultExpertValue = new ResultExpertValue(eCell.CommonType.ObjectType.Guid, eCell.CommonType.AttributeType.Guid, eCell.CellValue);
      list.Add((object) resultExpertValue);
    }
  }

  private static void CalcDoubleEntry(
    ExpertServer.ExpServTask ti,
    long objectID,
    IUserSession session,
    ArrayList list,
    eTable[] tables)
  {
    eTable table1 = tables[0];
    Hashtable cache = new Hashtable();
    int row = -1;
    int column = -1;
    IDBAttributable attributable = ExpertServer.GetAttributable(session, objectID);
    int num1 = -1;
    for (int index1 = 0; index1 < table1.ValuesTable.RowsCount; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < table1.FixedColumns.Count; ++index2)
      {
        eColumn fixedColumn = table1.FixedColumns[index2];
        eCell header = fixedColumn.Header;
        ExpertTableProcessor.Trace(ti, header);
        eCell cellValue = fixedColumn[index1];
        eCellSymbol cellSymbol = cellValue.CellSymbol;
        if (cellSymbol == eCellSymbol.None)
          cellSymbol = header.CellSymbol;
        if (cellValue.isEmpty)
        {
          if (index2 > num1)
          {
            flag = true;
            break;
          }
        }
        else
        {
          object attrValue = (object) null;
          if (!ExpertTableProcessor.ValueAccepted(cellSymbol, cellValue, ti, ref attrValue, cache, attributable, session, index1, index2))
          {
            flag = true;
            if (num1 >= index2)
            {
              num1 = index2 - 1;
              break;
            }
            break;
          }
          if (num1 < index2)
            num1 = index2;
        }
      }
      if (!flag)
      {
        row = index1;
        break;
      }
    }
    int num2 = -1;
    for (int index3 = 0; index3 < table1.ValuesTable.ColumnsCount; ++index3)
    {
      bool flag = false;
      for (int index4 = 0; index4 < table1.FixedRows.Count; ++index4)
      {
        eRow fixedRow = table1.FixedRows[index4];
        eCell header = fixedRow.Header;
        if (header != null)
        {
          ExpertTableProcessor.Trace(ti, header);
          eCell cellValue = fixedRow[index3];
          eCellSymbol cellSymbol = cellValue.CellSymbol;
          if (cellSymbol == eCellSymbol.None)
            cellSymbol = header.CellSymbol;
          if (cellValue.isEmpty)
          {
            if (index4 > num2)
            {
              flag = true;
              break;
            }
          }
          else
          {
            object attrValue = (object) null;
            if (!ExpertTableProcessor.ValueAccepted(cellSymbol, cellValue, ti, ref attrValue, cache, attributable, session, index4, index3))
            {
              flag = true;
              if (num2 >= index4)
              {
                num2 = index4 - 1;
                break;
              }
              break;
            }
            if (num2 < index4)
              num2 = index4;
          }
        }
      }
      if (!flag)
      {
        column = index3;
        break;
      }
    }
    if (row < 0 || column < 0)
      return;
    foreach (eTable table2 in tables)
    {
      eCell eCell = table2.ValuesTable[row, column];
      ResultExpertValue resultExpertValue = new ResultExpertValue(eCell.CommonType.ObjectType.Guid, eCell.CommonType.AttributeType.Guid, eCell.CellValue);
      list.Add((object) resultExpertValue);
    }
  }

  private static bool ProcEmptyCell(ArrayList tmpArray, int currentIndex, eCell valueCell)
  {
    if (!valueCell.isEmpty)
      return false;
    int num = currentIndex - 1;
    if (!tmpArray.Contains((object) num))
      tmpArray.Remove((object) currentIndex);
    return true;
  }

  private static void Trace(ExpertServer.ExpServTask ti, eCell headerCell)
  {
  }

  private static void TraceValue(
    ExpertServer.ExpServTask ti,
    object attrValue,
    eCell headerCell,
    eCell valueCell,
    bool accepted)
  {
    if ((ti.traceFlags & ExpertTraceFlags.TraceTables) <= ExpertTraceFlags.None)
      return;
    lock (ti)
    {
      eCellSymbol symbol = headerCell.CellSymbol;
      if (symbol.Equals((object) eCellSymbol.None))
        symbol = eCellSymbol.Equal;
      XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_193"), ExpertServer.ExpertNamespace);
      XmlAttribute attribute = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_194"));
      attribute.Value = $"{(attrValue != null ? (object) attrValue.ToString() : (object) "?")} {eCellSymbolHelper.GetSymbol(symbol)} {valueCell.ToString()}";
      element.Attributes.Append(attribute);
      element.InnerText = !accepted ? LocalizationHolder.rm.GetString("Expert.Server_196") : LocalizationHolder.rm.GetString("Expert.Server_195");
      ti.curNode.AppendChild(element);
    }
  }

  private static bool NullValue(object Value) => Value == null || Value == DBNull.Value;

  private static string GetSymbol(eCellSymbol symbol)
  {
    switch (symbol)
    {
      case eCellSymbol.None:
        return LocalizationHolder.rm.GetString("Expert.Server_33");
      case eCellSymbol.Other:
        return "*";
      case eCellSymbol.Set:
        return "{}";
      case eCellSymbol.Equal:
        return "=";
      case eCellSymbol.NotEqual:
        return "!=";
      case eCellSymbol.More:
        return ">";
      case eCellSymbol.MoreOrEqual:
        return ">=";
      case eCellSymbol.Less:
        return "<";
      case eCellSymbol.LessOrEqual:
        return "<=";
      default:
        return "???";
    }
  }

  private static bool ValueAccepted(
    eCellSymbol headerSymbol,
    eCell cellValue,
    ExpertServer.ExpServTask ti,
    ref object attrValue,
    Hashtable cache,
    IDBAttributable parentObject,
    IUserSession session,
    int row,
    int col)
  {
    return ExpertTableProcessor.ValueAccepted(headerSymbol, cellValue, ti, ref attrValue, cache, parentObject, session, row, col, false);
  }

  private static bool HasValue(eCell cellValue, Hashtable cache, IUserSession session)
  {
    int num = -1;
    if (!cellValue.CommonType.ObjectType.Guid.Equals(Guid.Empty))
      num = ExpertServer.AttributableTypeId(ExpertServer.GetAttributableType(session, cellValue.CommonType.ObjectType.Guid));
    AttribPair key = new AttribPair(session.GetAttributeType(cellValue.CommonType.AttributeType.Guid).AttributeID, num);
    return cache.ContainsKey((object) key) && !ExpertTableProcessor.NullValue(cache[(object) key]);
  }

  private static bool ValueAccepted(
    eCellSymbol headerSymbol,
    eCell cellValue,
    ExpertServer.ExpServTask ti,
    ref object attrValue,
    Hashtable cache,
    IDBAttributable parentObject,
    IUserSession session,
    int row,
    int col,
    bool noEntry)
  {
    XmlNode node = (XmlNode) null;
    int num1 = -1;
    int num2 = -1;
    bool flag = false;
    string Name = string.Format(LocalizationHolder.rm.GetString("Expert.Server_237"), (object) (row + 1), (object) (col + 1));
    if (ti.makeTrace && (ti.traceFlags & ExpertTraceFlags.TraceTables) > ExpertTraceFlags.None)
    {
      node = ti.traceAddElement(Name);
      ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_238"), $"\"{ExpertTableProcessor.GetSymbol(headerSymbol)}\"");
      ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_239"), $"\"{cellValue.ToString()}\"");
    }
    try
    {
      eCellDestination cellDestination = cellValue.CellDestination;
      eCellSymbol eCellSymbol;
      if (cellDestination.Equals((object) eCellDestination.HeaderData))
      {
        eCellSymbol = cellValue.CellSymbol.Equals((object) eCellSymbol.None) ? headerSymbol : cellValue.CellSymbol;
      }
      else
      {
        cellDestination = cellValue.CellDestination;
        if (!cellDestination.Equals((object) eCellDestination.Data))
          return false;
        eCellSymbol = headerSymbol;
      }
      if (eCellSymbol.Equals((object) eCellSymbol.None))
        eCellSymbol = eCellSymbol.Equal;
      if (eCellSymbol.Equals((object) eCellSymbol.Other))
      {
        flag = true;
        return true;
      }
      if (!cellValue.CommonType.ObjectType.Guid.Equals(Guid.Empty))
        num1 = ExpertServer.AttributableTypeId(ExpertServer.GetAttributableType(session, cellValue.CommonType.ObjectType.Guid));
      IDBAttributeType attributeType = session.GetAttributeType(cellValue.CommonType.AttributeType.Guid);
      num2 = attributeType.AttributeID;
      ExpertTableProcessor.curDataType = DataTypeConvertor.AttrType2DataType(attributeType.AttributeType);
      AttribPair attribPair = new AttribPair(num2, num1);
      long num3 = ExpertServer.AttributableId(parentObject);
      if (ExpertTableProcessor.NullValue(attrValue))
      {
        attrValue = ExpertTableProcessor.es.GetParmValue(ti.taskId, num3, num2);
        if (ExpertTableProcessor.NullValue(attrValue) && cache.ContainsKey((object) attribPair))
          attrValue = cache[(object) attribPair];
        if (ExpertTableProcessor.NullValue(attrValue))
        {
          try
          {
            int orCalc = (int) ExpertTableProcessor.es.GetOrCalc(ti.taskId, num1, num2, num3, (ti.traceFlags & ExpertTraceFlags.TraceTables) == ExpertTraceFlags.None, out attrValue);
          }
          catch (ExpertServerException ex)
          {
            if (!noEntry)
              throw;
          }
        }
        if (ExpertTableProcessor.NullValue(attrValue))
        {
          if (noEntry)
          {
            flag = true;
            return true;
          }
          ti.NeededAttrs.AddAttr(ExpertServer.AttributableId(parentObject), num1, num2, true);
          throw new EAbort(ExpertResult.NoCalcParms);
        }
      }
      if (ExpertTableProcessor.NullValue(attrValue))
        return false;
      if (!cache.ContainsKey((object) attribPair))
        cache.Add((object) attribPair, attrValue);
      switch (eCellSymbol)
      {
        case eCellSymbol.Set:
          flag = !(cellValue.CellValue.Value is PacketValue expValue) ? ExpertTableProcessor.vEquals(attrValue, cellValue.CellValue, session, attribPair) : ExpertTableProcessor.vIn(attrValue, expValue, session, attribPair);
          return flag;
        case eCellSymbol.Equal:
          flag = ExpertTableProcessor.vEquals(attrValue, cellValue.CellValue, session, attribPair);
          return flag;
        case eCellSymbol.NotEqual:
          flag = ExpertTableProcessor.vNotEquals(attrValue, cellValue.CellValue, session, attribPair);
          return flag;
        case eCellSymbol.More:
          flag = ExpertTableProcessor.vMore(attrValue, cellValue.CellValue);
          return flag;
        case eCellSymbol.MoreOrEqual:
          flag = ExpertTableProcessor.vMoreOrEquals(attrValue, cellValue.CellValue);
          return flag;
        case eCellSymbol.Less:
          flag = ExpertTableProcessor.vLess(attrValue, cellValue.CellValue);
          return flag;
        case eCellSymbol.LessOrEqual:
          flag = ExpertTableProcessor.vLessOrEquals(attrValue, cellValue.CellValue);
          return flag;
        default:
          return false;
      }
    }
    catch (Exception ex)
    {
      if (!(ex is EAbort))
      {
        if (node == null)
          node = ti.traceAddElement(Name);
        if (node != null)
          ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_91"), ex.Message);
      }
    }
    finally
    {
      if (node != null)
      {
        string str = $"{MetaDataHelper.GetObjectTypeName(num1)}.{MetaDataHelper.GetAttributeTypeName(num2)}";
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_22"), str);
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_93"), attrValue != null ? attrValue.ToString() : LocalizationHolder.rm.GetString("Expert.Server_33"));
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_92"), flag ? LocalizationHolder.rm.GetString("Expert.Server_32") : LocalizationHolder.rm.GetString("Expert.Server_33"));
      }
    }
    return flag;
  }

  private static bool vIn(object attrValue, PacketValue expValue, IUserSession ius, AttribPair ap)
  {
    bool flag;
    if (ExpertTableProcessor.curDataType == DataType.ObjectLink)
    {
      long int64 = Convert.ToInt64(attrValue);
      IDBObject dbObject = (IDBObject) null;
      List<long> folders = new List<long>();
      for (int index = 0; index < expValue.Count; ++index)
      {
        ExpertValue expertValue = expValue[index];
        if (expertValue.ValueType == DataType.Integer || expertValue.ValueType == DataType.ObjectLink)
          folders.Add(Convert.ToInt64(expertValue.Value));
      }
      flag = folders.Contains(int64);
      if (!Convert.ToBoolean(flag))
      {
        switch (ExpertServer.Calculator.IsImbaseObject(ap))
        {
          case ImbaseCatalogSelectMode.imcmSelectFolder:
            long id = 0;
            QuickObjectInfo objectInfo = ius.GetObjectInfo(int64);
            if (!objectInfo.Empty)
              id = objectInfo.ID;
            IDBRelationCollection relationCollection = ius.GetRelationCollection(ius.IdentHelper.SortedRelationTypeID);
            if (relationCollection != null)
            {
              relationCollection.LocalTypesMode = true;
              flag = relationCollection.IsObjectInFolders(id, folders.ToArray());
              break;
            }
            break;
          case ImbaseCatalogSelectMode.imcmCreateObject:
            if (dbObject == null)
              dbObject = ius.GetObject(int64, false);
            IDBAttribute attributeById = dbObject?.GetAttributeByID(ExpertConsts.Consts.attrIMBASECode);
            if (attributeById != null && attributeById.Value != DBNull.Value)
            {
              flag = ExpertServer.Calculator.ImbaseObjectInFolders(Convert.ToInt64(attributeById.Value), folders, ius);
              break;
            }
            break;
        }
      }
    }
    else if (ap.attribID == -7)
    {
      if (ExpertTableProcessor.curDataType != DataType.Integer)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_106"));
      flag = ExpertTableProcessor.IsObjTypeInPacket(Convert.ToInt32(attrValue), expValue, ius);
    }
    else
      flag = ExpertTableProcessor.IsInPacket(attrValue, ExpertTableProcessor.curDataType, expValue);
    return flag;
  }

  private static bool vEquals(
    object attrValue,
    ExpertValue expValue,
    IUserSession ius,
    AttribPair ap)
  {
    switch (expValue.ValueType)
    {
      case DataType.Integer:
      case DataType.ObjectLink:
        bool flag = false;
        if (expValue.ValueType != DataType.Packet || ((PacketValue) expValue.Value).Count == 1 && ((PacketValue) expValue.Value)[0].ValueType == DataType.Integer)
        {
          long int64 = Convert.ToInt64(attrValue);
          long num = expValue.ValueType != DataType.Packet ? Convert.ToInt64(expValue.Value) : Convert.ToInt64(((PacketValue) expValue.Value)[0].Value);
          List<long> folders = new List<long>(1);
          folders.Add(num);
          flag = num.Equals(int64);
          if (!Convert.ToBoolean(flag))
          {
            switch (ExpertServer.Calculator.IsImbaseObject(ap))
            {
              case ImbaseCatalogSelectMode.imcmSelectFolder:
                long id = 0;
                QuickObjectInfo objectInfo = ius.GetObjectInfo(int64);
                if (!objectInfo.Empty)
                  id = objectInfo.ID;
                IDBRelationCollection relationCollection = ius.GetRelationCollection(ius.IdentHelper.SortedRelationTypeID);
                if (relationCollection != null)
                {
                  relationCollection.LocalTypesMode = true;
                  flag = relationCollection.IsObjectInFolders(id, folders.ToArray());
                  break;
                }
                flag = false;
                break;
              case ImbaseCatalogSelectMode.imcmCreateObject:
                IDBAttribute attributeById = ius.GetObject(int64, false)?.GetAttributeByID(ExpertConsts.Consts.attrIMBASECode);
                if (attributeById != null && attributeById.Value != DBNull.Value)
                {
                  flag = ExpertServer.Calculator.ImbaseObjectInFolders(Convert.ToInt64(attributeById.Value), folders, ius);
                  break;
                }
                break;
            }
          }
        }
        return flag;
      case DataType.Float:
        return Math.Abs(Convert.ToDouble(attrValue) - Convert.ToDouble(expValue.Value)) < ExpertConsts.Epsilon;
      case DataType.Measured:
        MeasuredValue val2 = expValue.Value as MeasuredValue;
        return MeasureHelper.Compare(attrValue as MeasuredValue, val2).Equals((object) CompareResult.Equal);
      default:
        return object.Equals(attrValue, expValue.Value);
    }
  }

  private static bool vLess(object attrValue, ExpertValue expValue)
  {
    switch (expValue.ValueType)
    {
      case DataType.Integer:
        return Convert.ToInt64(attrValue) < Convert.ToInt64(expValue.Value);
      case DataType.Float:
        return Convert.ToDouble(attrValue) < Convert.ToDouble(expValue.Value);
      case DataType.Measured:
        MeasuredValue val2 = expValue.Value as MeasuredValue;
        return MeasureHelper.Compare(attrValue as MeasuredValue, val2).Equals((object) CompareResult.Less);
      case DataType.String:
        return string.Compare(Convert.ToString(attrValue), Convert.ToString(expValue.Value)) < 0;
      case DataType.Date:
        return DateTime.Compare(Convert.ToDateTime(attrValue), Convert.ToDateTime(expValue.Value)) < 0;
      default:
        return false;
    }
  }

  private static bool vLessOrEquals(object attrValue, ExpertValue expValue)
  {
    switch (expValue.ValueType)
    {
      case DataType.Integer:
        return Convert.ToInt64(attrValue) <= Convert.ToInt64(expValue.Value);
      case DataType.Float:
        return Convert.ToDouble(attrValue) <= Convert.ToDouble(expValue.Value);
      case DataType.Measured:
        MeasuredValue val2 = expValue.Value as MeasuredValue;
        MeasuredValue val1 = attrValue as MeasuredValue;
        return MeasureHelper.Compare(val1, val2).Equals((object) CompareResult.Less) || MeasureHelper.Compare(val1, val2).Equals((object) CompareResult.Equal);
      case DataType.String:
        return string.Compare(Convert.ToString(attrValue), Convert.ToString(expValue.Value)) <= 0;
      case DataType.Date:
        return DateTime.Compare(Convert.ToDateTime(attrValue), Convert.ToDateTime(expValue.Value)) <= 0;
      default:
        return false;
    }
  }

  private static bool vMore(object attrValue, ExpertValue expValue)
  {
    switch (expValue.ValueType)
    {
      case DataType.Integer:
        return Convert.ToInt64(attrValue) > Convert.ToInt64(expValue.Value);
      case DataType.Float:
        return Convert.ToDouble(attrValue) > Convert.ToDouble(expValue.Value);
      case DataType.Measured:
        MeasuredValue val2 = expValue.Value as MeasuredValue;
        return MeasureHelper.Compare(attrValue as MeasuredValue, val2).Equals((object) CompareResult.More);
      case DataType.String:
        return string.Compare(Convert.ToString(attrValue), Convert.ToString(expValue.Value)) > 0;
      case DataType.Date:
        return DateTime.Compare(Convert.ToDateTime(attrValue), Convert.ToDateTime(expValue.Value)) > 0;
      default:
        return false;
    }
  }

  private static bool vMoreOrEquals(object attrValue, ExpertValue expValue)
  {
    switch (expValue.ValueType)
    {
      case DataType.Integer:
        return Convert.ToInt64(attrValue) >= Convert.ToInt64(expValue.Value);
      case DataType.Float:
        return Convert.ToDouble(attrValue) >= Convert.ToDouble(expValue.Value);
      case DataType.Measured:
        MeasuredValue val2 = expValue.Value as MeasuredValue;
        MeasuredValue val1 = attrValue as MeasuredValue;
        return MeasureHelper.Compare(val1, val2).Equals((object) CompareResult.More) || MeasureHelper.Compare(val1, val2).Equals((object) CompareResult.Equal);
      case DataType.String:
        return string.Compare(Convert.ToString(attrValue), Convert.ToString(expValue.Value)) >= 0;
      case DataType.Date:
        return DateTime.Compare(Convert.ToDateTime(attrValue), Convert.ToDateTime(expValue.Value)) >= 0;
      default:
        return false;
    }
  }

  private static bool vNotEquals(
    object attrValue,
    ExpertValue expValue,
    IUserSession ius,
    AttribPair ap)
  {
    return !ExpertTableProcessor.vEquals(attrValue, expValue, ius, ap);
  }

  internal static bool IsObjTypeInPacket(int objTypeId, PacketValue pv, IUserSession ius)
  {
    for (int index = 0; index < pv.Count; ++index)
    {
      switch (pv[index].ValueType)
      {
        case DataType.Integer:
          int int32_1 = Convert.ToInt32(pv[index].Value);
          if (objTypeId == int32_1 || ExpertTableProcessor.CheckObjectType(objTypeId, int32_1, ius))
            return true;
          break;
        case DataType.Diap:
          DiapValue diapValue = (DiapValue) pv[index].Value;
          if (diapValue.Low.ValueType != DataType.Integer || diapValue.High.ValueType != DataType.Integer)
            throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_107"));
          int int32_2 = Convert.ToInt32(diapValue.Low.Value);
          int int32_3 = Convert.ToInt32(diapValue.High.Value);
          if (objTypeId >= int32_2 && objTypeId <= int32_3)
            return true;
          for (int rootId = int32_2; rootId <= int32_3; ++rootId)
          {
            if (ExpertTableProcessor.CheckObjectType(objTypeId, rootId, ius))
              return true;
          }
          break;
        default:
          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_108"));
      }
    }
    return false;
  }

  internal static bool CheckObjectType(int testId, int rootId, IUserSession ius)
  {
    if (testId == rootId)
      return true;
    IDBObjectType objectType = ius.GetObjectType(testId);
    int parentTypeId;
    do
    {
      parentTypeId = objectType.ParentTypeID;
      if (parentTypeId == rootId)
        return true;
      if (parentTypeId != -1)
        objectType = ius.GetObjectType(parentTypeId);
    }
    while (parentTypeId != -1);
    return false;
  }

  internal static bool IsInPacket(object val, DataType valType, PacketValue pv)
  {
    for (int index = 0; index < pv.Count; ++index)
    {
      switch (pv[index].ValueType)
      {
        case DataType.Integer:
          if ((valType == DataType.Integer || valType == DataType.String) && Convert.ToInt64(val) == Convert.ToInt64(pv[index].Value))
            return true;
          break;
        case DataType.Float:
          if ((valType == DataType.Float || valType == DataType.String || valType == DataType.Integer) && Math.Abs(Convert.ToDouble(val) - Convert.ToDouble(pv[index].Value)) < ExpertConsts.Epsilon)
            return true;
          break;
        case DataType.Measured:
          if (val is MeasuredValue && MeasureHelper.Compare((MeasuredValue) pv[index].Value, (MeasuredValue) val) == CompareResult.Equal || valType == DataType.String && Convert.ToString((object) (MeasuredValue) pv[index].Value) == Convert.ToString(val))
            return true;
          break;
        case DataType.String:
          if ((valType == DataType.Integer || valType == DataType.String) && Convert.ToString(val) == Convert.ToString(pv[index].Value))
            return true;
          break;
        case DataType.Diap:
          DiapValue diapValue = (DiapValue) pv[index].Value;
          switch (diapValue.Low.ValueType)
          {
            case DataType.Integer:
              long int64 = Convert.ToInt64(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && int64 >= Convert.ToInt64(diapValue.Low.Value) && int64 <= Convert.ToInt64(diapValue.High.Value))
                return true;
              continue;
            case DataType.Float:
              double num = Convert.ToDouble(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && num >= Convert.ToDouble(diapValue.Low.Value) - ExpertConsts.Epsilon && num <= Convert.ToDouble(diapValue.High.Value) + ExpertConsts.Epsilon)
                return true;
              continue;
            case DataType.Measured:
              if (valType == DataType.Measured)
              {
                MeasuredValue val1_1 = (MeasuredValue) val;
                MeasuredValue val1_2 = (MeasuredValue) diapValue.Low.Value;
                MeasuredValue val2_1 = (MeasuredValue) diapValue.High.Value;
                MeasuredValue val2_2 = val1_1;
                CompareResult compareResult1 = MeasureHelper.Compare(val1_2, val2_2);
                CompareResult compareResult2 = MeasureHelper.Compare(val1_1, val2_1);
                if ((compareResult1 == CompareResult.Equal || compareResult1 == CompareResult.Less) && (compareResult2 == CompareResult.Equal || compareResult2 == CompareResult.Less))
                  return true;
                continue;
              }
              continue;
            case DataType.String:
              string strA = Convert.ToString(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && string.Compare(strA, Convert.ToString(diapValue.Low.Value)) >= 0 && string.Compare(strA, Convert.ToString(diapValue.High.Value)) <= 0)
                return true;
              continue;
            default:
              continue;
          }
      }
    }
    return false;
  }
}
