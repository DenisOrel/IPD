// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalCompositionReader`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Базовый класс, для чтения состава проекта ECAD</summary>
/// <typeparam name="TProxy"></typeparam>
public abstract class ElectricalCompositionReader<TProxy>
{
  protected List<BoardData<TProxy>> boards;
  protected ECADIntegratorSettings integratorSettings;
  protected IIntegratorOutput outputSvc;

  public ElectricalCompositionReader(
    List<BoardData<TProxy>> boards,
    ECADIntegratorSettings integratorSettings,
    IIntegratorOutput outputSvc)
  {
    this.boards = boards;
    this.integratorSettings = integratorSettings;
    this.outputSvc = outputSvc;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bomAssemblies">Описатели сборок, которые должны попасть в ПЭ</param>
  /// <returns></returns>
  public LinkedList<InitialArticleData> ReadArticles(out ElectricalSchemeDescriptors bomAssemblies)
  {
    List<string> presentParts = new List<string>();
    LinkedList<InitialArticleData> articleBlanks = new LinkedList<InitialArticleData>();
    InitialArticleData initialArticleData = (InitialArticleData) null;
    Dictionary<InitialArticleData, BoardData<TProxy>> childAssemblies = new Dictionary<InitialArticleData, BoardData<TProxy>>();
    bomAssemblies = new ElectricalSchemeDescriptors();
    foreach (BoardData<TProxy> board1 in this.boards)
    {
      BoardData<TProxy> board = board1;
      Guid guid = Guid.NewGuid();
      InitialArticleData assemblyData;
      List<SimpleRecord> simpleRecords;
      PrintBoardDescriptor printBoardDescriptor = this.BoardProcessing(board, out assemblyData, out simpleRecords, presentParts, (ICollection<InitialArticleData>) articleBlanks, bomAssemblies);
      assemblyData.CustomSections.Set((object) new AssemblyIDSection(guid));
      ElectricalSchemeDescriptor schemeDescriptor = bomAssemblies.Find((Predicate<ElectricalSchemeDescriptor>) (x => x.Designation.Equals(board.Designation)));
      if (schemeDescriptor == null)
      {
        schemeDescriptor = new ElectricalSchemeDescriptor(board.Designation, board.Name);
        bomAssemblies.Add(schemeDescriptor);
      }
      if (printBoardDescriptor != null)
      {
        printBoardDescriptor.Guid = guid;
        schemeDescriptor.PrintBoards.Add(printBoardDescriptor);
      }
      schemeDescriptor.SimpleRecords = simpleRecords;
      if (board.MainSchema)
        initialArticleData = assemblyData;
      else
        childAssemblies.Add(assemblyData, board);
    }
    if (initialArticleData != null)
    {
      if (childAssemblies.Count > 0)
        this.CreateComposition(initialArticleData.CustomSections.Get<ElectricalArticleCache>(), childAssemblies);
    }
    else if (childAssemblies.Count > 1)
    {
      PrintBoardDescriptor descriptor;
      this.OnCreateRootAssembly(articleBlanks, childAssemblies, out descriptor);
      descriptor.IsVirtual = true;
      ElectricalSchemeDescriptor schemeDescriptor1 = new ElectricalSchemeDescriptor(descriptor.Designation, descriptor.Name);
      schemeDescriptor1.PrintBoards.Add(descriptor);
      foreach (ElectricalSchemeDescriptor schemeDescriptor2 in (List<ElectricalSchemeDescriptor>) bomAssemblies)
      {
        schemeDescriptor1.PrintBoards.AddRange((IEnumerable<PrintBoardDescriptor>) schemeDescriptor2.PrintBoards);
        if (schemeDescriptor2.SimpleRecords != null && schemeDescriptor2.SimpleRecords.Count > 0)
          schemeDescriptor1.SimpleRecords.AddRange((IEnumerable<SimpleRecord>) schemeDescriptor2.SimpleRecords);
      }
      bomAssemblies.Clear();
      bomAssemblies.Add(schemeDescriptor1);
    }
    return articleBlanks;
  }

  protected void CreateComposition(
    ElectricalArticleCache article,
    Dictionary<InitialArticleData, BoardData<TProxy>> childAssemblies)
  {
    if (article.Composition == null)
      article.Composition = new List<CompositionItem>();
    foreach (KeyValuePair<InitialArticleData, BoardData<TProxy>> childAssembly in childAssemblies)
      article.Composition.Add(CompositionItem.CreateSimple(childAssembly.Key.ArticleKey, ((IElectricalComponent) childAssembly.Value.AsmComponent).PosGuid, string.Empty));
  }

  protected virtual void OnCreateRootAssembly(
    LinkedList<InitialArticleData> articleBlanks,
    Dictionary<InitialArticleData, BoardData<TProxy>> childAssemblies,
    out PrintBoardDescriptor descriptor)
  {
    descriptor = (PrintBoardDescriptor) null;
  }

  protected InitialArticleData MakeAssembly(
    BoardData<TProxy> board,
    ICollection<InitialArticleData> articleBlanks,
    out PrintBoardDescriptor descriptor,
    out ElectricalArticleCache article,
    bool include)
  {
    InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.Autodetect)
    {
      DisplayName = $"{board.Designation}({board.Name})",
      ArticleKey = board.ArticleKey,
      InitialDocumentType = this.boards.Count == 1 ? ArticleInitialDocumentType.Normal : (board.MainSchema ? ArticleInitialDocumentType.Normal : ArticleInitialDocumentType.Hidden)
    };
    descriptor = this.MakeDescriptor(board.AsmComponent, this.integratorSettings.AssemblyAttributesTable, board.MainSchema);
    article = new ElectricalArticleCache(board.AsmComponent, ArticleTypes.Assembly);
    initialArticleData.CustomSections.Set((object) article);
    if (include)
      articleBlanks.Add(initialArticleData);
    return initialArticleData;
  }

  protected PrintBoardDescriptor MakeDescriptor(
    IValueBagContainer asmComponent,
    List<Tuple<StringKey, StringKey, bool>> attributesTable,
    bool root)
  {
    Tuple<StringKey, StringKey, bool> tuple1 = attributesTable?.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (x => x.Item1 == (StringKey) IDCache.Default.Name.Text));
    Tuple<StringKey, StringKey, bool> tuple2 = attributesTable?.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (x => x.Item1 == (StringKey) IDCache.Default.Designation.Text));
    string name = tuple1 != null ? this.GetAssemblyPropertyValue(asmComponent, (string) tuple1.Item2) : string.Empty;
    return new PrintBoardDescriptor(tuple2 != null ? this.GetAssemblyPropertyValue(asmComponent, (string) tuple2.Item2) : string.Empty, name, root);
  }

  protected abstract string GetAssemblyPropertyValue(
    IValueBagContainer asmComponent,
    string propertyName);

  private PrintBoardDescriptor BoardProcessing(
    BoardData<TProxy> board,
    out InitialArticleData assemblyData,
    out List<SimpleRecord> simpleRecords,
    List<string> presentParts,
    ICollection<InitialArticleData> articleBlanks,
    ElectricalSchemeDescriptors bomAssemblies)
  {
    PrintBoardDescriptor descriptor;
    ElectricalArticleCache article;
    assemblyData = this.MakeAssembly(board, articleBlanks, out descriptor, out article, false);
    Dictionary<IElectricalComponent, CompositionVariants> items = this.ReadAndFilterBoardComponents(board.Proxy);
    if (items.Count > 0)
      article.Composition = new List<CompositionItem>(items.Count);
    PrintBoardDescriptor newDescriptor;
    List<SimpleRecord> newSimpleRecords;
    List<Tuple<ElectricalArticleCache, ComponentsGroups>> articleComponentGroups;
    if (this.BeforeBoardCompositionHandle(board.Proxy, bomAssemblies, presentParts, articleBlanks, items, descriptor, out newDescriptor, out newSimpleRecords, out articleComponentGroups))
    {
      simpleRecords = newSimpleRecords;
      descriptor = newDescriptor;
    }
    else
    {
      articleBlanks.Add(assemblyData);
      ComponentsGroups componentsGroups = this.HandleComponents(article, items, presentParts, articleBlanks, out simpleRecords);
      if (articleComponentGroups == null)
        articleComponentGroups = new List<Tuple<ElectricalArticleCache, ComponentsGroups>>(1);
      articleComponentGroups.Add(new Tuple<ElectricalArticleCache, ComponentsGroups>(article, componentsGroups));
    }
    foreach (Tuple<ElectricalArticleCache, ComponentsGroups> tuple in articleComponentGroups)
    {
      List<Guid> posGuidsCache = new List<Guid>();
      foreach (ComponentsGroup group in (List<ComponentsGroup>) tuple.Item2)
        (!this.CheckGroup(group, tuple.Item1, articleComponentGroups) ? this.GetSimpleCompositionRelation(group, tuple.Item1) : this.GetUnionCompositionRelation(group, tuple.Item1)).Handle(posGuidsCache);
    }
    return descriptor;
  }

  protected virtual IECADCompositionRelation GetUnionCompositionRelation(
    ComponentsGroup group,
    ElectricalArticleCache assembly)
  {
    return (IECADCompositionRelation) new UnionECADCompositionRelation(this.integratorSettings, group, assembly);
  }

  protected virtual IECADCompositionRelation GetSimpleCompositionRelation(
    ComponentsGroup group,
    ElectricalArticleCache assembly)
  {
    return (IECADCompositionRelation) new SimpleECADCompositionRelation(this.integratorSettings, group, assembly);
  }

  protected virtual bool BeforeBoardCompositionHandle(
    TProxy proxy,
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
    return false;
  }

  protected bool CheckGroup(
    ComponentsGroup group,
    ElectricalArticleCache currentArticle,
    List<Tuple<ElectricalArticleCache, ComponentsGroups>> articleComponentGroups)
  {
    if (articleComponentGroups != null)
    {
      string key = group.Key;
      foreach (Tuple<ElectricalArticleCache, ComponentsGroups> tuple in articleComponentGroups.FindAll((Predicate<Tuple<ElectricalArticleCache, ComponentsGroups>>) (x => x.Item1 != currentArticle)))
      {
        List<ComponentsGroup> all = tuple.Item2.FindAll((Predicate<ComponentsGroup>) (x => x.PartName.Equals(group.PartName)));
        if (all != null && all.Count != 0 && !all.Exists((Predicate<ComponentsGroup>) (y => y.Key.Equals(key))))
          return false;
      }
    }
    return CompositionAttributesReader.CheckRelationAttributes(group.Components, this.integratorSettings);
  }

  private void CreateSimpleRecord(
    List<SimpleRecord> simpleRecords,
    string namePart,
    string posDesignation,
    IElectricalComponent component)
  {
    List<Tuple<string, object>> attributes = CompositionAttributesReader.ReadAttributes(component, this.integratorSettings);
    if (attributes != null)
      simpleRecords.Add((SimpleRecord) new SimpleAttributableRecord(namePart, posDesignation, attributes));
    else
      simpleRecords.Add(new SimpleRecord(namePart, posDesignation));
  }

  protected ComponentsGroups HandleComponents(
    ElectricalArticleCache article,
    Dictionary<IElectricalComponent, CompositionVariants> items,
    List<string> presentParts,
    ICollection<InitialArticleData> articleBlanks,
    out List<SimpleRecord> simpleRecords)
  {
    simpleRecords = new List<SimpleRecord>();
    List<CompositionVariants> compositionVariantsList = new List<CompositionVariants>((IEnumerable<CompositionVariants>) new CompositionVariants[2]
    {
      CompositionVariants.Specification,
      CompositionVariants.SpecificationAndElementsList
    });
    List<Tuple<string, string[], Guid[], string, List<IElectricalComponent[]>>> tupleList1 = new List<Tuple<string, string[], Guid[], string, List<IElectricalComponent[]>>>();
    ComponentsGroups componentsGroups1 = new ComponentsGroups();
    foreach (KeyValuePair<IElectricalComponent, CompositionVariants> keyValuePair in items)
    {
      string posDesignation;
      string displayName;
      Guid posGuid;
      bool replace;
      bool tuning;
      string namePart;
      if (this.CheckBoardComponent(keyValuePair.Key, out namePart, out posDesignation, out displayName, out posGuid, out replace, out tuning, false))
      {
        if (!compositionVariantsList.Contains(keyValuePair.Value))
          this.CreateSimpleRecord(simpleRecords, namePart, posDesignation, keyValuePair.Key);
        else if (string.IsNullOrEmpty(namePart))
        {
          this.outputSvc.WriteLine($"Компонент схемы {keyValuePair.Key.UID} не обработан, так как у него отсутствует идентифицирующий атрибут соотвествующий наименованию");
        }
        else
        {
          if (!presentParts.Contains(namePart))
          {
            presentParts.Add(namePart);
            if (compositionVariantsList.Contains(keyValuePair.Value))
            {
              InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.Autodetect)
              {
                DisplayName = displayName,
                ArticleKey = namePart
              };
              initialArticleData.CustomSections.Set((object) new ElectricalArticleCache((IValueBagContainer) keyValuePair.Key, ArticleTypes.Component));
              articleBlanks.Add(initialArticleData);
            }
            else
            {
              this.CreateSimpleRecord(simpleRecords, namePart, posDesignation, keyValuePair.Key);
              continue;
            }
          }
          string groupID = ComponentsGroupHelper.GetGroupID(keyValuePair.Key.FunctionalGroup, posDesignation, replace, tuning);
          ComponentsGroup componentsGroup = componentsGroups1.Find((Predicate<ComponentsGroup>) (x => x.PartName.Equals(namePart) && x.GroupID == groupID));
          if (componentsGroup == null)
          {
            componentsGroups1.Add(new ComponentsGroup(namePart, posGuid, groupID, posDesignation, keyValuePair.Key, keyValuePair.Value));
          }
          else
          {
            List<IElectricalComponent> electricalComponentList;
            if (!componentsGroup.Components.TryGetValue(posDesignation, out electricalComponentList))
            {
              electricalComponentList = new List<IElectricalComponent>();
              componentsGroup.Components.Add(posDesignation, electricalComponentList);
              componentsGroup.PosGuids.Add(posGuid);
            }
            electricalComponentList.Add(keyValuePair.Key);
          }
        }
      }
    }
    ComponentsGroups componentsGroups2 = new ComponentsGroups();
    Regex regex = new Regex("\\d+");
    foreach (ComponentsGroup prototype in (List<ComponentsGroup>) componentsGroups1)
    {
      if (prototype.Components.Count == 1)
      {
        componentsGroups2.Add(prototype);
      }
      else
      {
        List<Tuple<double, string, List<IElectricalComponent>, Guid>> tupleList2 = new List<Tuple<double, string, List<IElectricalComponent>, Guid>>();
        bool flag1 = false;
        int index1 = 0;
        foreach (KeyValuePair<string, List<IElectricalComponent>> component in prototype.Components)
        {
          double number;
          if (NumberParserAdvanced.ParseNumber(component.Key, true, out number, out string _, out string _))
          {
            tupleList2.Add(new Tuple<double, string, List<IElectricalComponent>, Guid>(number, component.Key, component.Value, prototype.PosGuids[index1]));
            ++index1;
          }
          else
          {
            componentsGroups2.Add(prototype);
            flag1 = true;
            break;
          }
        }
        if (!flag1)
        {
          tupleList2.Sort((Comparison<Tuple<double, string, List<IElectricalComponent>, Guid>>) ((x1, x2) => x1.Item1.CompareTo(x2.Item1)));
          bool flag2 = false;
          for (int index2 = 1; index2 < tupleList2.Count; ++index2)
          {
            if (tupleList2[index2].Item1 - tupleList2[index2 - 1].Item1 > 1.0)
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2)
          {
            componentsGroups2.Add(prototype);
          }
          else
          {
            ComponentsGroup componentsGroup = (ComponentsGroup) null;
            double number = 0.0;
            foreach (Tuple<double, string, List<IElectricalComponent>, Guid> tuple in tupleList2)
            {
              if (componentsGroup == null)
              {
                componentsGroup = this.CreateNewGroup(prototype, tuple, out number);
                componentsGroups2.Add(componentsGroup);
              }
              else
              {
                if (tuple.Item1 - number > 1.0)
                {
                  bool flag3 = false;
                  for (double num = number + 1.0; num < tuple.Item1; ++num)
                  {
                    Match match = regex.Match(tuple.Item2);
                    if (match.Success)
                    {
                      string foundPosDes = tuple.Item2.Replace(match.Value, num.ToString());
                      if (componentsGroups1.Exists((Predicate<ComponentsGroup>) (x => x.Components.ContainsKey(foundPosDes))))
                      {
                        componentsGroup = this.CreateNewGroup(prototype, tuple, out number);
                        componentsGroups2.Add(componentsGroup);
                        flag3 = true;
                        break;
                      }
                    }
                  }
                  if (flag3)
                    continue;
                }
                componentsGroup.Components.Add(tuple.Item2, tuple.Item3);
                componentsGroup.PosGuids.Add(tuple.Item4);
                number = tuple.Item1;
              }
            }
          }
        }
      }
    }
    return componentsGroups2;
  }

  private ComponentsGroup CreateNewGroup(
    ComponentsGroup prototype,
    Tuple<double, string, List<IElectricalComponent>, Guid> item,
    out double number)
  {
    ComponentsGroup prototype1 = prototype.CreatePrototype(prototype.GroupID + item.Item2);
    prototype1.Components.Add(item.Item2, item.Item3);
    prototype1.PosGuids.Add(item.Item4);
    number = item.Item1;
    return prototype1;
  }

  private bool CheckBoardComponent(
    IElectricalComponent item,
    out string namePart,
    out string posDesignation,
    out string displayName,
    out Guid posGuid,
    out bool replace,
    out bool tuning,
    bool throwException)
  {
    namePart = string.Empty;
    posDesignation = string.Empty;
    displayName = string.Empty;
    posGuid = Guid.Empty;
    replace = false;
    tuning = false;
    namePart = item.PartNumber;
    string str1 = string.IsNullOrEmpty(namePart) ? "<пусто>" : namePart;
    posDesignation = item.PosDesignation;
    if (string.IsNullOrEmpty(posDesignation))
    {
      string str2 = $"Компонент схемы {str1} ({item.UID}) не обработан, так как у него отсутствует позиционное обозначение";
      this.outputSvc.WriteLine(str2);
      if (throwException)
        throw new Exception(str2);
      return false;
    }
    displayName = $"{str1}({item.UID}) {posDesignation}";
    string str3 = Convert.ToString(item.GetPropertyValue(ElectricalConsts.PosGuidAttribute));
    posGuid = string.IsNullOrEmpty(str3) || !GuidHelper.IsGuid(str3) ? Guid.NewGuid() : new Guid(str3);
    replace = this.ValueInList(this.integratorSettings.ReplaceParameters, item);
    tuning = this.ValueInList(this.integratorSettings.TuningParameters, item);
    int startIndex = posDesignation.IndexOf('*');
    if (startIndex > 0)
    {
      if (!tuning)
        tuning = true;
      posDesignation = posDesignation.Remove(startIndex);
    }
    if (item.FunctionalGroup == null)
      item.FunctionalGroup = this.ReadFunctionalGroupFromComponent((IPropertiesCollection) item);
    return true;
  }

  protected virtual FunctionalGroup ReadFunctionalGroupFromComponent(IPropertiesCollection component)
  {
    return (FunctionalGroup) null;
  }

  protected abstract IComponentsListFilter GetFilter(ComponentsListFilterType type);

  protected abstract List<IElectricalComponent> ReadComponents(TProxy projectItem);

  /// <summary>
  /// Получить отфильтрованную коллекцию компонентов из схемы/платы
  /// </summary>
  private Dictionary<IElectricalComponent, CompositionVariants> ReadAndFilterBoardComponents(
    TProxy projectItem)
  {
    Dictionary<IElectricalComponent, CompositionVariants> dictionary = new Dictionary<IElectricalComponent, CompositionVariants>();
    IComponentsListFilter filter = this.GetFilter(ComponentsListFilterType.CompositionAndElementsList);
    foreach (IElectricalComponent readComponent in this.ReadComponents(projectItem))
    {
      CompositionVariants variant;
      if (!filter.InFilter(readComponent, out variant))
        dictionary.Add(readComponent, variant);
    }
    return dictionary;
  }

  /// <summary>
  /// Ищет у компонента заданный в списке параметр со значением
  /// </summary>
  private bool ValueInList(List<Tuple<StringKey, StringKey>> list, IElectricalComponent component)
  {
    if (list != null && list.Count > 0)
    {
      foreach (Tuple<StringKey, StringKey> tuple in list)
      {
        string str = Convert.ToString(component.GetPropertyValue((string) tuple.Item1));
        if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty((string) tuple.Item2) || (StringKey) str == tuple.Item2)
          return true;
      }
    }
    return false;
  }
}
