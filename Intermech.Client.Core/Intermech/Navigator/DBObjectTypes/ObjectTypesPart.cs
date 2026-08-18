
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjectTypes;

public class ObjectTypesPart : ObjectTypesItems, INodePart, INodeItems, INodeQuerySupport
{
  private readonly int objTypeId;
  /// <summary>
  /// Составное значение: атрибут "Ручная выборка" : источник - объект
  /// </summary>
  public static NodeColumnID ncHANDS_SELECTION = new NodeColumnID((object) new Guid("cad00155-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object);

  public ObjectTypesPart(int objTypeId) => this.objTypeId = objTypeId;

  public object Owner { get; set; }

  public virtual INodeQuery GetQuery()
  {
    return (INodeQuery) new ObjectTypesQuery((INodeQuerySupport) this, this.objTypeId, this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null);
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddAllColumns(columns);
    return columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок. Пустая строка - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddAllColumns(columns);
    return columns;
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок. Если null - есть только название по умолчанию (пустая строка)
  /// </summary>
  /// <returns></returns>
  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public object MapColumnToField(NodeColumn column) => Helper.MapColumnToField(column);

  public List<object> GetSpecialFields()
  {
    List<object> specialFields = new List<object>();
    specialFields.Add((object) "F_OBJECT_TYPE");
    if (this.Owner is ObjectTypeNode owner && (owner.ObjTypeID == Intermech.Navigator.Services._objectTypeIDCommonSelection || owner.ObjTypeID == Intermech.Navigator.Services._objectTypeIDPersonalSelection))
      specialFields.Add((object) ObjectTypesPart.ncHANDS_SELECTION);
    return specialFields;
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) "F_OBJECT_TYPE")]);
    if (int32 != Intermech.Navigator.Services._objectTypeIDPersonalSelection && int32 != Intermech.Navigator.Services._objectTypeIDCommonSelection || adapter.GetFieldIndex((object) ObjectTypesPart.ncHANDS_SELECTION) < 0)
      return (INodeID) new NodeID(int32, AccessRights.NotDefined);
    bool handSelection = adapter.GetFieldIndex((object) ObjectTypesPart.ncHANDS_SELECTION) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectTypesPart.ncHANDS_SELECTION)] != DBNull.Value && Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectTypesPart.ncHANDS_SELECTION)]) == 1L;
    return (INodeID) new SelectionNodeID(int32, handSelection);
  }

  public object CreateRecordId(INodeID nodeId) => (object) nodeId.TypeID;
}
