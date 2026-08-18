
// Type: Intermech.Client.Core.CompositionView.CompositionCacheServices
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Composition treeview's type cache service</summary>
internal class CompositionCacheServices
{
  /// <summary>Содержимое кеша</summary>
  private Dictionary<CompositionCacheServices.TypeCacheKey, CompositionCacheServices.TypeCacheRec> _typeCache;

  /// <summary>Конструктор</summary>
  public CompositionCacheServices()
  {
    this._typeCache = new Dictionary<CompositionCacheServices.TypeCacheKey, CompositionCacheServices.TypeCacheRec>();
  }

  /// <summary>Получение записи из кеша по условиям</summary>
  /// <param name="guid1"></param>
  /// <param name="guid2"></param>
  /// <param name="createIfNotExists"></param>
  /// <returns></returns>
  public CompositionCacheServices.TypeCacheRec GetTypeCacheRec(
    Guid guid1,
    Guid guid2,
    bool createIfNotExists)
  {
    CompositionCacheServices.TypeCacheRec typeCacheRec = (CompositionCacheServices.TypeCacheRec) null;
    CompositionCacheServices.TypeCacheKey key = new CompositionCacheServices.TypeCacheKey(guid1, guid2);
    if (!this._typeCache.TryGetValue(key, out typeCacheRec) & createIfNotExists)
    {
      typeCacheRec = new CompositionCacheServices.TypeCacheRec();
      this._typeCache.Add(key, typeCacheRec);
    }
    return typeCacheRec;
  }

  /// <summary>Режим отображения дерева</summary>
  internal enum TreeViewDrawingType
  {
    /// <summary>Нет определенного дерева</summary>
    None,
    /// <summary>Работа с потомками текущего объекта</summary>
    ChildrenTypes,
    /// <summary>Работа с родителем текущего объекта</summary>
    ParentTypes,
    /// <summary>Юзерское дерево</summary>
    UserMode,
  }

  /// <summary>Type cache's key</summary>
  private class TypeCacheKey
  {
    private Guid _guid1 = Guid.Empty;
    private Guid _guid2 = Guid.Empty;

    /// <summary>Конструктор</summary>
    /// <param name="guid1"></param>
    /// <param name="guid2"></param>
    public TypeCacheKey(Guid guid1, Guid guid2)
    {
      this._guid1 = guid1;
      this._guid2 = guid2;
    }

    /// <summary>Получение пустого значения</summary>
    public static CompositionCacheServices.TypeCacheKey Empty
    {
      get => new CompositionCacheServices.TypeCacheKey(Guid.Empty, Guid.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => $"{this._guid1} : {this._guid2}".GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      if (!(obj is CompositionCacheServices.TypeCacheKey))
        return base.Equals(obj);
      CompositionCacheServices.TypeCacheKey typeCacheKey = obj as CompositionCacheServices.TypeCacheKey;
      return this._guid1.Equals(typeCacheKey._guid1) && this._guid2.Equals(typeCacheKey._guid2);
    }
  }

  /// <summary>Type cache's record</summary>
  internal class TypeCacheRec
  {
    /// <summary>Режим отображения дерева</summary>
    public CompositionCacheServices.TreeViewDrawingType DrawingType;
    /// <summary>Юзерский контрол</summary>
    public CVButtonBase UserButton;
    /// <summary>Дерево навигатора</summary>
    public NavigatorTreeView TreeView;
  }
}
