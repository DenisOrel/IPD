// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Расцеховочный маршрут</summary>
public class CehRouteClass : CehRouteElementContainer
{
  /// <summary>Список шаблонов расцеховки</summary>
  private readonly CustomTechClassList<CehRouteTemplateClass> _templateList;

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта</param>
  public CehRouteClass(long objectId)
    : base(objectId)
  {
    this._templateList = new CustomTechClassList<CehRouteTemplateClass>((CustomTechClass) this);
  }

  /// <summary>Очистка / удаление содержимого объекта</summary>
  public override void Clear()
  {
    base.Clear();
    this.TemplateList.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  public override void LoadData(IUserSession session)
  {
    this.Clear();
    base.LoadData(session);
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0)
    };
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TemplRouteBaseID).ToArray(), (object) null, LogicalOperators.NONE, 0, false)
    };
    DataTable childSostavData = DataHelper.GetChildSostavData(new ObjInfoItem(this.ObjectId), session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    if (childSostavData == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
    {
      long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
      if (int64Value != 0L)
      {
        TemplRouteClass templRouteClass1 = new TemplRouteClass(int64Value, DataSetProcessor.GetInt64Value(row, 1, 0L));
        templRouteClass1.OrderID = DataSetProcessor.GetInt64Value(row, 2, 0L);
        TemplRouteClass templRouteClass2 = templRouteClass1;
        templRouteClass2.LoadData(session);
        this.TemplateList.Add((CehRouteTemplateClass) templRouteClass2);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override void SaveData(IUserSession session) => base.SaveData(session);

  /// <summary>Список шаблонов расцеховки</summary>
  public CustomTechClassList<CehRouteTemplateClass> TemplateList => this._templateList;
}
