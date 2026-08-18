
// Type: Intermech.Navigator.DBObjects.CustomObjectDescriptor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Drawing;
using System.Reflection;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор ноды объекта IPS, позволяющий указать тип создаваемой ноды и в конструкторе указать заголовок объекта и иконки для его отображения</summary>
/// <typeparam name="TNodeType">Тип ноды, который будет создаваться</typeparam>
public class CustomObjectDescriptor<TNodeType> : 
  Descriptor,
  INodesFactory,
  IDescriptor,
  INodeItems,
  IPersistable,
  ICloneable,
  IDescriptorElementStatuses,
  IContextAware
  where TNodeType : INode
{
  /// <summary>Свойство XML для записи заголовка ноды</summary>
  private const string PropCaption = "ObjCaption";
  /// <summary>Свойство XML для записи основной иконки ноды</summary>
  private const string PropMainIcon = "ObjMainIcon";
  /// <summary>Свойство XML для записи предварительной иконки ноды</summary>
  private const string PropPrefixIcon = "ObjPrefixIcon";

  /// <summary>Инициализация</summary>
  private void Init([CanBeNull] string caption, [CanBeNull] Image prefixIcon, [CanBeNull] Image mainIcon)
  {
    this.Caption = caption;
    this.PrefixIcon = prefixIcon;
    this.MainIcon = mainIcon;
    this.NodesFactory = (INodesFactory) this;
  }

  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objGuid">Guid версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public CustomObjectDescriptor(
    long objID,
    Guid objGuid,
    ObjectFiltrationState state,
    [CanBeNull] string caption = null,
    [CanBeNull] Image prefixIcon = null,
    [CanBeNull] Image mainIcon = null)
    : base(objID, objGuid, state)
  {
    this.Init(caption, prefixIcon, mainIcon);
  }

  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public CustomObjectDescriptor(long objID, [CanBeNull] string caption = null, [CanBeNull] Image prefixIcon = null, [CanBeNull] Image mainIcon = null)
    : base(objID)
  {
    this.Init(caption, prefixIcon, mainIcon);
  }

  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public CustomObjectDescriptor(
    long objID,
    ObjectFiltrationState state,
    [CanBeNull] string caption = null,
    [CanBeNull] Image prefixIcon = null,
    [CanBeNull] Image mainIcon = null)
    : base(objID, state)
  {
    this.Init(caption, prefixIcon, mainIcon);
  }

  /// <summary>Заголовок ноды</summary>
  [CanBeNull]
  public string Caption { get; set; }

  /// <summary>Специальная основная предварительная иконка ноды</summary>
  [CanBeNull]
  public Image PrefixIcon { get; set; }

  /// <summary>Специальная основная иконка ноды</summary>
  [CanBeNull]
  public Image MainIcon { get; set; }

  /// <summary>Формирует сериализованное представление объекта.</summary>
  /// <param name="state">Контейнер значений для хранения сериализованного представления объекта</param>
  public override void GetObjectData([NotNull] PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjCaption", (object) this.Caption);
    state.AddValue("ObjMainIcon", (object) this.MainIcon);
    state.AddValue("ObjPrefixIcon", (object) this.PrefixIcon);
  }

  /// <summary>Создать точную копию экземпляра объекта</summary>
  /// <returns>Точная копия экземпляра объекта</returns>
  public new object Clone()
  {
    return (object) new CustomObjectDescriptor<TNodeType>(this._objID, this._objGuid, this._state, this.Caption, this.MainIcon, this.PrefixIcon);
  }

  /// <summary>Отразить указанную колонку в идентификатор атрибута</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор атрибута</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return !string.IsNullOrEmpty(this.Caption) && (column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) || column.ID is string id && id.Equals("F_CAPTION")) ? (object) ObligatoryObjectAttributes.CAPTION : base.MapColumnToField(column);
  }

  /// <summary>Возвращает значения полей для объекта, описываемого унифицированным дескриптором. Метод может возвращать null, если объект не
  /// доступен или не существует.</summary>
  /// <param name="nodeID">Унифицированный дескриптор.</param>
  /// <param name="fields">Массив идентификаторов полей данных, значения которых должны быть получены в результате выполнения запроса.</param>
  /// <returns>An array of object</returns>
  public override object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = base.GetRecordValues(nodeID, fields);
    if (!string.IsNullOrEmpty(this.Caption))
    {
      for (int index = 0; index < recordValues.Length; ++index)
      {
        if (fields[index].Equals((object) ObligatoryObjectAttributes.CAPTION))
          recordValues[index] = (object) this.Caption;
      }
    }
    return recordValues;
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    ConstructorInfo constructor = typeof (TNodeType).GetConstructor(new Type[4]
    {
      typeof (int),
      typeof (long),
      typeof (Image),
      typeof (Image)
    });
    Intermech.Diagnostics.Check.NotNull<ConstructorInfo>(constructor, $"{typeof (TNodeType)} must have public constructor with params (int, long, Image, Image) to be used with {typeof (CustomObjectDescriptor<TNodeType>)} class");
    return (INode) constructor.Invoke(new object[4]
    {
      (object) ((NodeID) nodeID).ObjectTypeID,
      (object) ((NodeID) nodeID).ObjectID,
      (object) this.PrefixIcon,
      (object) this.MainIcon
    });
  }

  /// <summary>Возвращает элемент из пространства навигации указанной категории и типа.</summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  public INode GetNode(int categoryID, int typeID) => throw new NotImplementedException();

  /// <summary>Возвращает элемент из пространства навигации указанной категории и типа.</summary>
  /// <param name="nodeID">Унифицированный идентификатор элемента.</param>
  /// <param name="args">Массив параметров, которые будут переданы конструктору элемента.</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  public virtual INode GetNode(INodeID nodeID, params object[] args)
  {
    ConstructorInfo constructor = typeof (TNodeType).GetConstructor(new Type[2]
    {
      typeof (int),
      typeof (long)
    });
    Intermech.Diagnostics.Check.NotNull<ConstructorInfo>(constructor, $"{typeof (TNodeType)} must have public constructor with params (int, long) to be used with {typeof (CustomObjectDescriptor<TNodeType>)} class");
    return (INode) constructor.Invoke(new object[4]
    {
      (object) ((NodeID) nodeID).ObjectTypeID,
      (object) ((NodeID) nodeID).ObjectID,
      (object) this.PrefixIcon,
      (object) this.MainIcon
    });
  }
}
