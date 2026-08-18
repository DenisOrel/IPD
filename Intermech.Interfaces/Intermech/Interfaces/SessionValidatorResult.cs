
// Type: Intermech.Interfaces.SessionValidatorResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Описывает результат проверки состояния пользовательской сессии сервера приложений.
    /// </summary>
    public sealed class SessionValidatorResult
    {
      private static readonly SessionValidatorResult noErrors = new SessionValidatorResult(true);
      private bool isSuccessful;
      private Exception errorException;

      /// <summary>Создает объект.</summary>
      /// <param name="isSuccessful">Признак успешного или неуспешного прохождения проверки</param>
      public SessionValidatorResult(bool isSuccessful)
      {
        this.isSuccessful = isSuccessful;
        this.errorException = (Exception) null;
      }

      /// <summary>Создает объект.</summary>
      /// <param name="errorException">Объект исключения, описывающий ошибку в состоянии пользовательской сессии</param>
      /// <exception cref="T:ArgumentNullException">errorException</exception>
      public SessionValidatorResult(Exception errorException)
      {
        if (errorException == null)
          throw new ArgumentNullException(nameof (errorException));
        this.isSuccessful = false;
        this.errorException = errorException;
      }

      /// <summary>Создает объект.</summary>
      /// <param name="isSuccessful">Признак успешного или неуспешного прохождения проверки</param>
      /// <param name="errorException">Объект исключения, описывающий ошибку в состоянии пользовательской сессии. Параметр может быть не задан</param>
      public SessionValidatorResult(bool isSuccessful, Exception errorException)
      {
        this.isSuccessful = isSuccessful;
        this.errorException = errorException;
      }

      /// <summary>
      /// Возвращает признак успешного или неуспешного прохождения проверки.
      /// </summary>
      public bool IsSuccessful
      {
        [DebuggerStepThrough] get => this.isSuccessful;
      }

      /// <summary>
      /// Возвращает объект исключения, описывающий ошибку в состоянии пользовательской сессии.
      /// Значение свойства может быть не задано, даже если состояние сессии невалидно.
      /// </summary>
      public Exception ErrorException
      {
        [DebuggerStepThrough] get => this.errorException;
      }

      /// <summary>
      /// Возвращает объект, описывающий успешное прохождение проверки.
      /// </summary>
      public static SessionValidatorResult Success => SessionValidatorResult.noErrors;
    }
}
