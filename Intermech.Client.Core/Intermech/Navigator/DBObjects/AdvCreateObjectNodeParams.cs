
// Type: Intermech.Navigator.DBObjects.AdvCreateObjectNodeParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Параметры для создания описания узла</summary>
public class AdvCreateObjectNodeParams : CreateObjectNodeParams
{
  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  protected string filtrationOwnerID;
  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  protected List<long> contexts;
  /// <summary>Идентификатор типа родительского объекта</summary>
  protected int projObjType;
  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  protected List<int> attributes;
  /// <summary>Список значений этих атрибутов</summary>
  protected object[] values;

  /// <summary>
  /// Создать пустые параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  public AdvCreateObjectNodeParams()
  {
  }

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public AdvCreateObjectNodeParams(object source) => this.Assign(source);

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="prjLinkId">Идентификатор связи</param>
  /// <param name="checkedOutBy">Кем объект взят на изменение</param>
  /// <param name="lcStepID">Шаг жизненного цикла</param>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="owner">Идентификатор владельца объекта</param>
  /// <param name="sorting">Значение атрибута "Сортировка" (если объект - в составе)</param>
  /// <param name="state">Состояние фильтрации версии</param>
  /// <param name="version">Номер версии объекта</param>
  /// <param name="baseVersion"></param>
  /// <param name="siteID">Узлы информационной системы</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  /// <param name="projObjType">Тип родительского объекта</param>
  /// <param name="projID">Идентификатор родительского объекта</param>
  /// <param name="relGuid">Guid связи</param>
  /// <param name="modificationID">Номер группы изменений</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  /// <param name="values">Список значений дополнительных атрибутов</param>
  public AdvCreateObjectNodeParams(
    int objTypeId,
    long objId,
    long id,
    long checkedOutBy,
    long prjLinkId,
    int lcStepID,
    string caption,
    int relTypeID,
    long owner,
    long sorting,
    ObjectFiltrationState state,
    long version,
    long baseVersion,
    string siteID,
    string filtrationOwnerID,
    List<long> contexts,
    int projObjType,
    long projID,
    Guid relGuid,
    long modificationID,
    List<int> attributes,
    object[] values)
    : base(objTypeId, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, state, version, baseVersion, siteID, projID, relGuid, modificationID)
  {
    this.filtrationOwnerID = filtrationOwnerID;
    this.contexts = contexts;
    this.projObjType = projObjType;
    this.attributes = attributes;
    this.values = values;
  }

  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public virtual string FiltrationOwnerID
  {
    [DebuggerStepThrough] get => this.filtrationOwnerID;
    set => this.filtrationOwnerID = value;
  }

  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  public virtual List<long> Contexts
  {
    [DebuggerStepThrough] get => this.contexts;
    set => this.contexts = value;
  }

  /// <summary>Идентификатор типа родительского объекта</summary>
  public virtual int ProjObjType
  {
    [DebuggerStepThrough] get => this.projObjType;
    set => this.projObjType = value;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public virtual List<int> Attributes
  {
    [DebuggerStepThrough] get => this.attributes;
    set => this.attributes = value;
  }

  /// <summary>Список значений дополнительных атрибутов</summary>
  public virtual object[] Values
  {
    [DebuggerStepThrough] get => this.values;
    set => this.values = value;
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.filtrationOwnerID = string.Empty;
    this.contexts = (List<long>) null;
    this.projObjType = -1;
    this.projID = 0L;
    this.attributes = (List<int>) null;
    this.values = (object[]) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is AdvCreateObjectNodeParams objectNodeParams))
      return;
    this.filtrationOwnerID = objectNodeParams.FiltrationOwnerID;
    this.contexts = objectNodeParams.Contexts;
    this.projObjType = objectNodeParams.ProjObjType;
    this.projID = objectNodeParams.ProjID;
    this.attributes = objectNodeParams.Attributes;
    this.values = objectNodeParams.Values;
  }
}
