// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.DocumentTypeSettingsHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.HelperClasses;

/// <summary>Вспомогательный класс получения DocumentTypeSettings</summary>
internal class DocumentTypeSettingsHelper
{
  private static Dictionary<int, DocumentTypeSettings> set = new Dictionary<int, DocumentTypeSettings>();

  /// <summary>Обновить кэш DocumentTypeSettings для типа документа</summary>
  /// <param name="documentType">Тип документа</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>DocumentTypeSettings</returns>
  internal static void ReloadSettings(
    IUserSession session,
    IDocumentTypeSettingsService docSettingsService,
    int documentType)
  {
    if (docSettingsService == null)
      return;
    DocumentTypeSettings settings = docSettingsService.GetSettings(session.SessionGUID, documentType);
    DocumentTypeSettingsHelper.set[documentType] = settings;
  }

  /// <summary>Получить DocumentTypeSettings по типу документа</summary>
  /// <param name="documentType">Тип документа</param>
  /// <returns>DocumentTypeSettings</returns>
  internal static DocumentTypeSettings GetSettings(int documentType)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(documentType, AvsIDCache.ObjType_Document))
      return DocumentTypeSettings.CreateDefault();
    if (!DocumentTypeSettingsHelper.set.ContainsKey(documentType))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDocumentTypeSettingsService customService = sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService;
        DocumentTypeSettingsHelper.ReloadSettings(sessionKeeper.Session, customService, documentType);
      }
    }
    return DocumentTypeSettingsHelper.set[documentType];
  }

  /// <summary>
  /// Получить имя типа документа для вывода в штампе документа
  /// </summary>
  /// <param name="documentType"></param>
  /// <returns></returns>
  internal static string GetDocTypeName(int documentType)
  {
    DocumentTypeSettings settings = DocumentTypeSettingsHelper.GetSettings(documentType);
    string docTypeName = "";
    if (settings.DocumentNameInStamp)
      docTypeName = settings.DocumentTypeName;
    return docTypeName;
  }
}
