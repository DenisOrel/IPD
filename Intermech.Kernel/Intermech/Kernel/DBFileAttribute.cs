// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBFileAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;


namespace Intermech.Kernel;

internal class DBFileAttribute : DBStorageAttribute, IDBFileAttribute, IDBAttribute, IDBSessionable
{
  internal bool _ValidateUniqueFileName = true;
  public const string FileIndexMacro = "FileIndex";
  private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();
  private static readonly char[] InvalidFileNameAndRelativePathChars = DBFileAttribute.CreateInvalidFileNameAndRelativePathChars(FileNameHelper.InvalidFileNameChars);

  private static char[] CreateInvalidFileNameAndRelativePathChars(char[] invalidFileNameChars)
  {
    List<char> charList = new List<char>((IEnumerable<char>) invalidFileNameChars);
    charList.Remove(Path.DirectorySeparatorChar);
    charList.Remove(Path.AltDirectorySeparatorChar);
    return charList.ToArray();
  }

  public DBFileAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBFileAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected override void UpdateObjectModifyDate()
  {
  }

  public override bool IsNull => this.AsString.Trim() == string.Empty;

  public void Rename(string newFileName)
  {
    ((IBlobWriter) this).OpenBlob(((IBlobReader) this).OpenBlob(-1) with
    {
      FileName = newFileName
    }, true);
    if (this.BlobState == BlobAttributeStates.Closed)
      return;
    this.CloseBlob();
  }

  protected override void ValidateBlobInfo(BlobInformation blobInfo)
  {
    if (blobInfo.FileName != string.Empty)
    {
      if (this.UserSession.IdentHelper.FileAttributeID == this.AttributeID)
      {
        if (Path.IsPathRooted(blobInfo.FileName))
          throw new KernelExceptionID(sc_12507.ssp_appserver_12508(501954403), (object) blobInfo.FileName);
        if (blobInfo.FileName.TrimStart().StartsWith("."))
          throw new KernelExceptionID(sc_12507.ssp_appserver_12509(968223213), (object) blobInfo.FileName);
        if (blobInfo.FileName.IndexOf(".\\") > -1)
          throw new KernelExceptionID(sc_12507.ssp_appserver_12510(1761176233), (object) blobInfo.FileName);
        if (blobInfo.FileName.IndexOf('\\') == 0)
          throw new KernelExceptionID(sc_12507.ssp_appserver_12511(1536206086));
        if (blobInfo.FileName.IndexOf('/') >= 0)
          throw new KernelExceptionID(sc_12507.ssp_appserver_12512(579737097), (object) '/', (object) blobInfo.FileName);
      }
      if (blobInfo.FileName.IndexOfAny(DBFileAttribute.InvalidFileNameAndRelativePathChars) >= 0)
        throw new KernelExceptionID(sc_12507.ssp_appserver_12513(514951788), (object) blobInfo.FileName[blobInfo.FileName.IndexOfAny(DBFileAttribute.InvalidFileNameAndRelativePathChars)], (object) blobInfo.FileName);
      if (this.ValuesCount > 1 && this._ValidateUniqueFileName)
      {
        string upperInvariant = blobInfo.FileName.ToUpperInvariant();
        for (int index = 0; index < this.ValuesCount; ++index)
        {
          if (index != this.Index && this._ValuesTable[index]["F_STRING_VALUE"] != null && this._ValuesTable[index]["F_STRING_VALUE"].ToString().ToUpperInvariant() == upperInvariant)
            throw new KernelExceptionID(sc_12507.ssp_appserver_12514(664529960), (object) blobInfo.FileName);
        }
      }
    }
    else if (blobInfo.RealFileSize > 0L || blobInfo.PackedFileSize > 0L)
      throw new KernelException($"Попытка присвоить пустое имя файла значению атрибута '{this.Name}'");
    base.ValidateBlobInfo(blobInfo);
  }

  protected override void ValidateOpenBlob(bool checkAccess)
  {
    if (this.ParentObject is DBObject & checkAccess)
      this.ParentObject.CheckAccess(ActionType.View, this.ParentObject.GetDefaultAccess(ActionType.View), true);
    base.ValidateOpenBlob(checkAccess);
  }

  internal override void SetContentDate()
  {
    if (!this.IsContentFile())
      return;
    base.SetContentDate();
  }

  public override bool IsContentFile() => base.IsContentFile();

  protected override void CheckRedliningAccess(BlobInformation blobInfo)
  {
    if (this.AttributeID != this.UserSession.IdentHelper.FileAttributeID || this.Index <= 0)
      return;
    IRedliningService service = ((ICustomServices) ServerServices.GetService(typeof (ICustomServices))).GetService(typeof (IRedliningService)) as IRedliningService;
    if (!service.DeleteFiles || !this.IsObjectAttribute || (this.ParentObject as IDBLifecycleLevel).LevelID != service.LevelID)
      return;
    object obj = this._ValuesTable[0]["F_STRING_VALUE"];
    string mainFilePath = obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
    if (service.IsRedliningFile(mainFilePath, blobInfo.FileName))
      throw new KernelExceptionID(409, (object) (this.ParentObject as IDBLifecycleLevel).LevelName);
  }

  protected override bool SaveStringInfo()
  {
    bool flag1 = false;
    bool validatingOn = this.ValidatingOn;
    try
    {
      this.ValidatingOn = this.IsContentFile();
      if (base.AsString != this._FileStruct.FileName)
      {
        IDbManager dataManager = this.UserSession.DataManager;
        IDbDataParameter dbDataParameter1 = dataManager.Parameter("fname_old", (object) base.AsString.ToUpperInvariant());
        base.AsString = this._FileStruct.FileName;
        flag1 = true;
        if (this.AttributeID == this.UserSession.IdentHelper.FileAttributeID)
        {
          IDbDataParameter dbDataParameter2 = dataManager.Parameter("objID", (object) this.DBObjectID);
          IDbDataParameter dbDataParameter3 = dataManager.Parameter("fname", (object) this._FileStruct.FileName.ToUpperInvariant());
          IDbDataParameter dbDataParameter4 = !this.IsObjectAttribute ? dataManager.Parameter("id1", (object) this.DBObjectID) : dataManager.Parameter("id1", (object) (this.ParentObject as IDBObject).ID);
          if (this._FileStruct.FileName.Trim() != string.Empty)
          {
            DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_STRING_VALUE, F_INLIST_ID, F_INTEGER_VALUE FROM {this._ValuesTableName} WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID <> :lstID", dbDataParameter2, dataManager.Parameter("attrID", (object) this.AttributeID), dataManager.Parameter("lstID", (object) this.Index));
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (dbDataParameter3.Value.ToString() == dataTable.Rows[index][0].ToString().ToUpperInvariant())
                throw new KernelExceptionID(sc_12507.ssp_appserver_12515(1254293955), (object) this._FileStruct.FileName);
            }
          }
          DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_KEY FROM IMS_FILENAMES WHERE (F_FILENAME = :fname_old and F_KEY = :objID) OR (F_FILENAME = :fname AND F_ID <> :id1)", dbDataParameter1, dbDataParameter3, dbDataParameter2, dbDataParameter4);
          bool flag2 = false;
          for (int index = 0; index < dataTable1.Rows.Count; ++index)
          {
            long int64 = Convert.ToInt64(dataTable1.Rows[index][0]);
            if (int64 == this.DBObjectID)
            {
              flag2 = true;
            }
            else
            {
              IDBObject dbObject = this.UserSession.GetObject(int64, false);
              string str1 = string.Empty;
              bool flag3 = false;
              if (dbObject != null)
              {
                if (!dbObject.IsCreationMode)
                {
                  if ((dbObject as IDBLifecycleLevel).LevelID == this.UserSession.IdentHelper.DeletedID)
                    flag3 = true;
                  str1 = dbObject.NameInMessages;
                }
                else
                  continue;
              }
              else if (this.UserSession.GetRelation(int64, false) != null)
                str1 = sc_12507.ssp_appserver_12516() + int64.ToString();
              else
                dataManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE F_KEY = :keyID", dataManager.Parameter("keyID", (object) int64));
              if (str1 != string.Empty)
              {
                string str2 = !flag3 ? string.Empty : " Внимание! Данный объект находится в корзине (на уровне продвижения 'Удалено').";
                throw new KernelExceptionID(sc_12507.ssp_appserver_12517(316304253), (object) this._FileStruct.FileName, (object) str1, (object) str2);
              }
            }
          }
          if (flag2)
            dataManager.ExecuteNonQuery("UPDATE IMS_FILENAMES SET F_FILENAME = :fname WHERE F_KEY = :objID AND F_FILENAME = :fname_old", dbDataParameter2, dbDataParameter3, dbDataParameter1);
          else
            dataManager.ExecuteNonQuery("INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:fname, :objID, :id1)", dbDataParameter3, dbDataParameter2, dbDataParameter4);
        }
      }
      if (base.AsDateTime != this._FileStruct.ModifyDate)
      {
        base.AsDateTime = this._FileStruct.ModifyDate + this.UserSession.TimeZoneOffset;
        flag1 = true;
      }
      if (this.IsObjectAttribute)
      {
        if ((this.AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
        {
          if (this._Attributes != null)
          {
            if ((this._Attributes.AssignMode & Consts.CheckOutMode) != 0)
              goto label_35;
          }
          this.UserSession.AddAttrToIndexQueue(this.AsString, (IDBAttribute) this);
        }
      }
    }
    finally
    {
      this.ValidatingOn = validatingOn;
    }
label_35:
    return flag1;
  }

  internal void CheckUniqueFileNames(long id)
  {
    string commandText = "SELECT F_KEY FROM IMS_FILENAMES WHERE F_FILENAME = :fname AND F_ID <> :id1";
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("id1", (object) id);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("fname", (object) string.Empty);
    for (int index = 0; index < this.ValuesCount; ++index)
    {
      this.Index = index;
      dbDataParameter2.Value = (object) this.AsString.ToUpperInvariant();
      DataTable dataTable = dataManager.ExecuteDataTable(commandText, dbDataParameter2, dbDataParameter1);
      if (dataTable.Rows.Count > 0)
      {
        IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), false);
        if (dbObject != null && (dbObject as IDBLifecycleLevel).LevelID != this.UserSession.IdentHelper.DeletedID)
          throw new KernelExceptionID(341, (object) this.ParentObject.ObjectName, (object) this.ParentObject.ObjectID, (object) dbObject.NameInMessages, (object) dbObject.ObjectID, (object) this.AsString).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ParentObject.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      }
    }
  }

  public override string AsString
  {
    get => base.AsString;
    set => throw new OperationNotApplicableException();
  }

  public override DateTime AsDateTime
  {
    get => base.AsDateTime;
    set => throw new OperationNotApplicableException();
  }

  private string GetFileNameByFormula(string formula, long prototypeObjectID)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    bool flag = false;
    DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
    for (int index = 0; index < formula.Length; ++index)
    {
      char ch = formula[index];
      switch (ch)
      {
        case '[':
          flag = !flag ? true : throw new KernelExceptionID(sc_12507.ssp_appserver_12519(1684766943), (object) '[', (object) formula, (object) prototypeObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(prototypeObjectID));
          break;
        case ']':
          if (flag)
          {
            if (stringBuilder2.ToString() == "FileIndex")
            {
              stringBuilder1.Append(this.Index.ToString());
            }
            else
            {
              DataRow[] dataRowArray = table.Select("F_NAME = " + SqlHelper.QString(stringBuilder2.ToString()));
              int attributeID = dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]) : throw new KernelExceptionID(sc_12507.ssp_appserver_12520(1929663577), (object) stringBuilder2.ToString(), (object) formula, (object) prototypeObjectID);
              if (this.ParentObject is IDBAttributable parentObject)
              {
                object[] valuesById = parentObject.GetValuesByID(attributeID, false);
                if (valuesById != null && valuesById.Length != 0)
                {
                  if (attributeID == -2)
                    valuesById[0] = (object) Math.Abs(this.DBObjectID);
                  stringBuilder1.Append(valuesById[0].ToString());
                }
              }
            }
            flag = false;
            stringBuilder2.Length = 0;
            break;
          }
          throw new KernelExceptionID(sc_12507.ssp_appserver_12521(29077915), (object) ']', (object) formula, (object) prototypeObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(prototypeObjectID));
        default:
          if (flag)
          {
            stringBuilder2.Append(ch);
            break;
          }
          stringBuilder1.Append(ch);
          break;
      }
    }
    string fileName = FileNameHelper.ReplaceInvalidProtoFileNameChars(stringBuilder1.ToString().Trim());
    if (new FileInfo(formula).Extension == string.Empty)
    {
      FileInfo fileInfo = new FileInfo(fileName);
      string str = string.Empty;
      if (this.ParentObject != null && this.UserSession.DBCache.IsDocument(this.ParentObject.Attributes.ObjectType))
        str = (ServerServices.GetService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(this.UserSession.SessionGUID, this.ParentObject.Attributes.ObjectType).DocumentFileExt;
      if (str != string.Empty && fileInfo.Extension != str)
        fileName += str;
    }
    return fileName;
  }

  public string GetNewFileName()
  {
    IDBAttribute protoAttribute = (IDBAttribute) null;
    long[] filePrototype = this.UserSession.DBCache.GetFilePrototype(this.AttributeID, this.TypeID, this.UserSession.UserID);
    if (filePrototype != null && filePrototype.Length >= 1)
    {
      IDBObject dbObject = this.UserSession.GetObject(filePrototype[0], false);
      if (dbObject != null)
        protoAttribute = dbObject.GetAttributeByID(this.UserSession.IdentHelper.FileAttributeID);
    }
    return this.GetNewFileName(protoAttribute);
  }

  private string GetNewFileName(IDBAttribute protoAttribute)
  {
    string str = string.Empty;
    if (protoAttribute != null)
    {
      BlobInformation blobInformation = (protoAttribute as IBlobReader).OpenBlob(-1);
      if (blobInformation.Note != string.Empty)
      {
        string val = this.GetFileNameByFormula(blobInformation.Note, protoAttribute.DBObjectID);
        if (val != string.Empty)
        {
          object[] valuesByGuid = (protoAttribute as DBAttribute).ParentObject.GetValuesByGuid(new Guid("cadd9456-306c-11d8-b4e9-00304f19f545"), false);
          if (valuesByGuid != null && Convert.ToBoolean(valuesByGuid[0]))
            val = SQLStringHelper.Translit(val);
          return val;
        }
      }
      str = new FileInfo(blobInformation.FileName).Extension;
    }
    string newFileName;
    if (this.AttributeID == this.UserSession.IdentHelper.FileAttributeID)
    {
      if (str == string.Empty)
      {
        DocumentTypeSettings settings = (this.UserSession.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(this.UserSession.SessionGUID, this.TypeID);
        if (settings.DocumentFileExt != null && settings.DocumentFileExt != string.Empty)
        {
          str = settings.DocumentFileExt;
          if (str.IndexOf('.') != 0)
            str = "." + str;
        }
      }
      newFileName = $"F{Math.Abs(this.DBObjectID)}_{this.Index}{str}";
    }
    else
      newFileName = $"F{Math.Abs(this.DBObjectID)}_{this.Index}_{this.AttributeID}{str}";
    return newFileName;
  }

  public void SetDefaultFileName(IDBAttribute protoAttribute)
  {
    ((IBlobWriter) this).OpenBlob((protoAttribute == null ? ((IBlobReader) this).OpenBlob(-1) : (protoAttribute as IBlobReader).OpenBlob(-1)) with
    {
      FileName = this.GetNewFileName(protoAttribute),
      ModifyDate = DateTime.UtcNow + this.UserSession.TimeZoneOffset,
      Note = string.Empty
    }, true);
  }

  public long[] SetPrototype(long prototypeID)
  {
    IDBAttribute dbAttribute = (IDBAttribute) null;
    long[] numArray = (long[]) null;
    if (prototypeID == 0L)
    {
      if (this.IsObjectAttribute)
      {
        numArray = this.UserSession.DBCache.GetFilePrototype(this.AttributeID, this.TypeID, this.UserSession.UserID);
        if (numArray != null)
        {
          try
          {
            if (numArray.Length == 1)
              dbAttribute = this.UserSession.GetObject(numArray[0]).GetAttributeByID(this.UserSession.IdentHelper.FileAttributeID);
          }
          catch
          {
            dbAttribute = (IDBAttribute) null;
          }
        }
      }
    }
    else
      dbAttribute = this.UserSession.GetObject(prototypeID).GetAttributeByID(this.UserSession.IdentHelper.FileAttributeID);
    if (dbAttribute != null)
    {
      this.UserSession.StartTransaction();
      try
      {
        int index1 = this.Index;
        this.Assign(dbAttribute);
        for (int index2 = 0; index2 < this.ValuesCount; ++index2)
        {
          this.Index = index2;
          dbAttribute.Index = index2;
          if (dbAttribute.AsString.Trim() == string.Empty)
            throw new KernelExceptionID(sc_12507.ssp_appserver_12522(1091863318), (object) dbAttribute.DBObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbAttribute.DBObjectID));
          this.SetDefaultFileName(dbAttribute);
        }
        this.UserSession.Commit();
        this.Index = index1;
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    return numArray;
  }

  public override int DeleteValue()
  {
    bool validatingOn = this.ValidatingOn;
    BlobInformation blobInformation = ((IBlobReader) this).OpenBlob(-1);
    if (blobInformation.FileType != FileTypes.ftNormal)
    {
      this.ValidatingOn = false;
      try
      {
        this.CheckAccess(ActionType.Write);
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted);
      }
      catch
      {
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
        throw;
      }
      if (blobInformation.FileType == FileTypes.ftAuthentical)
        this.CheckLCStepAccess(ActionType.EditAuthenticalFiles, true);
    }
    try
    {
      return base.DeleteValue();
    }
    finally
    {
      this.ValidatingOn = validatingOn;
    }
  }

  public override int AddValue(object newValue)
  {
    this.CheckForClosed();
    this._FileStruct = (FileInfoStruct) null;
    bool validatingOn = this.ValidatingOn;
    if (newValue is FileTypes && Convert.ToInt32(newValue) != 0)
    {
      this.ValidatingOn = false;
      try
      {
        this.CheckAccess(ActionType.Write);
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted);
      }
      catch
      {
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
        throw;
      }
      if (Convert.ToInt32(newValue) == 4)
        this.CheckLCStepAccess(ActionType.EditAuthenticalFiles, true);
      this._FileStruct = new FileInfoStruct();
      this._FileStruct.FileType = (FileTypes) newValue;
    }
    try
    {
      int num = base.AddValue(newValue);
      this._FileStruct = (FileInfoStruct) null;
      return num;
    }
    finally
    {
      this.ValidatingOn = validatingOn;
    }
  }

  public FileTypes FileType
  {
    get
    {
      if (this.BlobState == BlobAttributeStates.Closed)
        return ((IBlobReader) this).OpenBlob(-1).FileType;
      return this._FileStruct != null ? this._FileStruct.FileType : throw new KernelException("Ошибка получения типа файла - пустая структура FileStruct у открытого блоба.");
    }
  }

  public BlobInformation[] GetBlobInformation()
  {
    this.CheckForClosed();
    BlobInformation[] blobInformation = new BlobInformation[this.ValuesCount];
    int index1 = this.Index;
    try
    {
      for (int index2 = 0; index2 < this.ValuesCount; ++index2)
      {
        this.Index = index2;
        blobInformation[index2] = ((IBlobReader) this).OpenBlob(-1);
      }
    }
    finally
    {
      this.Index = index1;
    }
    return blobInformation;
  }

  [SpecialName]
  FieldTypes IDBAttribute.get_DataType() => this.DataType;
}
