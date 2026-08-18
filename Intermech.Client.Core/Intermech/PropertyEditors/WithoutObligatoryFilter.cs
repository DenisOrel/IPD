
// Type: Intermech.PropertyEditors.WithoutObligatoryFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>
/// Фильт для отображения выбора атрибутов.
/// Исключаются все обязательные атрибуты кроме обязательных атрибутов объекта и обязательных атрибутов связи
/// </summary>
public class WithoutObligatoryFilter : ISelectorFilter
{
  private List<AttributeSourceTypes> _types;

  public WithoutObligatoryFilter(params AttributeSourceTypes[] types)
  {
    if (types != null && types.Length != 0)
    {
      this._types = new List<AttributeSourceTypes>(types.Length);
      for (int index = 0; index < types.Length; ++index)
        this._types.Add(types[index]);
    }
    else
      this._types = new List<AttributeSourceTypes>(0);
  }

  public bool IsInFilter(int category, object id)
  {
    if (category != 3)
      return true;
    int attribute = (int) id;
    return attribute < 0 && !this._types.Contains(ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attribute));
  }
}
