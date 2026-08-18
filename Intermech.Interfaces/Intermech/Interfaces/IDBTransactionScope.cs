
// Type: Intermech.Interfaces.IDBTransactionScope
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с транзакцией. При создании объекта с данным интерфейсом транзакция стартует автоматом, а при вызове Dispose() - откатывается.
    /// </summary>
    public interface IDBTransactionScope : IDisposable
    {
      /// <summary>Завершить транзакцию</summary>
      void Commit();

      /// <summary>Откатить транзакцию</summary>
      void Rollback();

      /// <summary>Возвращает true если стартована транзакция</summary>
      bool InTransaction { get; }

      /// <summary>
      /// Включает/выключает режим автоматического отката транзакций ядром при возникновении ошибок (по умолчанию включен). Пользоваться только при понимании и из try..finally.
      /// </summary>
      bool AutoRollback { get; set; }
    }
}
