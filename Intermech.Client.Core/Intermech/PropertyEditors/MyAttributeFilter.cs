
// Type: Intermech.PropertyEditors.MyAttributeFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>Класс-фильтр типов атрибутов в форме SelectorForm</summary>
public class MyAttributeFilter : ISelectorFilter
{
  /// <summary>Коллекция ID запрещённых к выбору атрибутов</summary>
  public ArrayList ExcludedAttributes = new ArrayList();
  /// <summary>
  /// Надо ли использовать указанный тип для отбора атрибутов строго определённого
  /// типа или руководствоваться только списком запрещённых атрибутов для фильтрации
  /// </summary>
  public bool UseAttrType = true;
  /// <summary>Тип атрибута для фильтрации. По умолчанию - строка</summary>
  public List<FieldTypes> AttrType = new List<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[1]
  {
    FieldTypes.ftString
  });
  /// <summary>
  /// Является ли атрибут AttrType системным атрибутом (ftSystem)
  /// </summary>
  public bool IsSystemAttr;
  /// <summary>
  /// Идёт ли отбор по атрибутам, связанным с пользователями
  /// </summary>
  public bool IsUserAttr;

  /// <summary>
  /// Создать фильтр атрибутов. По умолчанию отбор идёт по типу FieldTypes.ftString
  /// </summary>
  public MyAttributeFilter()
  {
  }

  /// <summary>
  /// Создать фильтр атрибутов по указанному типу AnAttrType
  /// </summary>
  /// <param name="AnAttrTypes">Допустимые типы атрибутов</param>
  /// <param name="AnIsSystemAttr">Является ли атрибут данного типа системным</param>
  /// <param name="ExcludeAttrs">Список ID атрибутов, которые нельзя отображать в дереве</param>
  public MyAttributeFilter(
    List<FieldTypes> AnAttrTypes,
    bool AnIsSystemAttr,
    object[] ExcludeAttrs)
  {
    this.ExcludedAttributes.Add((object) MyAttributeHelper.GetAttrID("cad0002e-306c-11d8-b4e9-00304f19f545"));
    if (ExcludeAttrs != null && ExcludeAttrs.Length != 0)
    {
      for (int index = 0; index < ExcludeAttrs.Length; ++index)
        this.ExcludedAttributes.Add(ExcludeAttrs[index]);
    }
    this.UseAttrType = AnAttrTypes != null && AnAttrTypes.Count > 0;
    if (this.UseAttrType)
    {
      this.AttrType = AnAttrTypes ?? new List<FieldTypes>();
      this.IsSystemAttr = AnIsSystemAttr;
    }
    else
      this.UseAttrType = false;
  }

  public bool IsInFilter(int category, object id) => !this.IsInFilterInternal(category, id);

  private bool IsInFilterInternal(int category, object id)
  {
    switch (category)
    {
      case 3:
        if (id != null && this.ExcludedAttributes.IndexOf(id) < 0)
        {
          string AttrName = "";
          string AttrGUID = "";
          FieldTypes AttrType = FieldTypes.ftUnknown;
          bool IsSystemType = false;
          if (!MyAttributeHelper.GetAttrInfo(Convert.ToInt32(id), ref AttrName, ref AttrGUID, ref AttrType, ref IsSystemType) || AttributeTypeHelper.IsSystemAttributeTypeID(Convert.ToInt32(id)) && ObligatoryObjectAttributesHelper.IsObligatoryAttribute(AttrName) && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) Convert.ToInt32(id)) != AttributeSourceTypes.Object || AttrType == FieldTypes.ftSystem && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) Convert.ToInt32(id)) != AttributeSourceTypes.Object)
            return false;
          if (!this.UseAttrType)
            return MyAttributeHelper.IsValidType(AttrType);
          if (this.IsUserAttr)
            return MyAttributeHelper.IsUserIDType(Convert.ToInt32(id));
          for (int index = 0; index < this.AttrType.Count; ++index)
          {
            if (MyAttributeHelper.IsComparable(this.AttrType[index], AttrType))
              return true;
          }
          return false;
        }
        break;
      case 12:
        return true;
    }
    return false;
  }
}
