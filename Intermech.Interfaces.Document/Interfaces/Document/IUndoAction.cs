// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IUndoAction
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

public interface IUndoAction
{
  bool DoAction();

  /// <summary>Создание Redo действия</summary>
  /// <returns></returns>
  IUndoAction CreateRedoAction();

  /// <summary>Заголовок для списка действий</summary>
  string Caption { get; }

  /// <summary>
  /// Изменение Id любого объекта, требуется для изменения Id если он хранится в действии
  /// </summary>
  /// <param name="oldValue"></param>
  /// <param name="newValue"></param>
  void IdChanged(string oldValue, string newValue);
}
