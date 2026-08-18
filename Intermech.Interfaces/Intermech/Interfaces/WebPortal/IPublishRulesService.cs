
// Type: Intermech.Interfaces.WebPortal.IPublishRulesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Серверный сервис правил публикации объектов на портал</summary>
    public interface IPublishRulesService : ITransferSettingsService
    {
      /// <summary>
      /// Регистрировать запрещенный к публикации атрибут, при публикации определенного типа объектов или связей
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов или типа связей для которого запрещена публикация этого атрибута</param>
      /// <param name="attributeID">Идентификатор атрибута</param>
      void RegisterForbiddenAttribute(Guid typeGuid, int attributeID);

      /// <summary>Регистрировать запрещенный к публикации атрибут</summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      void RegisterForbiddenAttribute(int attributeID);

      /// <summary>Запрещен ли для публикации атрибут</summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов или типа связей, которым принадлежит этот атрибут</param>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <returns></returns>
      bool IsForbiddenAttribute(Guid typeGuid, int attributeID);

      /// <summary>
      /// Максимальный уровень доступа с которым могут публиковаться объекты на портал
      /// </summary>
      int MaxAccessLevel { get; set; }

      /// <summary>Фильтровать документы по абоненту ОТД</summary>
      bool OTDFiltering { get; set; }

      /// <summary>
      /// Список узлов, на которые документы будут публиковаться вне зависимости от наличия этих узлов в листах рассылки документов. Настройка действует только в том случае, если включена настройка OTDFiltering.
      /// </summary>
      List<long> BeSurePublishForSites { get; set; }

      /// <summary>Синхронно публикуемые типы объектов</summary>
      List<Tuple<int, int>> InseparableObjectTypes { get; set; }

      /// <summary>Файловый шкаф для публикуемых данных</summary>
      long BlobStorageID { get; set; }

      /// <summary>
      /// Приоритет автоматически формируемых задач с публикацией квитанции импорта пакета
      /// </summary>
      TaskPriority Receipt4packetTaskPriority { get; set; }

      /// <summary>
      /// Приоритет автоматически формируемых задач с публикацией ответа об успешном импорте для узла, инициатора импорта.
      /// </summary>
      TaskPriority AnswerTaskPriority { get; set; }

      /// <summary>
      /// Типы объектов, при импорте и публикации объектов которых необходимо делать соответствующие записи в журнал
      /// </summary>
      List<int> LoggingTransferObjectTypes { get; set; }

      /// <summary>
      /// Типы объектов, при импорте и публикации объектов которых необходимо делать соответствующие записи в журнал
      /// </summary>
      List<int> LoggingTransferObjectTypesWithChildTypes { get; }

      /// <summary>
      /// Список узлов, задачи публикации на которые всегда разрешены, независимо от настроек
      /// </summary>
      List<long> EnableTrueTaskForSites { get; set; }

      /// <summary>Разрешена ли публикация на узлы без проверки СБ</summary>
      /// <param name="sities"></param>
      /// <returns></returns>
      bool IsEnableTrueTaskForSites(string sities, bool defaultValue);
    }
}
