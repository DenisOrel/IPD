
// Type: Intermech.Scripting.Common.Debugging.IDebugExecutor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Scripting.Common.Debugging
{
    /// <summary>
    /// Дополнительный интерфейс сервиса выполнения сценариев, используемый для отладки сценариев.
    /// Реализация обязана быть thread safe.
    /// </summary>
    public interface IDebugExecutor
    {
      /// <summary>
      /// Проверяет возможность отладки сценариев текущим пользователем в текущей конфигурации IPS.
      /// </summary>
      /// <param name="clientToken">Токен клиента</param>
      /// <returns>Признак возможности отладки</returns>
      bool CanDebug(int clientToken);

      /// <summary>Выполняет код сценария в режиме отладки.</summary>
      /// <param name="clientToken">Токен клиента</param>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="options">Опции выполнения кода сценария</param>
      /// <param name="arguments">Аргументы вызова сценария</param>
      /// <returns>Результат выполнения сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="options" /> не должен быть равен null; параметр <paramref name="arguments" /> не должен быть равен null</exception>
      /// <exception cref="T:System.Exception">Код сценария не содержит необходимых элементов, либо произошла ошибка при выполнении сценария</exception>
      DebugExecuteResult DebugExecute(
        int clientToken,
        string scriptCode,
        object options,
        params object[] arguments);
    }
}
