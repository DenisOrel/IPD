// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IParentFlow
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Интерфейс родительского элемента цепочки
/// распределения данных потока (IFlowElement).
/// Служит для группировки цепочек потоков и организации их в иерархию.</summary>
public interface IParentFlow : IFlowElement
{
  /// <summary>Добавить дочерний элемент цепочки</summary>
  /// <param name="child">Дочерний элемент цепочки</param>
  void AddChildFlowElement(IFlowElement child);

  /// <summary>Удалить дочерний элемент цепочки</summary>
  /// <param name="child">Дочерний элемент цепочки</param>
  void RemoveChildFlowElement(IFlowElement child);
}
