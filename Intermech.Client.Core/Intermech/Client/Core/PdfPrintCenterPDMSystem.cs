
// Type: Intermech.Client.Core.PdfPrintCenterPDMSystem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.PdfPrintCenter.Connector;
using Intermech.Remoting.Ipc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;


namespace Intermech.Client.Core;

/// <summary>
/// Сервис получения и записи настроек центра печати из базы данных. Реализация является thread safe
/// </summary>
internal sealed class PdfPrintCenterPDMSystem : MarshalByRefObject, IPDMSystem, IReliableIpcObject
{
  private static readonly string PrintersSettingsConfigName = "PdfPrintCenterPrintersSettings.xml";
  private static readonly string WatermarkSettingsConfigName = "PdfPrintCenterWatermarkSettings.xml";
  private static readonly string WindowSettingsConfigName = "PdfPrintCenterWindowSettings.xml";
  private PdfPrintCenterLayoutIdCache printCenterLayoutIdCache;
  private IInvokeService invokeService;
  private readonly object syncRoot = new object();

  public PdfPrintCenterPDMSystem(
    PdfPrintCenterLayoutIdCache printCenterLayoutIdCache,
    IInvokeService invokeService)
  {
    if (printCenterLayoutIdCache == null)
      throw new ArgumentNullException(nameof (printCenterLayoutIdCache));
    if (invokeService == null)
      throw new ArgumentNullException(nameof (invokeService));
    this.printCenterLayoutIdCache = printCenterLayoutIdCache;
    this.invokeService = invokeService;
  }

  /// <summary>
  /// Инициализирует сервис управления временем жизни для текущего объекта.
  /// </summary>
  /// <returns>null, так как это long-life object</returns>
  public override object InitializeLifetimeService() => (object) null;

  /// <summary>
  /// Позволяет выбрать макет из списка сохраненных в базе данных
  /// </summary>
  /// <returns>id выбранного макета (Int64) либо null, если элемент не был выбран</returns>
  public object ChooseLayout()
  {
    lock (this.syncRoot)
    {
      long[] numArray;
      try
      {
        numArray = this.invokeService.InvokeFunc<long[]>(-1, (Func<long[]>) (() => SelectionWindow.SelectObjects("Центр печати PDF", "Выберите макет для открытия в редакторе", this.printCenterLayoutIdCache.LayoutLocalId, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect)));
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
      if (numArray != null)
        return (object) numArray[0];
      return (object) null;
    }
  }

  /// <summary>Возвращает список id всех макетов в базе данных</summary>
  /// <returns>Список id макетов</returns>
  public List<object> GetLayoutsId()
  {
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
    dbRecordSetParams.RecordCount = -1;
    dbRecordSetParams.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    lock (this.syncRoot)
    {
      List<object> layoutsId;
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable dataTable = sessionKeeper.Session.ObjectsSelect(this.printCenterLayoutIdCache.LayoutLocalId, dbRecordSetParams);
          layoutsId = new List<object>(dataTable.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            layoutsId.Add((object) int64);
          }
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
      return layoutsId;
    }
  }

  /// <summary>
  /// Загружает информацию о макете из базы данных по его id
  /// </summary>
  /// <param name="layoutId">id макета</param>
  /// <returns>Имя макета и информация о нем в xml-формате либо null, если объект не найден</returns>
  public PDMLayoutInfo LoadLayout(object layoutId)
  {
    if (layoutId == null)
      throw new ArgumentNullException(nameof (layoutId));
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(layoutId), false);
          return dbObject != null ? new PDMLayoutInfo(dbObject.GetAttributeByID(this.printCenterLayoutIdCache.LayoutNameLocalId).AsString, (string) dbObject.GetAttributeByID(this.printCenterLayoutIdCache.LayoutContentLocalId).Value) : (PDMLayoutInfo) null;
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Сохраняет в базу данных информацию о макете <paramref name="layoutInfo" />
  /// </summary>
  /// <param name="layoutInfo">Структура, содержащая имя макета и информацию о нем в формате</param>
  /// <param name="layoutId">id макета либо null, если требуется создать новый макет</param>
  /// <returns>id сохраненного макета</returns>
  public object SaveLayout(PDMLayoutInfo layoutInfo, object layoutId = null)
  {
    if (layoutInfo == null)
      throw new ArgumentNullException(nameof (layoutInfo));
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBObject dbObject = layoutId == null ? session.GetObjectCollection(this.printCenterLayoutIdCache.LayoutLocalId).Create() : session.GetObject(Convert.ToInt64(layoutId), true);
          dbObject.GetAttributeByID(this.printCenterLayoutIdCache.LayoutNameLocalId).Value = (object) layoutInfo.Name;
          dbObject.GetAttributeByID(this.printCenterLayoutIdCache.LayoutContentLocalId).Value = (object) layoutInfo.Content;
          if (dbObject.IsCreationMode)
            dbObject.CommitCreation(true);
          return (object) dbObject.ObjectID;
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Получает из базы данных настройки принтеров в виде xml-документа
  /// </summary>
  /// <returns>Настройки принтеров в виде xml-документа</returns>
  public string GetPrintersSettings()
  {
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          byte[] config_file;
          session.Configurations.LoadConfigData(PdfPrintCenterPDMSystem.PrintersSettingsConfigName, out BlobInformation _, out config_file, session.UserID);
          return this.GetConfigAsText(config_file, Encoding.UTF8);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Заносит в базу данных настройки принтеров в виде xml-документа
  /// </summary>
  /// <param name="xmlPrintersSettings">Настройки принтеров в виде xml-документа </param>
  public void PutPrintersSettings(string xmlPrintersSettings)
  {
    if (string.IsNullOrEmpty(xmlPrintersSettings))
      throw new ArgumentNullException(nameof (xmlPrintersSettings));
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          byte[] configFromText = this.GetConfigFromText(xmlPrintersSettings);
          BlobInformation config_info = new BlobInformation((long) configFromText.Length, (long) configFromText.Length, DateTime.Now, PdfPrintCenterPDMSystem.PrintersSettingsConfigName, ArcMethods.NotPacked, "");
          byte[] config_file = configFromText;
          long userId = session.UserID;
          configurations.WriteConfigData(config_info, config_file, userId);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Получает из базы данных настройки водяного знака в виде xml-документа
  /// </summary>
  /// <returns>Настройки водяного знака в виде xml-документа</returns>
  public string GetWatermakSettings()
  {
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          BlobInformation blobInformation = new BlobInformation();
          string settingsConfigName = PdfPrintCenterPDMSystem.WatermarkSettingsConfigName;
          ref BlobInformation local1 = ref blobInformation;
          byte[] configFile;
          ref byte[] local2 = ref configFile;
          long userId = session.UserID;
          configurations.LoadConfigData(settingsConfigName, out local1, out local2, userId);
          return this.GetConfigAsText(configFile, Encoding.UTF8);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Заносит в базу данных настройки водяного знака в виде xml-документа
  /// </summary>
  /// <param name="xmlPrintersSettings">Настройки водяного знака в виде xml-документа </param>
  public void PutWatermarkSettings(string xmlWatermarkSettings)
  {
    if (string.IsNullOrEmpty(xmlWatermarkSettings))
      throw new ArgumentNullException(nameof (xmlWatermarkSettings));
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          byte[] configFromText = this.GetConfigFromText(xmlWatermarkSettings);
          BlobInformation config_info = new BlobInformation((long) configFromText.Length, (long) configFromText.Length, DateTime.Now, PdfPrintCenterPDMSystem.WatermarkSettingsConfigName, ArcMethods.NotPacked, "");
          byte[] config_file = configFromText;
          long userId = session.UserID;
          configurations.WriteConfigData(config_info, config_file, userId);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Возвращает имя пользователя, выводящего на печать документ
  /// </summary>
  /// <returns>Имя пользователя, выводящего на печать документ</returns>
  public string GetCurrentUserName()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.UserName;
  }

  /// <summary>
  /// Получает из базы данных настройки основного окна в виде xml-документа
  /// </summary>
  /// <returns>Настройки основного окна в виде xml-документа</returns>
  public string GetWindowSettings()
  {
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          BlobInformation blobInformation = new BlobInformation();
          string settingsConfigName = PdfPrintCenterPDMSystem.WindowSettingsConfigName;
          ref BlobInformation local1 = ref blobInformation;
          byte[] configFile;
          ref byte[] local2 = ref configFile;
          long userId = session.UserID;
          configurations.LoadConfigData(settingsConfigName, out local1, out local2, userId);
          return this.GetConfigAsText(configFile, Encoding.UTF8);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  /// <summary>
  /// Заносит в базу данных настройки основного окна в виде xml-документа
  /// </summary>
  /// <param name="xmlWindowSettings">Настройки основного окна в виде xml-документа </param>
  public void PutWindowSettings(string xmlWindowSettings)
  {
    if (string.IsNullOrEmpty(xmlWindowSettings))
      throw new ArgumentNullException(nameof (xmlWindowSettings));
    lock (this.syncRoot)
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          byte[] configFromText = this.GetConfigFromText(xmlWindowSettings);
          BlobInformation config_info = new BlobInformation((long) configFromText.Length, (long) configFromText.Length, DateTime.Now, PdfPrintCenterPDMSystem.WindowSettingsConfigName, ArcMethods.NotPacked, "");
          byte[] config_file = configFromText;
          long userId = session.UserID;
          configurations.WriteConfigData(config_info, config_file, userId);
        }
      }
      catch (Exception ex)
      {
        throw IpcFaultException.FromOriginalException(ex);
      }
    }
  }

  public void KnockKnock()
  {
  }

  private string GetConfigAsText(byte[] configFile, Encoding encoding)
  {
    if (configFile.Length == 0)
      return string.Empty;
    using (MemoryStream memoryStream = new MemoryStream(configFile, false))
    {
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream, encoding))
        return streamReader.ReadToEnd();
    }
  }

  /// <summary>Преобразует строку в массив байтов</summary>
  private byte[] GetConfigFromText(string xmlConfig)
  {
    using (MemoryStream memoryStream = new MemoryStream(2 * xmlConfig.Length + 3))
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream, Encoding.UTF8))
      {
        streamWriter.Write(xmlConfig);
        streamWriter.Flush();
        return memoryStream.ToArray();
      }
    }
  }
}
