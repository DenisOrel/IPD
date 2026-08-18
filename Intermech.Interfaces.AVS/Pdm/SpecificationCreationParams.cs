// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SpecificationCreationParams
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Параметры, с которыми начинает работать форма "Создание новой спецификации"
/// </summary>
[Serializable]
public class SpecificationCreationParams
{
  /// <summary>Режим работы формы</summary>
  public SpecificationCreationMode Mode;
  public ObjectsClassifyType ClassifyType;
  /// <summary>Путь к сканированному файлу</summary>
  public string ScanFile;
  /// <summary>
  /// Идентификатор спецификации-прототипа (на её основе создаётся новая спецификация)
  /// </summary>
  public long SpecPrototypeID;
  /// <summary>Тип изделия прототипа на которую выпущена прототип СП</summary>
  public int PrototypeProductType = -1;
  /// <summary>
  /// Идентификатор(ы) старого специфицируемого объекта (исполнений), с которым работает спецификация-прототип
  /// </summary>
  public List<long> ProductsIDs = new List<long>(1);
  /// <summary>Является ли указанная новая спецификация заготовкой</summary>
  public bool IsBlank = true;
  /// <summary>
  /// Идентификаторы типов связей, которые требуется создать
  /// </summary>
  public int[] RelationTypeIDs;
  /// <summary>
  /// Идентификаторы родительских объектов, с которыми требуется создавать связи указанных типов
  /// </summary>
  public long[] RelatedObjectIDs;
  /// <summary>
  /// Идентификатор(ы) нового специфицируемого объекта (исполнений) для новой спецификации
  /// </summary>
  public List<long> NewSpecArticleIDs = new List<long>();
  /// <summary>Идентификаторы новых объектов созданных автоматом</summary>
  public List<long> NewObjectIDs = new List<long>();
  /// <summary>Идентификатор найденного изделия с таким же обозначением как заданное в диалоге</summary>
  public long OldObjectID = -1;
  /// <summary>
  /// Список идентификаторов версий новых объектов (можно рассылать в "Навигаторе" с помощью службы уведомлений)
  /// </summary>
  public List<long> NewObjects = new List<long>();
  /// <summary>
  /// Список идентификаторов новых связей (можно рассылать в "Навигаторе" с помощью службы уведомлений)
  /// </summary>
  public List<long> NewRelations = new List<long>();
  /// <summary>
  /// Список идентификаторов родительских объектов новых связей
  /// </summary>
  public List<long> NewRelationsProjIDs = new List<long>();
  /// <summary>Список идентификаторов типов новых связей</summary>
  public List<int> NewRelationsTypeIDs = new List<int>();
  /// <summary>Требуется ли открывать спецификацию в редакторе AVS</summary>
  public bool openInEditor;

  /// <summary>Идентификатор новой спецификации</summary>
  public long NewSpecID { get; private set; }

  /// <summary>Guid новой спецификации</summary>
  public Guid NewSpecObjectGuid { get; private set; } = Guid.Empty;

  /// <summary>Идентификатор типа новой спецификации</summary>
  public int NewSpecObjectType { get; private set; } = -1;

  public void SetNewSpecObjectInfo(IDBObject dbObject)
  {
    if (dbObject != null)
    {
      this.NewSpecID = dbObject.ObjectID;
      this.NewSpecObjectType = dbObject.ObjectType;
      this.NewSpecObjectGuid = dbObject.ObjectGUID;
    }
    else
    {
      this.NewSpecID = 0L;
      this.NewSpecObjectType = -1;
      this.NewSpecObjectGuid = Guid.Empty;
    }
  }

  /// <summary>
  /// Прочие значения атрибутов классификатора (кроме обозначение, наименования)
  /// </summary>
  public List<AttributeValues> OtherClassificationAttrValues { get; set; }

  /// <summary>ID типа создаваемого объекта</summary>
  public int ObjectTypeId { get; set; } = -1;

  /// <summary>GUID типа создаваемого объекта</summary>
  public Guid ObjectTypeGuid
  {
    get
    {
      return this.ObjectTypeId == -1 ? Guid.Empty : MetaDataHelper.GetObjectTypeGuid(this.ObjectTypeId);
    }
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="specTemplateID">Идентификатор объекта-прототипа (на его основе создаётся новая спецификация)</param>
  public SpecificationCreationParams(long specTemplateID)
    : this(specTemplateID, (List<long>) null, SpecificationCreationMode.CreateBySpcTemplate)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="mode">В каком режиме открывается форма "Создание новой спецификации"</param>
  /// <param name="specTemplateID">Идентификатор объекта-прототипа (на его основе создаётся новая спецификация)</param>
  public SpecificationCreationParams(long specTemplateID, SpecificationCreationMode mode)
    : this(specTemplateID, (List<long>) null, mode)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="specTemplateID">Идентификатор объекта-прототипа спецификации (на его основе создаётся новая спецификация)</param>
  /// <param name="productsIDs">Идентификатор(ы) старого специфицируемого объекта (исполнений)</param>
  /// <param name="mode">В каком режиме открывается форма "Создание новой спецификации"</param>
  public SpecificationCreationParams(
    long specTemplateID,
    List<long> productsIDs,
    SpecificationCreationMode mode)
  {
    this.SpecPrototypeID = specTemplateID;
    this.ProductsIDs = productsIDs ?? this.ProductsIDs;
    this.Mode = mode;
  }
}
