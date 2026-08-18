
// Type: Intermech.ExceptionDataExtensions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    public static class ExceptionDataExtensions
    {
      private static readonly string SavedToLogFileKey = "SavedToLogFile";
      private static readonly string OriginalStackTraceKey = "OriginalStackTrace";
      private const string RecoveryInfoProperty = "RecoveryInfo";
      private static readonly ErrorRecoveryInfo emptyRecoveryInfo = new ErrorRecoveryInfo(new ErrorRecoveryAction[0]);

      public static bool? GetSavedToLogFileFlag(this Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        object obj = exception.Data[(object) ExceptionDataExtensions.SavedToLogFileKey];
        return obj != null && obj is bool flag ? new bool?(flag) : new bool?();
      }

      public static void SetSavedToLogFileFlag(this Exception exception, bool? newValue)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (newValue.HasValue)
          exception.Data[(object) ExceptionDataExtensions.SavedToLogFileKey] = (object) newValue.Value;
        else
          exception.Data.Remove((object) ExceptionDataExtensions.SavedToLogFileKey);
      }

      public static bool IsSavedToLogFile(this Exception exception)
      {
        return exception.GetSavedToLogFileFlag() ?? false;
      }

      public static string GetOriginalStackTrace(this Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        return exception.Data[(object) ExceptionDataExtensions.OriginalStackTraceKey] as string;
      }

      public static void SetOriginalStackTrace(this Exception exception, string newValue)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (newValue != null)
          exception.Data[(object) ExceptionDataExtensions.OriginalStackTraceKey] = (object) newValue;
        else
          exception.Data.Remove((object) ExceptionDataExtensions.OriginalStackTraceKey);
      }

      /// <summary>
      /// Проверка является ли исключение (или же inner) указанным типом или порожденным от него
      /// </summary>
      /// <param name="exception"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public static bool IsSubclassOf(this Exception exception, Type type)
      {
        Type type1 = exception != null ? exception.GetType() : throw new ArgumentNullException(nameof (exception));
        if (type1 == type || type1.IsSubclassOf(type))
          return true;
        return exception.InnerException != null && exception.InnerException.IsSubclassOf(type);
      }

      /// <summary>
      /// Добавляет к исключению контейнер с действиями для восстановления после этого исключения.
      /// В диалоге отображения исключения такие действия будут преобразованы в гиперссылки в тексте исключения.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="recoveryInfo">Контейнер с действиями для востановления</param>
      /// <returns>Объект исключения</returns>
      public static Exception WithRecoveryInfo(this Exception exception, ErrorRecoveryInfo recoveryInfo)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        exception.Data[(object) "RecoveryInfo"] = recoveryInfo != null ? (object) recoveryInfo : throw new ArgumentNullException(nameof (recoveryInfo));
        return exception;
      }

      /// <summary>
      /// Возвращает контейнер с действиями для восстановления, связанный с исключением.
      /// Если действий нет, то метод возвращает пустой контейнер.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <returns>Контейнер с действиями для восстановления</returns>
      public static ErrorRecoveryInfo GetRecoveryInfo(this Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        return !(exception.Data[(object) "RecoveryInfo"] is ErrorRecoveryInfo errorRecoveryInfo) ? ExceptionDataExtensions.emptyRecoveryInfo : errorRecoveryInfo;
      }

      /// <summary>
      /// Добавляет к исключению действия для восстановления после этого исключения.
      /// В диалоге отображения исключения такие действия будут преобразованы в гиперссылки в тексте исключения.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="recoveryActions">Добавляемые действия для востановления</param>
      /// <returns>Объект исключения</returns>
      public static Exception WithRecoveryActions(
        this Exception exception,
        params ErrorRecoveryAction[] recoveryActions)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (recoveryActions == null)
          throw new ArgumentNullException(nameof (recoveryActions));
        ErrorRecoveryInfo recoveryInfo = exception.GetRecoveryInfo();
        if (recoveryInfo.Actions.Count != 0)
        {
          List<ErrorRecoveryAction> errorRecoveryActionList = new List<ErrorRecoveryAction>(recoveryInfo.Actions.Count + recoveryActions.Length);
          errorRecoveryActionList.AddRange((IEnumerable<ErrorRecoveryAction>) recoveryInfo.Actions);
          errorRecoveryActionList.AddRange((IEnumerable<ErrorRecoveryAction>) recoveryActions);
          exception.Data[(object) "RecoveryInfo"] = (object) new ErrorRecoveryInfo(errorRecoveryActionList.ToArray());
        }
        else
          exception.Data[(object) "RecoveryInfo"] = (object) new ErrorRecoveryInfo(recoveryActions);
        return exception;
      }

      /// <summary>
      /// Перечисляет действия для восстановления, связанные с исключением.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <returns>Перечислитель действий для восстановления</returns>
      public static IEnumerable<ErrorRecoveryAction> EnumerateRecoveryActions(this Exception exception)
      {
        ErrorRecoveryInfo errorRecoveryInfo = exception != null ? exception.GetRecoveryInfo() : throw new ArgumentNullException(nameof (exception));
        if (errorRecoveryInfo.Actions.Count != 0)
        {
          foreach (ErrorRecoveryAction action in (IEnumerable<ErrorRecoveryAction>) errorRecoveryInfo.Actions)
            yield return action;
        }
        if (exception.InnerException != null)
        {
          foreach (ErrorRecoveryAction enumerateRecoveryAction in exception.InnerException.EnumerateRecoveryActions())
            yield return enumerateRecoveryAction;
        }
      }
    }
}
