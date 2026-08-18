// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIEmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Создает объект.</summary>
/// <param name="integrator">Ссылка на объект интегратора</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
public class CIEmbedAttributesDriver(IIntegrator integrator) : MechanicalEmbedAttributesDriver(integrator)
{
  protected override ICollection<StringKey> DoGetEmbeddableAttributes(
    long documentId,
    int documentType)
  {
    CADSettings settingsObject = (CADSettings) this.SettingsService.GetSettingsObject();
    return settingsObject.JTDerivativesEnabled && documentType == settingsObject.JTDerivedDocumentType.Id ? (ICollection<StringKey>) new StringKey[0] : base.DoGetEmbeddableAttributes(documentId, documentType);
  }
}
