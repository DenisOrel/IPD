
// Type: Intermech.Navigator.DBObjects.AdvRelationsNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Описание узла состава</summary>
public class AdvRelationsNodeID : NodeID
{
  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public string FiltrationOwnerID
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).FiltrationOwnerID;
  }

  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  public List<long> Contexts
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).Contexts;
  }

  /// <summary>Идентификатор типа родительского объекта</summary>
  public int ProjObjType
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).ProjObjType;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public new long ProjID
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).ProjID;
  }

  /// <summary>Guid связи</summary>
  public new Guid RelGuid
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).RelGuid;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public List<int> Attributes
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).Attributes;
  }

  /// <summary>Список значений дополнительных атрибутов</summary>
  public virtual object[] Values
  {
    [DebuggerStepThrough] get => (this.pars as AdvCreateObjectNodeParams).Values;
  }

  /// <summary>Значение указанного атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>null, если значение атрибута не найдено</returns>
  public virtual object this[int attributeID]
  {
    get
    {
      return !(this.pars as AdvCreateObjectNodeParams).Attributes.Contains(attributeID) ? (object) null : (this.pars as AdvCreateObjectNodeParams).Values[(this.pars as AdvCreateObjectNodeParams).Attributes.IndexOf(attributeID)];
    }
    set
    {
      if (!(this.pars as AdvCreateObjectNodeParams).Attributes.Contains(attributeID))
        return;
      (this.pars as AdvCreateObjectNodeParams).Values[(this.pars as AdvCreateObjectNodeParams).Attributes.IndexOf(attributeID)] = value;
    }
  }

  /// <summary>
  /// Создать описание узла на основании указанных параметров
  /// </summary>
  /// <param name="e">Параметры для создания описания узла</param>
  public AdvRelationsNodeID(CreateObjectNodeParams e)
    : base(e)
  {
    this.pars = (CreateObjectNodeParams) new AdvCreateObjectNodeParams((object) e);
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is AdvRelationsNodeID advRelationsNodeId) ? base.Equals(obj) : advRelationsNodeId.PrjLinkID == this.PrjLinkID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.PrjLinkID.GetHashCode();
}
