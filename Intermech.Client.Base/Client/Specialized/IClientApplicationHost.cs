
// Type: Intermech.Client.Specialized.IClientApplicationHost
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using System;


namespace Intermech.Client.Specialized
{
    /// <summary>
    /// Интерфейс объекта для интеграции с хост-приложением специализированного клиента IPS.
    /// </summary>
    public interface IClientApplicationHost
    {
      /// <summary>
      /// Возвращает провайдер для параметров логина в специализированном клиенте IPS.
      /// Значение свойства должно быть задано.
      /// </summary>
      Func<SimpleSessionPoolLoginInfo> LoginInfoProvider { get; }
    }
}
