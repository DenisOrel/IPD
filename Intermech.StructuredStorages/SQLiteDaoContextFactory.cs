using Intermech.Data.DaoModel;
using System.Diagnostics;


namespace Intermech.Data.SQLite
{
    /// <summary>
    /// Позволяет реализовать фабрику объектов, связанных с конкретным экземпляром базы данных SQLite.
    /// Тип является thread safe, когда его состояние заморожено.
    /// </summary>
    public class SQLiteDaoContextFactory : DaoContextFactory
    {
      private string dbFilePath;
      private int? cacheSizeInKBytes;
      private bool? asynWritesMode;

      /// <summary>Создает объект.</summary>
      public SQLiteDaoContextFactory() => this.SqlProviderType = typeof (SQLiteProviderServices);

      /// <summary>
      /// Возвращает или задает путь к файлу базы данных. Значение свойства может быть не задано.
      /// Если значение свойства ConnectionString не задано, то это свойство используется для автоматического
      /// заполнения ConnectionString.
      /// </summary>
      public string DbFilePath
      {
        [DebuggerStepThrough] get => this.dbFilePath;
        [DebuggerStepThrough] set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (DbFilePath));
          this.dbFilePath = value;
        }
      }

      /// <summary>
      /// Возвращает или задает кэш базы данных в памяти. Значение свойства может быть не задано.
      /// Если значение свойства ConnectionString не задано, то это свойство используется для автоматического
      /// заполнения ConnectionString.
      /// </summary>
      public int? CacheSizeInKBytes
      {
        [DebuggerStepThrough] get => this.cacheSizeInKBytes;
        [DebuggerStepThrough] set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (CacheSizeInKBytes));
          this.cacheSizeInKBytes = value;
        }
      }

      /// <summary>
      /// Возвращает или задает режим асинхронной записи данных на диск. При включении позволяет значительно поднять скорость работы базы данных за счет надежности. Значение свойства может быть не задано.
      /// Если значение свойства ConnectionString не задано, то это свойство используется для автоматического
      /// заполнения ConnectionString.
      /// </summary>
      public bool? AsyncWritesMode
      {
        [DebuggerStepThrough] get => this.asynWritesMode;
        [DebuggerStepThrough] set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AsyncWritesMode));
          this.asynWritesMode = value;
        }
      }

      protected override void DoValidate()
      {
        if (string.IsNullOrEmpty(this.ConnectionString))
        {
          if (string.IsNullOrEmpty(this.DbFilePath))
            throw new DaoContextException("Не задано значение свойства DbFilePath.");
          int? cacheSizeInKbytes = this.CacheSizeInKBytes;
          if (cacheSizeInKbytes.HasValue)
          {
            cacheSizeInKbytes = this.CacheSizeInKBytes;
            if (cacheSizeInKbytes.Value > 0)
              goto label_6;
          }
          this.CacheSizeInKBytes = new int?(1024 /*0x0400*/);
    label_6:
          bool? asyncWritesMode = this.AsyncWritesMode;
          if (!asyncWritesMode.HasValue)
            this.AsyncWritesMode = new bool?(false);
          string dbFilePath = this.DbFilePath;
          cacheSizeInKbytes = this.CacheSizeInKBytes;
          int cacheSizeInKBytes = cacheSizeInKbytes.Value;
          asyncWritesMode = this.AsyncWritesMode;
          int num = asyncWritesMode.Value ? 1 : 0;
          this.ConnectionString = SQLiteUtils.MakeConnectionString(dbFilePath, 8192 /*0x2000*/, cacheSizeInKBytes, num != 0);
        }
        base.DoValidate();
      }
    }
}
