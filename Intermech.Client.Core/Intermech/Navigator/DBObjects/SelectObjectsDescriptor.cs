
// Type: Intermech.Navigator.DBObjects.SelectObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор для узла, описывающего недавние объекты</summary>
public sealed class SelectObjectsDescriptor : ListDescriptor
{
  /// <summary>Создает дескриптор.</summary>
  public SelectObjectsDescriptor(string caption, List<long> objectIDs)
    : base(Intermech.Navigator.Consts.CategorySelectObjectsNode, 0, caption, (IList) objectIDs)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state">Состояние</param>
  protected SelectObjectsDescriptor(PersistentState state)
    : base(state)
  {
  }

  public override INode GetChild(INodeID nodeID) => (INode) new SelectObjectsNode(this._objectIDs);

  /// <summary>
  /// Отобразить колонку "Навигатора" на идентификатор или название атрибута
  /// </summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор или название атрибута, либо null</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    object field = base.MapColumnToField(column);
    if (field != null)
      return field;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }
}
