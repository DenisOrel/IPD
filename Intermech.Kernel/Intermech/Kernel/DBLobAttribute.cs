// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLobAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBLobAttribute : DBAdditionalAttribute
{
  protected string _BlobTableName;
  private BlobAttributeStates _BlobState;
  protected int _CurrentBlobSize;
  protected int _BlobPosition;
  protected int _DataBlockSize;

  public DBLobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._AutoSaveHistory = false;
  }

  public DBLobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
    this._AutoSaveHistory = false;
  }

  public BlobAttributeStates BlobState
  {
    get => this._BlobState;
    set
    {
      if (this._BlobState == value)
        return;
      this._BlobState = value == BlobAttributeStates.Closed || this._BlobState == BlobAttributeStates.Closed ? value : throw new KernelExceptionID(sc_12527.ssp_appserver_12528(1757132439));
    }
  }

  protected void CheckForClosed()
  {
    if (this.BlobState != BlobAttributeStates.Closed)
    {
      string str = this.BlobState != BlobAttributeStates.OpenedForRead ? LocalizationHolder.rm.GetString("Kernel_241") : LocalizationHolder.rm.GetString("Kernel_240");
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12527.ssp_appserver_12529()), (object) this.Name, (object) str));
    }
  }

  protected override object GetDefaultValue()
  {
    long defaultValue = 0;
    if (this.TemporaryAttribute)
      return (object) 1;
    IDbManager dataManager = this.UserSession.DataManager;
    if (dataManager.DataProvider.Name != "Sql")
    {
      defaultValue = dataManager.DataProvider.NextGeneratorValue(this._BlobTableName + "_GEN", dataManager);
      dataManager.ExecuteNonQuery($"INSERT INTO {this._BlobTableName} (F_KEY) VALUES ({defaultValue})");
    }
    else
    {
      using (dataManager.WithOpenConnection())
      {
        dataManager.ExecuteNonQuery($"INSERT INTO {this._BlobTableName} (F_VALUE) VALUES (NULL)");
        defaultValue = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
      }
    }
    return (object) defaultValue;
  }

  protected virtual void UpdateObjectModifyDate()
  {
    this._AutoSaveHistory = true;
    this.AsDateTime = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
    this._AutoSaveHistory = false;
    if (!this.IsObjectAttribute)
      return;
    this.SetContentDate();
  }

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    this.SetCalculatedValue((object) Convert.ToInt64(defValue), true);
  }

  private void DeleteInBlobs()
  {
    if (this.TemporaryAttribute)
      return;
    this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._BlobTableName} WHERE F_KEY = :blobID1", this.UserSession.DataManager.Parameter("blobID1", (object) this.AsInteger));
  }

  protected override void DoDeleteValue()
  {
    this.DeleteInBlobs();
    base.DoDeleteValue();
  }

  private void DeleteAllInBlobs()
  {
    for (int index = 0; index < this.ValuesCount; ++index)
    {
      this.Index = index;
      this.DeleteInBlobs();
    }
  }

  protected override int DoDelete()
  {
    this.DeleteAllInBlobs();
    return base.DoDelete();
  }

  internal override void Purge(bool purgeOwner)
  {
    this.DeleteAllInBlobs();
    base.Purge(purgeOwner);
  }

  public override bool AsBoolean
  {
    set => throw new OperationNotApplicableException();
  }

  public override double AsDouble
  {
    set => throw new OperationNotApplicableException();
  }

  public override long AsInteger
  {
    get => base.AsInteger;
    set => throw new OperationNotApplicableException();
  }

  public override object Value
  {
    get => (object) this.AsString;
    set => throw new OperationNotApplicableException();
  }

  protected override string GetDescription() => this.AsString;
}
