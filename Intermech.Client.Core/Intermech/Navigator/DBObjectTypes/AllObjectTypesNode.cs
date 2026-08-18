
// Type: Intermech.Navigator.DBObjectTypes.AllObjectTypesNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Класс, реализующий элемент "Типы объектов" из пространства навигации.
/// </summary>
public sealed class AllObjectTypesNode : CompositeNode, IContextAware
{
  /// <summary>
  /// Создать элемент пространства навигации "Типы объектов"
  /// </summary>
  public AllObjectTypesNode() => this.options = NodeOptions.CanContainsObjectTypesList;

  /// <summary>Создать слоты-папки</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new ObjectTypesPart(-1))
    };
  }

  /// <summary>Вернуть слоты-не-папки</summary>
  /// <returns>Слоты-не-папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IObjectTypeNodeOptionsHolder) ? this.Services.GetService(typeof (IObjectTypeNodeOptionsHolder)) : base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Контекст (контейнер сервисов) для элемента пространства навигации
  /// </summary>
  public IServiceProvider Services { [DebuggerStepThrough] get; set; }
}
