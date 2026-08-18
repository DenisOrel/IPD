// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OldAVSFile
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Victor;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class OldAVSFile : AVS6_File
{
  private AVSDocument _document;
  private OldAVSFields _avsFields;
  private SpecificationSection curSection;
  private List<byte> foundCustomFieldTypes = new List<byte>();
  private List<ProductInfo> origProductsOrder;
  private List<ProductInfo> docProductsOrder;
  private RecordNew curMainRec;
  private string artId = "";
  private string docId = "";
  private string position = string.Empty;
  private string posDesignation = string.Empty;
  private string designation = string.Empty;
  private string name = string.Empty;
  private string okpCode = string.Empty;
  private string count = string.Empty;
  private string note = string.Empty;
  private int currentRecordIndex;
  private int foundRecordIndex;
  private int indexOfCurrentProduct = -1;
  private readonly List<string> rowCountsFormB = new List<string>();
  private IArticleService artService;

  internal OldAVSFields FieldDefs
  {
    get => this._avsFields;
    set
    {
      if (this._avsFields == null)
        this._avsFields = value;
      else
        this._avsFields.DefaultFields = value;
    }
  }

  public OldAVSFile()
  {
    this._avsFields = AVS6_From_Avs6Main._inMemoryIniFile6 != null ? new OldAVSFields(AVS6_From_Avs6Main._inMemoryIniFile6) : throw new Exception("Файл настроек AVS6 не найден.");
  }

  public OldAVSFile(AVSDocument document)
  {
    this._document = document;
    if ((AVS6_From_Avs6Main._list_ElDocList == null || AVS6_From_Avs6Main._list_ElDocList.Count == 0) && !AVS6_From_Avs6Main.Read() && (!document.ReadOnly || !document.IsGeneratedDoc))
    {
      DialogResult dialogResult = DialogResult.No;
      if (AvsConfig.General.AskUserForOldSPIniFile)
        dialogResult = MessageBox.Show("Не найден файл настроек.Выбрать ini-файл вручную?", "Файл настроек не найден", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
      if (dialogResult == DialogResult.Yes)
      {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.RestoreDirectory = true;
        openFileDialog.Filter = "Ini файлы (*.ini)|*.ini";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          AVS6_From_Avs6Main._isAvs6 = 1;
          AVS6_From_Avs6Main._pathAvs6Cfg = Path.GetFullPath(openFileDialog.FileName);
          AVS6_From_Avs6Main._fileIni6 = openFileDialog.FileName;
          AVS6_From_Avs6Main.Load_From_AVS6MAIN();
        }
      }
    }
    this._avsFields = AVS6_From_Avs6Main._inMemoryIniFile6 != null ? new OldAVSFields(AVS6_From_Avs6Main._inMemoryIniFile6) : throw new Exception("Файл настроек AVS6 не найден.");
  }

  /// <summary>Применить информацию из файла AVS старого формата к документу</summary>
  /// <param name="objIdList">Список идентификаторов найденных объектов в записях</param>
  /// <param name="objTypeList">Список типов найденных объектов в записях</param>
  internal void ApplyToDocument(List<long> objIdList, List<int> objTypeList)
  {
    if (this._document == null)
      throw new ArgumentNullException("_document");
    if (objIdList == null)
      throw new ArgumentNullException(nameof (objIdList));
    if (objTypeList == null)
      throw new ArgumentNullException(nameof (objTypeList));
    this._document = this._document;
    if (!this._document.IsSpecification)
      this.curSection = this._document.commonDataChapter as SpecificationSection;
    if (this._pasport == null)
      throw new NullReferenceException("Не прочитана информационная запись (паспорт) документа.");
    int count1 = this._pasport._listR2.Count;
    this._pasport._listFields.Select<RecordNewField, byte>((Func<RecordNewField, byte>) (f => f._fieldType_Avs6)).ToArray<byte>();
    long num1 = -1;
    long num2 = -1;
    this._document.ChangeGroupDocumentForm(this.GroupForm);
    (this.origProductsOrder, this.docProductsOrder) = this._document.FindProductsFromOldSPFile2(this);
    this._document.SortProductsByDocOrder(this.docProductsOrder);
    SkipLinesSchema skipLinesSchema = this._document.GetSkipLinesSchema();
    if (this.FieldDefs != null)
      skipLinesSchema.CopyParamsFrom(this.FieldDefs.SkipLinesSchema);
    if (!this._document.ReadOnly)
      skipLinesSchema.SaveParams();
    int count2 = this._listRecords.Count;
    if (count2 <= 0)
      return;
    Dictionary<int, long> sectionIdDictionary;
    Dictionary<int, long> partIdDictionary;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sectionIdDictionary = AVSPlugin.GetSectionNumToSectionIdDictionary(sessionKeeper.Session);
      partIdDictionary = AVSPlugin.GetPartNumToPartIdDictionary(sessionKeeper.Session);
    }
    bool flag = true;
    if (!this._document.IsSpecification)
    {
      this.artService = ServicesManager.GetService(typeof (IArticleService)) as IArticleService;
      if (this.artService == null)
        throw new Exception("Недоступен сервис ArticleService");
    }
    for (int index = 0; index < count2; ++index)
    {
      this.artId = "";
      this.docId = "";
      this.position = string.Empty;
      this.posDesignation = string.Empty;
      this.designation = string.Empty;
      this.name = string.Empty;
      this.okpCode = string.Empty;
      this.note = string.Empty;
      this.count = string.Empty;
      this.rowCountsFormB.Clear();
      this.curMainRec = this._listRecords[index];
      char recordTypeAvs6 = this.curMainRec._recordType_Avs6;
      this.indexOfCurrentProduct = 0;
      TableData rowTemplate = (TableData) null;
      switch (recordTypeAvs6)
      {
        case 'I':
          if (this._document.IsSpecification)
          {
            this.ProcessSpecificationInfoRecord(num2, num1);
            break;
          }
          this.ProcessInfoRecord(objIdList, objTypeList);
          break;
        case 'N':
          this.currentRecordIndex = 0;
          this.foundRecordIndex = 0;
          string fieldTextAvs6_1 = this.curMainRec.FieldByType((byte) 5)?._fieldText_Avs6;
          this.indexOfCurrentProduct = fieldTextAvs6_1 != null ? this._document.GetProductIndexByHisCaption(fieldTextAvs6_1) : -1;
          break;
        case 'P':
          this.currentRecordIndex = 0;
          this.foundRecordIndex = 0;
          int result1;
          if (int.TryParse(this.curMainRec.FieldByType((byte) 9)?._fieldText_Avs6 ?? "", out result1))
          {
            if (!partIdDictionary.TryGetValue(result1, out num2))
            {
              num2 = -1L;
              break;
            }
            break;
          }
          num2 = -1L;
          break;
        case 'R':
          rowTemplate = this._document.note1Template;
          flag = false;
          break;
        case 'S':
          this.currentRecordIndex = 0;
          this.foundRecordIndex = 0;
          int result2;
          if (int.TryParse(this.curMainRec.FieldByType((byte) 10)?._fieldText_Avs6 ?? "", out result2))
          {
            if (!sectionIdDictionary.TryGetValue(result2, out num1))
            {
              num1 = -1L;
              break;
            }
            break;
          }
          num1 = -1L;
          break;
        case 'T':
          rowTemplate = this._document.note2Template;
          flag = false;
          break;
        case 'X':
          rowTemplate = this._document.additionalNote1Template;
          flag = false;
          break;
        case 'Y':
          rowTemplate = this._document.additionalNote2Template;
          flag = false;
          break;
      }
      if (!flag && rowTemplate != null)
      {
        flag = true;
        string fieldTextAvs6_2 = this.curMainRec.FieldByType((byte) 11)?._fieldText_Avs6;
        this.indexOfCurrentProduct = fieldTextAvs6_2 != null ? this._document.GetProductIndexByHisCaption(fieldTextAvs6_2) : -1;
        DocumentTreeNode[] contextNodes = this._document.GetContextNodes(num2, num1, this.indexOfCurrentProduct);
        this._document.InsertNewNoteDocRow(this._document.GetContextChapters(contextNodes.Length != 0 ? contextNodes[0] : (DocumentTreeNode) null), fieldTextAvs6_2, rowTemplate, false, false);
      }
    }
    this._document.UpdateViewNodes(false, false, false, !this._document.IsSpecification, true, EmptyRowUpdateMode.DontChange);
  }

  /// <summary>Применить информацию из файла AVS старого формата к документу</summary>
  /// <param name="document">Объект документа</param>
  /// <param name="objIdList">Список идентификаторов найденных объектов в записях</param>
  /// <param name="objTypeList">Список типов найденных объектов в записях</param>
  internal void ApplyToDocument(AVSDocument document, List<long> objIdList, List<int> objTypeList)
  {
    this._document = document;
    this.ApplyToDocument(objIdList, objTypeList);
  }

  private void ProcessInfoRecord(List<long> objIdList, List<int> objTypeList)
  {
    this.foundCustomFieldTypes.Clear();
    int num1 = 0;
    if (this.FieldDefs != null)
    {
      foreach (byte num2 in this.curMainRec._listFields.Select<RecordNewField, byte>((Func<RecordNewField, byte>) (f => f._fieldType_Avs6)).ToArray<byte>())
      {
        if (num2 > (byte) 50 && num2 < (byte) 100)
          this.foundCustomFieldTypes.Add(num2);
        ++num1;
      }
    }
    this.position = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Position)?._fieldText_Avs6 ?? "").Trim();
    this.designation = this.curMainRec.Desigation().Trim();
    this.name = this.curMainRec.Name().Trim();
    this.okpCode = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_OkpCode)?._fieldText_Avs6 ?? "").Trim();
    this.count = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Count)?._fieldText_Avs6 ?? "").Trim();
    this.posDesignation = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_PosDesignation)?._fieldText_Avs6 ?? "").Trim();
    this.note = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Note)?._fieldText_Avs6 ?? "").Trim();
    int objType = -1;
    long num3 = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      num3 = this.artService.FindArticleID(this.designation, this.okpCode, this.name, this._document.FiltrationOwnerID, (object) sessionKeeper.Session);
      if (num3 == 0L)
        num3 = -1L;
      if (!num3.IsUndefinedId())
      {
        sessionKeeper.Session.GetObjectInfo(num3);
        objType = sessionKeeper.Session.GetObjectInfo(num3).ObjectTypeID;
        objIdList.Add(num3);
        objTypeList.Add(objType);
      }
    }
    AVSRow row = new AVSRow(this._document, num3, Guid.Empty, objType, -1L, Guid.Empty, -1, Guid.Empty, -1L);
    this.curSection.AddRow(row, false);
    row.UpdateDocRow((TableData) null, (List<AvsRowAttributeInfo>) null, true, false, false, EmptyRowUpdateMode.DontChange);
    row.SetFieldValue(this._document.Field_PosDesignation, -1, -1, (object) this.posDesignation, false, false, true, false, false, false);
    if (!string.IsNullOrWhiteSpace(this.designation))
      row.SetFieldValue(this._document.Field_Designation, -1, -1, (object) this.designation, false, false, true, false, false, false);
    row.SetFieldValue(this._document.Field_Name, -1, -1, (object) this.name, false, false, true, false, false, false);
    row.SetFieldValue(this._document.Field_Count, -1, -1, (object) this.count, false, false, true, false, false, false);
    row.SetFieldValue(row.Field_Note, -1, -1, (object) this.note, false, false, true, false, false, false);
    if (row.Section != null)
    {
      if (this.foundRecordIndex >= row.Section.Rows.Count || row.Section.Rows[this.foundRecordIndex] != row)
      {
        this.foundRecordIndex = row.Section.Rows.IndexOf(row);
        if (this.foundRecordIndex != -1 && this.foundRecordIndex < row.Section.Rows.Count)
        {
          row.Section.Rows.RemoveAt(this.foundRecordIndex);
          row.Section.Rows.Insert(this.currentRecordIndex, row);
          this.foundRecordIndex = this.currentRecordIndex + 1;
        }
      }
      else
        ++this.foundRecordIndex;
    }
    ++this.currentRecordIndex;
    if (this.foundCustomFieldTypes.Count > 0 && this.FieldDefs != null)
    {
      for (int index = 0; index < this.foundCustomFieldTypes.Count; ++index)
      {
        byte foundCustomFieldType = this.foundCustomFieldTypes[index];
        string fieldTextAvs6 = this.curMainRec.FieldByType(foundCustomFieldType)?._fieldText_Avs6;
        OldAVSField oldAvsField;
        if (fieldTextAvs6 != string.Empty && this.FieldDefs.List.TryGetValue((int) foundCustomFieldType, out oldAvsField))
        {
          ConvertField convertField = oldAvsField.ConvertField;
          ConvertFullData fullDataForRecord = convertField.GetConvertFullDataForRecord(row.RelType, row.ObjType);
          switch (fullDataForRecord.Action)
          {
            case ConvertAction.Write:
              switch (fullDataForRecord.Target)
              {
                case ConvertTarget.ToDocumentField:
                  row.DocNode.SetAttributeValue(convertField.OldCaption, fieldTextAvs6);
                  continue;
                case ConvertTarget.ToObjectAttribute:
                  row.SetFieldValue(new AvsRowAttributeInfo(false, convertField.NewAttributeID), -1, this.indexOfCurrentProduct, (object) fieldTextAvs6, true, false, true, true, false, false);
                  continue;
                case ConvertTarget.ToRelationAttribute:
                  row.SetFieldValue(new AvsRowAttributeInfo(true, convertField.NewAttributeID), -1, this.indexOfCurrentProduct, (object) fieldTextAvs6, true, false, true, true, false, false);
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
      }
    }
    int result = 0;
    if (int.TryParse(this.curMainRec.FieldByType((byte) 13)?._fieldText_Avs6?.Trim() ?? "", out result))
      row.SkipLinesBefore = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 14)?._fieldText_Avs6?.Trim() ?? "", out result))
      row.SkipLinesAfter = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 17)?._fieldText_Avs6?.Trim() ?? "", out result))
      row.PositionStepBefore = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 18)?._fieldText_Avs6?.Trim() ?? "", out result))
      row.PositionStepAfter = new int?(result);
    if (!int.TryParse(this.curMainRec.FieldByType((byte) 16 /*0x10*/)?._fieldText_Avs6?.Trim() ?? "", out result))
      return;
    if (result == 0)
      row.FromNewPage = new bool?(true);
    else
      row.SkipPagesAfter = result;
  }

  private void ProcessSpecificationInfoRecord(long activePartId, long activeSectionId)
  {
    this.foundCustomFieldTypes.Clear();
    int num1 = 0;
    byte[] array = this.curMainRec._listFields.Select<RecordNewField, byte>((Func<RecordNewField, byte>) (f => f._fieldType_Avs6)).ToArray<byte>();
    if (this.FieldDefs != null)
    {
      foreach (byte num2 in array)
      {
        if (num2 > (byte) 50 && num2 < (byte) 100)
          this.foundCustomFieldTypes.Add(num2);
        ++num1;
      }
    }
    this.artId = this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_ArtId)?._fieldText_Avs6 ?? "";
    this.docId = this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_DocId)?._fieldText_Avs6 ?? "";
    this.position = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Position)?._fieldText_Avs6 ?? "").Trim();
    this.posDesignation = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_PosDesignation)?._fieldText_Avs6 ?? "").Trim();
    this.designation = this.curMainRec.Desigation().Trim();
    this.name = this.curMainRec.Name().Trim();
    this.okpCode = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_OkpCode)?._fieldText_Avs6 ?? "").Trim();
    this.count = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Count)?._fieldText_Avs6 ?? "").Trim();
    this.note = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Note)?._fieldText_Avs6 ?? "").Trim();
    int count1 = this.curMainRec._listR2.Count;
    if (count1 > 0)
    {
      for (int index = 0; index < count1; ++index)
      {
        RecordNew recordNew = this.curMainRec._listR2[index];
        int count2 = recordNew._listFields.Count;
        if (index == 0)
          this.indexOfCurrentProduct = 0;
        int count3 = recordNew._listR2.Count;
        recordNew._listFields.Select<RecordNewField, byte>((Func<RecordNewField, byte>) (f => f._fieldType_Avs6)).ToArray<byte>();
        this.count = (recordNew.FieldByType(AvsIDCache.OldAVDFieldNum_Count)?._fieldText_Avs6 ?? "").Trim();
        this.rowCountsFormB.Add(this.count);
      }
    }
    AVSRow avsRow = (AVSRow) null;
    if (this.name != "" || this.designation != "" || this.position != "")
      avsRow = this._document.GetRowByParams(this.name, this.designation, this.okpCode, this.position, this.count, this.rowCountsFormB, this.origProductsOrder, this.indexOfCurrentProduct, activePartId, activeSectionId, this.artId, this.docId);
    if (avsRow == null)
      return;
    this._document.ImportCountSpecialSymbolFromSP(avsRow, this.count, this.rowCountsFormB, this.origProductsOrder, this.indexOfCurrentProduct);
    if (avsRow.Section != null && avsRow.Index != this.currentRecordIndex)
    {
      LogManager.AddLine($"AVS.SP. Смена индекса {avsRow.Index} на {this.currentRecordIndex} для записи {avsRow}");
      avsRow.Section.Rows.RemoveAt(avsRow.Index);
      if (this.currentRecordIndex > avsRow.Section.Rows.Count)
        this.currentRecordIndex = avsRow.Section.Rows.Count;
      avsRow.Section.Rows.Insert(this.currentRecordIndex, avsRow);
    }
    if (activePartId != -1L)
    {
      AdditionalChapterSettings additionalChapterSettings = this._document.AVSCommonPropertiesSchema.AdditionalChapters.Find((Predicate<AdditionalChapterSettings>) (x => x.ChapterID == activePartId));
      if (additionalChapterSettings != null)
      {
        AVSDocument document = this._document;
        List<AVSRow> specRows = new List<AVSRow>();
        specRows.Add(avsRow);
        AdditionalChapterSettings newChapterSettings = additionalChapterSettings;
        document.MoveSpecRowToChapter(specRows, newChapterSettings);
      }
    }
    ++this.currentRecordIndex;
    if (!avsRow.IsDocRelation && string.IsNullOrEmpty(avsRow.GetFieldStringValue(avsRow.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false)))
    {
      string str = (this.curMainRec.FieldByType(AvsIDCache.OldAVDFieldNum_Format)?._fieldText_Avs6 ?? "").Trim();
      if (!string.IsNullOrEmpty(str))
        avsRow.SetFieldValue(avsRow.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, (object) str, false, false, true, false, false, false);
    }
    if (this.foundCustomFieldTypes.Count > 0 && this.FieldDefs != null)
    {
      for (int index = 0; index < this.foundCustomFieldTypes.Count; ++index)
      {
        byte foundCustomFieldType = this.foundCustomFieldTypes[index];
        string attributeValue = this.curMainRec.FieldByType(foundCustomFieldType)?._fieldText_Avs6 ?? string.Empty;
        OldAVSField oldAvsField;
        if (attributeValue != string.Empty && this.FieldDefs.List.TryGetValue((int) foundCustomFieldType, out oldAvsField))
        {
          ConvertField convertField = oldAvsField.ConvertField;
          ConvertFullData fullDataForRecord = convertField.GetConvertFullDataForRecord(avsRow.RelType, avsRow.ObjType);
          switch (fullDataForRecord.Action)
          {
            case ConvertAction.Write:
              switch (fullDataForRecord.Target)
              {
                case ConvertTarget.ToDocumentField:
                  avsRow.DocNode.SetAttributeValue(convertField.OldCaption, attributeValue);
                  continue;
                case ConvertTarget.ToObjectAttribute:
                  avsRow.SetFieldValue(new AvsRowAttributeInfo(false, convertField.NewAttributeID), -1, this.indexOfCurrentProduct, (object) attributeValue, true, false, true, true, false, false);
                  continue;
                case ConvertTarget.ToRelationAttribute:
                  avsRow.SetFieldValue(new AvsRowAttributeInfo(true, convertField.NewAttributeID), -1, this.indexOfCurrentProduct, (object) attributeValue, true, false, true, true, false, false);
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
      }
    }
    int result = 0;
    if (int.TryParse(this.curMainRec.FieldByType((byte) 13)?._fieldText_Avs6?.Trim() ?? string.Empty, out result))
      avsRow.SkipLinesBefore = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 14)?._fieldText_Avs6?.Trim() ?? string.Empty, out result))
      avsRow.SkipLinesAfter = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 17)?._fieldText_Avs6?.Trim() ?? string.Empty, out result))
      avsRow.PositionStepBefore = new int?(result);
    if (int.TryParse(this.curMainRec.FieldByType((byte) 18)?._fieldText_Avs6?.Trim() ?? string.Empty, out result))
      avsRow.PositionStepAfter = new int?(result);
    if (!int.TryParse(this.curMainRec.FieldByType((byte) 16 /*0x10*/)?._fieldText_Avs6?.Trim() ?? string.Empty, out result))
      return;
    if (result == 0)
      avsRow.FromNewPage = new bool?(true);
    else
      avsRow.SkipPagesAfter = result;
  }
}
