// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ArticleService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// 
/// </summary>
public class ArticleService : IArticleService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticleID(designation, okpCode, name, filtrationRuleSettings, (object) userSession.SessionGUID) : 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <param name="firstInMaterials"></param>
  /// <returns></returns>
  public long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticleID(designation, okpCode, name, filtrationRuleSettings, (object) userSession.SessionGUID, firstInMaterials) : 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticleObject(designation, okpCode, name, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <param name="firstInMaterials"></param>
  /// <returns></returns>
  public IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticleObject(designation, okpCode, name, filtrationRuleSettings, (object) userSession.SessionGUID, firstInMaterials) : (IDBObject) null;
  }

  /// <summary>Найти все исполнения по документу</summary>
  /// <param name="documentID">Идентификатор версии документа</param>
  /// <param name="filtrationRuleSettings">Настройки фильтрации</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public long[] FindArticles(long documentID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticles(documentID, filtrationRuleSettings, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// Найти все исполнения по документу не учитывая фильтрацию
  /// </summary>
  /// <param name="documentID">Идентификатор версии документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public long[] FindArticlesWithoutFiltration(long documentID, string versionsRule, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticlesWithoutFiltration(documentID, versionsRule, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long[] FindArticlesByGroupID(long articleID, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticlesByGroupID(articleID, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="versionsRule"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long[] FindArticlesByGroupIDWithoutFiltration(long articleID, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindArticlesByGroupIDWithoutFiltration(articleID, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long FindMainDocumentID(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMainDocumentID(articleID, filtrationRuleSettings, (object) userSession.SessionGUID) : 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long[] FindMainDocuments(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMainDocuments(articleID, filtrationRuleSettings, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindMainDocument(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMainDocument(articleID, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleIDs"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public long[] FindMainDocuments(long[] articleIDs, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMainDocuments(articleIDs, filtrationRuleSettings, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// <inheritdoc cref="M:Intermech.Interfaces.Pdm.IArticleService.FindArticlesByGroupIDWithoutFiltration(System.Int64,System.Object)" />
  /// </summary>
  public long[] FindMainDocumentIDsForAllDrawings(
    long[] articleIDs,
    string filtrationRuleSettings,
    object session)
  {
    return session is IUserSession userSession && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMainDocumentIDsForAllDrawings(articleIDs, filtrationRuleSettings, (object) userSession.SessionGUID) : (long[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="documentID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindBaseArticle(long documentID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindBaseArticle(documentID, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="documentID"></param>
  /// <param name="value"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindBaseArticleForValue(
    long documentID,
    string value,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindBaseArticleForValue(documentID, value, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="articleID"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public List<long> GetListInstances(long articleID, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.GetListInstances(articleID, (object) userSession.SessionGUID) : new List<long>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="groupID"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public List<long> GetListInstances(object groupID, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.GetListInstances(groupID, (object) userSession.SessionGUID) : new List<long>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="documentID"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public List<QuickObjectInfo> FindListInstances(
    long documentID,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindListInstances(documentID, filtrationRuleSettings, (object) userSession.SessionGUID) : new List<QuickObjectInfo>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMaterial(designation, okpCode, name, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="designation"></param>
  /// <param name="okpCode"></param>
  /// <param name="name"></param>
  /// <param name="materialType"></param>
  /// <param name="filtrationRuleSettings"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    int materialType,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindMaterial(designation, okpCode, name, materialType, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }

  public long GetMaterialID(string name, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.GetMaterialID(name, filtrationRuleSettings, (object) userSession.SessionGUID) : 0L;
  }

  public long GetMaterialID(
    string name,
    string filtrationRuleSettings,
    object session,
    bool trueMaterialsOnly)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.GetMaterialID(name, filtrationRuleSettings, (object) userSession.SessionGUID, trueMaterialsOnly) : 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="materialID"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public string GetMaterialName(long materialID, object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.GetMaterialName(materialID, (object) userSession.SessionGUID) : string.Empty;
  }

  public long FindDocumentID(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindDocumentID(designation, name, filtrationRuleSettings, (object) userSession.SessionGUID) : 0L;
  }

  public IDBObject FindDocumentObject(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = session is IUserSession ? session as IUserSession : (IUserSession) null;
    return userSession != null && userSession.GetCustomService(typeof (IArticleService)) is IArticleService customService ? customService.FindDocumentObject(designation, name, filtrationRuleSettings, (object) userSession.SessionGUID) : (IDBObject) null;
  }
}
