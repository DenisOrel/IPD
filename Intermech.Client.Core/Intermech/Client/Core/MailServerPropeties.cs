
// Type: Intermech.Client.Core.MailServerPropeties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.Client.Core;

/// <summary>Настройки отправки почты</summary>
[Serializable]
public class MailServerPropeties
{
  /// <summary>Адрес SMTP-сервера</summary>
  private string _smtpServer = string.Empty;
  /// <summary>Порт SMTP-сервера</summary>
  private int _smtpPort = 25;
  /// <summary>Логин</summary>
  private string _smtpLogin = string.Empty;
  /// <summary>Пароль</summary>
  private string _smtpPassword = string.Empty;
  /// <summary>E-mail отправителя по-умолчанию</summary>
  private string _smtpDefaultEmail = string.Empty;
  /// <summary>Адрес POP3-сервера</summary>
  private string _pop3Server = string.Empty;
  /// <summary>Порт POP3-сервера</summary>
  private int _pop3Port = 25;
  /// <summary>Логин POP3</summary>
  private string _pop3login = string.Empty;
  /// <summary>Пароль POP3</summary>
  private string _pop3password = string.Empty;
  /// <summary>Наименование ящика</summary>
  private string _name = string.Empty;

  [CustomDisplayName("Attribute.Client.Core_263")]
  [CustomDescription("Attribute.Client.Core_264")]
  [CustomCategory("Attribute.Client.Core_265")]
  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  /// <summary>Адрес SMTP-сервера</summary>
  [CustomDisplayName("Attribute.Client.Core_266")]
  [CustomDescription("Attribute.Client.Core_267")]
  [CustomCategory("Attribute.Client.Core_268")]
  public string SMTPServer
  {
    get => this._smtpServer;
    set => this._smtpServer = value;
  }

  /// <summary>Порт SMTP-сервера</summary>
  [CustomDisplayName("Attribute.Client.Core_269")]
  [CustomDescription("Attribute.Client.Core_269")]
  [CustomCategory("Attribute.Client.Core_268")]
  public int SMPTPort
  {
    get => this._smtpPort;
    set => this._smtpPort = value;
  }

  /// <summary>Логин</summary>
  [CustomDisplayName("Attribute.Client.Core_270")]
  [CustomDescription("Attribute.Client.Core_271")]
  [CustomCategory("Attribute.Client.Core_268")]
  public string SMPTLoginName
  {
    get => this._smtpLogin;
    set => this._smtpLogin = value;
  }

  /// <summary>Логин</summary>
  [CustomDisplayName("Attribute.Client.Core_272")]
  [CustomDescription("Attribute.Client.Core_273")]
  [TypeConverter(typeof (PasswordTypeConverter))]
  [Editor(typeof (NewPasswordEditor), typeof (UITypeEditor))]
  [CustomCategory("Attribute.Client.Core_268")]
  public string SMPTPassword
  {
    get => this._smtpPassword;
    set => this._smtpPassword = value;
  }

  /// <summary>E-mail отправителя по-умолчанию</summary>
  [CustomDisplayName("Attribute.Client.Core_274")]
  [CustomDescription("Attribute.Client.Core_275")]
  [CustomCategory("Attribute.Client.Core_268")]
  public string SMPTDefaultEmail
  {
    get => this._smtpDefaultEmail;
    set => this._smtpDefaultEmail = value;
  }

  /// <summary>Адрес POP3-сервера</summary>
  [CustomDisplayName("Attribute.Client.Core_276")]
  [CustomDescription("Attribute.Client.Core_277")]
  [CustomCategory("Attribute.Client.Core_278")]
  public string POP3Server
  {
    get => this._pop3Server;
    set => this._pop3Server = value;
  }

  /// <summary>Порт SMTP-сервера</summary>
  [CustomDisplayName("Attribute.Client.Core_279")]
  [CustomDescription("Attribute.Client.Core_279")]
  [CustomCategory("Attribute.Client.Core_278")]
  public int POP3Port
  {
    get => this._pop3Port;
    set => this._pop3Port = value;
  }

  /// <summary>Логин</summary>
  [CustomDisplayName("Attribute.Client.Core_280")]
  [CustomDescription("Attribute.Client.Core_281")]
  [CustomCategory("Attribute.Client.Core_278")]
  public string POP3LoginName
  {
    get => this._pop3login;
    set => this._pop3login = value;
  }

  /// <summary>Логин</summary>
  [CustomDisplayName("Attribute.Client.Core_282")]
  [CustomDescription("Attribute.Client.Core_283")]
  [TypeConverter(typeof (PasswordTypeConverter))]
  [Editor(typeof (NewPasswordEditor), typeof (UITypeEditor))]
  [CustomCategory("Attribute.Client.Core_278")]
  public string POP3Password
  {
    get => this._pop3password;
    set => this._pop3password = value;
  }
}
