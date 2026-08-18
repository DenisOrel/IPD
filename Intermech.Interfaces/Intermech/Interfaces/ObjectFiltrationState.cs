
// Type: Intermech.Interfaces.ObjectFiltrationState
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Статус последней фильтрации текущего объекта по правилу подбора версий
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_310")]
    [Category("Misc")]
    public enum ObjectFiltrationState
    {
      /// <summary>
      /// Фильтрация по правилам подбора версий не требуется. Выбрано правило "Все версии объектов".
      /// </summary>
      [CustomDescription("Attribute.Interfaces_311")] fsNotRequired,
      /// <summary>
      /// Не найдена версия объекта в переданных данных при конкретизации версии в составе
      /// </summary>
      [CustomDescription("Attribute.Interfaces_312")] fsCompositeVersionNotFound,
      /// <summary>
      /// Версия не удовлетворяет основным критериям правила подбора
      /// </summary>
      [CustomDescription("Attribute.Interfaces_313")] fsFiltrationStopped,
      /// <summary>Не найдена версия объекта в переданных данных</summary>
      [CustomDescription("Attribute.Interfaces_314")] fsVersionNotFound,
      /// <summary>
      /// Не удалось получить правило подбора версий для фильтрации
      /// </summary>
      [CustomDescription("Attribute.Interfaces_315")] fsInvalidRule,
      /// <summary>
      /// Внимание! Не существует версий соответствующих основному критерию подбора.
      /// Версия отобрана по дополнительному критерию подбора
      /// </summary>
      [CustomDescription("Attribute.Interfaces_316")] fsVariance,
      /// <summary>
      /// Объекту не требуется фильтрация по правилам подбора версий. Объект неверсионный
      /// </summary>
      [CustomDescription("Attribute.Interfaces_317")] fsNonVersionable,
      /// <summary>
      /// Объект полностью соответствует правилу подбора версий.
      /// Причём он единственный из версий, прошедший фильтрацию по основным критериям подбора.
      /// </summary>
      [CustomDescription("Attribute.Interfaces_318")] fsCorrespondingSingle,
      /// <summary>
      /// Внимание! Существует несколько версий объекта, соответствующих основному критерию подбора.
      /// Версия отобрана по дополнительному критерию подбора.
      /// </summary>
      [CustomDescription("Attribute.Interfaces_319")] fsCorresponding,
      /// <summary>Подбор выполнен по конкретизации версии на связи</summary>
      [CustomDescription("Attribute.Interfaces_320")] fsCompositeVersion,
      /// <summary>
      /// Версия подобрана по основному контексту редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_435")] fsVersionFromMainContext,
      /// <summary>
      /// Версия подобрана по связанному контексту редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_436")] fsVersionFromLinkedContext,
      /// <summary>
      /// В текущем контексте редактирования находится другая версия объекта
      /// </summary>
      [CustomDescription("Attribute.Interfaces_506")] fsVersionConflictsWithContext,
      /// <summary>
      /// В условиях применения данной версии объекта в сериях изделий и датах выпуска не найдено указанное головное изделие
      /// </summary>
      [CustomDescription("Attribute.Interfaces_507")] fsMainArticleNotFound,
      /// <summary>Версия подобрана по дате выпуска</summary>
      [CustomDescription("Attribute.Interfaces_508")] fsVersionByDate,
      /// <summary>Версия подобрана по сериям изделий</summary>
      [CustomDescription("Attribute.Interfaces_509")] fsVersionBySeries,
      /// <summary>
      /// Внимание! Не существует версий, соответствующих указанной серии или дате.
      /// Версия отобрана по текущим настройкам фильтрации составов
      /// </summary>
      [CustomDescription("Attribute.Interfaces_510")] fsVarianceSeriesDate,
      /// <summary>Подбор выполнен по мягкой конкретизации</summary>
      [CustomDescription("Attribute.Interfaces_320")] fsSoftConcretised,
    }
}
