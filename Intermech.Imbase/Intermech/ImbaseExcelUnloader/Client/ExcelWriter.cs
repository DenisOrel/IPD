// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.ExcelWriter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Intermech.DataFormats;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

internal class ExcelWriter : IDisposable
{
  private SpreadsheetDocument _Document;
  private OpenXmlWriter _Writer;
  private List<WorksheetPart> _SheetList = new List<WorksheetPart>();
  private IExtendedBackgroundTask _BackGroundTask;
  private StringBuilder _ErrorMessages;
  private int _CurrentRowNumber;
  private UnloadFlags _Flags;
  private List<Guid> _AttrGuidLst;
  private int _CommonColsCount;
  private int _CatalogStartIndx;
  private int _FolderStartIndx;
  private int _TableRefStartIndx;
  private int _CatalogRecStartIndx;
  private int _CommonAttrStartIndx;
  private List<string> _Captions;
  private HashSet<int> _AddObjTypes;
  private List<long> _ObjIDsList;
  private HashSet<IDBTypedObjectID> _Items;

  public ExcelWriter(
    HashSet<IDBTypedObjectID> AItems,
    List<Guid> AAttrGuidLst,
    UnloadFlags AFlags,
    StringBuilder AErrorMessages,
    IExtendedBackgroundTask ATask)
  {
    this._Items = AItems;
    this._Flags = AFlags;
    this._AttrGuidLst = AAttrGuidLst;
    this._AddObjTypes = new HashSet<int>();
    this._ObjIDsList = new List<long>();
    this._Captions = new List<string>();
    this._BackGroundTask = ATask;
    this._ErrorMessages = AErrorMessages;
  }

  private void AddSheetsToWorkBook()
  {
    this._Writer = OpenXmlWriter.Create((OpenXmlPart) this._Document.WorkbookPart);
    this._Writer.WriteStartElement((OpenXmlElement) new Workbook());
    this._Writer.WriteStartElement((OpenXmlElement) new Sheets());
    UInt32Value uint32Value = (UInt32Value) 1U;
    foreach (WorksheetPart sheet in this._SheetList)
    {
      this._Writer.WriteElement((OpenXmlElement) new Sheet()
      {
        Name = (StringValue) ("Лист" + uint32Value.ToString()),
        SheetId = uint32Value,
        Id = (StringValue) this._Document.WorkbookPart.GetIdOfPart((OpenXmlPart) sheet)
      });
      uint32Value = (UInt32Value) ((uint) uint32Value + 1U);
    }
    this._Writer.WriteEndElement();
    this._Writer.WriteEndElement();
    this._Writer.Close();
  }

  private void AddRow(string[] AObjectData)
  {
    this._Writer.WriteStartElement((OpenXmlElement) new Row(), (IEnumerable<OpenXmlAttribute>) new List<OpenXmlAttribute>()
    {
      new OpenXmlAttribute("r", (string) null, this._CurrentRowNumber.ToString())
    });
    for (int index = 0; index < AObjectData.Length; ++index)
    {
      this._Writer.WriteStartElement((OpenXmlElement) new Cell(), (IEnumerable<OpenXmlAttribute>) new List<OpenXmlAttribute>()
      {
        new OpenXmlAttribute("t", (string) null, "str")
      });
      try
      {
        this._Writer.WriteElement((OpenXmlElement) new CellValue(AObjectData[index]));
      }
      catch (Exception ex)
      {
        this._ErrorMessages.AppendLine(ex.Message);
      }
      finally
      {
        this._Writer.WriteEndElement();
      }
    }
    this._Writer.WriteEndElement();
    ++this._CurrentRowNumber;
    if (this._CurrentRowNumber <= Consts.MaxExcelRowCount)
      return;
    ExcelWriter.CloseWorksheetPart(this._Writer);
    this.CreateWorksheetPart();
    this.AddCaptions();
  }

  private void CreateWorksheetPart()
  {
    WorksheetPart worksheetPart = this._Document.WorkbookPart.AddNewPart<WorksheetPart>();
    this._SheetList.Add(worksheetPart);
    this._Writer = OpenXmlWriter.Create((OpenXmlPart) worksheetPart);
    this._Writer.WriteStartElement((OpenXmlElement) new Worksheet());
    this._Writer.WriteStartElement((OpenXmlElement) new SheetData());
  }

  private static void CloseWorksheetPart(OpenXmlWriter oxw)
  {
    oxw.WriteEndElement();
    oxw.WriteEndElement();
    oxw.Close();
  }

  private void PrepareData(IUserSession ASession)
  {
    if ((this._Flags & UnloadFlags.Catalog) == UnloadFlags.Catalog)
    {
      this._AddObjTypes.Add(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
      this._Captions.Add(LocalizationHolder.rm.GetString("Imbase_Catalog"));
      ++this._FolderStartIndx;
      ++this._TableRefStartIndx;
      ++this._CatalogRecStartIndx;
      ++this._CommonAttrStartIndx;
    }
    if ((this._Flags & UnloadFlags.Folder) == UnloadFlags.Folder)
    {
      this._Captions.Add(LocalizationHolder.rm.GetString("Imbase_Folder"));
      ++this._TableRefStartIndx;
      ++this._CatalogRecStartIndx;
      ++this._CommonAttrStartIndx;
    }
    if ((this._Flags & UnloadFlags.TableRef) == UnloadFlags.TableRef)
    {
      this._AddObjTypes.Add(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      this._Captions.Add(LocalizationHolder.rm.GetString("Imbase_Table"));
      ++this._CatalogRecStartIndx;
      ++this._CommonAttrStartIndx;
    }
    if ((this._Flags & UnloadFlags.CatalogRec) == UnloadFlags.CatalogRec)
    {
      this._AddObjTypes.Add(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
      this._Captions.Add(LocalizationHolder.rm.GetString("Imbase_Catalog_Record"));
      ++this._CommonAttrStartIndx;
    }
    if ((this._Flags & UnloadFlags.TableData) == UnloadFlags.TableData)
      this._AddObjTypes.Add(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    foreach (IDBTypedObjectID dbTypedObjectId in this._Items)
    {
      this._ObjIDsList.Add(dbTypedObjectId.ObjectID);
      this.GetObjIDsList(ASession.SessionGUID, dbTypedObjectId.ObjectID, this._ObjIDsList, this._AddObjTypes.ToArray<int>());
    }
    foreach (Guid anAttributeGuid in this._AttrGuidLst)
      this._Captions.Add(ASession.GetAttributeType(anAttributeGuid).Name);
    this._CommonColsCount = this._Captions.Count;
  }

  private void GetObjIDsList(
    Guid AsessionGuid,
    long AobjectID,
    List<long> AobjIDsList,
    int[] AAddTypes)
  {
    DataTable subfolders = ServiceHolder.ImbaseServer.GetSubfolders(AsessionGuid, AobjectID, AAddTypes);
    if (subfolders == null)
      return;
    for (int index = 0; index < subfolders.Rows.Count && !this._BackGroundTask.IsProcessStoped; ++index)
    {
      long int64 = Convert.ToInt64(subfolders.Rows[index]["F_OBJECT_ID"]);
      AobjIDsList.Add(int64);
      this.GetObjIDsList(AsessionGuid, int64, AobjIDsList, AAddTypes);
    }
  }

  private void AddCaptions()
  {
    this._CurrentRowNumber = 1;
    this.AddRow(this._Captions.ToArray());
  }

  private int GetObjNameIndx(int objTypeID)
  {
    int objNameIndx = 0;
    if ((this._Flags & UnloadFlags.Catalog) == UnloadFlags.Catalog && objTypeID == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
      objNameIndx = this._CatalogStartIndx;
    if ((this._Flags & UnloadFlags.Folder) == UnloadFlags.Folder && objTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID)
      objNameIndx = this._FolderStartIndx;
    if ((this._Flags & UnloadFlags.TableRef) == UnloadFlags.TableRef && objTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      objNameIndx = this._TableRefStartIndx;
    if ((this._Flags & UnloadFlags.CatalogRec) == UnloadFlags.CatalogRec && objTypeID == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
      objNameIndx = this._CatalogRecStartIndx;
    return objNameIndx;
  }

  private string[] GetRecordData(
    IUserSession session,
    AttributeTypeProperties[] recordsAtts,
    DataRow row,
    string APath,
    long AObjectID,
    bool nameReferences)
  {
    List<string> stringList1 = new List<string>();
    for (int index1 = 0; index1 < this._CommonColsCount; ++index1)
    {
      if (index1 >= this._CommonAttrStartIndx)
      {
        Guid attrGuid = this._AttrGuidLst[index1 - this._CommonAttrStartIndx];
        if (attrGuid == Consts.ImbasePathAttrGuid)
        {
          stringList1.Add(APath);
          continue;
        }
        int num;
        if (attrGuid == Consts.ImbaseKeyAttrGuid)
        {
          List<string> stringList2 = stringList1;
          object[] objArray = new object[4]
          {
            (object) "IK",
            (object) AObjectID.ToString(),
            (object) ".",
            null
          };
          DataRow dataRow = row;
          num = -2;
          string columnName = num.ToString();
          objArray[3] = dataRow[columnName];
          string str = string.Concat(objArray);
          stringList2.Add(str);
          continue;
        }
        if (attrGuid == Consts.ImbaseRecordGuidAttrGuid)
        {
          List<string> stringList3 = stringList1;
          DataRow dataRow = row;
          num = -12;
          string columnName = num.ToString();
          string str = dataRow[columnName].ToString();
          stringList3.Add(str);
          continue;
        }
        if (((IEnumerable<AttributeTypeProperties>) recordsAtts).Any<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (x => x.AttributeGuid == attrGuid)))
        {
          string columnName = ((IEnumerable<AttributeTypeProperties>) recordsAtts).First<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (x => x.AttributeGuid == attrGuid)).AttributeID.ToString();
          string caption = row[columnName].ToString();
          if (nameReferences)
          {
            int index2 = TableEditor.IndexOfAttProp(attrGuid, recordsAtts);
            Guid result;
            if (index2 != -1 && recordsAtts[index2].FieldType == FieldTypes.ftObjectLink && Guid.TryParse(caption, out result))
            {
              QuickObjectInfo objectInfo = session.GetObjectInfo(result);
              if (!objectInfo.Empty && !string.IsNullOrWhiteSpace(objectInfo.Caption))
                caption = objectInfo.Caption;
            }
          }
          stringList1.Add(caption);
          continue;
        }
      }
      stringList1.Add(string.Empty);
    }
    return stringList1.ToArray();
  }

  private string[] GetObjectData(IDBObject AObject, int ACaptionIndex, string AFullPath)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this._CommonColsCount; ++index)
    {
      if (index == ACaptionIndex)
      {
        stringList.Add(AObject.Caption);
      }
      else
      {
        if (index >= this._CommonAttrStartIndx)
        {
          Guid AttributeGUID = this._AttrGuidLst[index - this._CommonAttrStartIndx];
          if (AttributeGUID == Consts.ImbasePathAttrGuid)
          {
            stringList.Add(AFullPath);
            continue;
          }
          IDBAttribute byGuid = AObject.Attributes.FindByGUID(AttributeGUID);
          if (byGuid != null)
          {
            string name = byGuid.Name;
            stringList.Add(byGuid.AsString);
            continue;
          }
        }
        stringList.Add(string.Empty);
      }
    }
    return stringList.ToArray();
  }

  private string GetFullPath(Guid sessionGuid, long objID)
  {
    DataTable foldersForObjects = ServiceHolder.ImbaseServer.GetFoldersForObjects(sessionGuid, new long[1]
    {
      objID
    }, (long[]) null);
    foldersForObjects.DefaultView.Sort = "F_PATH ASC";
    DataTable table = foldersForObjects.DefaultView.ToTable();
    List<string> values = new List<string>();
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      values.Add((string) row["CAPTION"]);
    }
    return string.Join("/", (IEnumerable<string>) values);
  }

  public void GenerateFile(IUserSession ASession, string AFullFileName)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    this._BackGroundTask.Name = LocalizationHolder.rm.GetString("Imbase_ExportToExcel");
    this._BackGroundTask.Value = (object) 0;
    this.PrepareData(ASession);
    this._BackGroundTask.MaximumValue = this._ObjIDsList.Count;
    this._Document = SpreadsheetDocument.Create(AFullFileName, SpreadsheetDocumentType.Workbook);
    this._Document.AddWorkbookPart();
    this.CreateWorksheetPart();
    this.AddCaptions();
    for (int index1 = 0; index1 < this._ObjIDsList.Count && !this._BackGroundTask.IsProcessStoped; ++index1)
    {
      long objIds = this._ObjIDsList[index1];
      IDBObject AObject = ASession.GetObject(objIds, false);
      if (AObject != null)
      {
        int objectType = AObject.ObjectType;
        string fullPath = this.GetFullPath(ASession.SessionGUID, objIds);
        if ((this._Flags & UnloadFlags.Catalog) == UnloadFlags.Catalog && objectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID || (this._Flags & UnloadFlags.Folder) == UnloadFlags.Folder && objectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || (this._Flags & UnloadFlags.TableRef) == UnloadFlags.TableRef && objectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID || (this._Flags & UnloadFlags.CatalogRec) == UnloadFlags.CatalogRec && objectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        {
          int objNameIndx = this.GetObjNameIndx(objectType);
          this.AddRow(this.GetObjectData(AObject, objNameIndx, fullPath));
        }
        if ((this._Flags & UnloadFlags.TableData) == UnloadFlags.TableData && AObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          string filter = "";
          AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
          ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
          DataTable recordsTable;
          ServiceHolder.ImbaseServer.LoadRecords(ASession.SessionGUID, objIds, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
          for (int index2 = 0; index2 < recordsTable.Rows.Count; ++index2)
          {
            DataRow row = recordsTable.Rows[index2];
            this.AddRow(this.GetRecordData(ASession, columnsAttributes, row, fullPath, objIds, (this._Flags & UnloadFlags.NameObjectReferences) == UnloadFlags.NameObjectReferences));
          }
        }
      }
      this._BackGroundTask.IncProgress();
    }
    ExcelWriter.CloseWorksheetPart(this._Writer);
    this.AddSheetsToWorkBook();
    stopwatch.Stop();
  }

  public void Dispose()
  {
    if (this._Document == null)
      return;
    this._Document.Close();
    this._Document.Dispose();
  }
}
