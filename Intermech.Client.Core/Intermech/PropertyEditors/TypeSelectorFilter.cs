
// Type: Intermech.PropertyEditors.TypeSelectorFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator;
using System;


namespace Intermech.PropertyEditors;

/// <summary>Фильтр диалога выбора типов</summary>
public class TypeSelectorFilter : ISelectorFilter
{
  /// <summary>Допустимые типы</summary>
  public int[] AllowableTypes;
  /// <summary>Допускать дочерние (наследники) типы</summary>
  public bool AllowChildTypes = true;
  /// <summary>Допускать абстрактные типы объектов (данный флажок имеет наивысший приоритет)</summary>
  public bool AllowAbstractTypes = true;

  /// <summary>Конструктор</summary>
  /// <param name="allowableTypes">Допустимые типы</param>
  /// <param name="allowChildTypes">Допускать дочерние (наследники) типы</param>
  /// <param name="allowAbstractTypes">Допускать абстрактные типы объектов (данный флажок имеет наивысший приоритет)</param>
  public TypeSelectorFilter(int[] allowableTypes, bool allowChildTypes, bool allowAbstractTypes)
  {
    this.AllowableTypes = allowableTypes;
    this.AllowChildTypes = allowChildTypes;
    this.AllowAbstractTypes = allowAbstractTypes;
  }

  /// <summary>Прошел фильтр</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <returns></returns>
  public bool IsInFilter(int category, object id)
  {
    switch (category)
    {
      case 3:
        int num1 = (int?) id ?? 0;
        if (id != null && this.AllowableTypes != null && this.AllowableTypes.Length != 0)
          return Array.IndexOf<int>(this.AllowableTypes, num1) != -1;
        break;
      case 4:
        int num2 = id != null ? (int) id : -1;
        if (!this.AllowAbstractTypes)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(num2);
          if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract)
            return false;
        }
        if (id != null && this.AllowableTypes != null && this.AllowableTypes.Length != 0)
        {
          for (int index = 0; index < this.AllowableTypes.Length; ++index)
          {
            if (num2 == this.AllowableTypes[index] || TypeSelectorFilter.IsParentObjectType(num2, this.AllowableTypes[index]) || this.AllowChildTypes && TypeSelectorFilter.IsParentObjectType(this.AllowableTypes[index], num2))
              return true;
          }
          break;
        }
        break;
      case 6:
        int num3 = id != null ? (int) id : -1;
        if (id != null && this.AllowableTypes != null && this.AllowableTypes.Length != 0)
        {
          for (int index = 0; index < this.AllowableTypes.Length; ++index)
          {
            if (num3 == this.AllowableTypes[index])
              return true;
          }
          break;
        }
        break;
    }
    return false;
  }

  public static bool IsParentObjectType(int parent, int child)
  {
    if (parent == child)
      return true;
    IObjectTypesInheritanceCache inheritanceCache = (IObjectTypesInheritanceCache) CacheManager.Cache("ObjectTypeInheritanceCache");
    while (child > -1)
    {
      child = inheritanceCache.GetParentType(child);
      if (child == parent && child > -1)
        return true;
    }
    return false;
  }
}
