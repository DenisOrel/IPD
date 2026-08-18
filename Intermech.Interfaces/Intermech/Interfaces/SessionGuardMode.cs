
// Type: Intermech.Interfaces.SessionGuardMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Задает режим работы защиты для методов и свойств объектов сервера приложений от использования вне SessionKeeper.
    /// </summary>
    public enum SessionGuardMode
    {
      /// <summary>
      /// Обычный режим защиты. Подходит для большинства методов и свойств.
      /// </summary>
      Normal,
      /// <summary>
      /// Защита отключена, метод или свойство может быть использовано вне SessionKeeper. Не использовать без крайней необходимости!
      /// </summary>
      Disabled,
    }
}
