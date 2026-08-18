
// Type: Intermech.Data.DaoModel.DaoService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.DaoModel
{
    /// <summary>
    /// Базовый класс для сервисов, реализующих трансляцию объектного API в SQL, выполняемый с помощью ADO.NET.
    /// </summary>
    public class DaoService
    {
      private DaoContext daoContext;

      protected virtual void RequireStopped()
      {
        if (this.daoContext == null)
          return;
        this.daoContext.RequireClosed();
      }

      protected virtual void RequireStarted()
      {
        if (this.daoContext == null)
          throw new InvalidOperationException("Сервис не является частью контекста.");
        this.daoContext.RequireOpen();
      }

      protected internal DaoContext DaoContext
      {
        get => this.daoContext;
        internal set => this.daoContext = value;
      }

      protected IDbConnectionPool ConnectionPool => this.daoContext.ConnectionPool;

      /// <summary>
      /// Проверяет корректность конфигурации сервиса. Метод вызывается непосредственно перед открытием контекста и запуском всех сервисов.
      /// </summary>
      protected internal virtual void ValidateConfiguration()
      {
      }

      /// <summary>
      /// Выполняет создание/обновление метаданных, а также другие задачи обслуживания базы данных.
      /// Метод вызывается в процессе открытия первого контекста после подключения к базе данных, но перед запуском сервисов.
      /// </summary>
      /// <param name="info">Параметры режима обслуживания и информация о базе данных</param>
      protected internal virtual void RunMaintenance(DbMaintenanceInfo info)
      {
      }

      /// <summary>Выполняет запуск сервиса.</summary>
      protected internal virtual void Start()
      {
      }

      /// <summary>
      /// Выполняет останов сервиса. Метод вызывается при временном закрытии контекста, а также
      /// в случае ошибки запуска этого сервиса или любого другого сервиса в этом контексте.
      /// В типовых случаях работы с контекстом он не вызывается никогда.
      /// </summary>
      protected internal virtual void Stop()
      {
      }
    }
}
