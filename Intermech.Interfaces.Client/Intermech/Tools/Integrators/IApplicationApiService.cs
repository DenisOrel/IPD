// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IApplicationApiService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора, предоставляющий доступ к состоянию и API-объекту интегрируемого приложения.
/// </summary>
public interface IApplicationApiService : IExternalApiService, IIntegratorService
{
  /// <summary>
  /// Возвращает название приложения, с которым осуществляется интеграция.
  /// </summary>
  string ApplicationName { get; }

  /// <summary>
  /// Возвращает true, если версия приложения, указанная в настройках интегратора, установлена на компьютере.
  /// Если интеграция не настроена, либо настройки интегратора содержат ошибки, то метод возвращает false.
  /// Ошибки, связанные с определением наличия приложения на компьютере, подавляются и отображаются на закладке
  /// "Вывод".
  /// </summary>
  bool IsApplicationInstalled { get; }

  /// <summary>
  /// Возвращает true, если версия приложения, указанная в настройках интегратора, выполняется в данный момент.
  /// Если интеграция не настроена, либо настройки интегратора содержат ошибки, то метод возвращает false.
  /// Ошибки, связанные с определением наличия/работоспособности приложения, подавляются и отображаются на закладке
  /// "Вывод".
  /// </summary>
  bool IsApplicationRunning { get; }

  /// <summary>
  /// Возвращает API-объект приложения. Этот метод требует предварительного открытия сессии доступа к API интегрируемого приложения.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  /// <exception cref="T:System.InvalidOperationException">Требуется предварительное открытие сессии доступа к API интегрируемого приложению</exception>
  object GetApplicationObject();

  /// <summary>
  /// Закрывает подключение к приложению и освобождает API-объект приложения. Этот метод можно использовать только тогда, когда нет открытых сессий доступа к API интегрируемого приложения.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Невозможно освободить API-объект приложения, пока он используется</exception>
  void ReleaseApplicationObject();
}
