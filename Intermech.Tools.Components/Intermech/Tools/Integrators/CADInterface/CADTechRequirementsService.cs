// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADTechRequirementsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Сервис для получения тех. требований для Cadmech 3D</summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CADTechRequirementsService(IIntegrator owner) : TechRequirementsService(owner)
{
  /// <summary>
  /// Создает сессию CAD-системы, использующуюся для обновления тех. требований документов
  /// </summary>
  public override IDisposable CreateApiSession()
  {
    return (IDisposable) new CADApiSession(this.Integrator);
  }

  /// <summary>
  /// Возвращает из документа класс, предоставляющий документ IMTEXT
  /// </summary>
  public override IIMTextDocumentProvider GetIMTextDocumentProvider(
    long documentId,
    string documentFilePath,
    IDisposable apiSession)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentNullException(nameof (documentFilePath));
    if (apiSession == null)
      throw new ArgumentNullException(nameof (apiSession));
    return (IIMTextDocumentProvider) ((ApplicationApiSession<CADSystemProxy>) apiSession).Application.OpenDocument(documentFilePath, true);
  }

  /// <summary>
  /// Информация можно ли для заднного типа объектов получить ТТ
  /// </summary>
  /// <param name="documentTypeID">тип объекта для которого нужно проверит доступность получения ТТ</param>
  /// <returns></returns>
  public override bool CanGetTechRequirements(int documentTypeID)
  {
    CADSettingsService service;
    if (this.Integrator.TryGetService<CADSettingsService>(out service))
    {
      CADSettings settings = service.GetSettings();
      DocumentGroup byName1 = settings.FileDocumentGroups.FindByName("Assembly", false);
      DocumentGroup byName2 = settings.FileDocumentGroups.FindByName("Part", false);
      List<GlobalId<int>> globalIdList = new List<GlobalId<int>>();
      if (byName1 != null)
        globalIdList.AddRange((IEnumerable<GlobalId<int>>) byName1.DocumentTypes);
      if (byName2 != null)
        globalIdList.AddRange((IEnumerable<GlobalId<int>>) byName2.DocumentTypes);
      if (globalIdList.FindIndex((Predicate<GlobalId<int>>) (x => x.Id == documentTypeID)) != -1)
        return true;
    }
    return false;
  }
}
