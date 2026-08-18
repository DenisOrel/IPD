
// Type: Intermech.Interfaces.ICompositionsAutomaticSortingService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы правилам сортировки составов
    /// </summary>
    public interface ICompositionsAutomaticSortingService
    {
      /// <summary>
      /// Получить правило сортировки и отображения составов для пользователя-владельца указанной сессии
      /// (правило может также быть получено из кэша). Будет возвращена точная копия правила, с которой
      /// можно делать что угодно
      /// </summary>
      /// <param name="session">IUserSession (вызов на серверной стороне) или Guid сессии (вызов с клиентской стороны)</param>
      /// <param name="forceReload">true - правило принудительно загружается из базы данных</param>
      /// <returns>Правило сортировки и отображения составов (точная копия)</returns>
      CompositionsAutosortRule GetAutosortRule(object session, bool forceReload);

      /// <summary>
      /// Создать сессию для расчета / назначения атрибута сортировки для связей согласно правилам сортировки составов
      /// </summary>
      /// <remarks>Не забываем освобождать сессию через DisposeSession для каждого вызова метода CreateSession!
      /// Сессия создается один раз - при повторном вызове - увеличивается "счетчик ссылок" и
      /// возвращается раннее созданный экземпляр.</remarks>
      /// <param name="session">IUserSession (вызов на серверной стороне) или Guid сессии (вызов с клиентской стороны)</param>
      /// <returns>Сессия назначения / расчета сортировки </returns>
      ICompositionsAutomaticSortingSession CreateSession(object session);

      /// <summary>Проверка наличия сессии</summary>
      /// <param name="session">IUserSession (вызов на серверной стороне) или Guid сессии (вызов с клиентской стороны)</param>
      /// <returns>0 - если сессия не найдена, в противном случае кол-во ссылок / вызовов сессии</returns>
      int IsSessionPresent(object session);

      /// <summary>
      /// Уничтожение/освобождение сессии назначения сортировки
      /// </summary>
      /// <remarks>При вызове метода уменьшается счетчик ссылок и при достижения 0 - сессия удаляется</remarks>
      /// &gt;
      ///             <param name="session">IUserSession (вызов на серверной стороне) или Guid сессии (вызов с клиентской стороны)</param>
      void DisposeSession(object session);
    }
}
