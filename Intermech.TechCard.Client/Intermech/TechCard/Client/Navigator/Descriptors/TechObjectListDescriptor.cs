// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechObjectListDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.TechCard.Client.Navigator.Nodes;
using System.Collections;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>
/// Declare custom TechCard descriptor to display
/// object list (typed or not) with columns customization by type
/// </summary>
public class TechObjectListDescriptor : ListDescriptor
{
  /// <summary>Descriptor keys mode</summary>
  protected TechObjectListMode _mode;

  /// <summary>Constructor</summary>
  /// <param name="categoryId"></param>
  /// <param name="typeId"></param>
  /// <param name="caption"></param>
  /// <param name="objectIDs"></param>
  public TechObjectListDescriptor(int categoryId, int typeId, string caption, IList objectIDs)
    : base(categoryId, typeId, caption, objectIDs)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public TechObjectListDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public bool ExpandNodes { get; set; }

  /// <summary>Descriptor key mode</summary>
  public TechObjectListMode Mode
  {
    get => this._mode;
    set => this._mode = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeId)
  {
    return (INode) new TechObjectListNode((IDescriptor) this, this._objectIDs, this._typeID, this.ExpandNodes);
  }

  /// <summary>Отобразить колонку в поле</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Поле</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    if ((column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid) && column.ID.Equals((object) "F_CAPTION"))
      return (object) "F_CAPTION";
    object fieldName = Helper.MapColumnToFieldName(column);
    if (fieldName != null)
      return fieldName;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }
}
