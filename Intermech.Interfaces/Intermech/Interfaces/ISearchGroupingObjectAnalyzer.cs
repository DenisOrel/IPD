
// Type: Intermech.Interfaces.ISearchGroupingObjectAnalyzer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для поиска группирующих объектов</summary>
    public interface ISearchGroupingObjectAnalyzer
    {
      /// <summary>Название анализатора</summary>
      string Name { get; }

      /// <summary>
      /// Выполнить поиск группирующих объектов, при необходимости добавить в список
      /// дополнительные идентификаторы версий объектов, которые были проанализированы
      /// или которые требуется проанализировать.
      /// </summary>
      /// <param name="userSession">Сессия, в рамках которой выполняется анализ</param>
      /// <param name="searchGroupingObjects">Список анализируемых версий объектов</param>
      /// <returns>Количество добавленных для изменения объектов</returns>
      int Analyze(IUserSession userSession, SearchGroupingObjects searchGroupingObjects);
    }
}
