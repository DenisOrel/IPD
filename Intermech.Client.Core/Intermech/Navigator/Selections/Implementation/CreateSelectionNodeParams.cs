
// Type: Intermech.Navigator.Selections.Implementation.CreateSelectionNodeParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>Параметры для создания описания узла</summary>
public class CreateSelectionNodeParams : CreateObjectNodeParams
{
  /// <summary>Флажок "Ручная сортировка"</summary>
  protected bool handSelection;
  /// <summary>Принадлежность выборки</summary>
  protected SelectionType selectionType;
  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  protected int bindedObjectTypeID = -1;
  /// <summary>Назначение выборки</summary>
  protected int sampleFunction;
  /// <summary>Искать среди объектов локальных и глобальных типов</summary>
  protected bool searchInLocalTypes;

  /// <summary>
  /// Создать пустые параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  public CreateSelectionNodeParams()
  {
  }

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public CreateSelectionNodeParams(object source) => this.Assign(source);

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
  /// <param name="handSelection">Флажок "Ручная сортировка"</param>
  /// <param name="selectionType"></param>
  /// <param name="siteID">Узел информационной системы</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="relGuid">Guid связи</param>
  /// <param name="modificationID">Номер группы изменений</param>
  /// <param name="bindedObjectTypeID">Идентификатор типа объекта, с которым связана выборка</param>
  /// <param name="sampleFunction">Назначение выборки</param>
  /// <param name="searchInLocalTypes">Искать среди объектов локальных и глобальных типов</param>
  public CreateSelectionNodeParams(
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
    bool handSelection,
    SelectionType selectionType,
    string siteID,
    long projID,
    Guid relGuid,
    long modificationID,
    int bindedObjectTypeID,
    int sampleFunction,
    bool searchInLocalTypes)
    : base(objTypeId, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, state, version, baseVersion, siteID, projID, relGuid, modificationID)
  {
    this.handSelection = handSelection;
    this.selectionType = selectionType;
    this.bindedObjectTypeID = bindedObjectTypeID;
    this.sampleFunction = sampleFunction;
    this.searchInLocalTypes = searchInLocalTypes;
  }

  /// <summary>Список значений дополнительных атрибутов</summary>
  public virtual bool HandSelection
  {
    [DebuggerStepThrough] get => this.handSelection;
    set => this.handSelection = value;
  }

  /// <summary>Принадлежность выборки</summary>
  public virtual SelectionType SelectionType
  {
    [DebuggerStepThrough] get => this.selectionType;
    set => this.selectionType = value;
  }

  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  public int BindedObjectTypeID
  {
    [DebuggerStepThrough] get => this.bindedObjectTypeID;
    set => this.bindedObjectTypeID = value;
  }

  /// <summary>Назначение выборки</summary>
  public int SampleFunction
  {
    [DebuggerStepThrough] get => this.sampleFunction;
    set => this.sampleFunction = value;
  }

  /// <summary>Искать среди объектов локальных и глобальных типов</summary>
  public bool SearchInLocalTypes
  {
    [DebuggerStepThrough] get => this.searchInLocalTypes;
    set => this.searchInLocalTypes = value;
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.handSelection = false;
    this.selectionType = SelectionType.None;
    this.bindedObjectTypeID = -1;
    this.sampleFunction = 0;
    this.searchInLocalTypes = false;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is CreateSelectionNodeParams selectionNodeParams))
      return;
    this.handSelection = selectionNodeParams.HandSelection;
    this.selectionType = selectionNodeParams.SelectionType;
    this.bindedObjectTypeID = selectionNodeParams.BindedObjectTypeID;
    this.sampleFunction = selectionNodeParams.SampleFunction;
    this.searchInLocalTypes = selectionNodeParams.SearchInLocalTypes;
  }
}
