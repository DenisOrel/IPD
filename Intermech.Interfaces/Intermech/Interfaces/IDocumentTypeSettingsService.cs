
// Type: Intermech.Interfaces.IDocumentTypeSettingsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс службы получения параметров для документов, наследованных от типа объектов "Документ"
    /// </summary>
    public interface IDocumentTypeSettingsService
    {
      /// <summary>вернуть настройки типа документов</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="documentType">тип документа</param>
      /// <returns></returns>
      DocumentTypeSettings GetSettings(Guid sessionGuid, int documentType);

      /// <summary>установить настройки типа документов</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="documentType">тип документа</param>
      /// <param name="documentTypeSettingsData">настройки</param>
      void SetSettings(
        Guid sessionGuid,
        int documentType,
        DocumentTypeSettings documentTypeSettingsData);

      /// <summary>
      /// Вернуть список типов документов, использующих расширение fileExt
      /// </summary>
      /// <param name="sessionGuid"></param>
      /// <param name="fileExt"></param>
      /// <returns></returns>
      int[] GetDocumentTypesByFileExt(Guid sessionGuid, string fileExt);

      /// <summary>
      /// Вернуть список типов документов, у которых указан в качестве выпускаемого
      /// хотя бы один из перечисленных в outputObjectTypes типов объектов
      /// </summary>
      /// <param name="sessionGuid"></param>
      /// <param name="outputObjectTypes"></param>
      /// <param name="rootDocumentObjectType">Корневой тип документов, относительно которого производить поиск. Логично не указывать тип выше по иерархии чем тип "Документы"</param>
      /// <returns></returns>
      int[] GetDocumentTypesByOutputObjectTypes(
        Guid sessionGuid,
        int[] outputObjectTypes,
        int rootDocumentObjectType);

      /// <summary>проверить наследование от типа "документы"</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="documentType"></param>
      /// <returns></returns>
      bool InheritedFromDocuments(Guid sessionGuid, int documentType);

      /// <summary>
      /// проверить наследование от типа "конструкторские документы"
      /// </summary>
      /// <param name="sessionGuid"></param>
      /// <param name="documentType"></param>
      /// <returns></returns>
      bool InheritedFromConstructorDocuments(Guid sessionGuid, int documentType);

      /// <summary>
      /// Возвращает список всех кодов типов документов, зарегистрированных в базе данных IPS
      /// </summary>
      /// <returns>Список кодов</returns>
      List<string> GetDocSuffixes();
    }
}
