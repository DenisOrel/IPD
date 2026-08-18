
// Type: Intermech.Interfaces.IObjectsDeleteAnalyzer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс анализатора списка удаляемых объектов.</summary>
    public interface IObjectsDeleteAnalyzer
    {
      /// <summary>
      /// Уникальный идентификатор анализатора
      /// (по данному идентификатору происходит регистрация и
      /// удаление анализатора в службе анализаторов)
      /// </summary>
      Guid Guid { get; }

      /// <summary>
      /// Выполнить анализ удаляемых объектов, при необходимости добавить в граф
      /// дополнительные идентификаторы версий объектов, которые тоже требуется удалить.
      /// На верхнем уровне - первоначальный список удаляемых версий объектов
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
      /// <param name="deletingObjects">Список удаляемых версий объектов</param>
      /// <param name="options">Параметры</param>
      /// <returns>Количество добавленных к удалению объектов</returns>
      int Analyze(IUserSession session, DeletingObjects deletingObjects, DeleteAnalyzerOptions options);
    }
}
