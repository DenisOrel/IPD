
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemeNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Описание виртуального узла "Схема жизненного цикла"</summary>
public class LifeCycleSchemeNodeID : INodeID
{
  /// <summary>Название схемы жизненного цикла</summary>
  protected internal string caption;
  /// <summary>Идентификатор схемы жизненного цикла</summary>
  protected internal int id;
  /// <summary>Печенюга</summary>
  private object cookie;

  /// <summary>Создать экземпляр класса</summary>
  public LifeCycleSchemeNodeID()
  {
  }

  /// <summary>Создать экземпляр класса, заполнить его данными</summary>
  /// <param name="id">Идентификатор схемы жизненного цикла</param>
  public LifeCycleSchemeNodeID(int id)
  {
    this.id = id;
    this.caption = MetaDataHelper.GetLCSchemaName(id);
  }

  /// <summary>Категория</summary>
  public int CategoryID
  {
    [DebuggerStepThrough] get => Intermech.Navigator.Consts.CategoryLifeCycleSchemeNode;
  }

  /// <summary>Тип</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.id;
  }

  /// <summary>Печенюга</summary>
  public object Cookie
  {
    [DebuggerStepThrough] get => this.cookie;
    [DebuggerStepThrough] set => this.cookie = value;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is LifeCycleSchemeNodeID cycleSchemeNodeId && this.id == cycleSchemeNodeId.id;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => this.id.GetHashCode();
}
