// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.AssemblyStructureManagerProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Data;
using Intermech.UI;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class AssemblyStructureManagerProxy : StructureManagerProxyBase
{
  private const long NoSubstitution = 0;
  private readonly StructureComponentOccurenceCodec occurenceCodec;
  private readonly Lazy<string> substGroupAttribute;
  private readonly Lazy<string> substNumberAttribute;
  private readonly Lazy<string[]> substAllAttributes;
  private const string AppConditionsGUID = "App_Conditions_GUID";

  public AssemblyStructureManagerProxy(CADSystemProxy appProxy)
    : base(appProxy)
  {
    this.occurenceCodec = new StructureComponentOccurenceCodec(this.propFormatter);
    this.substGroupAttribute = new Lazy<string>((Func<string>) (() => this.attributeLocalizer.GetAttributeNameByID(EAttributeID.ATTR_SubstitutesGroupNumber)));
    this.substNumberAttribute = new Lazy<string>((Func<string>) (() => this.attributeLocalizer.GetAttributeNameByID(EAttributeID.ATTR_SubstituteNumber)));
    this.substAllAttributes = new Lazy<string[]>((Func<string[]>) (() => new string[2]
    {
      this.substGroupAttribute.Value,
      this.substNumberAttribute.Value
    }));
  }

  public List<ModelConfigurationProxy> GetStructureComponents(CADDocumentProxy document)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CADDocumentProxy>("AssemblyStructureManagerProxy.GetStructureComponents()", document);
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    StructureElement[] structureElementArray = this.sm.GetStructureForAVS(document.RawObject) ?? new StructureElement[0];
    List<ModelConfigurationProxy> structureComponents = new List<ModelConfigurationProxy>(structureElementArray.Length);
    foreach (IStructureElement structureElement in structureElementArray)
    {
      if (!structureElement.IsVirtual && !structureElement.ExcludedFromSpec)
      {
        ModelConfigurationProxy configurationWrapper = this.CreateComponentConfigurationWrapper(document, structureElement.GetConfiguration());
        string path = AssemblyStructureManagerProxy.SafeReadMasterFile(configurationWrapper);
        if (!string.IsNullOrEmpty(path) && Path.IsPathRooted(path) && (configurationWrapper.Document.IsInMemory || File.Exists(path)))
          structureComponents.Add(configurationWrapper);
      }
    }
    return structureComponents;
  }

  public List<AssemblyStructureRecord> GetStructure(
    CADDocumentProxy document,
    bool enableSubstitutions)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CADDocumentProxy, bool>("AssemblyStructureManagerProxy.GetStructure()", document, enableSubstitutions);
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    List<AssemblyStructureRecord> structure = new List<AssemblyStructureRecord>(1024 /*0x0400*/);
    StructureElement[] structureForAvs = this.sm.GetStructureForAVS(document.RawObject);
    if (structureForAvs.Length != 0 && CADInterfaceTracing.Proxies.TraceVerbose)
      Trace.WriteLine($"Document: {document.FullName}");
    foreach (IStructureElement structureElement in structureForAvs)
    {
      if (!structureElement.IsVirtual && !structureElement.ExcludedFromSpec)
      {
        ModelConfigurationProxy configurationWrapper = this.CreateComponentConfigurationWrapper(document, structureElement.GetConfiguration());
        string path = AssemblyStructureManagerProxy.SafeReadMasterFile(configurationWrapper);
        if (!string.IsNullOrEmpty(path) && Path.IsPathRooted(path) && (configurationWrapper.Document.IsInMemory || File.Exists(path)))
        {
          Guid structureElementGuid = this.ParseStructureElementGuid(structureElement);
          ValueBag valueBag = this.DecodeOccurenceAttributes(structureElement);
          int num = valueBag.Read<int>((StringKey) IDCache.Default.Position.Text, -1);
          string position = num > 0 ? num.ToString() : (string) null;
          string str1 = valueBag.Read<string>((StringKey) IDCache.Default.Note.Text, (string) null);
          string blockName = valueBag.Read<string>((StringKey) "App_Conditions_GUID", "");
          string str2 = (string) null;
          string str3 = (string) null;
          if (!string.IsNullOrEmpty(blockName))
          {
            byte[] bytes1 = document.ReadCustomData(blockName);
            str2 = bytes1 != null ? Encoding.UTF8.GetString(bytes1) : "";
            byte[] bytes2 = document.ReadCustomData(blockName + "x");
            str3 = bytes2 != null ? Encoding.UTF8.GetString(bytes2) : "";
          }
          foreach (ProjectRelatedData projData in (IEnumerable<ProjectRelatedData>) this.DecodeProjectRelatedData(structureElement, document, enableSubstitutions))
          {
            AssemblyStructureRecord record = new AssemblyStructureRecord();
            record.ProjectConfiguration = (string) (projData.ProjectConfiguration != null ? projData.ProjectConfiguration.Name : (StringKey) null);
            record.OccurenceGuid = structureElementGuid;
            record.ComponentConfiguration = configurationWrapper;
            record.ComponentMasterFile = path;
            record.Attributes.Add((StringKey) IDCache.Default.Count.Text, (object) projData.Count);
            record.Attributes.Add((StringKey) IDCache.Default.Position.Text, (object) position, typeof (string));
            record.Attributes.Add((StringKey) IDCache.Default.Note.Text, (object) str1, typeof (string));
            record.Attributes.Add((StringKey) IDCache.Default.PDMConfigCriteria.Text, (object) str2, typeof (string));
            record.Attributes.Add((StringKey) IDCache.Default.PDMConfigContext.Text, (object) str3, typeof (string));
            if (enableSubstitutions)
            {
              if (projData.SubstGroup != 0L || projData.SubstNumber != 0L)
              {
                record.Attributes.Add((StringKey) IDCache.Default.SubstitutionGroup.Text, (object) projData.SubstGroup);
                record.Attributes.Add((StringKey) IDCache.Default.SubstitutionNumber.Text, (object) projData.SubstNumber);
              }
              else
              {
                record.Attributes.Add((StringKey) IDCache.Default.SubstitutionGroup.Text, (object) TypedNull.Int64);
                record.Attributes.Add((StringKey) IDCache.Default.SubstitutionNumber.Text, (object) TypedNull.Int64);
              }
            }
            record.Attributes.SetFlagForAll(NamedFlags.ReadOnly);
            structure.Add(record);
            if (CADInterfaceTracing.Proxies.TraceVerbose)
              AssemblyStructureManagerProxy.TraceStructureRecord(record, (object) position, projData);
          }
        }
      }
    }
    if (structure.Count > 0 && CADInterfaceTracing.Proxies.TraceVerbose)
      Trace.WriteLine(string.Empty);
    return structure;
  }

  private static void TraceStructureRecord(
    AssemblyStructureRecord record,
    object position,
    ProjectRelatedData projData)
  {
    string str = record.ProjectConfiguration;
    if (string.IsNullOrEmpty(str))
      str = "<all configurations>";
    Trace.WriteLine($"Component: guid={record.OccurenceGuid:D}, position={position}, count={record.Attributes.Read<MeasuredValue>((StringKey) IDCache.Default.Count.Text, (MeasuredValue) null)}, subst=({projData.SubstGroup}, {projData.SubstNumber}) in '{str}'");
  }

  private ValueBag DecodeOccurenceAttributes(IStructureElement structElem)
  {
    StringKey[] attributeKeys = new StringKey[3]
    {
      (StringKey) IDCache.Default.Position.Text,
      (StringKey) IDCache.Default.Note.Text,
      (StringKey) "App_Conditions_GUID"
    };
    return this.occurenceCodec.ReadAttributes((IValueBagContainer) new CADInterfaceValueBagContainer((IParametersContainerProxy) this.CreateParametersWrapper((IParametersContainer) structElem)), (ICollection<StringKey>) attributeKeys, DecodeAttributesOptions.Empty).Bag;
  }

  private static string SafeReadMasterFile(ModelConfigurationProxy cfg)
  {
    try
    {
      return cfg.Document.MasterFile;
    }
    catch (ApplicationProxyException ex)
    {
      return (string) null;
    }
  }

  private ICollection<ProjectRelatedData> DecodeProjectRelatedData(
    IStructureElement structElem,
    CADDocumentProxy document,
    bool decodeSubstitution)
  {
    Tuple<long, long> tuple = decodeSubstitution ? this.DecodeSubstitution(structElem) : new Tuple<long, long>(0L, 0L);
    ICollection<ProjectRelatedData> projectRelatedDatas = (ICollection<ProjectRelatedData>) new LinkedList<ProjectRelatedData>();
    if (structElem.SameForAllConfigurations)
    {
      MeasuredValue count = this.DecodeCount((IStructureModification) structElem.GetCommonVariant());
      projectRelatedDatas.Add(new ProjectRelatedData((ModelConfigurationProxy) null, count, tuple.Item1, tuple.Item2));
    }
    else
    {
      foreach (StructureModification modification in structElem.GetModifications())
      {
        MeasuredValue count = this.DecodeCount((IStructureModification) modification);
        projectRelatedDatas.Add(new ProjectRelatedData(this.CreateAssemblyConfigurationWrapper(modification.GetAssemblyConfiguration(), document), count, tuple.Item1, tuple.Item2));
      }
    }
    return projectRelatedDatas;
  }

  private MeasuredValue DecodeCount(IStructureModification structMod)
  {
    ValueRecord valueRecord = this.propFormatter.Read((IValueBagContainer) new CADInterfaceValueBagContainer((IParametersContainerProxy) this.CreateParametersWrapper((IParametersContainer) structMod)), (ICollection<StringKey>) new StringKey[1]
    {
      (StringKey) IDCache.Default.Count.Text
    }).Bag.Find((StringKey) IDCache.Default.Count.Text);
    int actualQuantity = structMod.ActualQuantity;
    if (valueRecord != null && !valueRecord.IsNull)
    {
      if (valueRecord.DataType == typeof (int) || valueRecord.DataType == typeof (long) || valueRecord.DataType == typeof (double) || valueRecord.DataType == typeof (float))
      {
        double num = Convert.ToDouble(valueRecord.Value);
        if (MathUtils.AlmostZero(num))
          num = Convert.ToDouble(actualQuantity);
        return new MeasuredValue(num, IDCache.Default.ItemsMeasure.Id);
      }
      if (valueRecord.Value is IPhysicalQuantity)
      {
        try
        {
          return this.cadSystem.PhysicalValues.ToMeasuredValue((IPhysicalQuantity) valueRecord.Value);
        }
        catch (Exception ex)
        {
          if (UIReport.Enabled)
            UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_228"), (object) valueRecord.Key, valueRecord.Value, (object) ex.Message), TraceLevel.Warning);
        }
      }
    }
    return new MeasuredValue((double) actualQuantity, IDCache.Default.ItemsMeasure.Id);
  }

  private Tuple<long, long> DecodeSubstitution(IStructureElement structElem)
  {
    ValueBag source = new ValueBag((ICollection<ValueRecord>) this.CreateParametersWrapper((IParametersContainer) structElem).GetParameters((IList<string>) this.substAllAttributes.Value));
    ValueBag target = new ValueBag();
    LinkedList<IAction> linkedList = new LinkedList<IAction>();
    linkedList.AddLast((IAction) new DecodeConvertibleValueAction(source, target, (StringKey) this.substGroupAttribute.Value, typeof (long)));
    linkedList.AddLast((IAction) new DecodeConvertibleValueAction(source, target, (StringKey) this.substNumberAttribute.Value, typeof (long)));
    foreach (IAction action in linkedList)
      action.Perform();
    return new Tuple<long, long>(target.Read<long>((StringKey) this.substGroupAttribute.Value, 0L), target.Read<long>((StringKey) this.substNumberAttribute.Value, 0L));
  }
}
