// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.ObjectSearchCondition
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Условие поиска объекта</summary>
public sealed class ObjectSearchCondition
{
  /// <summary>
  /// Список типов атрибутов и их значений для поиска.
  /// Объединяются с помощью логической операции "И"
  /// </summary>
  public List<Tuple<Guid, object>> Attributes = new List<Tuple<Guid, object>>();

  /// <summary>Создать пустое условие</summary>
  public ObjectSearchCondition()
  {
  }

  /// <summary>Создать условие, заполнить его значением</summary>
  /// <param name="attrType">Тип атрибута</param>
  /// <param name="attrValue">Значение атрибута</param>
  public ObjectSearchCondition(Guid attrType, object attrValue)
  {
    this.Attributes.Add(new Tuple<Guid, object>(attrType, attrValue));
  }

  /// <summary>Создать условие, заполнить его значениями</summary>
  /// <param name="attributes">Список типов атрибутов и их значений</param>
  public ObjectSearchCondition(params Tuple<Guid, object>[] attributes)
  {
    this.Attributes.AddRange((IEnumerable<Tuple<Guid, object>>) attributes);
  }

  /// <summary>
  /// Создать условия для запроса в "ядро" - поиск атрибута с указанным значением
  /// в которых требуется искать указанное значение
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>Условия для поиска или пустой список</returns>
  public List<ConditionStructure> GetAttributeCondition(IUserSession session)
  {
    List<ConditionStructure> attributeCondition = new List<ConditionStructure>();
    if (this.Attributes == null || this.Attributes.Count == 0 || session == null)
      return attributeCondition;
    foreach (Tuple<Guid, object> attribute in this.Attributes)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute.Item1);
      if (attributeType != null && (attributeType.Options & AttributeOptions.Identifier) == AttributeOptions.Identifier)
        attributeCondition.Add(new ConditionStructure(attribute.Item1, RelationalOperators.Equal, attribute.Item2, LogicalOperators.AND, 0));
    }
    return attributeCondition;
  }
}
