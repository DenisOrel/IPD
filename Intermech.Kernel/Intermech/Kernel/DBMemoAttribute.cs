// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMemoAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBMemoAttribute : DBLobAttribute, IMemoReader, IMemoWriter
{
  private char[] _Buffer;

  public DBMemoAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._BlobTableName = "IMS_MEMOS";
  }

  public DBMemoAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
    this._BlobTableName = "IMS_MEMOS";
  }

  protected override int DoDelete()
  {
    int index1 = this.Index;
    for (int index2 = 0; index2 < this.ValuesCount; ++index2)
    {
      this.Index = 0;
      this.WriteToGlobalIndex((object) DBNull.Value);
    }
    this.Index = index1;
    return base.DoDelete();
  }

  protected override void DoDeleteValue()
  {
    this.WriteToGlobalIndex((object) DBNull.Value);
    base.DoDeleteValue();
  }

  public override bool IsNull
  {
    get
    {
      if (!this.TemporaryAttribute)
        return this._ValuesTable[this.Index]["F_STRING_VALUE"] == DBNull.Value;
      return this._Buffer == null || this._Buffer.Length == 0;
    }
  }

  protected override void DoClear()
  {
    if (!this.TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_STRING_VALUE = NULL WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("inlistID", (object) this.Index));
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_MEMOS SET F_VALUE = NULL WHERE F_KEY = :fkeyID", this.UserSession.DataManager.Parameter("fkeyID", (object) this.AsInteger));
      this.UpdateObjectModifyDate();
      this.WriteToGlobalIndex((object) string.Empty);
    }
    this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) DBNull.Value;
    this._Buffer = new char[0];
    this._CurrentBlobSize = 0;
    this._BlobPosition = 0;
    this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
    this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
  }

  public override string AsString
  {
    set
    {
      if (value.Length == 0)
      {
        this.Clear();
      }
      else
      {
        if (!((IMemoWriter) this).OpenMemo(value.Length))
          return;
        this.WriteDataBlock(value.ToCharArray());
        this.CloseMemo();
      }
    }
  }

  public override object Value
  {
    get
    {
      try
      {
        if (((IMemoReader) this).OpenMemo(0) <= 0)
          return (object) string.Empty;
        char[] chArray = this.ReadDataBlock();
        this.CloseMemo();
        return (object) new string(chArray);
      }
      finally
      {
        this.CloseMemo();
      }
    }
    set => this.AsString = Convert.ToString(value);
  }

  public void CloseMemo()
  {
    this.BlobState = BlobAttributeStates.Closed;
    if (this.TemporaryAttribute)
      return;
    this._Buffer = (char[]) null;
  }

  public char[] ReadDataBlock() => this.ReadDataBlock(this._DataBlockSize);

  public char[] ReadDataBlock(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForRead)
      throw new KernelExceptionID(sc_12534.ssp_appserver_12535(1864827735));
    if (dataBlockSize == 0)
      dataBlockSize = this._DataBlockSize;
    if (dataBlockSize > this._CurrentBlobSize - this._BlobPosition)
    {
      dataBlockSize = this._CurrentBlobSize - this._BlobPosition;
      this.BlobState = BlobAttributeStates.Closed;
    }
    char[] chArray = new char[dataBlockSize];
    if (dataBlockSize == 0)
    {
      this.BlobState = BlobAttributeStates.Closed;
    }
    else
    {
      int index = 0;
      while (index < dataBlockSize)
        chArray[index] = this._Buffer[this._BlobPosition + index++];
      this._BlobPosition += dataBlockSize;
    }
    return chArray;
  }

  int IMemoReader.OpenMemo(int dataBlockSize)
  {
    this.CheckForClosed();
    if (dataBlockSize < 0)
      throw new KernelException(sc_12534.ssp_appserver_12536());
    if (dataBlockSize > 0)
      this._DataBlockSize = dataBlockSize;
    this._BlobPosition = 0;
    if (!this.TemporaryAttribute)
    {
      object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_MEMOS WHERE F_KEY = :keyID AND F_VALUE IS NOT NULL", this.UserSession.DataManager.Parameter("keyID", (object) this.AsInteger));
      if (obj != null && obj != DBNull.Value)
      {
        string str = obj.ToString();
        this._Buffer = str.ToCharArray();
        this._CurrentBlobSize = str.Length;
        this.BlobState = BlobAttributeStates.OpenedForRead;
        if (dataBlockSize == 0)
          this._DataBlockSize = this._CurrentBlobSize;
      }
      else
        this._CurrentBlobSize = 0;
    }
    else if (this._CurrentBlobSize > 0)
    {
      this.BlobState = BlobAttributeStates.OpenedForRead;
      if (dataBlockSize == 0)
        this._DataBlockSize = this._CurrentBlobSize;
    }
    return this._CurrentBlobSize;
  }

  public bool WriteDataBlock(char[] data)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForWrite)
      throw new KernelExceptionID(sc_12534.ssp_appserver_12537(1234036007));
    if (data.Length + this._BlobPosition > this._CurrentBlobSize)
      throw new KernelExceptionID(sc_12534.ssp_appserver_12538(2081902256));
    for (int index = 0; index < data.Length; ++index)
      this._Buffer[this._BlobPosition++] = (char) (int) data[index];
    if (this._BlobPosition != this._CurrentBlobSize)
      return true;
    if (!this.TemporaryAttribute)
    {
      this.UserSession.StartTransaction();
      try
      {
        this.BlobState = BlobAttributeStates.Closed;
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(this._Buffer.Length))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(this._Buffer, 0, this._Buffer.Length);
          int int32 = Convert.ToInt32(this.AsInteger);
          if (int32 == 0)
          {
            int32 = Convert.ToInt32(this.GetDefaultValue());
            this.DirectSetValue("F_INTEGER_VALUE", (object) int32);
          }
          this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_MEMOS SET F_VALUE = :value WHERE F_KEY = :id", this.UserSession.DataManager.Parameter("id", (object) int32), this.UserSession.DataManager.Parameter("value", (object) stringBuilder.ToString()));
          if (this._Buffer.Length > Consts.MaxStringSize)
            base.AsString = stringBuilder.ToString().Substring(0, Consts.MaxStringSize);
          else
            base.AsString = stringBuilder.ToString();
          this.UpdateObjectModifyDate();
          this.WriteToGlobalIndex((object) stringBuilder.ToString());
        }
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
    else
    {
      int num = Consts.MaxStringSize;
      if (this._Buffer.Length < num)
        num = this._Buffer.Length;
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(num))
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append(this._Buffer, 0, num);
        this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) stringBuilder.ToString();
      }
      this.BlobState = BlobAttributeStates.Closed;
      this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
    }
    return false;
  }

  bool IMemoWriter.OpenMemo(int memoSize)
  {
    if (!this.ValidateDirectWrite((object) null))
      return false;
    if (memoSize == 0)
    {
      this.Clear();
      return false;
    }
    if (memoSize < 0)
      throw new KernelException(sc_12534.ssp_appserver_12539());
    if ((long) memoSize > this.AttributeType.SizeType)
      throw new KernelExceptionID(sc_12534.ssp_appserver_12540(378733969), (object) memoSize, (object) this.Name, (object) this.AttributeType.SizeType);
    this._Buffer = new char[memoSize];
    this._CurrentBlobSize = memoSize;
    this._BlobPosition = 0;
    this.BlobState = BlobAttributeStates.OpenedForWrite;
    return true;
  }

  public void CancelWrite() => this.CloseMemo();

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
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

  protected override bool IsNullValue(object newValue)
  {
    return base.IsNullValue(newValue) || newValue.ToString() == string.Empty;
  }

  public override DateTime AsDateTime
  {
    set => throw new OperationNotApplicableException();
  }

  protected override void SetDefaultValue(object defValue)
  {
    switch (defValue)
    {
      case long _:
      case int _:
        this.DirectSetValue("F_INTEGER_VALUE", defValue);
        break;
      default:
        base.SetDefaultValue(defValue);
        if (this.AttributeType.Computed != ComputeValueModes.StoredValue)
          break;
        object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
        if (calculatedValue == null || calculatedValue == DBNull.Value)
          break;
        this.SetCalculatedValue((object) Convert.ToString(calculatedValue), true);
        break;
    }
  }
}
