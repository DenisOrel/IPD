
// Type: Intermech.Navigator.ArticlesList.ArticlesListDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System.Collections.Generic;


namespace Intermech.Navigator.ArticlesList;

public class ArticlesListDescriptor : DictDescriptor
{
  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public ArticlesListDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="objects">Список идентификаторов объектов</param>
  public ArticlesListDescriptor(Dictionary<int, List<long>> objects, int objectTypeID)
    : base(Intermech.Navigator.Consts.CategoryVersionsObjectNode, objectTypeID, "Исполнения", objects)
  {
    this._expandNodes = false;
  }

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

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ArticlesListNode(this._objectIDs, this._expandNodes);
  }

  public void SetArticles(List<long> objectIDs, int objectTypeID)
  {
    if (objectIDs != null)
      this._objectIDs = new Dictionary<int, List<long>>()
      {
        {
          objectTypeID,
          objectIDs
        }
      };
    else
      this._objectIDs = (Dictionary<int, List<long>>) null;
    this._typeID = objectTypeID;
  }
}
