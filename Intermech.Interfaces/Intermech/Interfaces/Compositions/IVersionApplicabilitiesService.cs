
// Type: Intermech.Interfaces.Compositions.IVersionApplicabilitiesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Интерфейс, позволяющий проверить применяемость версии объекта в составе по датам выпуска/действия и(или) сериям изделий
    /// </summary>
    public interface IVersionApplicabilitiesService
    {
      /// <summary>
      /// Выполнить проверку применяемости указанной версии по дате и(или) номеру серии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="applicabilities">Условия применения объекта по датам и сериям</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <param name="masterArticle">Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)</param>
      /// <param name="date">Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue</param>
      /// <param name="series">Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue</param>
      /// <returns>Статус указанной версии:
      /// - fsNotRequired - фильтрация не требуется (успешная фильтрация),
      /// - fsVersionNotFound - не задана проверяемая версия объекта,
      /// - fsFiltrationStopped - ошибка (недостаточно исходных данных),
      /// - fsMainArticleNotFound - не найдено головное изделие в условиях применения,
      /// - fsVersionBySeries - версия успешно подобрана по серии (успешная фильтрация),
      /// - fsVersionByDate - версия успешно подобрана по дате (успешная фильтрация),
      /// - fsVarianceSeriesDate - версия не прошла подбор по серии и по дате</returns>
      ObjectFiltrationState CheckApplicabilities(
        IUserSession session,
        string applicabilities,
        long objectID,
        long masterArticle,
        DateTime date,
        int series);
    }
}
