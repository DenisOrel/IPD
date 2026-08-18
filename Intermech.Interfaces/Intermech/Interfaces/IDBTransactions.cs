
// Type: Intermech.Interfaces.IDBTransactions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IDBTransactions
    {
      /// <summary>Стартовать транзакцию</summary>
      void StartTransaction();

      /// <summary>Завершить транзакцию</summary>
      void Commit();

      /// <summary>Откатить транзакцию</summary>
      void Rollback();

      /// <summary>Возвращает true если стартована транзакция</summary>
      bool InTransaction { get; }

      /// <summary>
      /// Стартует режим регистрации создания объектов и связей. Регистрация ведётся как основной сессией, так и всеми её клонами.
      /// Все созданные за время регистрации связи и версии объектов могут быть удалены вызовом метода RollBackCreationLog.
      /// </summary>
      void StartCreationLog();

      /// <summary>
      /// Завершает режим регистрации создания объектов и связей.
      /// </summary>
      void CommitCreationLog();

      /// <summary>
      /// Удаляет все версии объектов и связей, созданные с момента вызова метода StartCreationLog(). После режим регистрации
      /// создания версий объектов и связей завершается.
      /// </summary>
      void RollBackCreationLog();

      /// <summary>
      /// Удаляет указанные в purgeList версии объектов и их связи. Используется для отмены создания новых версий и объектов в диалогах.
      /// Для безопасности позволяет удалять только объекты этого же владельца.
      /// </summary>
      void RollBackCreationLog(long[] purgeList);

      /// <summary>
      /// Возвращает true, если включен режим регистрации создания версий объектов и связей.
      /// </summary>
      bool InCreationLogMode { get; }

      /// <summary>
      /// Приостанавливает ведение журнала созданных версий объектов и связей
      /// </summary>
      void SuspendCreationLog();

      /// <summary>
      /// Возобновляет ведение журнала созданных версий объектов и связей
      /// </summary>
      void ResumeCreationLog();

      /// <summary>
      /// Возвращат массив событий создания версий объектов и связей из лога их создания (инициированного командой StartCreationLog())
      /// </summary>
      CategoryValue[] GetCreationLog();

      /// <summary>
      /// Включает/выключает режим автоматического отката транзакций ядром при возникновении ошибок (по умолчанию включен). Пользоваться только при понимании и из try..finally.
      /// </summary>
      bool AutoRollback { get; set; }
    }
}
