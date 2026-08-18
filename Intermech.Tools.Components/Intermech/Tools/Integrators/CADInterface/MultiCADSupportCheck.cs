// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.MultiCADSupportCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class MultiCADSupportCheck : CADSettingsCheck
{
  private readonly IIntegrator integrator;

  public MultiCADSupportCheck(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    if (context != SettingsValidatorContext.Generic)
      return (string) null;
    if (settings.JTDerivativesEnabled)
    {
      IntegratorObject integratorObject = IntegratorServices.Find(settings.JTDerivedDocumentType.Id);
      if (integratorObject == null || integratorObject.Id != this.integrator.Id)
        return "Требуется обновить настройки интегратора для корректной поддержки проектирования с помощью технологии MultiCAD.";
    }
    return (string) null;
  }
}
