
// Type: Intermech.Interfaces.IResetToDefaults
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс предназначен для сброса значений полей класса на умалчиваемые значения
    /// </summary>
    public interface IResetToDefaults
    {
      /// <summary>
      /// Выполнить сброс значений полей класса, реализующего данный интерфейс, на умалчиваемые значения
      /// </summary>
      /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
      void ResetToDefaults(IUserSession session);
    }
}
