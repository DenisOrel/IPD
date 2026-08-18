
// Type: Intermech.Interfaces.ConnectionBroker.IConnectionBroker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.ConnectionBroker
{
    /// <summary>Интерфейс брокера подключений</summary>
    public interface IConnectionBroker
    {
      /// <summary>
      /// Возвращает адрес сервера приложений в соответствии с текущим режимом работы брокера
      /// </summary>
      /// <param name="dbConnectionString">Строка подключения к базе данных</param>
      /// <param name="forceCheckConnection">Требовать насильственной проверки работоспособности серверов. С этим параметром вызывается в случае, если предыдущий вызов вернул неправду.</param>
      /// <returns>Адрес сервера приложений. Возвращает пустую строку, если нет доступных серверов приложений.</returns>
      string GetAppServerURL(string dbConnectionString, bool forceCheckConnection);
    }
}
