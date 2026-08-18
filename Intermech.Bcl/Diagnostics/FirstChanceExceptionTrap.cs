
// Type: Intermech.Diagnostics.FirstChanceExceptionTrap
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Базовый класс ловушек для события падения исключения. Ловушки вызываются непосредственно в месте
    /// падения исключения до того, как runtime перейдет к поиску по call stack подходящего обработчика для исключения.
    /// </summary>
    public class FirstChanceExceptionTrap
    {
      private bool enabled;
      private ThreadLocal<bool> alreadyInHandler;

      /// <summary>Создает объект.</summary>
      public FirstChanceExceptionTrap() => this.alreadyInHandler = new ThreadLocal<bool>();

      /// <summary>Активирует и деактивирует текущий объект.</summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
        set
        {
          if (this.enabled == value)
            return;
          this.EnabledChanging(value);
          this.enabled = value;
        }
      }

      private void EnabledChanging(bool newValue)
      {
        if (newValue)
          this.DoEnableHandler();
        else
          this.DoDisableHandler();
      }

      /// <summary>Активирует текущий объект.</summary>
      protected virtual void DoEnableHandler()
      {
        AppDomain.CurrentDomain.FirstChanceException += new EventHandler<FirstChanceExceptionEventArgs>(this.OnFirstChanceException);
      }

      /// <summary>Деактивирует текущий объект.</summary>
      protected virtual void DoDisableHandler()
      {
        AppDomain.CurrentDomain.FirstChanceException -= new EventHandler<FirstChanceExceptionEventArgs>(this.OnFirstChanceException);
      }

      /// <summary>Реализует обработку события падения исключения.</summary>
      /// <param name="sender">AppDomain, в котором произошло падение исключения</param>
      /// <param name="e">Аргументы события</param>
      private void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
      {
        bool flag;
        try
        {
          flag = !this.alreadyInHandler.Value;
        }
        catch (ObjectDisposedException ex)
        {
          flag = false;
        }
        if (!flag)
          return;
        try
        {
          this.alreadyInHandler.Value = true;
          if (e == null || e.Exception == null)
            return;
          this.DoProcessException(e.Exception);
          this.RaiseProcessException(e);
        }
        catch
        {
        }
        finally
        {
          this.alreadyInHandler.Value = false;
        }
      }

      /// <summary>
      /// Обрабатывает исключение в месте его падения.
      /// Метод вызывается в том потоке (thread), где произошло падение исключения. Поэтому реализация метода должна быть thread safe.
      /// Любые исключения в этом методе будут подавлены.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      protected virtual void DoProcessException(Exception exception)
      {
      }

      private void RaiseProcessException(FirstChanceExceptionEventArgs e)
      {
        EventHandler<FirstChanceExceptionEventArgs> processException = this.ProcessException;
        if (processException == null)
          return;
        processException((object) this, e);
      }

      /// <summary>
      /// Позволяет обработать событие падения исключения безопасным способом без риска падения текущего процесса.
      /// Событие вызывается в том потоке (thread), где произошло падение исключения. Поэтому обработчик должен быть thread safe.
      /// Любые исключения в обработчике будут подавлены.
      /// </summary>
      public event EventHandler<FirstChanceExceptionEventArgs> ProcessException;
    }
}
