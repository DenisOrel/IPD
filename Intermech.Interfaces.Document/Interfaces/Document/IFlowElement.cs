// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IFlowElement
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Интерфейс элемента цепочки для представления (распределения)
/// потока данных на странице</summary>
public interface IFlowElement
{
  /// <summary>Родительский элемент цепочки</summary>
  IParentFlow ParentFlow { get; set; }

  /// <summary>Следующий элемент цепочки</summary>
  IFlowElement NextFlowElement { get; set; }

  /// <summary>Предыдущий элемент цепочки</summary>
  IFlowElement PrevFlowElement { get; set; }

  /// <summary>Присвоить значение ParentFlow без вызова
  /// ParentFlow.AddChildFlowElement или ParentFlow.RemoveChildFlowElement</summary>
  /// <param name="value">Новое значение ParentFlow</param>
  void AssignParentFlow(IParentFlow value);

  /// <summary>Получить первый элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Первый элемент цепочки для заданного потока данных</returns>
  IFlowElement GetFirstFlowElement(FlowID flow, ref IFlowElement flowElementByName);

  /// <summary>Получить следующий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Следующий элемент цепочки для заданного потока данных</returns>
  IFlowElement GetNextFlowElement(FlowID flow, ref IFlowElement flowElementByName);

  /// <summary>Получить последний элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Последний элемент цепочки для заданного потока данных</returns>
  IFlowElement GetLastFlowElement(FlowID flow, ref IFlowElement flowElementByName);

  /// <summary>Получить предыдущий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Предыдущий элемент цепочки для заданного потока данных</returns>
  IFlowElement GetPrevFlowElement(FlowID flow, ref IFlowElement flowElementByName);

  /// <summary>Цепочка не содержит данных заданного потока</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <returns>Возвращает true, если цепочка не содержит данных потока</returns>
  bool FlowIsEmpty(FlowID flow);

  /// <summary>Цепочка не содержит данных ни одного потока</summary>
  /// <returns>Возвращает true, если цепочка не содержит данных ни одного потока</returns>
  bool AllFlowsIsEmpty();
}
