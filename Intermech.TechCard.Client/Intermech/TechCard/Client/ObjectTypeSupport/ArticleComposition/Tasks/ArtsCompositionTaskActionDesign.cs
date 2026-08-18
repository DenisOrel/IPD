// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.ArtsCompositionTaskActionDesign
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>
/// Действие задачи по развороту состава конструкторской сборочной единицы (КСЕ)
/// </summary>
internal class ArtsCompositionTaskActionDesign : CompositionTaskActionBase
{
  /// <summary>
  /// 
  /// </summary>
  private readonly List<long> _compositionContextIds;
  /// <summary>
  /// 
  /// </summary>
  private readonly SearchDirection _searchDirection;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="compositionContextIds">Контексты состава ( фильтрация контекстов состава )</param>
  /// <param name="searchDirection">Направление получения данных</param>
  public ArtsCompositionTaskActionDesign(
    List<long> compositionContextIds,
    SearchDirection searchDirection)
  {
    this._compositionContextIds = compositionContextIds;
    this._searchDirection = searchDirection;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override ICompositionService StartCompositionService(IUserSession session)
  {
    ICompositionService service = ServiceUtils.GetService<ICompositionService>((object) session, false);
    if (service == null)
      return (ICompositionService) null;
    HybridDictionary Tags = new HybridDictionary(0, true);
    Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) this._compositionContextIds;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes));
    childrenIdRecursive.AddRange((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes);
    RuntimeSearchScheme compositionQuantityScheme = RuntimeSearchScheme.GetCompositionQuantityScheme(session, childrenIdRecursive.ToArray(), TechCardConsts.RelTypes.ArtsCompositionRelations.AsArrayOf<int>());
    compositionQuantityScheme.Direction = this._searchDirection;
    if (this.ObjectGrouping)
      compositionQuantityScheme.Options = compositionQuantityScheme.Options.AddFlags<SearchOptions>(SearchOptions.ObjectGrouping);
    List<ColumnDescriptor> schemeDescriptors = RuntimeSearchScheme.GetCompositionQuantitySchemeDescriptors(session);
    schemeDescriptors.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    schemeDescriptors.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    if (this.ExtraColumns != null)
      schemeDescriptors.AddRange(this.ExtraColumns);
    service.CancelSelect(this._taskGuid);
    service.Select(session.SessionGUID, this._objInfoItem.ObjectID, compositionQuantityScheme, schemeDescriptors, this._taskGuid, string.Empty, Tags);
    return service;
  }

  /// <summary>Перечень дополнительных полей</summary>
  public IEnumerable<ColumnDescriptor> ExtraColumns { get; set; }

  /// <summary>Группировка объектов (подсчет общего количества)</summary>
  public bool ObjectGrouping { get; set; } = true;
}
