// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ITechRequirementsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Интерфейс сервиса для получения тех. требований</summary>
public interface ITechRequirementsService : IIntegratorService
{
  /// <summary>
  /// Создает сессию CAD-системы, использующуюся для обновления тех. требований документов
  /// </summary>
  /// <param name="integrator">Инткгратор с CAD-системой</param>
  IDisposable CreateApiSession();

  /// <summary>
  /// Возвращает из документа класс, предоставляющий документ IMTEXT
  /// </summary>
  IIMTextDocumentProvider GetIMTextDocumentProvider(
    long documentId,
    string documentFilePath,
    IDisposable apiSession);

  /// <summary>
  /// Информация можно ли для заднного типа объектов получить ТТ
  /// </summary>
  /// <param name="documentTypeID">тип объекта для которого нужно проверит доступность получения ТТ</param>
  /// <returns></returns>
  bool CanGetTechRequirements(int documentTypeID);
}
