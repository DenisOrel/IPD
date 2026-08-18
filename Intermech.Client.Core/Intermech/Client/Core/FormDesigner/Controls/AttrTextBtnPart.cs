
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtnPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class AttrTextBtnPart : RelatedObjectsPart
{
  private int _childObjTypeID = -1;
  private ConditionStructure[] _selectionConditions;

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных с ним объектов.
  /// Созданная часть будет возвращать объекты из состава/применяемости обрабатываемого объекта, связанные с ним любым типом связи и удовлетворяющие указанному условию.
  /// </summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта</param>
  /// <param name="childObjTypeID"></param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="selectionConditions">условия контекстной выборки, null если контекстной выборки нет</param>
  public AttrTextBtnPart(
    int objTypeID,
    long objID,
    int childObjTypeID,
    int relTypeID,
    IServiceProvider services,
    ConditionStructure[] selectionConditions)
    : base(objTypeID, objID, RelatedObjectsRole.Composition, relTypeID, services)
  {
    this._childObjTypeID = childObjTypeID;
    this._selectionConditions = selectionConditions;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditions"></param>
  /// <returns></returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    ConditionStructure[] conditionStructureArray = conditions;
    if (this._selectionConditions != null)
      conditionStructureArray = ConditionStructure.Join(this._selectionConditions, conditionStructureArray);
    return (INodeQuery) new AttrTextBtnQuery((INodeQuerySupport) this, this._objTypeID, this._objID, this._childObjTypeID, this._relTypeID, conditionStructureArray);
  }
}
