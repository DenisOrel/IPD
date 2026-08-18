
// Type: Intermech.Navigator.DBObjects.VirtualGrouingObjectsNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Описание виртуального узла "Найденные группирующие объекты"
/// </summary>
public class VirtualGrouingObjectsNodeID : INodeID
{
  /// <summary>Заголовок узла</summary>
  protected string _caption;
  /// <summary>Печенюга</summary>
  private object cookie;

  /// <summary>Создать экземпляр класса</summary>
  public VirtualGrouingObjectsNodeID()
  {
    this._caption = LocalizationHolder.rm.GetString("Client.Core_333");
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок узла</param>
  public VirtualGrouingObjectsNodeID(string caption) => this._caption = caption;

  /// <summary>Категория</summary>
  public int CategoryID => Intermech.Navigator.Consts.CategoryGroupingObjectsNode;

  /// <summary>Тип</summary>
  public int TypeID => 0;

  /// <summary>Печенюга</summary>
  public object Cookie
  {
    get => this.cookie;
    set => this.cookie = value;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => obj is VirtualGrouingObjectsNodeID;

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => base.GetHashCode();
}
