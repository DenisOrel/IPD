// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSColumnScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

#nullable disable
namespace Intermech.AVS;

public class AVSColumnScheme : INodeColumnScheme, IObjectWithIcon, IComparer<int>
{
  protected List<object> _possibleAttributesIDs = new List<object>();
  protected Guid _schemeGuid = Guid.NewGuid();
  protected Dictionary<object, NodeColumn> _createdColumns = new Dictionary<object, NodeColumn>();

  public ReadOnlyCollection<object> PossibleAttributesIDs
  {
    get => this._possibleAttributesIDs.AsReadOnly();
  }

  public virtual bool IsRelationColumn(NodeColumn nc) => false;

  public Guid SchemeGuid
  {
    get => this._schemeGuid;
    set => this._schemeGuid = value;
  }

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public virtual string Name => string.Empty;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    return this.IsSupportedColumnID(columnID) ? columnID.ToString() : (string) null;
  }

  /// <summary>
  /// Восстанавливает идентификатор виртуальной колонки по ее
  /// постоянному имени, которое действительно только на текущий сеанс
  /// работы универсального клиента. Если восстанавливаемая колонка не
  /// существует, то метод должен вернуть null.
  /// </summary>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns>Идентификатор виртуальной колонки</returns>
  public object PersistNameToColumnID(string persistName)
  {
    int result;
    return int.TryParse(persistName, out result) ? (object) result : (object) null;
  }

  /// <summary>
  /// Создает виртуальную колонку без сортировки по указанному
  /// идентификатору. Если колонки с заданным идентификатором в схеме нет -
  /// то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Виртуальная колонка</returns>
  public virtual NodeColumn CreateColumn(Guid schemeGuid, object columnID)
  {
    if (!this.IsSupportedColumnID(columnID))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
    if (column.Priority == SchemeColumnPriority.Standard)
      column.Priority = SchemeColumnPriority.High;
    return column;
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с таким идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  public virtual NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (!this.IsSupportedColumnID(columnID))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, (int) columnID, sortOrder, sortIndex);
    if (column.Priority == SchemeColumnPriority.Standard)
      column.Priority = SchemeColumnPriority.High;
    return column;
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;

  public virtual Icon Icon => (Icon) null;

  private bool IsSupportedColumnID(object columnID)
  {
    return columnID != null && this._possibleAttributesIDs.Contains(columnID);
  }

  protected virtual NodeColumn CreateColumn(
    Guid schemeGuid,
    int columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    string caption = this.GetCaption(columnID);
    bool systemAttr = (this.GetColumnAttributeOptions(columnID) & AttributeOptions.Internal) != 0;
    NodeColumn nodeColumn = new NodeColumn(schemeGuid, (object) columnID, this.GetColumnType(columnID), this.GetColumnFieldTypes(columnID), caption, sortOrder, sortIndex, caption, caption, systemAttr);
    AttributeInfo attributeInfo;
    if ((attributeInfo = this.FindAttributeInfo(nodeColumn)) != null)
      nodeColumn.Source = (INodeColumnSource) attributeInfo;
    this._createdColumns[(object) columnID] = nodeColumn;
    return nodeColumn;
  }

  public virtual NodeColumn GetColumnByAttributeID(object atributeID)
  {
    return this._createdColumns.ContainsKey(atributeID) ? this._createdColumns[atributeID] : (NodeColumn) null;
  }

  private FieldTypes GetColumnFieldTypes(int columnID)
  {
    FieldTypes fieldTypes;
    return ColumnsCache._cachedFieldTypes.TryGetValue(columnID, out fieldTypes) ? fieldTypes : FieldTypes.ftUnknown;
  }

  private AttributeOptions GetColumnAttributeOptions(int columnID)
  {
    AttributeOptions attributeOptions;
    return ColumnsCache._cachedAttributeOptions.TryGetValue(columnID, out attributeOptions) ? attributeOptions : AttributeOptions.None;
  }

  private Type GetColumnType(int columnID)
  {
    Type type;
    return ColumnsCache._cachedAttributesTypes.TryGetValue(columnID, out type) ? type : (Type) null;
  }

  private string GetCaption(int columnID)
  {
    string str;
    return ColumnsCache._cachedAttributesCaptions.TryGetValue(columnID, out str) ? str : (string) null;
  }

  protected void LoadPossibleAttributes(List<IMSAttribute4> attrs)
  {
    foreach (IMSAttribute4 attr in attrs)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attr.AttributeID);
      if (attributeType != null && attributeType.IsGridable && !this._possibleAttributesIDs.Contains((object) attributeType.AttributeID))
      {
        int attributeId = attributeType.AttributeID;
        this._possibleAttributesIDs.Add((object) attributeId);
        if (!ColumnsCache._cachedAttributesCaptions.ContainsKey(attributeId))
        {
          ColumnsCache._cachedAttributesCaptions[attributeId] = attributeType.Name;
          ColumnsCache._cachedFieldTypes[attributeId] = attributeType.FieldType;
          ColumnsCache._cachedAttributeOptions[attributeId] = attributeType.Options;
          ColumnsCache._cachedAttributesTypes[attributeId] = Helper.ConvertType(attributeType.FieldType);
        }
      }
    }
  }

  public virtual AttributeInfo FindAttributeInfo(NodeColumn nodeColumn)
  {
    return AVSColumnScheme.MakeSourceAttributeInfoForColumn(FieldSource.Object, nodeColumn);
  }

  internal static AttributeInfo MakeSourceAttributeInfoForColumn(
    FieldSource source,
    NodeColumn nodeColumn)
  {
    AttributeInfo attributeInfo = (AttributeInfo) null;
    if (nodeColumn != null)
    {
      int id = (int) nodeColumn.ID;
      Guid attributeGuidById = DBHelper.GetAttributeGuidByID(id);
      if (attributeGuidById != Guid.Empty)
        attributeInfo = new AttributeInfo(source, attributeGuidById, id, nodeColumn.Caption);
    }
    return attributeInfo;
  }

  public int Compare(int x, int y)
  {
    int? nullable = new int?();
    try
    {
      string attributesCaption1 = ColumnsCache._cachedAttributesCaptions[x];
      string attributesCaption2 = ColumnsCache._cachedAttributesCaptions[y];
      if (attributesCaption1 != null)
      {
        if (attributesCaption2 != null)
          nullable = new int?(Comparer<string>.Default.Compare(attributesCaption1, attributesCaption2));
      }
    }
    finally
    {
      if (!nullable.HasValue)
        nullable = new int?(Comparer<int>.Default.Compare(x, y));
    }
    return nullable.Value;
  }
}
