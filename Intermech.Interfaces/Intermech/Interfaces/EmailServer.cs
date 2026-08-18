
// Type: Intermech.Interfaces.EmailServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Почтовый сервер.</summary>
    [Serializable]
    public class EmailServer
    {
      /// <summary>Глобальный идентификатор сервера.</summary>
      private Guid _guid = Guid.Empty;
      /// <summary>Наименование ящика.</summary>
      private string _name = string.Empty;
      /// <summary>Адрес SMTP-сервера.</summary>
      private string _smtpServer = string.Empty;
      /// <summary>Порт SMTP-сервера.</summary>
      private int _smtpPort = 25;
      /// <summary>Тип соединения с SMTP-сервером.</summary>
      private EmailConnectionTypes _smtpConnection;
      /// <summary>Адрес POP3-сервера.</summary>
      private string _pop3Server = string.Empty;
      /// <summary>Порт POP3-сервера.</summary>
      private int _pop3Port = 110;
      /// <summary>Тип соединения с POP3-сервером.</summary>
      private EmailConnectionTypes _pop3Connection;

      /// <summary>Глобальный идентификатор.</summary>
      [CustomDisplayName("Attribute.Interfaces_511")]
      [CustomDescription("Attribute.Interfaces_512")]
      [CustomCategory("Attribute.Interfaces_513")]
      [ReadOnly(true)]
      public Guid Guid
      {
        get => this._guid;
        set => this._guid = value;
      }

      /// <summary>Название.</summary>
      [CustomDisplayName("Attribute.Interfaces_514")]
      [CustomDescription("Attribute.Interfaces_515")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string Name
      {
        get => this._name;
        set => this._name = value;
      }

      /// <summary>Адрес SMTP-сервера.</summary>
      [CustomDisplayName("Attribute.Interfaces_516")]
      [CustomDescription("Attribute.Interfaces_517")]
      [CustomCategory("Attribute.Interfaces_518")]
      public string SMTPServer
      {
        get => this._smtpServer;
        set => this._smtpServer = value;
      }

      /// <summary>Порт SMTP-сервера.</summary>
      [CustomDisplayName("Attribute.Interfaces_519")]
      [CustomDescription("Attribute.Interfaces_519")]
      [CustomCategory("Attribute.Interfaces_518")]
      public int SMPTPort
      {
        get => this._smtpPort;
        set => this._smtpPort = value;
      }

      /// <summary>Соединение с SMTP-сервером.</summary>
      [CustomDisplayName("Attribute.Interfaces_535")]
      [CustomDescription("Attribute.Interfaces_536")]
      [CustomCategory("Attribute.Interfaces_518")]
      public EmailConnectionTypes SMPTConnectionType
      {
        get => this._smtpConnection;
        set => this._smtpConnection = value;
      }

      /// <summary>Адрес POP3-сервера.</summary>
      [CustomDisplayName("Attribute.Interfaces_520")]
      [CustomDescription("Attribute.Interfaces_521")]
      [CustomCategory("Attribute.Interfaces_522")]
      public string POP3Server
      {
        get => this._pop3Server;
        set => this._pop3Server = value;
      }

      /// <summary>Порт POP3-сервера.</summary>
      [CustomDisplayName("Attribute.Interfaces_523")]
      [CustomDescription("Attribute.Interfaces_523")]
      [CustomCategory("Attribute.Interfaces_522")]
      public int POP3Port
      {
        get => this._pop3Port;
        set => this._pop3Port = value;
      }

      /// <summary>Соединение с POP-сервером.</summary>
      [CustomDisplayName("Attribute.Interfaces_537")]
      [CustomDescription("Attribute.Interfaces_538")]
      [CustomCategory("Attribute.Interfaces_522")]
      public EmailConnectionTypes POP3ConnectionType
      {
        get => this._pop3Connection;
        set => this._pop3Connection = value;
      }
    }
}
