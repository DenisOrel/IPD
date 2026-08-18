// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBShortBlobAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.IO;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;


namespace Intermech.Kernel;

internal class DBShortBlobAttribute : DBLobAttribute, IBlobWriter, IBlobReader, IDBShortBlobAttribute
{
  private byte[] _Buffer;
  private BlobInformation _BlobInfo;

  public DBShortBlobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._BlobTableName = "IMS_BLOBS";
  }

  public DBShortBlobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
    if (temporary)
      throw new KernelExceptionID(sc_12555.ssp_appserver_12556(1683230908), (object) AttributesTypeHelper.GetCaption(this.AttributeType.AttributeType));
    this._BlobTableName = "IMS_BLOBS";
  }

  protected override void UpdateObjectModifyDate()
  {
    if (!(this._BlobInfo.ModifyDate > DateTime.MinValue))
      return;
    base.AsDateTime = this._BlobInfo.ModifyDate;
  }

  public override bool IsNull
  {
    get
    {
      object obj = this.UserSession.DataManager.ExecuteScalar($"SELECT F_KEY FROM IMS_BLOBS WHERE F_KEY = {this.AsInteger} AND F_VALUE IS NULL");
      return obj != null && obj != DBNull.Value;
    }
  }

  protected override void DoClear()
  {
    if (!this.TemporaryAttribute)
    {
      IDbManager dataManager = this.UserSession.DataManager;
      dataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_STRING_VALUE = NULL WHERE {this.ValuesKeyName} = {this.DBObjectID} AND F_ATTRIBUTE_ID = {this.AttributeID} AND F_INLIST_ID = {this.Index}");
      this._BlobInfo.ModifyDate = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
      dataManager.ExecuteNonQuery($"UPDATE IMS_BLOBS SET F_VALUE = NULL, F_FILESIZE = 0, F_ZIPSIZE = 0, F_ARC_METHOD = 0, F_FILEDATE = :dt WHERE F_KEY = {this.AsInteger}", dataManager.Parameter("dt", (object) this._BlobInfo.ModifyDate));
      this.UpdateObjectModifyDate();
    }
    this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) DBNull.Value;
    this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
  }

  public void CancelWrite() => this.CloseBlob();

  public bool WriteDataBlock(byte[] data) => this.WriteDataBlockEx(data, 0, data.Length);

  public bool WriteDataBlockEx(byte[] data, int index, int length)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForWrite)
      throw new KernelExceptionID(sc_12555.ssp_appserver_12557(1056226483));
    if ((long) (length + this._BlobPosition) > this._BlobInfo.PackedFileSize)
      throw new KernelExceptionID(sc_12555.ssp_appserver_12558(2144784121));
    for (int index1 = index; index1 < index + length; ++index1)
      this._Buffer[this._BlobPosition++] = (byte) (int) data[index1];
    if ((long) this._BlobPosition != this._BlobInfo.PackedFileSize)
      return true;
    this.WriteBlobInfoData();
    return false;
  }

  private void SetNote(string note)
  {
    if (note == null)
      base.AsString = string.Empty;
    else if (note.Length > Intermech.Consts.MaxNoteLength)
      throw new KernelExceptionID(412, (object) this.Name, (object) Intermech.Consts.MaxNoteLength);
    base.AsString = note;
  }

  private void WriteBlobInfoData()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      this.SetNote(this._BlobInfo.Note);
      if (this._Buffer != null)
      {
        IDbDataParameter dbDataParameter1 = dataManager.Parameter("val", (object) new DbTypedValue((object) this._Buffer, DbType.Binary));
        IDbDataParameter dbDataParameter2 = dataManager.Parameter("date1", (object) this._BlobInfo.ModifyDate);
        if (this._BlobInfo.ModifyDate == DateTime.MinValue)
          dbDataParameter2.Value = (object) DateTime.UtcNow;
        else
          dbDataParameter2 = dataManager.Parameter("date1", (object) (this._BlobInfo.ModifyDate - this.UserSession.TimeZoneOffset));
        dataManager.ExecuteNonQuery("UPDATE IMS_BLOBS SET F_VALUE = :val, F_FILESIZE = :fsize, F_FILEDATE = :date1, F_ARC_METHOD = :arc, F_ZIPSIZE = :zip WHERE F_KEY = :key1", dbDataParameter1, dataManager.Parameter("fsize", (object) this._BlobInfo.RealFileSize), dbDataParameter2, dataManager.Parameter("arc", (object) Convert.ToInt32((object) this._BlobInfo.ArcMethod)), dataManager.Parameter("zip", (object) this._BlobInfo.PackedFileSize), dataManager.Parameter("key1", (object) this.AsInteger));
        this.UpdateObjectModifyDate();
        this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
        this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
      }
      this.CloseBlob();
      this.UserSession.Commit();
    }
    catch
    {
      this.CloseBlob();
      this.UserSession.Rollback();
      throw;
    }
  }

  public bool OpenBlob(BlobInformation blobInfo, bool onlyInfo)
  {
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
    this.CheckForClosed();
    if (!this.ValidateDirectWrite((object) null))
      return false;
    if (blobInfo.PackedFileSize > this.AttributeType.SizeType)
      throw new KernelExceptionID(sc_12555.ssp_appserver_12559(534977511), (object) this.AttributeType.SizeType, (object) this.AttributeType.Name);
    this._BlobInfo = blobInfo;
    if (onlyInfo)
    {
      this._Buffer = (byte[]) null;
      this.WriteBlobInfoData();
      return false;
    }
    if (blobInfo.PackedFileSize == 0L)
    {
      this._Buffer = new byte[0];
      this.WriteBlobInfoData();
      return false;
    }
    this._Buffer = new byte[blobInfo.PackedFileSize];
    this._BlobPosition = 0;
    this.BlobState = BlobAttributeStates.OpenedForWrite;
    return true;
  }

  public void CloseBlob()
  {
    this.BlobState = BlobAttributeStates.Closed;
    if (this.TemporaryAttribute)
      return;
    this._Buffer = (byte[]) null;
  }

  public byte[] ReadDataBlock() => this.ReadDataBlock(0);

  public byte[] ReadDataBlock(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForRead)
      throw new KernelExceptionID(sc_12555.ssp_appserver_12560(1767567724));
    if (dataBlockSize == 0)
      dataBlockSize = this._DataBlockSize;
    if (dataBlockSize >= this._CurrentBlobSize - this._BlobPosition)
    {
      dataBlockSize = this._CurrentBlobSize - this._BlobPosition;
      this.BlobState = BlobAttributeStates.Closed;
    }
    byte[] numArray = new byte[dataBlockSize];
    if (dataBlockSize == 0)
    {
      this.BlobState = BlobAttributeStates.Closed;
    }
    else
    {
      int index = 0;
      while (index < dataBlockSize)
        numArray[index] = this._Buffer[this._BlobPosition + index++];
      this._BlobPosition += dataBlockSize;
    }
    return numArray;
  }

  private void ReadBlobFunc(IDataReader reader, ExecuteReaderArgs args)
  {
    BlobInformation blobInformation = new BlobInformation();
    if (reader.Read())
    {
      Tuple<byte, int> inputParam = args.InputParam as Tuple<byte, int>;
      if (inputParam.Item1 > (byte) 0)
      {
        if (!reader.IsDBNull(0))
        {
          if (this._Buffer == null || this._Buffer.Length < Intermech.Consts.MaxShortBlobSize)
            this._Buffer = new byte[Intermech.Consts.MaxShortBlobSize];
          this._CurrentBlobSize = Convert.ToInt32(reader.GetBytes(0, 0L, this._Buffer, 0, this._Buffer.Length));
          if (this._CurrentBlobSize > 0 && inputParam.Item2 == 0)
            this._DataBlockSize = this._CurrentBlobSize;
        }
        else
        {
          this._Buffer = new byte[0];
          this._CurrentBlobSize = 0;
        }
        blobInformation.RealFileSize = Convert.ToInt64(reader.GetValue((int) inputParam.Item1));
      }
      else
        blobInformation.RealFileSize = Convert.ToInt64(reader.GetValue((int) inputParam.Item1));
      if (!reader.IsDBNull((int) inputParam.Item1 + 1))
        blobInformation.ModifyDate = reader.GetDateTime((int) inputParam.Item1 + 1);
      int int32 = Convert.ToInt32(reader.GetValue((int) inputParam.Item1 + 2));
      blobInformation.ArcMethod = (ArcMethods) int32;
      blobInformation.PackedFileSize = Convert.ToInt64(reader.GetValue((int) inputParam.Item1 + 3));
      blobInformation.FileName = string.Empty;
      blobInformation.BlobID = this.AsInteger;
      blobInformation.Note = base.AsString;
    }
    else
      this._CurrentBlobSize = 0;
    args.Result = (object) blobInformation;
  }

  BlobInformation IBlobReader.OpenBlob(int dataBlockSize)
  {
    this.CheckForClosed();
    string str;
    byte num;
    if (dataBlockSize < 0)
    {
      str = "";
      num = (byte) 0;
      this.BlobState = BlobAttributeStates.Closed;
    }
    else
    {
      str = "F_VALUE,";
      num = (byte) 1;
      this.BlobState = BlobAttributeStates.OpenedForRead;
    }
    if (dataBlockSize > 0)
      this._DataBlockSize = dataBlockSize;
    this._BlobPosition = 0;
    long asInteger = this.AsInteger;
    ExecuteReaderArgs args = new ExecuteReaderArgs((object) new Tuple<byte, int>(num, dataBlockSize));
    this.UserSession.DataManager.ExecuteReader($"SELECT {str} F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE FROM IMS_BLOBS WHERE F_KEY = :fkeyID", new ExecuteReaderDelegate(this.ReadBlobFunc), args, this.UserSession.DataManager.Parameter("fkeyID", (object) asInteger));
    return (BlobInformation) args.Result;
  }

  public override string AsString
  {
    get => base.AsString;
    set => throw new OperationNotApplicableException();
  }

  public override DateTime AsDateTime
  {
    set => throw new OperationNotApplicableException();
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
        break;
      case AttributeValueField.Double:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID2";
        break;
      case AttributeValueField.String:
        inViewFieldName = "F" + this.AttributeID.ToString();
        break;
      case AttributeValueField.Date:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID3";
        break;
      default:
        inViewFieldName = string.Empty;
        break;
    }
    return inViewFieldName;
  }

  public override object Value
  {
    set
    {
      if (value is ShortBlobValue)
        this.SetBlobValue(value as ShortBlobValue);
      else
        base.Value = value;
    }
  }

  public override void Assign(IDBAttribute sourceAttribute)
  {
    if (sourceAttribute.DataType == FieldTypes.ftShortBlob && !this.TemporaryAttribute && !sourceAttribute.TemporaryAttribute)
      this.SetBlobValues((sourceAttribute as IDBShortBlobAttribute).GetBlobValues());
    else
      base.Assign(sourceAttribute);
  }

  private void GetBlobFunc(IDataReader reader, ExecuteReaderArgs args)
  {
    ShortBlobValue shortBlobValue = new ShortBlobValue(this.AsInteger);
    if (reader.Read())
    {
      shortBlobValue.PackedFileSize = Convert.ToInt64(reader.GetValue(0));
      shortBlobValue.RealFileSize = Convert.ToInt64(reader.GetValue(1));
      if (!reader.IsDBNull(2))
        shortBlobValue.ModifyDate = reader.GetDateTime(2);
      int int32 = Convert.ToInt32(reader.GetValue(3));
      shortBlobValue.ArcMethod = (ArcMethods) int32;
      if (!reader.IsDBNull(4))
      {
        shortBlobValue.Value = new byte[shortBlobValue.PackedFileSize];
        reader.GetBytes(4, 0L, shortBlobValue.Value, 0, (int) shortBlobValue.PackedFileSize);
      }
      else
        shortBlobValue.Value = new byte[0];
      shortBlobValue.Note = base.AsString;
    }
    args.Result = (object) shortBlobValue;
  }

  public ShortBlobValue GetBlobValue()
  {
    this.MustBeClosed(MethodBase.GetCurrentMethod().Name);
    IDbManager dataManager = this.UserSession.DataManager;
    ExecuteReaderArgs args = new ExecuteReaderArgs((object) null);
    dataManager.ExecuteReader("SELECT F_ZIPSIZE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_VALUE FROM IMS_BLOBS WHERE F_KEY = :fkeyID", new ExecuteReaderDelegate(this.GetBlobFunc), args, dataManager.Parameter("fkeyID", (object) this.AsInteger));
    return (ShortBlobValue) args.Result;
  }

  public byte[] GetData()
  {
    ShortBlobValue blobValue = this.GetBlobValue();
    return blobValue.Empty ? new byte[0] : blobValue.Value;
  }

  public ImChunkedStream GetDataStream()
  {
    ImChunkedStream outStream = new ImChunkedStream();
    ShortBlobValue blobValue = this.GetBlobValue();
    if (!blobValue.Empty)
    {
      if (blobValue.ArcMethod == ArcMethods.NotPacked)
      {
        outStream.Write(blobValue.Value, 0, (int) blobValue.PackedFileSize);
      }
      else
      {
        Stream inStream = (Stream) new MemoryStream(blobValue.Value);
        try
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, inStream);
        }
        finally
        {
          inStream.Close();
          inStream.Dispose();
        }
      }
    }
    outStream.Position = 0L;
    return outStream;
  }

  private void MustBeClosed(string funcName)
  {
    if (this.BlobState != BlobAttributeStates.Closed)
      throw new KernelException(string.Format(sc_12555.ssp_appserver_12561(), (object) funcName));
    if (this.TemporaryAttribute)
      throw new KernelException(string.Format(sc_12555.ssp_appserver_12562(), (object) funcName, (object) this.Name));
  }

  public void SetBlobValue(ShortBlobValue blobValue)
  {
    this.MustBeClosed(MethodBase.GetCurrentMethod().Name);
    long asInteger = this.AsInteger;
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("val", (object) new DbTypedValue((object) blobValue.Value, DbType.Binary));
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("date1", (object) blobValue.ModifyDate);
      if (this._BlobInfo.ModifyDate == DateTime.MinValue)
        dbDataParameter2.Value = (object) DateTime.UtcNow;
      else
        dbDataParameter2 = dataManager.Parameter("date1", (object) (blobValue.ModifyDate - this.UserSession.TimeZoneOffset));
      dataManager.ExecuteNonQuery("UPDATE IMS_BLOBS SET F_VALUE = :val, F_FILESIZE = :fsize, F_FILEDATE = :date1, F_ARC_METHOD = :arc, F_ZIPSIZE = :zip WHERE F_KEY = :key1", dbDataParameter1, dataManager.Parameter("fsize", (object) blobValue.RealFileSize), dbDataParameter2, dataManager.Parameter("arc", (object) Convert.ToInt32((object) blobValue.ArcMethod)), dataManager.Parameter("zip", (object) blobValue.PackedFileSize), dataManager.Parameter("key1", (object) this.AsInteger));
      if (blobValue.ModifyDate > DateTime.MinValue)
        base.AsDateTime = blobValue.ModifyDate;
      this.SetNote(blobValue.Note);
      this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
      this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public ShortBlobValue[] GetBlobValues()
  {
    if (this.ValuesCount == 1)
      return new ShortBlobValue[1]{ this.GetBlobValue() };
    this.MustBeClosed(MethodBase.GetCurrentMethod().Name);
    IDbManager dataManager = this.UserSession.DataManager;
    ShortBlobValue[] blobValues = new ShortBlobValue[this.ValuesCount];
    IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[this.ValuesCount];
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.ValuesCount; ++index)
    {
      string parameterName = "fkeyID" + index.ToString();
      dbDataParameterArray[index] = dataManager.Parameter(parameterName, (object) Convert.ToInt64(this._ValuesTable[index]["F_INTEGER_VALUE"]));
      stringBuilder.Append($":{parameterName},");
    }
    --stringBuilder.Length;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_KEY, F_ZIPSIZE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_VALUE FROM IMS_BLOBS WHERE F_KEY IN ({stringBuilder.ToString()})", dbDataParameterArray);
    for (int index1 = 0; index1 < this.ValuesCount; ++index1)
    {
      for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
      {
        if (Convert.ToInt64(this._ValuesTable[index1]["F_INTEGER_VALUE"]) == Convert.ToInt64(dataTable.Rows[index2][0]))
        {
          DateTime modifyDate = dataTable.Rows[index2][3] != DBNull.Value ? Convert.ToDateTime(dataTable.Rows[index2][3]) : DateTime.MinValue;
          blobValues[index1] = new ShortBlobValue(Convert.ToInt64(dataTable.Rows[index2][2]), Convert.ToInt64(dataTable.Rows[index2][1]), modifyDate, (ArcMethods) Convert.ToInt32(dataTable.Rows[index2][4]), this._ValuesTable[index1]["F_STRING_VALUE"].ToString(), Convert.ToInt64(dataTable.Rows[index2][0]));
          blobValues[index1].Value = !(dataTable.Rows[index2][5] is byte[]) ? new byte[0] : dataTable.Rows[index2][5] as byte[];
          break;
        }
      }
    }
    return blobValues;
  }

  public void SetBlobValues(ShortBlobValue[] blobValues)
  {
    this.MustBeClosed(MethodBase.GetCurrentMethod().Name);
    this.Values = (object[]) blobValues;
  }
}
