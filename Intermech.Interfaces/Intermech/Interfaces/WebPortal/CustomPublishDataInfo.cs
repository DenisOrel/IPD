
// Type: Intermech.Interfaces.WebPortal.CustomPublishDataInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Информация по запускаемому удаленному процессу</summary>
    [Serializable]
    public class CustomPublishDataInfo
    {
      /// <summary>
      /// Наименование процесса запуска для отображения в списке задач
      /// </summary>
      public string Name;
      /// <summary>
      /// Глобальный идентификатор узла для которого происходит публикация
      /// </summary>
      public char SiteRecipient;
      /// <summary>Идентификаторы необходимых вложения для процесса</summary>
      public List<long> Attachments;
      /// <summary>Опции публикации состава</summary>
      public ExtendedPublishOptions Options;
      /// <summary>
      /// Правило подбора версий для поиска состава, необходимо
      /// при CompositionType не равном SelectCompositionType.None
      /// </summary>
      public string VersionsRule;
      /// <summary>Данные</summary>
      public string Data;

      /// <summary>Конструктор</summary>
      /// <param name="name">Наименование процесса запуска для отображения в списке задач</param>
      /// <param name="siteGuid">Глобальный идентификатор узла</param>
      /// <param name="attachments">Идентификаторы необходимых вложения для процесса</param>
      /// <param name="data">Данные процесса</param>
      /// <param name="compositionType">Тип запроса состава</param>
      /// <param name="versionsRule">Правило подбора версий для поиска состава, необходимо
      /// при CompositionType не равном SelectCompositionType.None</param>
      public CustomPublishDataInfo(
        string name,
        char siteRecipient,
        List<long> attachments,
        string data,
        ExtendedPublishOptions options,
        string versionsRule)
      {
        this.Name = name;
        this.SiteRecipient = siteRecipient;
        this.Attachments = attachments;
        this.Data = data;
        this.Options = options;
        this.VersionsRule = versionsRule;
      }
    }
}
