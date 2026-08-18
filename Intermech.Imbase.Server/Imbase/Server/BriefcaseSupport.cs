// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.BriefcaseSupport
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.Server;

internal class BriefcaseSupport : ICategoryExport
{
  public string ExporterName => "Imbase.BriefcaseSupport";

  public long[] GetLinkedObjectVersions(IUserSession session, int category, object id)
  {
    return (long[]) null;
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    if (kind != AttributableElements.Object || attributeId != Intermech.Imbase.Consts.ImbaseTableDataAttID || !(attrValueOriginal is MemoryStream serializationStream) || serializationStream.Length <= 0L)
      return (ExportAttribute[]) null;
    serializationStream.Position = 0L;
    DataSet dataSet = new BinaryFormatter().Deserialize((Stream) serializationStream) as DataSet;
    DataTable table1 = dataSet.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = dataSet.Tables["IMS_DATA"];
    int columnIndex = table1.Columns.IndexOf("F_ATTRIBUTE_GUID");
    DataRowCollection rows = table1.Rows;
    int count = rows.Count;
    List<object> objectList = new List<object>(count);
    List<string> refColumns = new List<string>(count);
    List<long> longList = new List<long>();
    for (int index = 0; index < count; ++index)
    {
      string g = rows[index][columnIndex].ToString();
      Guid anAttributeGuid = new Guid(g);
      IDBAttributeType attributeType = session.GetAttributeType(anAttributeGuid, false);
      if (attributeType != null)
      {
        objectList.Add((object) attributeType.AttributeID);
        if (attributeType.AttributeType == FieldTypes.ftObjectLink)
          refColumns.Add(g);
        else if ((attributeType.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
          refColumns.Add(g);
      }
    }
    ExportAttribute[] linkedDataByAttribute;
    if (refColumns.Count > 0)
    {
      linkedDataByAttribute = new ExportAttribute[2];
      linkedDataByAttribute[1] = new ExportAttribute(1, this.ExtractObjectIds(session, refColumns, table2));
    }
    else
      linkedDataByAttribute = new ExportAttribute[1];
    linkedDataByAttribute[0] = new ExportAttribute(3, objectList.ToArray());
    return linkedDataByAttribute;
  }

  private object[] ExtractObjectIds(
    IUserSession session,
    List<string> refColumns,
    DataTable dataTable)
  {
    int count1 = refColumns.Count;
    char[] separator = new char[1]{ '.' };
    ArrayList arrayList = new ArrayList(count1);
    List<string> stringList = new List<string>(32 /*0x20*/);
    for (int index1 = 0; index1 < count1; ++index1)
    {
      string refColumn = refColumns[index1];
      int columnIndex = dataTable.Columns.IndexOf(refColumn);
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          object obj1 = row[columnIndex];
          if (obj1 != null && !DBNull.Value.Equals(obj1))
          {
            bool flag = obj1 is ValuesArray;
            string str1 = Convert.ToString(row[columnIndex]);
            if (!string.IsNullOrEmpty(str1))
            {
              long result = -1;
              stringList.Clear();
              if (!flag)
              {
                stringList.Add(str1);
              }
              else
              {
                object[] array = (obj1 as ValuesArray).GetArray();
                if (array != null)
                {
                  foreach (object obj2 in array)
                  {
                    if (obj2 != null && !DBNull.Value.Equals(obj2))
                    {
                      string str2 = Convert.ToString(obj2);
                      if (!string.IsNullOrEmpty(str2) && !stringList.Contains(str2))
                        stringList.Add(str2);
                    }
                  }
                }
              }
              int count2 = stringList.Count;
              for (int index2 = 0; index2 < count2; ++index2)
              {
                string sguid = stringList[index2];
                Guid guid;
                if (ImbaseHelper.IsGuid(sguid, out guid))
                {
                  result = session.GetObjectInfo(guid).ObjectID;
                  if (result < 0L && session.GetObjectInfo(result).Empty)
                    result = Math.Abs(result);
                }
                else if (sguid.StartsWith("IK", StringComparison.InvariantCultureIgnoreCase))
                {
                  string[] strArray = sguid.Substring(2).Split(separator, StringSplitOptions.RemoveEmptyEntries);
                  if (strArray.Length == 2)
                  {
                    if (ImbaseHelper.IsGuid(strArray[0], out guid))
                      result = session.GetObjectInfo(guid).ObjectID;
                    else
                      long.TryParse(strArray[0], out result);
                  }
                }
                if (result != 0L && result != -1L && !arrayList.Contains((object) result))
                  arrayList.Add((object) result);
              }
            }
          }
        }
      }
    }
    return arrayList.ToArray();
  }

  public bool ProcessShortBlobs => true;
}
