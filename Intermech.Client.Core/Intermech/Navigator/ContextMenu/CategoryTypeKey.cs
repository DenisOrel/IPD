
// Type: Intermech.Navigator.ContextMenu.CategoryTypeKey
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует ключ для словаря кластеров элементов навигации.
/// </summary>
internal class CategoryTypeKey
{
  private int _categoryID;
  private int _typeID;

  /// <summary>
  /// Создает новый ключ, позволяя указать категорию и тип элементов навигации,
  /// входящих в кластер.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  public CategoryTypeKey(int categoryID, int typeID)
  {
    this._categoryID = categoryID;
    this._typeID = typeID;
  }

  /// <summary>
  /// Возвращает идентификатор категории элементов навигации,
  /// входящих в кластер.
  /// </summary>
  public int CategoryID => this._categoryID;

  /// <summary>
  /// Возвращает идентификатор типа элементов навигации,
  /// входящих в кластер.
  /// </summary>
  public int TypeID => this._typeID;

  public override bool Equals(object obj)
  {
    CategoryTypeKey categoryTypeKey = (CategoryTypeKey) obj;
    return categoryTypeKey != null && this._categoryID == categoryTypeKey._categoryID && this._typeID == categoryTypeKey._typeID;
  }

  public override int GetHashCode()
  {
    return this._categoryID.GetHashCode() << 16 /*0x10*/ ^ this._typeID.GetHashCode();
  }
}
