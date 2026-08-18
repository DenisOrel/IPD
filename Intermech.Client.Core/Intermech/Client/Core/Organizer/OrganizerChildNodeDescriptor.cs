
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Client.Core.Organizer;

/// <summary>Дескриптор для узлов добавляемых в узел "Органайзер".</summary>
public class OrganizerChildNodeDescriptor : HiveDescriptor
{
  public readonly int RelTypeID;
  public readonly int[] ObjTypeIDs;

  /// <summary>Наименование узла.</summary>
  public new string Caption => this._caption;

  /// <summary>Коллекция колонок для отображения данных.</summary>
  public NodeColumnCollection Columns { get; private set; }

  /// <summary>Набор условий для выбора данных.</summary>
  public ConditionStructure[] Conditions { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public Guid Guid { get; private set; }

  /// <summary>
  /// Если заполнено, используется для указания дополнительных параметров запроса.
  /// </summary>
  public HybridDictionary Tags { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="guid">Guid узла</param>
  /// <param name="categoryID">Категория узла, после регистрации в IGuidMapper</param>
  /// <param name="typeID">Идентификатор типа объектов, которые будут входить в данный узел</param>
  /// <param name="relTypeID"></param>
  /// <param name="objTypeIDs"></param>
  /// <param name="columns">Коллекция колонок для отображения данных</param>
  /// <param name="conditions">Набор условия для выбора данных</param>
  /// <param name="caption">Наименование узла</param>
  public OrganizerChildNodeDescriptor(
    Guid guid,
    int categoryID,
    int typeID,
    int relTypeID,
    int[] objTypeIDs,
    NodeColumnCollection columns,
    ConditionStructure[] conditions,
    string caption)
    : base(categoryID, typeID, caption)
  {
    this.Guid = guid;
    this.Columns = columns;
    this.Conditions = conditions;
    this.RelTypeID = relTypeID;
    this.ObjTypeIDs = objTypeIDs;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="state"></param>
  protected OrganizerChildNodeDescriptor(PersistentState state)
    : base(state)
  {
    this.Guid = Guid.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IDBRecords GetCollection(IUserSession session)
  {
    IDBRecords collection = (IDBRecords) null;
    if (session != null)
    {
      if (this.RelTypeID == 0)
      {
        collection = (IDBRecords) session.GetObjectCollection(this._typeID);
      }
      else
      {
        IDBRelationCollection relationCollection = session.GetRelationCollection(this.RelTypeID);
        relationCollection.ChildObjectTypes = (IList<int>) this.ObjTypeIDs;
        collection = (IDBRecords) relationCollection;
      }
    }
    return collection;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public INodePart GetPart(IServiceProvider services)
  {
    INodePart part;
    if (this.RelTypeID == 0)
    {
      part = (INodePart) new OrganizerChildNodePart(this._typeID, this.Columns, this.Conditions, services)
      {
        Tag = this.Tags
      };
    }
    else
    {
      long num = 0;
      for (int index = 0; index < this.Conditions.Length; ++index)
      {
        ConditionStructure condition = this.Conditions[index];
        if (condition.Attribute is int && (int) condition.Attribute == -22 && condition.RelationalOperator == RelationalOperators.Equal)
        {
          num = Convert.ToInt64(condition.Value);
          condition.RelationalOperator = RelationalOperators.NOP;
          break;
        }
      }
      part = (INodePart) new RelatedObjectsPart(this._typeID, num - 1L, RelatedObjectsRole.Applicability, this.RelTypeID, this.Conditions, services);
    }
    return part;
  }
}
