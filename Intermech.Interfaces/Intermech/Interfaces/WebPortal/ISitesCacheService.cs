
// Type: Intermech.Interfaces.WebPortal.ISitesCacheService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Интерфейс на кэш со списком узлов</summary>
    public interface ISitesCacheService
    {
      /// <summary>Все узлы</summary>
      List<SiteInfo> Sites { get; }

      /// <summary>Перечитать кэш</summary>
      /// <param name="session">При вызове с клиента передаем GUID сессии, при вызове с сервера - IUserSession</param>
      void Reload(object session);

      /// <summary>
      /// Получить информацию об узле информационной системы по его коду
      /// </summary>
      /// <param name="code">Код узла</param>
      /// <returns></returns>
      SiteInfo GetSite(char code);

      /// <summary>
      /// Получить информацию об узле информационной системы по его идентификатору
      /// </summary>
      /// <param name="id">Идентификатор версии объекта узла</param>
      /// <returns></returns>
      SiteInfo GetSite(long id);

      /// <summary>
      /// Получить информацию об узле информационной системы по его глобальному идентификатору
      /// </summary>
      /// <param name="guid">Глобальный идентификатор версии объекта узла</param>
      /// <returns></returns>
      SiteInfo GetSite(Guid guid);

      /// <summary>
      /// Получить информацию об узле информационной системы по его глобальному идентификатору
      /// </summary>
      /// <param name="name">Наименование узла</param>
      /// <returns></returns>
      SiteInfo GetSite(string name);

      /// <summary>
      /// Получить информацию об узле информационной системы по его глобальному идентификатору
      /// </summary>
      /// <param name="guid">Глобальный идентификатор версии объекта узла</param>
      /// <param name="throwException">Генерить exception если сайт не найден</param>
      /// <returns></returns>
      SiteInfo GetSite(Guid guid, bool throwException);

      /// <summary>
      /// Список идентификаторов зарегистрированных в системе узлов
      /// </summary>
      long[] SitesIDs { get; }

      /// <summary>Информация текущего узла</summary>
      SiteInfo Info { get; }

      /// <summary>Получить следующую букву для нового узла</summary>
      /// <returns></returns>
      char NextCode();

      /// <summary>
      /// Получить расшифровку значения поля F_SITE_ID у объекта
      /// </summary>
      /// <param name="siteID">Значение F_SITE_ID</param>
      /// <returns></returns>
      string GetSiteDescription(string siteID);

      /// <summary>Флаг того, что это портал</summary>
      bool IsPortal { get; set; }
    }
}
