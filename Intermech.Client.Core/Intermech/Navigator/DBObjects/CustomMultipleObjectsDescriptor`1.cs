
// Type: Intermech.Navigator.DBObjects.CustomMultipleObjectsDescriptor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор для списка нескольких объектов, создающий указанный тип ноды.
/// Наследовать от CustomNode.Descriptor пришлось дабы его нода она поддерживала
/// сервисы фильтрации, иначе с тулбаров пропадают соотв. команды</summary>
public class CustomMultipleObjectsDescriptor<TNodeType> : 
  Intermech.Navigator.CustomNode.Descriptor,
  IDescriptor,
  INodeItems,
  IPersistable
  where TNodeType : INode
{
  /// <summary>Создать дескриптор составного узла "Навигатора"</summary>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная "несколько объектов"</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  public CustomMultipleObjectsDescriptor(
    [NotNull] string caption,
    [CanBeNull] IEnumerable<IDescriptor> descriptors,
    [CanBeNull] Image mainIcon = null,
    [CanBeNull] Image prefixIcon = null)
    : base(Intermech.Navigator.Consts.CategoryMultipleObjectsNode, 0, Intermech.Diagnostics.Check.ArgumentNotNull<string>(caption, nameof (caption)), new DescriptorCollection(descriptors))
  {
    this.MainIcon = mainIcon;
    this.PrefixIcon = prefixIcon;
  }

  /// <summary>Основная иконка ноды</summary>
  [CanBeNull]
  public Image MainIcon { get; set; }

  /// <summary>Иконка-префикс ноды</summary>
  [CanBeNull]
  public Image PrefixIcon { get; set; }

  /// <summary>Перечисление дескрипторов входящих объектов</summary>
  [NotNull]
  public IEnumerable<Descriptor> DbObjectDescriptors => this._descriptors.OfType<Descriptor>();

  /// <summary>Перечисление идентификаторов версий входящих объектов</summary>
  [NotNull]
  public IEnumerable<long> DbObjectVersionIDs
  {
    get
    {
      return this.DbObjectDescriptors.Select<Descriptor, long>((Func<Descriptor, long>) (dbObjectDescriptor => dbObjectDescriptor.ObjectID));
    }
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) typeof (TNodeType).GetConstructor(new Type[3]
    {
      typeof (DescriptorCollection),
      typeof (Image),
      typeof (Image)
    }).Invoke(new object[3]
    {
      (object) this._descriptors,
      (object) this.MainIcon,
      (object) this.PrefixIcon
    });
  }

  /// <summary>Отразить указанную колонку в идентификатор атрибута</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор атрибута</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return !string.IsNullOrEmpty(this._caption) && (column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) || column.ID is string id && id.Equals("F_CAPTION", StringComparison.OrdinalIgnoreCase)) ? (object) ObligatoryObjectAttributes.CAPTION : base.MapColumnToField(column);
  }

  /// <summary>Возвращает значения полей для объекта, описываемого унифицированным дескриптором. Метод может возвращать null, если объект не
  /// доступен или не существует.</summary>
  /// <param name="nodeID">Унифицированный дескриптор.</param>
  /// <param name="fields">Массив идентификаторов полей данных, значения которых должны быть получены в результате выполнения запроса.</param>
  /// <returns>An array of object</returns>
  public override object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = base.GetRecordValues(nodeID, fields);
    if (!string.IsNullOrEmpty(this._caption))
    {
      for (int index = 0; index < recordValues.Length; ++index)
      {
        if (fields[index].Equals((object) ObligatoryObjectAttributes.CAPTION))
          recordValues[index] = (object) this._caption;
      }
    }
    return recordValues;
  }
}
