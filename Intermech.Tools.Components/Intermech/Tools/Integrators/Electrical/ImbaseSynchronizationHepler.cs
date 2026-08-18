// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ImbaseSynchronizationHepler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public static class ImbaseSynchronizationHepler
{
  /// <summary>Синхронизация состава с Imbase</summary>
  /// <param name="articles">Список изделий для синхронизации</param>
  /// <param name="settings">Настройки интегратора</param>
  /// <param name="partCodec">Кодек атрибутов изделия в составе (компонентов схемы)</param>
  /// <returns></returns>
  public static bool Synchronize(
    ICollection<InitialArticleData> articles,
    ECADIntegratorSettings settings,
    IAttributeCodec partCodec)
  {
    try
    {
      if (!settings.ImbaseSync)
        return true;
      if (settings.ImbaseSyncAttribute == null)
        throw new Exception("Не указано наименование атрибута, по которому производится синхронизация.");
      bool flag = false;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseUsingAttGUID);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true);
        foreach (InitialArticleData article in (IEnumerable<InitialArticleData>) articles)
        {
          ElectricalArticleCache electricalArticleCache = article.CustomSections.Get<ElectricalArticleCache>();
          if (electricalArticleCache.ArticleType == ArticleTypes.Component)
          {
            ContainerValues containerValues = partCodec.ReadFileProperties(electricalArticleCache.Article, (ICollection<StringKey>) new StringKey[2]
            {
              new StringKey(settings.ImbaseSyncAttribute.Name),
              new StringKey(IDCache.Default.ImbaseKey.Text)
            });
            if (containerValues.Bag != null && containerValues.Bag.Count > 0)
            {
              string identityValue = containerValues.Bag.Read<string>(containerValues.Bag.Keys[0], (string) null);
              string imbaseKey = containerValues.Bag.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null);
              if (string.IsNullOrEmpty(identityValue))
              {
                article.CustomSections.Set((object) new ImbaseSyncInfo(ImbaseSyncTypes.EmptyValue));
                flag = true;
              }
              else
              {
                long tableId;
                long recordId;
                if (Intermech.Tools.Data.ImbaseHelper.FindRecordByIndex(sessionKeeper.Session, (StringKey) settings.ImbaseSyncAttribute.Name, settings.ImbaseSyncAttribute.Id, identityValue, out tableId, out recordId))
                {
                  if (settings.ImbaseSyncCheckApplicability && !ImbaseSynchronizationHepler.CheckImbaseApplicability(sessionKeeper.Session, service, attributeTypeId, tableId, recordId))
                  {
                    article.CustomSections.Set((object) new ImbaseSyncInfo(ImbaseSyncTypes.Forbidden, tableId, recordId));
                    flag = true;
                  }
                  else
                  {
                    if (string.IsNullOrEmpty(imbaseKey))
                      imbaseKey = Intermech.Tools.Data.ImbaseHelper.CreateImbaseObject(tableId, recordId).Item3;
                    article.CustomSections.Set((object) new ImbaseSyncInfo(tableId, recordId, imbaseKey));
                    article.ArticleKind = MechanicalArticleKind.ImbaseObject;
                  }
                }
                else
                {
                  article.CustomSections.Set((object) new ImbaseSyncInfo(ImbaseSyncTypes.NotFound));
                  flag = true;
                }
              }
            }
          }
        }
        if (flag)
        {
          using (ImbaseSyncReport imbaseSyncReport = new ImbaseSyncReport(articles))
          {
            imbaseSyncReport.Initialize();
            int num = (int) imbaseSyncReport.ShowDialog();
            return false;
          }
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      throw new Exception($"Ошибка при синхронизации изделий с Imbase: {ex.Message}", ex);
    }
  }

  private static bool CheckImbaseApplicability(
    IUserSession session,
    IImbaseServer imbaseServerService,
    int attributeUsingID,
    long tableID,
    long recordID)
  {
    DataTable recordsTable = (DataTable) null;
    AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
    imbaseServerService.LoadRecords(session.SessionGUID, tableID, $"[{-2}]={recordID}", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
    if (recordsTable == null || recordsTable.Rows.Count <= 0)
      return false;
    return Array.FindIndex<AttributeTypeProperties>(columnsAttributes, (Predicate<AttributeTypeProperties>) (x => x.AttributeID.Equals(attributeUsingID))) < 0 || Convert.ToString(recordsTable.Rows[0][attributeUsingID.ToString()]).Equals("+");
  }

  public static string ImbaseBinding(string imbaseKey, string partName)
  {
    if (string.IsNullOrEmpty(partName))
      throw new ArgumentNullException(nameof (partName));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long num = 0;
      if (Intermech.Tools.Data.ImbaseHelper.IsImbaseKey(imbaseKey))
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid(imbaseKey.Substring(2)));
        if (!objectInfo.Empty)
          num = objectInfo.ObjectID;
      }
      if (num == 0L)
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad0038d-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(sessionKeeper.Session.IdentHelper.NameID, RelationalOperators.Equal, (object) partName, LogicalOperators.AND, 0, false)
        }, new object[1]{ (object) -2 }));
        if (dataTable.Rows.Count == 0)
          throw new Exception($"Связываемое изделие {partName} не найдено в базе IPS");
        num = Convert.ToInt64(dataTable.Rows[0][0]);
      }
      Tuple<long, long> tuple = ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service1 ? service1.SelectRecord("Связать с IMBASE", "", num) : throw new Exception("Сервис выбора из IMBASE не зарегистрирован");
      if (tuple != null)
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
          throw new Exception("Не удалось получить сервис для работы с IMBASE.");
        customService.FillObjectAttributes(sessionKeeper.Session.SessionGUID, num, tuple.Item1, tuple.Item2, true);
      }
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2)
        service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", num));
      return $"IG{sessionKeeper.Session.GetObjectInfo(num).VersionGuid:D}";
    }
  }
}
