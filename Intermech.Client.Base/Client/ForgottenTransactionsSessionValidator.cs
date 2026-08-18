
// Type: Intermech.Client.ForgottenTransactionsSessionValidator
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces;
using System;
using System.Threading;


namespace Intermech.Client
{
    /// <summary>
    /// Реализует защиту пользовательских сессий сервера приложений от незакрытых транзакций.
    /// </summary>
    internal sealed class ForgottenTransactionsSessionValidator : SessionValidator
    {
      private IServerEventLogService serverEventLog;

      public ForgottenTransactionsSessionValidator(IServerEventLogService serverEventLog)
      {
        this.serverEventLog = serverEventLog != null ? serverEventLog : throw new ArgumentNullException(nameof (serverEventLog));
      }

      /// <summary>
      /// Реализует проверку состояния пользовательской сессии. Если состояние сессии невалидно, то метод должен
      /// сделать запись об этом в журнале системы, предпринять попытку исправить состояние сессии и
      /// вернуть результат проверки в виде специального объекта. Метод не должен бросать исключений,
      /// если о невалидном состоянии сессии необходимо сообщить с помощью исключения, то
      /// объект исключения должен быть помещен в объект с результатом вызова в свойство ErrorException.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Результат проверки состояния сессии</returns>
      protected override SessionValidatorResult DoValidate(IUserSession session)
      {
        IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) session, true);
        if (!service.InTransaction)
          return base.DoValidate(session);
        service.Rollback();
        UserSessionForgottenTransactionException transactionException = new UserSessionForgottenTransactionException();
        this.serverEventLog.AddToTrace($"{transactionException.Message}{Environment.NewLine}{$"Exception thrown at [Client Thread ID: {Thread.CurrentThread.ManagedThreadId}, Client Thread Name: '{Thread.CurrentThread.Name}']"}{Environment.NewLine}Stack trace:{Environment.NewLine}{Environment.StackTrace}", "session_forgotten_transaction.log");
        transactionException.SetSavedToLogFileFlag(new bool?(true));
        return new SessionValidatorResult((Exception) transactionException);
      }
    }
}
