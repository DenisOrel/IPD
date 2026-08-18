// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectionErrorReporter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для вспомогательного объекта, отвечающего за вывод диагностических сообщений о подключении к <see cref="T:Intermech.Interfaces.IMServer" />
/// или о создании сессий сервера приложений <see cref="T:Intermech.Interfaces.IUserSession" />.
/// Реалзация должна быть thread safe.
/// </summary>
public class IMServerConnectionErrorReporter
{
  private readonly string outputViewCategory;

  /// <summary>Создает объект</summary>
  public IMServerConnectionErrorReporter()
  {
    this.outputViewCategory = "Пул сессий сервера приложений";
  }

  /// <summary>Выводит информацию о событии.</summary>
  /// <param name="isError">Признак сообщения об ошибке</param>
  /// <param name="eventMessage">Сообщение о наступлении события</param>
  public void ReportEvent(bool isError, string eventMessage)
  {
    if (eventMessage == null)
      return;
    this.TryGetEventLogService()?.FileLog.Write(eventMessage, EventLogItemType.Information);
    this.TryGetOutputView()?.WriteString(this.outputViewCategory, this.WithCurrentTime(eventMessage));
  }

  /// <summary>
  /// Выводит информацию о подавленном исключении.
  /// Метод используется в тех случаях, когда исключение не может быть обработано традиционным способом.
  /// </summary>
  /// <param name="exception">Объект исключения</param>
  /// <param name="errorMessage">Сообщение об ошибке</param>
  public void ReportException(Exception exception, string errorMessage)
  {
    if (exception == null || errorMessage == null)
      return;
    string str;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendLine(errorMessage);
      stringBuilder.AppendFormat("Причина: исключительная ситуация типа '{0}'", (object) exception.GetType()).AppendLine();
      stringBuilder.Append(exception.Message);
      str = stringBuilder.ToString();
    }
    this.TryGetEventLogService()?.FileLog.Write(str, EventLogItemType.Error);
    this.TryGetOutputView()?.WriteString(this.outputViewCategory, this.WithCurrentTime(str));
  }

  private string WithCurrentTime(string eventMessage)
  {
    return string.Format("{0:hh\\:mm\\:ss}> {1}", (object) DateTime.Now.TimeOfDay, (object) eventMessage);
  }

  /// <summary>
  /// Возвращает сервис доступа к журналам событий приложения.
  /// Метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса или null</returns>
  protected virtual IApplicationEventLogService TryGetEventLogService()
  {
    return (IApplicationEventLogService) ApplicationServices.Container.GetService(typeof (IApplicationEventLogService));
  }

  /// <summary>
  /// Возвращает сервис доступа к окну "Вывод" приложения.
  /// Метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса или null</returns>
  protected virtual IOutputView TryGetOutputView()
  {
    return (IOutputView) ApplicationServices.Container.GetService(typeof (IOutputView));
  }
}
