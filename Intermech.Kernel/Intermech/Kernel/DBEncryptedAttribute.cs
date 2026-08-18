// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBEncryptedAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Data;
using System.Threading;


namespace Intermech.Kernel;

internal class DBEncryptedAttribute : DBAdditionalAttribute, IDBEncryptedAttribute
{
  private DateTime _LastValidatePasswordTime = DateTime.Now;
  private int _DelayValue;

  public DBEncryptedAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBEncryptedAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  private void MakeDelay()
  {
    Thread.Sleep(this._DelayValue * 3000);
    if (DateTime.Now - this._LastValidatePasswordTime < TimeSpan.FromMinutes(1.0))
      ++this._DelayValue;
    this._LastValidatePasswordTime = DateTime.Now;
  }

  public bool ValidateCurrent(string nowValue)
  {
    this.MakeDelay();
    return CryptHelper.IsPasswordEqual(nowValue, this.AsString);
  }

  private DataTable ValidatePswRules(string psw, string pswHash)
  {
    return this.AttributeID == this.UserSession.IdentHelper.PasswordID ? CryptHelper.ValidatePswRules((IUserSession) this.UserSession, psw, pswHash, this.DBObjectID) : (DataTable) null;
  }

  public void ValidateNew(string newValue)
  {
    char cryptMethod = Convert.ToChar(this.UserSession.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly));
    string pswHash = CryptHelper.CryptPassword(newValue, cryptMethod);
    this.ValidatePswRules(newValue, pswHash);
  }

  public override string AsString
  {
    get => base.AsString;
    set
    {
      char cryptMethod = Convert.ToChar(this.UserSession.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly));
      string str = CryptHelper.CryptPassword(value, cryptMethod);
      if (!(this.AsString != str))
        return;
      DataTable oldPasswordsTable = this.ValidatePswRules(value, str);
      if (this.AttributeID == this.UserSession.IdentHelper.PasswordID && this.TypeID == this.UserSession.IdentHelper.UsersTypeID && this.UserSession.UserID == this.DBObjectID)
      {
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("ChangePasswordEvent"), (object) this.UserSession.UserName));
        this.DirectSetValues((object) str, (object) null, (object) null, (object) (DateTime.UtcNow + this.UserSession.TimeZoneOffset));
      }
      else
      {
        base.AsString = str;
        base.AsDateTime = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
      }
      if (oldPasswordsTable == null)
        return;
      this.SavePasswordHistory(oldPasswordsTable, str);
    }
  }

  private void SavePasswordHistory(DataTable oldPasswordsTable, string pswHash)
  {
    long num = this.UserSession.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_MEM", 0L, DBConfigMode.GlobalOnly);
    if ((long) oldPasswordsTable.Rows.Count < num)
    {
      DataRow row = oldPasswordsTable.NewRow();
      oldPasswordsTable.Rows.Add(row);
    }
    for (int index = oldPasswordsTable.Rows.Count - 1; index > 0; --index)
    {
      oldPasswordsTable.Rows[index]["F_PARAM_NAME"] = (object) index.ToString();
      oldPasswordsTable.Rows[index]["F_VALUE"] = (object) oldPasswordsTable.Rows[index - 1]["F_VALUE"].ToString();
    }
    oldPasswordsTable.Rows[0]["F_PARAM_NAME"] = (object) "0";
    oldPasswordsTable.Rows[0]["F_VALUE"] = (object) pswHash;
    oldPasswordsTable.AcceptChanges();
    this.UserSession.Configurations.WriteSection("KERNEL", "OLD_PSW", oldPasswordsTable, this.DBObjectID);
  }

  public void SetPasswordHash(string pswHash, bool methodSymbolExists)
  {
    if (!methodSymbolExists)
      pswHash = Convert.ToChar(this.UserSession.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly)).ToString() + pswHash;
    if (!this.UserSession.IsAdmin && !this.UserSession.Configurations.ReadBool("KERNEL", "SECURITY", "PSW_USER", true, DBConfigMode.GlobalOnly))
      throw new PasswordModifyException();
    if (this.AttributeID == this.UserSession.IdentHelper.PasswordID && this.TypeID == this.UserSession.IdentHelper.UsersTypeID && this.UserSession.UserID == this.DBObjectID)
    {
      this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("ChangePasswordEvent"), (object) this.UserSession.UserName));
      this.DirectSetValues((object) pswHash, (object) null, (object) null, (object) (DateTime.UtcNow + this.UserSession.TimeZoneOffset));
      if (this.UserSession.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_MEM", 0L, DBConfigMode.GlobalOnly) <= 0L)
        return;
      this.SavePasswordHistory(this.UserSession.Configurations.ReadSection("KERNEL", "OLD_PSW", this.UserSession.UserID), pswHash);
    }
    else
    {
      if (this.AttributeID == this.UserSession.IdentHelper.PasswordID && this.TypeID == this.UserSession.IdentHelper.UsersTypeID && this.UserSession.IsSystemSession && this.UserSession.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_MEM", 0L, DBConfigMode.GlobalOnly) > 0L)
        this.SavePasswordHistory(this.UserSession.Configurations.ReadSection("KERNEL", "OLD_PSW", this.DBObjectID), pswHash);
      base.AsString = pswHash;
      base.AsDateTime = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
    }
  }

  public override bool IsNull => this._ValuesTable[this.Index]["F_STRING_VALUE"] == DBNull.Value;

  public override bool AsBoolean
  {
    set => throw new OperationNotApplicableException();
  }

  public override double AsDouble
  {
    set => throw new OperationNotApplicableException();
  }

  public override DateTime AsDateTime
  {
    set => throw new OperationNotApplicableException();
  }

  public override long AsInteger
  {
    set => throw new OperationNotApplicableException();
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsString;
    set
    {
      if (value == null || value == DBNull.Value)
        this.Clear();
      else if (value is PswPackage)
      {
        PswPackage pswPackage = value as PswPackage;
        if ((int) ServerConsts.CryptMethod == (int) CryptHelper.SHA1Crypt)
          this.SetPasswordHash(pswPackage.SHA1CryptHash, true);
        else if ((int) ServerConsts.CryptMethod == (int) CryptHelper.MD5Crypt)
          this.SetPasswordHash(pswPackage.MD5CryptHash, true);
        else
          this.SetPasswordHash(pswPackage.NoneCryptStr, true);
      }
      else
        this.AsString = Convert.ToString(value);
    }
  }

  public char CurrentCryptMethod => ServerConsts.CryptMethod;

  public void SetPassword(PswPackage psw) => this.Value = (object) psw;
}
