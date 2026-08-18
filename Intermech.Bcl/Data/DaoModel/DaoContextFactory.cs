
// Type: Intermech.Data.DaoModel.DaoContextFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Data.DaoModel
{
    /// <summary>
    /// Позволяет реализовать фабрику объектов, связанных с конкретным экземпляром базы данных.
    /// Тип является thread safe, когда его состояние заморожено.
    /// </summary>
    public class DaoContextFactory : FreezableConfigurationObject
    {
      private Type sqlProviderType;
      private string connectionString;

      /// <summary>Возвращает или задает строку подключения.</summary>
      public string ConnectionString
      {
        [DebuggerStepThrough] get => this.connectionString;
        [DebuggerStepThrough] set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ConnectionString));
          this.connectionString = value;
        }
      }

      /// <summary>Возвращает или задает тип провайдера Sql-сервисов.</summary>
      public Type SqlProviderType
      {
        [DebuggerStepThrough] get => this.sqlProviderType;
        [DebuggerStepThrough] set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SqlProviderType));
          this.sqlProviderType = value;
        }
      }

      public override void Assign(FreezableConfigurationObject other)
      {
        if (other is DaoContextFactory daoContextFactory)
        {
          this.connectionString = daoContextFactory.connectionString;
          this.sqlProviderType = daoContextFactory.sqlProviderType;
        }
        else
        {
          this.connectionString = (string) null;
          this.sqlProviderType = (Type) null;
        }
      }

      protected override void DoValidate()
      {
        base.DoValidate();
        if (string.IsNullOrEmpty(this.connectionString))
          throw new DaoContextException("Не задано значение свойства ConnectionString.");
        if (this.sqlProviderType == (Type) null)
          throw new DaoContextException("Не задано значение свойства SqlProviderType.");
      }

      public ISqlProviderServices CreateSqlServices()
      {
        this.RequireFrozen();
        return (ISqlProviderServices) Activator.CreateInstance(this.sqlProviderType);
      }

      /// <summary>
      /// Освобождает все ресурсы, связанные с экземпляром базы данных.
      /// </summary>
      public void ReleaseDatabase()
      {
        this.RequireFrozen();
        DaoContext.ClearCache(this.connectionString);
        this.CreateSqlServices().ClearConnectionPool(this.connectionString);
      }
    }
}
