// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportBlob
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportBlob
{
  private UserSession _session;
  private bool _hintAppendEnable;
  private ICollection<string> _warnings;
  private static readonly ICollection<string> EmptyWarnings = (ICollection<string>) new string[0];

  public ImportBlob(UserSession session, bool hintAppendEnable)
  {
    this._session = session;
    this._hintAppendEnable = hintAppendEnable;
    this._warnings = ImportBlob.EmptyWarnings;
  }

  private void AddWarning(string text)
  {
    if (string.IsNullOrEmpty(text))
      return;
    if (this._warnings == ImportBlob.EmptyWarnings)
      this._warnings = (ICollection<string>) new List<string>();
    this._warnings.Add(text);
  }

  public ICollection<string> GetWarnings()
  {
    return this._warnings != ImportBlob.EmptyWarnings ? (ICollection<string>) new List<string>((IEnumerable<string>) this._warnings) : ImportBlob.EmptyWarnings;
  }

  private void ValidateBlobAttributeRecord(
    long attributableID,
    AttributeRecord attrRec,
    FieldTypes type,
    bool isNewBlob)
  {
    if (isNewBlob)
    {
      if (attrRec.IntegerValue != null && (long) attrRec.IntegerValue == 0L)
        return;
      this.AddWarning($"Некорректно инициализирован идентификатор у нового блоба. Он не должен быть равен {attrRec.IntegerValue} (требуется значение {0L}).");
    }
    else
    {
      if (attrRec.IntegerValue != null)
        return;
      this.AddWarning($"Некорректно инициализирован идентификатор у существующего блоба. Он не должен быть равен {attrRec.IntegerValue}.");
    }
  }

  private string StreamToMemo(Stream stream)
  {
    using (StreamReader streamReader = new StreamReader(stream, BriefcaseConsts.MemoEncoding))
      return streamReader.ReadToEnd();
  }

  public long Import(
    long attributableID,
    AttributeRecord attrRec,
    FieldTypes type,
    bool isNewBlob)
  {
    this.ValidateBlobAttributeRecord(attributableID, attrRec, type, isNewBlob);
    attrRec.FileSize = (object) Convert.ToInt64(attrRec.FileSize);
    bool flag1 = (long) attrRec.FileSize <= 0L;
    long num1 = 0;
    if (attrRec.StringValue == null)
      attrRec.StringValue = (object) string.Empty;
    if (attrRec.FileNote == null)
      attrRec.FileNote = (object) string.Empty;
    if (attrRec.DateValue == null)
      attrRec.DateValue = (object) DateTime.UtcNow;
    switch (type)
    {
      case FieldTypes.ftShortBlob:
        List<byte> byteList = new List<byte>();
        if (!flag1)
        {
          num1 = new FileInfo(attrRec.Path2File).Length;
          using (FileStream input = new FileStream(attrRec.Path2File, FileMode.Open, FileAccess.Read))
          {
            using (BinaryReader binaryReader = new BinaryReader((Stream) input))
            {
              for (byte[] collection = binaryReader.ReadBytes(Consts.BlobTransferBufferLength); collection.Length != 0; collection = binaryReader.ReadBytes(Consts.BlobTransferBufferLength))
                byteList.AddRange((IEnumerable<byte>) collection);
            }
          }
        }
        IDbDataParameter dbDataParameter1 = this._session.DataManager.Parameter("val1", (object) new DbTypedValue((object) byteList.ToArray(), DbType.Binary));
        IDbDataParameter dbDataParameter2 = this._session.DataManager.Parameter("date1", attrRec.DateValue);
        if ((DateTime) attrRec.DateValue == DateTime.MinValue)
          dbDataParameter2.Value = (object) DateTime.UtcNow;
        bool flag2 = true;
        if (!isNewBlob)
        {
          if (Convert.ToInt32(this._session.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_BLOBS WHERE F_KEY = :key1", this._session.DataManager.Parameter("key1", attrRec.IntegerValue))) > 0)
            flag2 = false;
        }
        if (flag2)
        {
          if ((long) attrRec.IntegerValue != 0L)
          {
            DBHelper.ExecuteNonQuery((IUserSession) this._session, (this._hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_BLOBS (F_KEY, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) VALUES (:key1, :val1, :fsize1, :date1, :arc1, :zip1)", dbDataParameter1, this._session.DataManager.Parameter("fsize1", attrRec.FileSize), dbDataParameter2, this._session.DataManager.Parameter("arc1", (object) Convert.ToInt32(attrRec.ArcMethod)), this._session.DataManager.Parameter("zip1", (object) num1), this._session.DataManager.Parameter("key1", attrRec.IntegerValue));
          }
          else
          {
            long num2;
            if (this._session.DataManager.DataProvider.Name != "Sql")
            {
              num2 = this._session.DataManager.DataProvider.NextGeneratorValue("IMS_BLOBS_GEN", this._session.DataManager);
              DBHelper.ExecuteNonQuery((IUserSession) this._session, (this._hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_BLOBS (F_KEY, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) VALUES (:key1, :val1, :fsize1, :date1, :arc1, :zip1)", dbDataParameter1, this._session.DataManager.Parameter("fsize1", attrRec.FileSize), dbDataParameter2, this._session.DataManager.Parameter("arc1", (object) Convert.ToInt32(attrRec.ArcMethod)), this._session.DataManager.Parameter("zip1", (object) num1), this._session.DataManager.Parameter("key1", (object) num2));
            }
            else
            {
              this._session.DataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) VALUES (:val1, :fsize1, :date1, :arc1, :zip1)", dbDataParameter1, this._session.DataManager.Parameter("fsize1", attrRec.FileSize), dbDataParameter2, this._session.DataManager.Parameter("arc1", (object) Convert.ToInt32(attrRec.ArcMethod)), this._session.DataManager.Parameter("zip1", (object) num1));
              num2 = Convert.ToInt64(this._session.DataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
            }
            return num2;
          }
        }
        else
          this._session.DataManager.ExecuteNonQuery("UPDATE IMS_BLOBS SET F_VALUE = :val1, F_FILESIZE = :fsize1, F_FILEDATE = :date1, F_ARC_METHOD = :arc1, F_ZIPSIZE = :zip1 WHERE F_KEY = :key1", dbDataParameter1, this._session.DataManager.Parameter("fsize1", attrRec.FileSize), dbDataParameter2, this._session.DataManager.Parameter("arc1", (object) Convert.ToInt32(attrRec.ArcMethod)), this._session.DataManager.Parameter("zip1", (object) num1), this._session.DataManager.Parameter("key1", attrRec.IntegerValue));
        return (long) attrRec.IntegerValue;
      case FieldTypes.ftFile:
      case FieldTypes.ftBlob:
        IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
        IBlobStorage storage = service.GetStorage(service.GetActiveStorageID((IUserSession) this._session), (IUserSession) this._session);
        attrRec.DoubleValue = (object) storage.StorageID;
        FileInfoStruct fileInfoStruct = new FileInfoStruct();
        fileInfoStruct.IsolatedCacheMode = false;
        try
        {
          if (flag1)
          {
            fileInfoStruct.FileBody = (Stream) null;
          }
          else
          {
            num1 = new FileInfo(attrRec.Path2File).Length;
            FileStream fileStream = new FileStream(attrRec.Path2File, FileMode.Open, FileAccess.Read);
            fileInfoStruct.FileBody = (Stream) fileStream;
            fileInfoStruct.IsolatedFileName = attrRec.Path2File;
          }
          fileInfoStruct.ArcMethod = attrRec.ArcMethod != null ? (ArcMethods) attrRec.ArcMethod : ArcMethods.NotPacked;
          fileInfoStruct.FileName = type == FieldTypes.ftFile ? attrRec.StringValue.ToString() : string.Empty;
          fileInfoStruct.FileID = (long) attrRec.IntegerValue != 0L ? (long) attrRec.IntegerValue : this._session.DataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", this._session.DataManager);
          fileInfoStruct.ModifyDate = attrRec.DateValue != null ? (DateTime) attrRec.DateValue : DateTime.UtcNow;
          fileInfoStruct.Note = type == FieldTypes.ftFile ? attrRec.FileNote.ToString() : attrRec.StringValue.ToString();
          fileInfoStruct.ObjectLinkID = attributableID;
          fileInfoStruct.AttributeID = attrRec.AttributeId;
          fileInfoStruct.PacketFileSize = num1;
          fileInfoStruct.RealFileSize = attrRec.FileSize != null ? (long) attrRec.FileSize : 0L;
          fileInfoStruct.FileType = attrRec.FileType != null ? (FileTypes) attrRec.FileType : FileTypes.ftNormal;
          fileInfoStruct.Author = attrRec.FileAuthor != null ? (long) attrRec.FileAuthor : 0L;
          if (!flag1)
            storage.CopyToTemporaryFile(fileInfoStruct);
          if (isNewBlob)
            storage.SetNewFileStruct(fileInfoStruct);
          else
            storage.SetFileStruct(fileInfoStruct);
        }
        finally
        {
          if (fileInfoStruct.FileBody != null)
            fileInfoStruct.FileBody.Close();
          service.ReleaseStorage(storage);
        }
        return fileInfoStruct.FileID;
      case FieldTypes.ftMemo:
        string str = string.Empty;
        if (!flag1)
        {
          using (FileStream inStream = new FileStream(attrRec.Path2File, FileMode.Open, FileAccess.Read))
          {
            if (attrRec.ArcMethod != null && (ArcMethods) attrRec.ArcMethod == ArcMethods.ZLibPacked)
            {
              using (MemoryStream outStream = new MemoryStream())
              {
                ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
                outStream.Position = 0L;
                str = this.StreamToMemo((Stream) outStream);
              }
            }
            else
            {
              inStream.Position = 0L;
              str = this.StreamToMemo((Stream) inStream);
            }
            if (str != string.Empty)
            {
              if (CompareValuesHelper.NormalizedValue(attrRec.StringValue) == null)
                attrRec.StringValue = str.Length <= Consts.MaxMemoStringValueSize ? (object) str : (object) str.Substring(0, Consts.MaxMemoStringValueSize);
            }
          }
        }
        else if (attrRec.StringValue != null)
          str = Convert.ToString(attrRec.StringValue);
        bool flag3 = true;
        if (!isNewBlob)
        {
          if (Convert.ToInt32(this._session.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_MEMOS WHERE F_KEY = :id1", this._session.DataManager.Parameter("id1", attrRec.IntegerValue))) > 0)
            flag3 = false;
        }
        if (flag3)
        {
          if ((long) attrRec.IntegerValue != 0L)
          {
            DBHelper.ExecuteNonQuery((IUserSession) this._session, (this._hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_MEMOS(F_VALUE, F_KEY) VALUES (:value1,:id1)", this._session.DataManager.Parameter("id1", attrRec.IntegerValue), this._session.DataManager.Parameter("value1", (object) str));
          }
          else
          {
            long num3;
            if (this._session.DataManager.DataProvider.Name != "Sql")
            {
              num3 = this._session.DataManager.DataProvider.NextGeneratorValue("IMS_MEMOS_GEN", this._session.DataManager);
              DBHelper.ExecuteNonQuery((IUserSession) this._session, (this._hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_MEMOS(F_VALUE, F_KEY) VALUES (:value1,:id1)", this._session.DataManager.Parameter("id1", (object) num3), this._session.DataManager.Parameter("value1", (object) str));
            }
            else
            {
              this._session.DataManager.ExecuteNonQuery("INSERT INTO IMS_MEMOS(F_VALUE) VALUES (:value1)", this._session.DataManager.Parameter("value1", (object) str));
              num3 = Convert.ToInt64(this._session.DataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
            }
            return num3;
          }
        }
        else
          this._session.DataManager.ExecuteNonQuery("UPDATE IMS_MEMOS SET F_VALUE = :value1 WHERE F_KEY = :id1", this._session.DataManager.Parameter("id1", attrRec.IntegerValue), this._session.DataManager.Parameter("value1", (object) str));
        return (long) attrRec.IntegerValue;
      default:
        return 0;
    }
  }

  public static string GetImportingBlobPath(
    long AttributableID,
    int AttributeID,
    long IntVAlue,
    string BriefcasePath,
    FieldTypes Type)
  {
    switch (Type)
    {
      case FieldTypes.ftShortBlob:
        BriefcasePath = Path.Combine(BriefcasePath, "ShortBlob");
        break;
      case FieldTypes.ftFile:
      case FieldTypes.ftBlob:
        BriefcasePath = Path.Combine(BriefcasePath, "Blob");
        break;
      case FieldTypes.ftMemo:
        BriefcasePath = Path.Combine(BriefcasePath, "Memo");
        break;
    }
    FileInfo fileInfo = new FileInfo(Type == FieldTypes.ftMemo ? BriefcaseBlobs.GetMemoFileName(AttributableID, AttributeID, IntVAlue, BriefcasePath, false) : BriefcaseBlobs.GetBlobFileName(AttributableID, AttributeID, IntVAlue, BriefcasePath, false));
    return !fileInfo.Exists ? (string) null : fileInfo.FullName;
  }
}
