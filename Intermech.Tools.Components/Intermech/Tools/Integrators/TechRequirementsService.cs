// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.TechRequirementsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Сервис для получения тех. требований</summary>
public abstract class TechRequirementsService : 
  IntegratorService,
  ITechRequirementsService,
  IIntegratorService
{
  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public TechRequirementsService(IIntegrator owner)
    : base(owner)
  {
  }

  /// <summary>
  /// Создает сессию CAD-системы, использующуюся для обновления тех. требований документов
  /// </summary>
  public abstract IDisposable CreateApiSession();

  /// <summary>
  /// Возвращает из документа класс, предоставляющий документ IMTEXT
  /// </summary>
  public abstract IIMTextDocumentProvider GetIMTextDocumentProvider(
    long documentId,
    string documentFilePath,
    IDisposable apiSession);

  /// <summary>
  /// Информация можно ли для заднного типа объектов получить ТТ
  /// </summary>
  /// <param name="documentTypeID">тип объекта для которого нужно проверит доступность получения ТТ</param>
  /// <returns></returns>
  public abstract bool CanGetTechRequirements(int documentTypeID);
}
