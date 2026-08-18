
// Type: Intermech.Interfaces.ICSharpScriptExecutor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса, позволяющего выполнять C#-сценарии в изолированном окружении.
    /// Для доступа из сценария к API основного приложения в сценарий через свойство ScriptContext передается специальный объект - контекст выполнения.
    /// </summary>
    public interface ICSharpScriptExecutor
    {
      /// <summary>Возвращает инфомарцию о среде выполнения сценариев.</summary>
      /// <returns>Информация о среде выполнения сценариев</returns>
      CSharpScriptRuntimeInfo GetRuntimeInfo();

      /// <summary>
      /// Проверяет, может ли сценарий быть выполнен в изолированном окружении.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <returns>true - код сценария содержит свойство ScriptContext и может быть выполнен в изолированном окружении</returns>
      bool CanExecuteInSandbox(string scriptCode);

      /// <summary>
      /// Выполняет код сценария. Код должен содержать класс Script с экземплярным свойством
      /// ScriptContext типа ICSharpScriptContext и экземплярный метод Execute,
      /// параметры которого должны соответствовать аргументам вызова сценария.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="options">Опции выполнения сценария</param>
      /// <param name="arguments">Аргументы вызова сценария</param>
      /// <returns>Результат выполнения сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="options" /> не должен быть равен null; параметр <paramref name="arguments" /> не должен быть равен null</exception>
      /// <exception cref="T:System.Exception">Код сценария не содержит необходимых элементов, либо произошла ошибка при выполнении сценария</exception>
      object Execute(
        string scriptCode,
        CSharpScriptInvocationOptions options,
        params object[] arguments);

      /// <summary>
      /// Создает и возвращает объект сценария, завернутый в объект-хранитель.
      /// Метод применяется в тех случаях, когда обращение к сценарию C# не может быть
      /// сведено к единственному вызову метода Execute. Код сценария должен содержать
      /// класс Script с экземплярным свойством ScriptContext типа ICSharpScriptContext.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="options">Опции выполнения сценария</param>
      /// <returns>Объект-хранитель, содержащий объект сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="options" /> не должен быть равен null</exception>
      /// <exception cref="T:System.Exception">Код сценария не содержит необходимых элементов, либо произошла ошибка при компиляции сценария</exception>
      CSharpScriptObjectKeeper CreateScriptObject(
        string scriptCode,
        CSharpScriptInvocationOptions options);
    }
}
