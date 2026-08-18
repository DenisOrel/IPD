// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechDictDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>TechCard dictionarty descriptor</summary>
public class TechDictDescriptor : DictDescriptor
{
  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public TechDictDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип (можно указать общий тип объектов)</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public TechDictDescriptor(
    int categoryID,
    int typeID,
    string caption,
    Dictionary<int, List<long>> objectIDs)
    : base(categoryID, typeID, caption, objectIDs)
  {
  }

  /// <summary>Отразить указанную колонку в идентификатор атрибута</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор атрибута</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    object field = base.MapColumnToField(column);
    if (field != null)
      return field;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }
}
