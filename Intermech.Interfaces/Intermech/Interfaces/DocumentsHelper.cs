
// Type: Intermech.Interfaces.DocumentsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Набор статических функция для работы с документами</summary>
    public class DocumentsHelper
    {
      /// <summary>
      /// Получить разделитель между обозначением документа и кодом типа документов
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Разделитель</returns>
      public static string GetSeparatorInDesignation(IUserSession session)
      {
        return session.Configurations.ReadString("KERNEL", "DOC_TYPES", "SEPARATOR_DESIGNATION", Consts.DefaultSeparatorInDesignation, DBConfigMode.GlobalOnly);
      }

      /// <summary>Добавляет код типа документа в обозначение документа.</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="designation">Обозначение документа</param>
      /// <param name="code">Код типа документа</param>
      /// <returns>Обновленное обозначение</returns>
      public static string AppendDocCode(IUserSession session, string designation, string code)
      {
        return DocumentsHelper.AppendDocCode(session, designation, code, false);
      }

      /// <summary>Добавляет код типа документа в обозначение документа.</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="designation">Обозначение документа</param>
      /// <param name="code">Код типа документа</param>
      /// <param name="appendToEmpty">Разрешается добавлять код к пустому обозначению</param>
      /// <returns>Обновленное обозначение</returns>
      public static string AppendDocCode(
        IUserSession session,
        string designation,
        string code,
        bool appendToEmpty)
      {
        if (code != string.Empty && designation.EndsWith(code))
          return designation;
        if (!(designation == string.Empty))
          return designation + DocumentsHelper.GetSeparatorInDesignation(session) + code;
        return !appendToEmpty ? designation : DocumentsHelper.GetSeparatorInDesignation(session) + code;
      }

      /// <summary>Убирает код типа документа из обозначения документы.</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="designation">Обозначение документа</param>
      /// <param name="code">Код типа документа</param>
      /// <returns>Обновленное обозначение</returns>
      public static string RemoveDocCode(IUserSession session, string designation, string code)
      {
        if (code != string.Empty && designation.EndsWith(code))
        {
          designation = designation.Remove(designation.Length - code.Length);
          string separatorInDesignation = DocumentsHelper.GetSeparatorInDesignation(session);
          if (separatorInDesignation != string.Empty && designation.EndsWith(separatorInDesignation))
            designation = designation.Remove(designation.Length - separatorInDesignation.Length);
        }
        return designation;
      }

      /// <summary>
      /// Определяющий ли документ типа docType для изделия типа atricleType
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="docTypeID">Тип документа</param>
      /// <param name="atricleTypeID">Тип изделия</param>
      /// <returns></returns>
      public static bool DefiningDocument(IUserSession session, int docTypeID, int atricleTypeID)
      {
        IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
        if (customService != null)
        {
          DocumentTypeSettings settings = customService.GetSettings(session.SessionGUID, docTypeID);
          if (settings.OutputObjectTypes != string.Empty)
            return settings.OutputObjectTypes.ToLower().Contains(Convert.ToString((object) (session.GetObjectType(atricleTypeID) as IDBGuid).GUID).ToLower());
        }
        return false;
      }
    }
}
