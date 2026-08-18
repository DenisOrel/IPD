
// Type: Intermech.Tools.Integrators.Notifications.IMShapeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators.Notifications;

/// <summary>Аргументы события для интеграции с IMShape.</summary>
public class IMShapeEventArgs : NotificationEventArgs
{
  private static readonly string updateDBeventName = "UpdateIMShapeDB";
  private readonly IIntegrator integrator;
  private readonly List<IMShapeDocumentInfo> documents;

  /// <summary>Создает объект.</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="integrator">Объект интегратора, который сохранял изменения в файлах</param>
  /// <param name="documents">Список документов, сохраненных интегратором</param>
  public IMShapeEventArgs(
    string eventName,
    IIntegrator integrator,
    List<IMShapeDocumentInfo> documents)
    : base(eventName)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (documents == null)
      throw new ArgumentNullException(nameof (documents));
    this.integrator = integrator;
    this.documents = documents;
  }

  /// <summary>
  /// Возвращает объект интегратора, который сохранял изменения в файлах.
  /// </summary>
  public IIntegrator Integrator => this.integrator;

  /// <summary>Список документов, сохраненных интегратором.</summary>
  public List<IMShapeDocumentInfo> Documents => this.documents;

  /// <summary>Имя события обновления базы IMShape.</summary>
  public static string UpdateDB => IMShapeEventArgs.updateDBeventName;
}
