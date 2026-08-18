
// Type: Intermech.Navigator.DBObjects.VirtualObjectNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Описание виртуального узла "Объект не существовал на указанную дату"
/// </summary>
public class VirtualObjectNodeID : INodeID
{
  /// <summary>Заголовок узла</summary>
  protected string _caption;
  /// <summary>Печенюга</summary>
  private object cookie;

  /// <summary>Создать экземпляр класса</summary>
  public VirtualObjectNodeID()
  {
    this._caption = LocalizationHolder.rm.GetString("Client.Core_331");
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок узла</param>
  public VirtualObjectNodeID(string caption) => this._caption = caption;

  /// <summary>Категория</summary>
  public int CategoryID => Intermech.Navigator.Consts.CategoryVirtualObjectNode;

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
  public override bool Equals(object obj) => obj is VirtualObjectNodeID;

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => base.GetHashCode();
}
