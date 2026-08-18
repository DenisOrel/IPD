
// Type: Intermech.Data.DaoModel.DaoContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;


namespace Intermech.Data.DaoModel
{
    /// <summary>
    /// <para>Базовый класс для объектов, предоставляющих объектный API для доступа к SQL-базе данных.
    /// Реализует подключение к базе данных, создание/обновление метаданных с помошью DDL, а также
    /// трансляцию объектного API в DML и выполнение с помощью ADO.NET.</para>
    /// <para>Класс не является thread-safe, клиент класса должен создавать новый экземпляр контекста
    /// при каждом обращении к базе данных.</para>
    /// </summary>
    public class DaoContext
    {
      private static readonly ConcurrentDictionary<string, IDbConnectionPool> connectionPoolCache = new ConcurrentDictionary<string, IDbConnectionPool>();
      private static readonly ConcurrentDictionary<string, DaoContextCacheData> connectionDataCache = new ConcurrentDictionary<string, DaoContextCacheData>();
      private static readonly TraceSwitch traceFlag = new TraceSwitch("Intermech.Data.DaoModel.DaoContext", "", "0");
      private readonly ISqlProviderServices sqlServices;
      private readonly DaoServiceList services;
      private readonly List<DaoService> startedServices;
      private DaoContextState state;
      private string connectionString;
      private IDbConnectionPool connectionPool;

      public DaoContext(ISqlProviderServices sqlServices)
      {
        this.sqlServices = sqlServices != null ? sqlServices : throw new ArgumentNullException(nameof (sqlServices));
        this.services = new DaoServiceList(this);
        this.startedServices = new List<DaoService>();
        this.state = DaoContextState.Closed;
      }

      public static void ClearCache()
      {
        KeyValuePair<string, IDbConnectionPool>[] array = DaoContext.connectionPoolCache.ToArray();
        DaoContext.connectionPoolCache.Clear();
        foreach (KeyValuePair<string, IDbConnectionPool> keyValuePair in array)
          keyValuePair.Value.ClearPool();
        DaoContext.connectionDataCache.ToArray();
        DaoContext.connectionDataCache.Clear();
      }

      public static void ClearCache(string connectionString)
      {
        if (connectionString == null)
          throw new ArgumentNullException(nameof (connectionString));
        IDbConnectionPool dbConnectionPool;
        if (DaoContext.connectionPoolCache.TryRemove(connectionString, out dbConnectionPool))
          dbConnectionPool.ClearPool();
        DaoContext.connectionDataCache.TryRemove(connectionString, out DaoContextCacheData _);
      }

      protected internal void RequireClosed()
      {
        if (this.state != DaoContextState.Closed && this.state != DaoContextState.Closing)
          throw new DaoContextException("Объект контекста должен быть закрыт.");
      }

      protected internal void RequireOpen()
      {
        if (this.state != DaoContextState.Open && this.state != DaoContextState.Opening)
          throw new DaoContextException("Объект контекста должен быть открыт.");
      }

      /// <summary>
      /// Инициализирует и открывает контекст, если это еще не было сделано.
      /// </summary>
      public void LazyOpen()
      {
        if (this.state == DaoContextState.Closed)
          this.Open();
        else
          this.RequireOpen();
      }

      /// <summary>
      /// Инициализирует и открывает контекст. При открытии первого контекста выполняется создание/обновление метаданных,
      /// а также другие задачи обслуживания базы данных.
      /// </summary>
      /// <exception cref="T:Intermech.Data.DaoModel.DaoException">Контекст уже был инициализирован и открыт</exception>
      public void Open()
      {
        this.RequireClosed();
        this.OpenCore();
      }

      private void OpenCore()
      {
        this.DoValidateConfiguration();
        this.state = DaoContextState.Opening;
        try
        {
          this.connectionPool = this.GetOrCreateConnectionPool();
          this.ProcessFirstOpen(DaoContext.connectionDataCache.GetOrAdd(this.connectionString, (Func<string, DaoContextCacheData>) (arg => new DaoContextCacheData())));
          foreach (DaoService service in (Collection<DaoService>) this.services)
          {
            this.SafelyStartService(service);
            this.startedServices.Add(service);
          }
          this.state = DaoContextState.Open;
        }
        catch
        {
          this.OptionalCloseCore();
          throw;
        }
      }

      private void ProcessFirstOpen(DaoContextCacheData connectionData)
      {
        lock (connectionData.FirstOpenSyncObject)
        {
          if (connectionData.FirstOpenComplete)
            return;
          int num = this.sqlServices.IsNewDatabase(this.connectionString) ? 1 : 0;
          if (num != 0)
            this.sqlServices.CreateNewDatabase(this.connectionString);
          this.DoRunMaintenance(new DbMaintenanceInfo(num != 0));
          connectionData.FirstOpenComplete = true;
        }
      }

      /// <summary>
      /// Закрывает открытый ранее контекст. Использование этого метода не является обязательным,
      /// и типовых случаях работы с контекстом он не вызывается никогда. Как правило,
      /// метод используется для временного закрытия контекста при необходимости изменения конфигурации контекста.
      /// </summary>
      public void OptionalClose()
      {
        if (this.state != DaoContextState.Open)
          return;
        this.OptionalCloseCore();
      }

      private void OptionalCloseCore()
      {
        this.state = DaoContextState.Closing;
        this.StopStartedServices();
        this.connectionPool = (IDbConnectionPool) null;
        this.state = DaoContextState.Closed;
      }

      private void StopStartedServices()
      {
        List<DaoService> daoServiceList = new List<DaoService>((IEnumerable<DaoService>) this.startedServices);
        daoServiceList.Reverse();
        foreach (DaoService service in daoServiceList)
          this.SafelyStopService(service);
        this.startedServices.Clear();
      }

      private void SafelyStartService(DaoService service)
      {
        try
        {
          service.Start();
        }
        catch
        {
          this.SafelyStopService(service);
          throw;
        }
      }

      private void SafelyStopService(DaoService service)
      {
        try
        {
          service.Stop();
        }
        catch (Exception ex)
        {
          this.TraceStopServiceException(ex);
        }
      }

      [ExcludeFromCodeCoverage]
      private void TraceStopServiceException(Exception x)
      {
        if (!DaoContext.traceFlag.TraceError)
          return;
        Trace.WriteLine("A exception occured in method DaoService.Stop(). This exception is suppressed.");
        Trace.WriteLine(x.Message);
        Trace.WriteLine(x.StackTrace);
      }

      /// <summary>
      /// Проверяет корректность конфигурации контекста. Метод вызывается непосредственно перед открытием контекста и запуском всех сервисов.
      /// Базовая реализация проверяет корректность конфигурации всех сервисов контекста.
      /// </summary>
      protected virtual void DoValidateConfiguration()
      {
        if (this.connectionString == null)
          throw new DaoContextException("Строка подключения не задана.");
        foreach (DaoService service in (Collection<DaoService>) this.services)
          service.ValidateConfiguration();
      }

      /// <summary>
      /// Выполняет создание/обновление метаданных, а также другие задачи обслуживания базы данных.
      /// Метод вызывается в процессе открытия первого контекста после подключения к базе данных, но перед запуском сервисов.
      /// </summary>
      /// <param name="info">Параметры режима обслуживания и информация о базе данных</param>
      protected virtual void DoRunMaintenance(DbMaintenanceInfo info)
      {
        foreach (DaoService service in (Collection<DaoService>) this.services)
          service.RunMaintenance(info);
      }

      /// <summary>
      /// Возвращает пул подключений, соответствующий строке подключения. Значение будет пусто, если контекст не был открыт.
      /// </summary>
      public IDbConnectionPool ConnectionPool => this.connectionPool;

      private IDbConnectionPool GetOrCreateConnectionPool()
      {
        return DaoContext.connectionPoolCache.GetOrAdd(this.connectionString, (Func<string, IDbConnectionPool>) (key => this.DoCreateConnectionPool()));
      }

      protected virtual IDbConnectionPool DoCreateConnectionPool()
      {
        return (IDbConnectionPool) new DbConnectionPool(this.sqlServices.Factory, this.connectionString, 16 /*0x10*/);
      }

      public object TryGetService(Type serviceType)
      {
        if (serviceType == (Type) null)
          throw new ArgumentNullException(nameof (serviceType));
        foreach (DaoService service in (Collection<DaoService>) this.services)
        {
          if (serviceType.IsAssignableFrom(service.GetType()))
            return (object) service;
        }
        return (object) null;
      }

      public object GetService(Type serviceType)
      {
        return this.TryGetService(serviceType) ?? throw new DaoContextException($"Сервис типа '{serviceType}' не предоставляется объектом контекста.");
      }

      public TService GetService<TService>() => (TService) this.GetService(typeof (TService));

      public ISqlProviderServices SqlServices => this.sqlServices;

      public DaoContextState State => this.state;

      /// <summary>
      /// Возвращает или задает строку подключения. Изменять строку подключения можно только до открытия контекста.
      /// </summary>
      public string ConnectionString
      {
        get => this.connectionString;
        set
        {
          this.RequireClosed();
          this.connectionString = value;
        }
      }

      public DaoServiceList Services => this.services;
    }
}
