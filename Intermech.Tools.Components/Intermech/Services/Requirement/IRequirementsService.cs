// Decompiled with JetBrains decompiler
// Type: Intermech.Services.Requirement.IRequirementsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Notifications;

#nullable disable
namespace Intermech.Services.Requirement;

public interface IRequirementsService
{
  /// <summary>Обновить тех. требования для заданного документа</summary>
  /// <param name="documentInfo">Контейнер со свединями о сохраняемом документе</param>
  /// <param name="integrator">Интегратор с CAD-системой</param>
  /// <param name="requirementSupport">Сервис для работы с тех. требованиями документа</param>
  void UpdateRequirements(
    CaptureChangesDocumentInfo documentInfo,
    IIntegrator integrator,
    ITechRequirementsService requirementSupport);
}
