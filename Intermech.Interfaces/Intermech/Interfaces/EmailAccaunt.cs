
// Type: Intermech.Interfaces.EmailAccaunt
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Аккаунт почтового сервера.</summary>
    [Serializable]
    public class EmailAccaunt
    {
      /// <summary>Глобальный идентификатор.</summary>
      private Guid _guid = Guid.Empty;
      /// <summary>Почтовый ящик.</summary>
      private string _email = string.Empty;
      /// <summary>Логин.</summary>
      private string _login = string.Empty;
      /// <summary>Пароль.</summary>
      private string _password = string.Empty;

      /// <summary>Глобальный идентификатор.</summary>
      [CustomDisplayName("Attribute.Interfaces_511")]
      [CustomDescription("Attribute.Interfaces_524")]
      [CustomCategory("Attribute.Interfaces_513")]
      [ReadOnly(true)]
      public Guid Guid
      {
        get => this._guid;
        set => this._guid = value;
      }

      /// <summary>Логин.</summary>
      [CustomDisplayName("Attribute.Interfaces_525")]
      [CustomDescription("Attribute.Interfaces_525")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string Email
      {
        get => this._email;
        set => this._email = value;
      }

      /// <summary>Логин.</summary>
      [CustomDisplayName("Attribute.Interfaces_526")]
      [CustomDescription("Attribute.Interfaces_527")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string Login
      {
        get => this._login;
        set => this._login = value;
      }

      /// <summary>Пароль.</summary>
      [CustomDisplayName("Attribute.Interfaces_528")]
      [CustomDescription("Attribute.Interfaces_529")]
      [CustomCategory("Attribute.Interfaces_513")]
      public string Password
      {
        get => this._password;
        set => this._password = value;
      }

      public override string ToString() => this._email;
    }
}
