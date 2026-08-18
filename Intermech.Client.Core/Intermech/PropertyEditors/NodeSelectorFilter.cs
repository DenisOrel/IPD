
// Type: Intermech.PropertyEditors.NodeSelectorFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// Фильтр используется для проверки, можно ли выбирать указанный узел в окне
/// </summary>
public class NodeSelectorFilter : INodeSelectorFilter
{
  /// <summary>
  /// 
  /// </summary>
  private readonly int[] _categories;

  /// <summary>
  /// 
  /// </summary>
  public NodeSelectorFilter()
    : this(new int[1]{ 4 })
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="categories"></param>
  public NodeSelectorFilter(int[] categories)
  {
    this._categories = categories != null ? categories : throw new ArgumentNullException(nameof (categories));
  }

  /// <summary>Можно ли выбирать указанный узел</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <param name="errorMessage">Если значение не равно String.Empty, то оно будет отображено в статусной строке окна</param>
  /// <returns>true, если выбор узла разрешён</returns>
  public bool CanSelectNode(int category, object id, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (Array.IndexOf<int>(this._categories, category) == -1)
      return false;
    if (category == 4)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(id != null ? (int) id : -1);
      if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract)
      {
        errorMessage = "Абстрактный тип объекта выбирать нельзя";
        return false;
      }
    }
    return true;
  }
}
