
// Type: Intermech.Interfaces.ProxyServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Прокси-сервер.</summary>
    [Serializable]
    public class ProxyServer
    {
      private ProxyType _type;
      private string _serverName = string.Empty;
      private int _port = 8080;
      private string _userName = string.Empty;
      private string _userPassword = string.Empty;

      /// <summary>Тип прокси.</summary>
      [TypeConverter(typeof (EnumDescConverter))]
      [CustomDisplayName("Attribute.Interfaces_539")]
      [CustomDescription("Attribute.Interfaces_540")]
      [CustomCategory("Attribute.Interfaces_513")]
      public ProxyType Type
      {
        get => this._type;
        set => this._type = value;
      }

      /// <summary>Имя сервера.</summary>
      [CustomDisplayName("Attribute.Interfaces_541")]
      [CustomDescription("Attribute.Interfaces_542")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string ServerName
      {
        get => this._serverName;
        set => this._serverName = value;
      }

      /// <summary>Порт.</summary>
      [CustomDisplayName("Attribute.Interfaces_543")]
      [CustomDescription("Attribute.Interfaces_544")]
      [CustomCategory("Attribute.Interfaces_513")]
      public int Port
      {
        get => this._port;
        set => this._port = value;
      }

      /// <summary>Имя пользователя.</summary>
      [CustomDisplayName("Attribute.Interfaces_545")]
      [CustomDescription("Attribute.Interfaces_546")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string UserName
      {
        get => this._userName;
        set => this._userName = value;
      }

      /// <summary>Пароль пользователя.</summary>
      [CustomDisplayName("Attribute.Interfaces_547")]
      [CustomDescription("Attribute.Interfaces_548")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string UserPassword
      {
        get => this._userPassword;
        set => this._userPassword = value;
      }
    }
}
