
// Type: Intermech.Interfaces.LifeCycles.ILifecycleService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>
    /// Интерфейс службы для работы с жизненными циклами объектов
    /// </summary>
    public interface ILifecycleService
    {
      /// <summary>
      /// Метод проверяет можно ли переводить по схемам ЖЦ указанные объекты. В любом случае проверяются все указанные объекты.
      /// </summary>
      /// <param name="objectIDs">Массив идентификаторов версий объектов</param>
      /// <param name="stepInfos">Информация о настройках перевода для типов объектов</param>
      /// <returns>Пусткая строка если можно всех переводить или отчет с ошибками перевода</returns>
      string ValidateChangeLCStep(long[] objectIDs, NewLCStepInfo[] stepInfos);

      /// <summary>
      /// Функция проверяет можно ли создать новую версию объекта id на шаге stepID и если нужно - вытесняет оттуда предыдущие версии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="id">Идентификатор объекта</param>
      /// <param name="stepID">Ид. шага ЖЦ, на котором хотят создать объект</param>
      /// <param name="errorMsg">Текст сообщения о том, почему создание на этом шаге не допустимо</param>
      /// <param name="modificationID">Номер группы изменений</param>
      /// <returns></returns>
      bool CanCreateObjectVersion(
        IUserSession session,
        long id,
        long modificationID,
        int stepID,
        out string errorMsg);
    }
}
