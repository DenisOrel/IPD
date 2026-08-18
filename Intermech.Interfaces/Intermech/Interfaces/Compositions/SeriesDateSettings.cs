
// Type: Intermech.Interfaces.Compositions.SeriesDateSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Контейнер настроек фильтрации составов по сериям изделий и датам выпуска/действия,
    /// который применяется в правиле подбора версий
    /// </summary>
    public sealed class SeriesDateSettings
    {
      /// <summary>
      /// Контейнер настроек фильтрации составов по сериям изделий и датам выпуска/действия
      /// </summary>
      public SeriesDateSettingsHolder Settings;
      /// <summary>Сервис по подбору версий по датам и сериям</summary>
      public IVersionApplicabilitiesService Service;
      /// <summary>Разрешён ли подбор по датам и сериям</summary>
      public bool Enabled;

      /// <summary>Создать пустой экземпляр класса</summary>
      public SeriesDateSettings()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="settings">Контейнер настроек фильтрации составов по сериям изделий и датам выпуска/действия</param>
      /// <param name="service">Сервис по подбору версий по датам и сериям</param>
      /// <param name="enabled">Разрешён ли подбор по датам и сериям</param>
      public SeriesDateSettings(
        SeriesDateSettingsHolder settings,
        IVersionApplicabilitiesService service,
        bool enabled)
      {
        this.Settings = settings;
        this.Service = service;
        this.Enabled = enabled;
      }

      /// <summary>
      /// Выполнить проверку применяемости указанной версии по дате и(или) номеру серии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="applicabilities">Условия применения объекта по датам и сериям</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <returns>Статус указанной версии:
      /// - fsNotRequired - фильтрация не требуется (успешная фильтрация),
      /// - fsVersionNotFound - не задана проверяемая версия объекта,
      /// - fsFiltrationStopped - ошибка (недостаточно исходных данных),
      /// - fsMainArticleNotFound - не найдено головное изделие в условиях применения,
      /// - fsVersionBySeries - версия успешно подобрана по серии (успешная фильтрация),
      /// - fsVersionByDate - версия успешно подобрана по дате (успешная фильтрация),
      /// - fsVarianceSeriesDate - версия не прошла подбор по серии и по дате</returns>
      public ObjectFiltrationState CheckApplicabilities(
        IUserSession session,
        string applicabilities,
        long objectID)
      {
        return this.Service == null || this.Settings == null ? ObjectFiltrationState.fsFiltrationStopped : this.Service.CheckApplicabilities(session, applicabilities, objectID, this.Settings.MasterArticle, this.Settings.Date, this.Settings.Series);
      }
    }
}
