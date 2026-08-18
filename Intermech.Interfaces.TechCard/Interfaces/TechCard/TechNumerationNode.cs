// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationNode
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Класс элемента нумерации</summary>
[Serializable]
public class TechNumerationNode : ITechNumerationNode, ICloneable
{
  /// <summary>Идентификатор объекта</summary>
  private long _objectID;
  /// <summary>Идентификатор правила нумерации</summary>
  private long _numRuleID;
  /// <summary>Тип нумеруемого объекта</summary>
  private Guid _objectTypeGuid;
  /// <summary>Нумеруемый атрибут</summary>
  private Guid _attributeTypeGuid;
  /// <summary>Режим нумерации</summary>
  private TechNumerationMode _numerationMode;
  /// <summary>Входимость нумеруемого объекта</summary>
  private List<Guid> _parentObjectTypeGuids;
  /// <summary>Список типов связей</summary>
  private List<Guid> _relationTypeGuids;
  /// <summary>Скрипт C# нумерации объектов</summary>
  private string _scriptData;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._objectID = 0L;
    this._numerationMode = TechNumerationMode.ObjectAndRelation;
    this._relationTypeGuids = new List<Guid>();
    this._parentObjectTypeGuids = new List<Guid>();
  }

  /// <summary>Конструктор</summary>
  /// <param name="numRuleID">Идентификатор правила нумерации</param>
  public TechNumerationNode(long numRuleID)
  {
    this._numRuleID = numRuleID;
    this.InitializeData();
  }

  /// <summary>Конструктор</summary>
  public TechNumerationNode()
    : this(0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="numNode"></param>
  public TechNumerationNode(ITechNumerationNode numNode) => this.CopyFrom(numNode);

  /// <summary>Загрузка параметров</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  public virtual void Load(IDBObject obj, IUserSession session)
  {
    if (obj == null || session == null)
      return;
    this._objectID = obj.ObjectID;
    foreach (AttributeValues attributesValue in obj.GetAttributesValues(GetAttributeValuesModes.IncludeGuid))
      this.Load(attributesValue, session);
    if (this._relationTypeGuids.Count != 0)
      return;
    this._relationTypeGuids.Add(TechCardConsts.RelTypes.TechRelationGuid);
    this._relationTypeGuids.Add(TechCardConsts.RelTypes.TechThroughtTPRelationGuid);
  }

  /// <summary>Загрузка параметров</summary>
  /// <param name="attrValues"></param>
  /// <param name="session"></param>
  public virtual void Load(AttributeValues attrValues, IUserSession session)
  {
    if (attrValues == null || attrValues.Values.Length == 0)
      return;
    if (attrValues.AttributeGuid == new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"))
    {
      string str = Convert.ToString(attrValues.Values[0]);
      if (str.Equals(string.Empty) || !GuidHelper.IsGuid(str))
        return;
      this._objectTypeGuid = new Guid(str);
    }
    else if (attrValues.AttributeGuid == new Guid("cad001d0-306c-11d8-b4e9-00304f19f545"))
    {
      string str = Convert.ToString(attrValues.Values[0]);
      if (str.Equals(string.Empty) || !GuidHelper.IsGuid(str))
        return;
      this._attributeTypeGuid = new Guid(str);
    }
    else if (attrValues.AttributeGuid == new Guid("cad00149-306c-11d8-b4e9-00304f19f545"))
    {
      this._parentObjectTypeGuids.Clear();
      foreach (object obj in attrValues.Values)
      {
        if (!obj.Equals((object) DBNull.Value) && !obj.Equals((object) string.Empty) && GuidHelper.IsGuid(obj.ToString()))
          this._parentObjectTypeGuids.Add(new Guid(obj.ToString()));
      }
    }
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationModeAttrGuid)
    {
      try
      {
        this._numerationMode = attrValues.Values[0] != DBNull.Value ? (TechNumerationMode) Convert.ToInt32(attrValues.Values[0]) : TechNumerationMode.ObjectAndRelation;
      }
      catch (Exception ex)
      {
        if (ex is FormatException)
          return;
        throw;
      }
    }
    else if (attrValues.AttributeGuid == new Guid("cad0014a-306c-11d8-b4e9-00304f19f545"))
    {
      this._relationTypeGuids.Clear();
      foreach (object obj in attrValues.Values)
      {
        if (!obj.Equals((object) DBNull.Value) && !obj.Equals((object) string.Empty) && GuidHelper.IsGuid(obj.ToString()))
          this._relationTypeGuids.Add(new Guid(obj.ToString()));
      }
    }
    else
    {
      if (!(attrValues.AttributeGuid == new Guid("cad00366-306c-11d8-b4e9-00304f19f545")))
        return;
      string str = Convert.ToString(attrValues.Values[0]);
      if (string.IsNullOrEmpty(str))
        return;
      this._scriptData = str;
    }
  }

  /// <summary>Сохранение параметров</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  public void Save(IDBObject obj, IUserSession session)
  {
    if (obj == null || session == null)
      return;
    int nameId = session.IdentHelper.NameID;
    int attributeId1 = MetaDataHelper.GetAttributeID((object) "cad001a0-306c-11d8-b4e9-00304f19f545");
    int attributeId2 = MetaDataHelper.GetAttributeID((object) "cad001d0-306c-11d8-b4e9-00304f19f545");
    int attributeId3 = MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationModeAttrGuid);
    int attributeId4 = MetaDataHelper.GetAttributeID((object) "cad00149-306c-11d8-b4e9-00304f19f545");
    int attributeId5 = MetaDataHelper.GetAttributeID((object) "cad0014a-306c-11d8-b4e9-00304f19f545");
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(7);
    attributeValuesList.Add(new AttributeValues(attributeId1, (object) this._objectTypeGuid.ToString()));
    attributeValuesList.Add(new AttributeValues(attributeId2, (object) this._attributeTypeGuid.ToString()));
    object[] array = this.ParentObjectTypeGuids.Select<Guid, object>((Func<Guid, object>) (item => (object) item)).ToArray<object>();
    attributeValuesList.Add(new AttributeValues(attributeId4, array.Length != 0 ? (object) array : (object) DBNull.Value));
    string initValue = "";
    IDBObjectType objectType = session.GetObjectType(this._objectTypeGuid);
    if (objectType != null)
      initValue = objectType.ObjectTypeName;
    IDBAttributeType attributeType = session.GetAttributeType(this._attributeTypeGuid);
    if (attributeType != null)
    {
      string name = attributeType.Name;
      initValue = initValue != "" ? $"{initValue}:{name}" : name;
    }
    attributeValuesList.Add(new AttributeValues(nameId, (object) initValue));
    attributeValuesList.Add(new AttributeValues(attributeId5, (object) this.RelationTypeGuids.Select<Guid, object>((Func<Guid, object>) (item => (object) item)).ToArray<object>()));
    attributeValuesList.Add(new AttributeValues(attributeId3, (object) (int) this.NumerationMode));
    if (!string.IsNullOrEmpty(this.ScriptData))
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) "cad00366-306c-11d8-b4e9-00304f19f545"), (object) this.ScriptData));
    obj.SetAttributesValues(attributeValuesList.ToArray());
  }

  /// <summary>Копирование данных правила</summary>
  /// <param name="numNode"></param>
  public void CopyFrom(ITechNumerationNode numNode)
  {
    if (numNode == null)
      return;
    this.NumRuleID = numNode.NumRuleID;
    this._objectID = numNode.ObjectID;
    this.ObjectTypeGuid = numNode.ObjectTypeGuid;
    this.AttributeTypeGuid = numNode.AttributeTypeGuid;
    this.NumerationMode = numNode.NumerationMode;
    this._parentObjectTypeGuids = numNode.ParentObjectTypeGuids != null ? new List<Guid>((IEnumerable<Guid>) numNode.ParentObjectTypeGuids) : (List<Guid>) null;
    this._relationTypeGuids = numNode.RelationTypeGuids != null ? new List<Guid>((IEnumerable<Guid>) numNode.RelationTypeGuids) : (List<Guid>) null;
    this._scriptData = numNode.ScriptData;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public object Clone() => this.MemberwiseClone();

  /// <summary>Идентификатор объекта</summary>
  public long ObjectID => this._objectID;

  /// <summary>Идентификатор правила нумерации</summary>
  public long NumRuleID
  {
    get => this._numRuleID;
    set => this._numRuleID = value;
  }

  /// <summary>Тип нумеруемого объекта</summary>
  public Guid ObjectTypeGuid
  {
    get => this._objectTypeGuid;
    set => this._objectTypeGuid = value;
  }

  /// <summary>Нумеруемый атрибут</summary>
  public Guid AttributeTypeGuid
  {
    get => this._attributeTypeGuid;
    set => this._attributeTypeGuid = value;
  }

  /// <summary>Режим нумерации</summary>
  public TechNumerationMode NumerationMode
  {
    get => this._numerationMode;
    set => this._numerationMode = value;
  }

  /// <summary>Входимость нумеруемого объекта</summary>
  public List<Guid> ParentObjectTypeGuids => this._parentObjectTypeGuids;

  /// <summary>
  /// 
  /// </summary>
  public List<Guid> RelationTypeGuids => this._relationTypeGuids;

  /// <summary>Скрипт C# нумерации объектов</summary>
  public string ScriptData
  {
    get => this._scriptData;
    set => this._scriptData = value;
  }
}
