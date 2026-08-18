// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADCompositionReader
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADCompositionReader : ElectricalCompositionReader<ADDocument>
{
  private readonly CaptureChangesDatabase _db;
  private readonly AddInProxy _proxy;
  private readonly IADProject _project;
  private bool _first = true;

  public ADCompositionReader(
    List<BoardData<ADDocument>> boards,
    ADIntegratorSettings integratorSettings,
    IIntegratorOutput outputSvc,
    CaptureChangesDatabase db,
    AddInProxy proxy,
    IADProject project)
    : base(boards, (ECADIntegratorSettings) integratorSettings, outputSvc)
  {
    this._proxy = proxy;
    this._db = db;
    this._project = project;
  }

  public static LinkedList<InitialArticleData> ReadArticles(
    List<BoardData<ADDocument>> boards,
    ADIntegratorSettings integratorSettings,
    IIntegratorOutput outputSvc,
    out ElectricalSchemeDescriptors bomAssemblies,
    CaptureChangesDatabase db,
    AddInProxy proxy,
    IADProject project)
  {
    return new ADCompositionReader(boards, integratorSettings, outputSvc, db, proxy, project).ReadArticles(out bomAssemblies);
  }

  protected override IComponentsListFilter GetFilter(ComponentsListFilterType type)
  {
    return (IComponentsListFilter) new ADComponentsListFilter((ADIntegratorSettings) this.integratorSettings, type);
  }

  private List<IElectricalComponent> SheetHandle(
    SchDocumentProxy sheet,
    List<Tuple<string, List<IElectricalComponent>>> funcGroupsComponents,
    List<SchDocumentProxy> additionalSheets)
  {
    List<IElectricalComponent> components = sheet.Components;
    for (ISchSheetSymbol nextSheetSymbol = sheet.GetNextSheetSymbol(); nextSheetSymbol != null; nextSheetSymbol = sheet.GetNextSheetSymbol())
    {
      string[] strArray1 = nextSheetSymbol.FileName.Split(';');
      string[] strArray2 = this.ReadFunctionalGroupPosDesignations(nextSheetSymbol.DesignatorText);
      List<Tuple<SchDocumentProxy, string, string, List<IElectricalComponent>>> tupleList = new List<Tuple<SchDocumentProxy, string, string, List<IElectricalComponent>>>();
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (string str3 in strArray1)
      {
        string symbolFileName = str3;
        SchDocumentProxy sheet1 = (SchDocumentProxy) null;
        if (additionalSheets != null)
          sheet1 = additionalSheets.Find((Predicate<SchDocumentProxy>) (x => x.FileName.Equals(symbolFileName)));
        if (sheet1 == null)
          sheet1 = new SchDocumentProxy(ApiHelper.GetSchDocument(this._proxy.AddIn, symbolFileName, true), symbolFileName, (ADIntegratorSettings) this.integratorSettings);
        if (str1 == string.Empty)
          str1 = string.IsNullOrEmpty(this.integratorSettings.FGName) ? string.Empty : Convert.ToString(sheet1.GetPropertyValue(this.integratorSettings.FGName));
        if (str2 == string.Empty)
          str2 = string.IsNullOrEmpty(this.integratorSettings.FGDesignation) ? string.Empty : Convert.ToString(sheet1.GetPropertyValue(this.integratorSettings.FGDesignation));
        if (funcGroupsComponents.Exists((Predicate<Tuple<string, List<IElectricalComponent>>>) (x => x.Item1.Equals(symbolFileName))))
        {
          tupleList.Add(new Tuple<SchDocumentProxy, string, string, List<IElectricalComponent>>(sheet1, str1, str2, funcGroupsComponents.Find((Predicate<Tuple<string, List<IElectricalComponent>>>) (x => x.Item1.Equals(symbolFileName))).Item2));
        }
        else
        {
          List<IElectricalComponent> electricalComponentList = this.SheetHandle(sheet1, funcGroupsComponents, additionalSheets);
          tupleList.Add(new Tuple<SchDocumentProxy, string, string, List<IElectricalComponent>>(sheet1, str1, str2, electricalComponentList));
          funcGroupsComponents.Add(new Tuple<string, List<IElectricalComponent>>(symbolFileName, electricalComponentList));
        }
      }
      foreach (string posDesignation in strArray2)
      {
        FunctionalGroup functionalGroup = (FunctionalGroup) null;
        foreach (Tuple<SchDocumentProxy, string, string, List<IElectricalComponent>> tuple in tupleList)
        {
          if (functionalGroup == null || functionalGroup.Designation != tuple.Item3 || functionalGroup.Name != tuple.Item2)
            functionalGroup = new FunctionalGroup(tuple.Item2, tuple.Item3, posDesignation);
          foreach (ADComponent adComponent1 in tuple.Item4)
          {
            ADComponent adComponent2 = adComponent1.Clone();
            if (adComponent2.Parent == null)
              adComponent2.Parent = (IDocumentFile) tuple.Item1;
            if (adComponent2.FunctionalGroup == null)
              adComponent2.FunctionalGroup = functionalGroup;
            components.Add((IElectricalComponent) adComponent2);
          }
        }
      }
    }
    return components;
  }

  protected override List<IElectricalComponent> ReadComponents(ADDocument document)
  {
    SchDocumentProxy firstSheet;
    List<SchDocumentProxy> additionalSheets;
    List<string> stringList = this.SheetFuncGroup(document, out firstSheet, out additionalSheets);
    List<Tuple<string, List<IElectricalComponent>>> funcGroupsComponents = new List<Tuple<string, List<IElectricalComponent>>>();
    List<IElectricalComponent> electricalComponentList = this.SheetHandle(firstSheet, funcGroupsComponents, additionalSheets);
    if (additionalSheets != null)
    {
      foreach (SchDocumentProxy sheet in additionalSheets)
      {
        if (!stringList.Contains(sheet.FileName))
        {
          List<IElectricalComponent> collection = this.SheetHandle(sheet, funcGroupsComponents, additionalSheets);
          if (collection.Count > 0)
            electricalComponentList.AddRange((IEnumerable<IElectricalComponent>) collection);
        }
      }
    }
    return electricalComponentList;
  }

  private List<string> SheetFuncGroup(
    ADDocument document,
    out SchDocumentProxy firstSheet,
    out List<SchDocumentProxy> additionalSheets)
  {
    List<string> groups = new List<string>();
    ParametersContainer properties = (ParametersContainer) document.Properties;
    firstSheet = new SchDocumentProxy((ISchDocument) properties.Parametrable, document.FullPath, (ADIntegratorSettings) this.integratorSettings);
    this.ReadFunctionalGroup(firstSheet, groups);
    additionalSheets = (List<SchDocumentProxy>) null;
    if (document.AdditionalDocuments != null && document.AdditionalDocuments.Count > 0)
    {
      additionalSheets = new List<SchDocumentProxy>(document.AdditionalDocuments.Count);
      foreach (ADDocument additionalDocument in document.AdditionalDocuments)
      {
        SchDocumentProxy sheet = new SchDocumentProxy(ApiHelper.GetSchDocument(this._proxy.AddIn, additionalDocument.FullPath, true), additionalDocument.FullPath, (ADIntegratorSettings) this.integratorSettings);
        additionalSheets.Add(sheet);
        this.ReadFunctionalGroup(sheet, groups);
      }
    }
    return groups;
  }

  protected override void OnCreateRootAssembly(
    LinkedList<InitialArticleData> articleBlanks,
    Dictionary<InitialArticleData, BoardData<ADDocument>> childAssemblies,
    out PrintBoardDescriptor descriptor)
  {
    throw new Exception("Текущая версия интегратора не поддерживает работу с многосхемными проектами.");
  }

  private void ReadFunctionalGroup(SchDocumentProxy sheet, List<string> groups)
  {
    for (ISchSheetSymbol nextSheetSymbol = sheet.GetNextSheetSymbol(); nextSheetSymbol != null; nextSheetSymbol = sheet.GetNextSheetSymbol())
    {
      string fileName = nextSheetSymbol.FileName;
      if (!groups.Contains(fileName))
        groups.Add(fileName);
    }
  }

  private string[] ReadFunctionalGroupPosDesignations(string designatorText)
  {
    if (designatorText.ToUpper().StartsWith("REPEAT("))
    {
      Match match = new Regex("REPEAT\\s?\\(\\s?(?<prefix>\\w+)\\s?\\,\\s?(?<start>\\d+)\\s?\\,\\s?(?<end>\\d+)\\s?\\)", RegexOptions.IgnoreCase).Match(designatorText);
      string str = match.Groups["prefix"].Value;
      int int32_1 = match.Groups["start"].Value != string.Empty ? Convert.ToInt32(match.Groups["start"].Value) : 0;
      int int32_2 = match.Groups["end"].Value != string.Empty ? Convert.ToInt32(match.Groups["end"].Value) : 0;
      if (str != string.Empty && int32_1 != 0 && int32_2 != 0 && int32_2 > int32_1)
      {
        List<string> stringList = new List<string>();
        for (int index = int32_1; index <= int32_2; ++index)
          stringList.Add($"{str}{index}");
        return stringList.ToArray();
      }
    }
    return new string[1]{ designatorText };
  }

  protected override bool BeforeBoardCompositionHandle(
    ADDocument proxy,
    ElectricalSchemeDescriptors bomAssemblies,
    List<string> presentParts,
    ICollection<InitialArticleData> articleBlanks,
    Dictionary<IElectricalComponent, CompositionVariants> items,
    PrintBoardDescriptor boardDescriptor,
    out PrintBoardDescriptor newDescriptor,
    out List<SimpleRecord> newSimpleRecords,
    out List<Tuple<ElectricalArticleCache, ComponentsGroups>> articleComponentGroups)
  {
    newDescriptor = (PrintBoardDescriptor) null;
    newSimpleRecords = (List<SimpleRecord>) null;
    articleComponentGroups = (List<Tuple<ElectricalArticleCache, ComponentsGroups>>) null;
    if (!this._first)
      return false;
    bool flag1 = false;
    this._first = false;
    int variantsCount = this._project.VariantsCount;
    if (variantsCount > 0)
    {
      Tuple<StringKey, StringKey, bool> designationProperty = this.integratorSettings.AssemblyAttributesTable.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (x => x.Item1 == (StringKey) IDCache.Default.Designation.Text));
      articleComponentGroups = new List<Tuple<ElectricalArticleCache, ComponentsGroups>>();
      for (int index = 0; index < variantsCount; ++index)
      {
        IVariant variant = this._project.GetVariant(index);
        bool flag2 = false;
        if (!this.FilterVariant(variant))
        {
          if (designationProperty != null)
          {
            Parameter parameter = Array.Find<Parameter>(variant.Parameters, (Predicate<Parameter>) (x => string.Compare(x.Name, (string) designationProperty.Item2, true) == 0));
            if (parameter != null && boardDescriptor.Designation.Equals(Convert.ToString(parameter.Value)))
            {
              flag1 = true;
              flag2 = true;
            }
          }
          PrintBoardDescriptor descriptor;
          ElectricalArticleCache article;
          this.MakeAssembly(new BoardData<ADDocument>(proxy, (IValueBagContainer) variant, false, string.Empty, variant.Description, variant.Description), articleBlanks, out descriptor, out article, true);
          article.Composition = new List<CompositionItem>();
          List<IVariation> variations = variant.Variations;
          Dictionary<IElectricalComponent, CompositionVariants> items1 = new Dictionary<IElectricalComponent, CompositionVariants>();
          foreach (KeyValuePair<IElectricalComponent, CompositionVariants> keyValuePair in items)
          {
            string search = keyValuePair.Key.PosDesignation;
            IVariation variation = variations.Find((Predicate<IVariation>) (x => x.DesignatorText.Equals(search)));
            if (variation == null || variation.VariationKind == 0)
              items1.Add((IElectricalComponent) ((ADComponent) keyValuePair.Key).Clone(), keyValuePair.Value);
            else if (variation.VariationKind != 1 && variation.VariationKind == 2)
              items1.Add((IElectricalComponent) new ADComponent((ISchComponent) ((TypedParametersContainer<IVariation>) variation).Instance, (ADIntegratorSettings) this.integratorSettings, keyValuePair.Key.Parent, keyValuePair.Key.FunctionalGroup), keyValuePair.Value);
          }
          List<SimpleRecord> simpleRecords;
          ComponentsGroups componentsGroups = this.HandleComponents(article, items1, presentParts, articleBlanks, out simpleRecords);
          articleComponentGroups.Add(new Tuple<ElectricalArticleCache, ComponentsGroups>(article, componentsGroups));
          if (flag2)
          {
            newDescriptor = descriptor;
            newSimpleRecords = simpleRecords;
          }
        }
      }
    }
    return flag1;
  }

  protected override IECADCompositionRelation GetSimpleCompositionRelation(
    ComponentsGroup group,
    ElectricalArticleCache assembly)
  {
    return (IECADCompositionRelation) new SimpleADCompositionRelation((ADIntegratorSettings) this.integratorSettings, group, assembly, this.outputSvc);
  }

  protected override IECADCompositionRelation GetUnionCompositionRelation(
    ComponentsGroup group,
    ElectricalArticleCache assembly)
  {
    return (IECADCompositionRelation) new UnionADCompositionRelation((ADIntegratorSettings) this.integratorSettings, group, assembly, this.outputSvc);
  }

  private bool FilterVariant(IVariant variant)
  {
    ADIntegratorSettings integratorSettings = (ADIntegratorSettings) this.integratorSettings;
    if (integratorSettings.VariantsFilter.Count == 0)
      return false;
    foreach (Tuple<StringKey, StringKey> tuple in integratorSettings.VariantsFilter)
    {
      Tuple<StringKey, StringKey> filter = tuple;
      if (!string.IsNullOrEmpty((string) filter.Item1))
      {
        Parameter parameter = Array.Find<Parameter>(variant.Parameters, (Predicate<Parameter>) (x => string.Compare(x.Name, (string) filter.Item1, true) == 0));
        if (parameter != null && (string.IsNullOrEmpty((string) filter.Item2) || filter.Item2.Equals(Convert.ToString(parameter.Value))))
          return true;
      }
    }
    return false;
  }

  protected override string GetAssemblyPropertyValue(
    IValueBagContainer asmComponent,
    string propertyName)
  {
    Parameter parameter = Array.Find<Parameter>(((ParametersContainer) asmComponent).Parameters, (Predicate<Parameter>) (x => string.Compare(x.Name, propertyName, true) == 0));
    return parameter == null ? string.Empty : Convert.ToString(parameter.Value);
  }
}
