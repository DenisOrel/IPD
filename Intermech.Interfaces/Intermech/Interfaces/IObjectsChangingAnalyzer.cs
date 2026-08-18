
// Type: Intermech.Interfaces.IObjectsChangingAnalyzer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс анализатора списка изменяемых объектов.</summary>
    public interface IObjectsChangingAnalyzer
    {
      /// <summary>
      /// Какое действие выполняется над объектами (проверка корректности анализатора)
      /// </summary>
      ObjectChangingAction Action { get; }

      /// <summary>
      /// Уникальный идентификатор анализатора
      /// (по данному идентификатору происходит регистрация и
      /// удаление анализатора в службе анализаторов)
      /// </summary>
      Guid Guid { get; }

      /// <summary>
      /// Выполнить анализ изменяемых объектов, при необходимости добавить в граф
      /// дополнительные идентификаторы версий объектов, которые тоже требуется изменить.
      /// На верхнем уровне - первоначальный список изменяемых версий объектов
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
      /// <param name="changingObjects">Список изменяемых версий объектов</param>
      /// <returns>Количество добавленных для изменения объектов</returns>
      int Analyze(IUserSession session, ChangingObjects changingObjects);
    }
}
