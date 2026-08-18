
// Type: Intermech.PropertyEditors.MyRuleTypeFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Класс-фильтр типов объектов в форме SelectorForm</summary>
public class MyRuleTypeFilter : ISelectorFilter
{
  /// <summary>Фильтруемые типы объектов (ID типов)</summary>
  public ArrayList PossibleTypes = new ArrayList();

  /// <summary>Создать фильтр типов объектов</summary>
  public MyRuleTypeFilter()
  {
  }

  /// <summary>Создать список-фильтр с указанными допустимыми типами</summary>
  /// <param name="APossibleTypes">Список допустимых типов объектов</param>
  public MyRuleTypeFilter(ArrayList APossibleTypes)
  {
    this.PossibleTypes.Clear();
    if (APossibleTypes.Count <= 0)
      return;
    for (int index = 0; index < APossibleTypes.Count; ++index)
      this.PossibleTypes.Add(APossibleTypes[index]);
  }

  /// <summary>
  /// Проверить, находится указанный тип объектов в списке допустимых значений
  /// </summary>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public bool IsInFilter(int category, object id)
  {
    return category == 4 && id != null && this.PossibleTypes.IndexOf(id) >= 0;
  }
}
