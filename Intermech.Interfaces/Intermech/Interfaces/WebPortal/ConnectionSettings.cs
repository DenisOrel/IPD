
// Type: Intermech.Interfaces.WebPortal.ConnectionSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Настройки соединения с порталом</summary>
    [Serializable]
    public struct ConnectionSettings(string url)
    {
      /// <summary>Наименование соединения</summary>
      public string Name = string.Empty;
      /// <summary>Адрес Web-службы портала</summary>
      public string Url = url;
      /// <summary>Код текущего узла</summary>
      public char SiteCode = Consts.NoSymbol;
      /// <summary>Код текущего узла</summary>
      public Guid SiteGuid = Guid.Empty;
      /// <summary>Имя системного пользователя</summary>
      public string UserLogin = string.Empty;
      /// <summary>Пароль</summary>
      public string Password = string.Empty;
      /// <summary>Адрес прокси-сервера</summary>
      public string ProxyAddress = string.Empty;
      /// <summary>Порт прокси-сервера</summary>
      public int ProxyPort = 0;
      /// <summary>Флаг офлайн-портала</summary>
      public bool IsOffline = ConnectionHelper.IsOffline(url);
      /// <summary>Флаг правильных данных в структуре</summary>
      public bool IsValid = false;
      /// <summary>Флаг того, что портал поддерживает асинхронные методы</summary>
      public bool AsyncSupported = true;
      /// <summary>
      /// Если установлен флаг, необходимо проверять на совместимость версию портала и узла
      ///  </summary>
      public bool ValidateVersion = true;
    }
}
