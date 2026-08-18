// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IExternalEditor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Интерфейс внешнего редактора для элементов</summary>
public interface IExternalEditor
{
  /// <summary>Вызов редактора</summary>
  /// <param name="nodes">контекст вызова</param>
  /// <returns>результат вызова, если false прпордолжается вызов других редакторов</returns>
  bool CallEditor(DocumentTreeNode[] nodes);

  /// <summary>Разрешен ли вызов редактора</summary>
  /// <param name="nodes">контекст вызова</param>
  /// <returns></returns>
  bool CanCallEditor(DocumentTreeNode[] nodes);
}
