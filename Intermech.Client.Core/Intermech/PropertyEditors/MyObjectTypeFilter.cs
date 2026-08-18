
// Type: Intermech.PropertyEditors.MyObjectTypeFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Класс-фильтр типов объектов в форме SelectorForm</summary>
public class MyObjectTypeFilter : ISelectorFilter
{
  /// <summary>Наличие данного атрибута в типе объекта обязательно</summary>
  public int AttrID;

  /// <summary>Создать фильтр типов объектов</summary>
  public MyObjectTypeFilter()
  {
  }

  /// <summary>
  /// Создать фильтр типов объектов по наличию в них указанного атрибута AnAttrID
  /// </summary>
  /// <param name="AnAttrID">ID атрибута, наличие которого обязательно в типе объектов</param>
  public MyObjectTypeFilter(int AnAttrID) => this.AttrID = AnAttrID;

  public bool IsInFilter(int category, object id) => category == 4 && id != null;
}
