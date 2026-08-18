
// Type: Intermech.Navigator.LifeCycle.LifeCycleStepNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Описание виртуального узла "Шаг жизненного цикла"</summary>
public class LifeCycleStepNodeID : INodeID
{
  /// <summary>Название шага жизненного цикла</summary>
  protected internal string caption;
  /// <summary>Идентификатор шага жизненного цикла</summary>
  protected internal int id;
  /// <summary>Печенюга</summary>
  private object cookie;

  /// <summary>Создать экземпляр класса</summary>
  public LifeCycleStepNodeID()
  {
  }

  /// <summary>Создать экземпляр класса, заполнить его данными</summary>
  /// <param name="id">Идентификатор шага жизненного цикла</param>
  public LifeCycleStepNodeID(int id)
  {
    this.id = id;
    this.caption = MetaDataHelper.GetLCStepName(id);
  }

  /// <summary>Категория</summary>
  public int CategoryID => Intermech.Navigator.Consts.CategoryLifeCycleStepNode;

  /// <summary>Тип</summary>
  public int TypeID => this.id;

  /// <summary>Печенюга</summary>
  public object Cookie
  {
    get => this.cookie;
    set => this.cookie = value;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is LifeCycleStepNodeID lifeCycleStepNodeId && this.id == lifeCycleStepNodeId.id;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => this.id.GetHashCode();
}
