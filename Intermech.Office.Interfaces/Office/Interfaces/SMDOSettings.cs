// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.SMDOSettings
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Office.Interfaces;

[Serializable]
public class SMDOSettings
{
  private string _smdoEmail = string.Empty;
  private string _companySMDOid = string.Empty;
  private string _companyName = string.Empty;
  private string _companyEmail = string.Empty;
  private string _userName = string.Empty;
  private string _password = string.Empty;
  private string _smdoHost = string.Empty;
  private int _smdoPort;
  private bool _ssl;
  private string _sysID = string.Empty;

  public SMDOSettings(
    string email,
    string smdoid,
    string name,
    string userName,
    string password,
    string smdoHost,
    int port,
    bool ssl,
    string companyEmail,
    string sysID)
  {
    this._smdoEmail = email;
    this._companySMDOid = smdoid;
    this._companyName = name;
    this._userName = userName;
    this._password = password;
    this._smdoHost = smdoHost;
    this._smdoPort = port;
    this._ssl = ssl;
    this._companyEmail = companyEmail;
    this._sysID = sysID;
  }

  public string SystemID
  {
    [DebuggerStepThrough] get => this._sysID;
  }

  public string UserName
  {
    [DebuggerStepThrough] get => this._userName;
  }

  public string Password
  {
    [DebuggerStepThrough] get => this._password;
  }

  public string SMDOHost
  {
    [DebuggerStepThrough] get => this._smdoHost;
  }

  /// <summary>Порт исходящей почты СМДО</summary>
  public int Port
  {
    [DebuggerStepThrough] get => this._smdoPort;
  }

  public bool SSL
  {
    [DebuggerStepThrough] get => this._ssl;
  }

  public string MyCompanyEmail
  {
    [DebuggerStepThrough] get => this._companyEmail;
  }

  /// <summary>Почтовый адрес сервера СМДО</summary>
  public string SmdoEmail
  {
    [DebuggerStepThrough] get => this._smdoEmail;
  }

  /// <summary>Идентификатор компании в системе СМДО</summary>
  public string CompanySMDOid
  {
    [DebuggerStepThrough] get => this._companySMDOid;
  }

  /// <summary>Наименование компании</summary>
  public string CompanyName
  {
    [DebuggerStepThrough] get => this._companyName;
  }
}
