// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Params.CreateTechNodeParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Params;

/// <summary>Параметры для создания описания узла</summary>
internal class CreateTechNodeParams : AdvCreateObjectNodeParams
{
  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок.
  /// ВНИМАНИЕ!!! В качестве ID можно использовать только Int32 !!!
  /// </summary>
  protected List<NodeColumnID> attributes;

  /// <summary>
  /// Создать пустые параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  public CreateTechNodeParams()
  {
  }

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public CreateTechNodeParams(object source) => this.Assign(source);

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
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Узлы информационной системы</param>
  /// <param name="modificationID">Номер группы изменений</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  /// <param name="projObjType">Тип родительского объекта</param>
  /// <param name="projID">Идентификатор родительского объекта</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  /// <param name="relationGuid">Guid связи</param>
  /// <param name="values">Список значений дополнительных атрибутов</param>
  public CreateTechNodeParams(
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
    long modificationID,
    string filtrationOwnerID,
    List<long> contexts,
    int projObjType,
    long projID,
    Guid relationGuid,
    List<NodeColumnID> attributes,
    object[] values)
    : base(objTypeId, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, state, version, baseVersion, siteID, filtrationOwnerID, contexts, projObjType, projID, relationGuid, modificationID, (List<int>) null, values)
  {
    this.attributes = attributes;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок.
  /// ВНИМАНИЕ!!! В качестве ID можно использовать только Int32 !!!
  /// </summary>
  public virtual List<NodeColumnID> Attributes
  {
    [DebuggerStepThrough] get => this.attributes;
    set => this.attributes = value;
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.attributes = (List<NodeColumnID>) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is CreateTechNodeParams createTechNodeParams))
      return;
    this.attributes = createTechNodeParams.Attributes;
  }
}
