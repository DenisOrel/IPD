// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.StructureManagerProxyBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Interop.CADInterface;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public abstract class StructureManagerProxyBase
{
  protected readonly CADSystemProxy cadSystem;
  protected readonly StructureManager sm;
  protected readonly IPSAttributeLocalizer attributeLocalizer;
  protected readonly IValueBagFormatter propFormatter;

  public StructureManagerProxyBase(CADSystemProxy appProxy)
  {
    this.cadSystem = appProxy != null ? appProxy : throw new ArgumentNullException();
    this.attributeLocalizer = new IPSAttributeLocalizer();
    this.sm = (StructureManager) new StructureManagerClass();
    this.sm.SetAttributeLocalizer((AttributeLocalizer) new AttributeLocalizerComAdapter((IAttributeLocalizer) this.attributeLocalizer));
    if (StructureManagerProxyBase.IStructureManager3Supported(this.sm))
      ((IStructureManager3) this.sm).SetUnitsForAVS(appProxy.PhysicalValues.GetAvsPhysicalUnits());
    this.propFormatter = (IValueBagFormatter) new CADInterfaceFormatter();
  }

  private static bool IStructureManager3Supported(StructureManager sm)
  {
    try
    {
      return sm is IStructureManager3;
    }
    catch
    {
      return false;
    }
  }

  public void CommitChanges() => this.sm.CommitChanges();

  protected Guid ParseStructureElementGuid(IStructureElement rawStructureElement)
  {
    return this.ParseStructureElementGuid(rawStructureElement.GUID);
  }

  protected Guid ParseStructureElementGuid(string guid)
  {
    Guid result;
    if (!string.IsNullOrEmpty(guid) && Guid.TryParse(guid, out result))
      return result;
    throw new FormatException($"Нераспознанный формат идентификатора GUID (нераспознанное значение \"{guid}\").");
  }

  protected ModelConfigurationProxy CreateAssemblyConfigurationWrapper(
    IModelConfiguration rawModelConfiguration,
    CADDocumentProxy assemblyDocument)
  {
    if (rawModelConfiguration == null)
      throw new ArgumentNullException(nameof (rawModelConfiguration));
    if (assemblyDocument == null)
      throw new ArgumentNullException(nameof (assemblyDocument));
    return this.cadSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new ExplicitModelConfigurationProvider(rawModelConfiguration), assemblyDocument, this.cadSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default);
  }

  protected ModelConfigurationProxy CreateComponentConfigurationWrapper(
    CADDocumentProxy parentAssemblyDocument,
    IModelConfiguration rawComponentConfiguration)
  {
    if (parentAssemblyDocument == null)
      throw new ArgumentNullException(nameof (parentAssemblyDocument));
    ExplicitModelConfigurationProvider configurationProvider = rawComponentConfiguration != null ? new ExplicitModelConfigurationProvider(rawComponentConfiguration) : throw new ArgumentNullException(nameof (rawComponentConfiguration));
    CADDocumentProxy document = this.cadSystem.Builder.CreateDocument((ICADDocumentProvider) new LinkedCADDocumentProvider((IModelConfigurationProvider) configurationProvider), this.cadSystem);
    return this.cadSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) configurationProvider, document, this.cadSystem, (IModelConfigurationCreationContext) new AssemblyComponentConfigurationContext(parentAssemblyDocument));
  }

  protected ModelComponentProxy CreateComponentWrapper(IModelComponent rawModelComponent)
  {
    if (rawModelComponent == null)
      throw new ArgumentNullException(nameof (rawModelComponent));
    return this.cadSystem.Builder.CreateModelComponent(rawModelComponent, this.cadSystem);
  }

  protected ParametersContainerProxy CreateParametersWrapper(IParametersContainer rawContainer)
  {
    return new ParametersContainerProxy((IParametersContainerProvider) new ExplicitParametersContainerProvider(rawContainer));
  }
}
