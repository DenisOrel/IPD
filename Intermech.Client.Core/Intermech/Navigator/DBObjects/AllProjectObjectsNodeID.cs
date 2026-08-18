
// Type: Intermech.Navigator.DBObjects.AllProjectObjectsNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Описание виртуального узла "Все объекты проекта"</summary>
public class AllProjectObjectsNodeID : INodeID
{
  /// <summary>Идентификатор версии проекта</summary>
  internal long projectID;
  /// <summary>Печенюга</summary>
  private object cookie;

  /// <summary>Создать экземпляр класса</summary>
  public AllProjectObjectsNodeID()
  {
  }

  /// <summary>Создать экземпляр класса, заполнить его данными</summary>
  /// <param name="projectID">Идентификатор версии проекта</param>
  public AllProjectObjectsNodeID(long projectID) => this.projectID = projectID;

  /// <summary>Категория</summary>
  public int CategoryID
  {
    [DebuggerStepThrough] get => Intermech.Navigator.Consts.CategoryAllProjectObjectsNode;
  }

  /// <summary>Тип</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => 0;
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
    return obj is AllProjectObjectsNodeID projectObjectsNodeId && this.projectID == projectObjectsNodeId.projectID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => this.projectID.GetHashCode();
}
