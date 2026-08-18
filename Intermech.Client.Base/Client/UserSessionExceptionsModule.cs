
// Type: Intermech.Client.UserSessionExceptionsModule
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using System;
using System.Runtime.ExceptionServices;


namespace Intermech.Client
{
    public sealed class UserSessionExceptionsModule : InitializerModule
    {
      private UserSessionExceptionsReporter exceptionsReporter;
      private FirstChanceExceptionTrap firstChanceExceptionTrap;

      public UserSessionExceptionsModule(UserSessionExceptionsReporter exceptionsReporter)
      {
        this.exceptionsReporter = exceptionsReporter != null ? exceptionsReporter : throw new ArgumentNullException(nameof (exceptionsReporter));
        this.firstChanceExceptionTrap = new FirstChanceExceptionTrap();
        this.firstChanceExceptionTrap.ProcessException += new EventHandler<FirstChanceExceptionEventArgs>(this.ProcessExceptionAtThrowSite);
      }

      /// <summary>Выполняет инициализацию текущего объекта.</summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.firstChanceExceptionTrap.Enabled = true;
      }

      /// <summary>
      /// Завершает работу текущего объекта.
      /// Если свойство IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации.
      /// </summary>
      protected override void DoShutdown()
      {
        this.firstChanceExceptionTrap.Enabled = false;
        base.DoShutdown();
      }

      private void ProcessExceptionAtThrowSite(object sender, FirstChanceExceptionEventArgs e)
      {
        Exception exception = e.Exception;
        UserSessionThreadConflictException specialException1 = this.TryGetSpecialException<UserSessionThreadConflictException>(exception);
        if (specialException1 != null)
        {
          this.exceptionsReporter.ReportExceptionToServerLog(exception, specialException1);
        }
        else
        {
          UserSessionProtectionException specialException2 = this.TryGetSpecialException<UserSessionProtectionException>(exception);
          if (specialException2 == null)
            return;
          this.exceptionsReporter.ReportExceptionToServerLog(exception, specialException2);
        }
      }

      private TException TryGetSpecialException<TException>(Exception exception) where TException : Exception
      {
        if (exception is TException specialException)
          return specialException;
        return exception.InnerException != null ? this.TryGetSpecialException<TException>(exception.InnerException) : default (TException);
      }
    }
}
