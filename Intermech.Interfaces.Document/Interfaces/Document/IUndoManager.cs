// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IUndoManager
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Document;

public interface IUndoManager
{
  void Clear();

  /// <summary>Заблокировать Undo</summary>
  void LockUndo();

  /// <summary>Разблокировать Undo</summary>
  void UnlockUndo();

  /// <summary>
  /// Проверка на блокировку, проверяется блокировка в менеджере и документе
  /// </summary>
  bool IsLocked { get; }

  bool CanUndo();

  bool CanRedo();

  VisualNode Document { get; }

  Control Form { get; }

  /// <summary>Списмок действий</summary>
  List<IUndoAction> Actions { get; }

  List<IUndoAction> RedoActions { get; }

  /// <summary>Начать создание сложного Undo</summary>
  /// <param name="caption">заголовок , если равен "" используется заголовок первого undo в списке</param>
  /// <returns></returns>
  IUndoAction BeginCreateMultyUndo(string caption);

  /// <summary>Начать создание сложного Undo</summary>
  /// <param name="caption">заголовок , если равен "" используется заголовок первого undo в списке</param>
  /// <returns></returns>
  IUndoAction BeginCreateMultyUndo(string caption, List<IUndoAction> actions);

  IUndoAction EndCreateMultyUndo();

  /// <summary>Создать пользоватеоьское действие</summary>
  /// <param name="action"></param>
  /// <param name="ignoreLock"></param>
  /// <returns></returns>
  IUndoAction CreateUndo(IUndoAction action, bool ignoreLock);

  /// <summary>Создать действие изменения свойства или поля</summary>
  /// <param name="node"></param>
  /// <param name="propertyName"></param>
  /// <param name="oldValue"></param>
  /// <param name="newValue"></param>
  /// <returns></returns>
  IUndoAction CreateUndo(DocumentTreeNode obj, string propertyName);

  /// <summary>Создать действие изменения свойства или поля</summary>
  /// <param name="node"></param>
  /// <param name="propertyName"></param>
  /// <param name="oldValue"></param>
  /// <param name="newValue"></param>
  /// <returns></returns>
  IUndoAction CreateUndo(object obj, string propertyName, object oldValue, object newValue);

  /// <summary>Создать действие удаления</summary>
  /// <param name="parent"></param>
  /// <param name="child"></param>
  /// <param name="removeIndex"></param>
  /// <returns></returns>
  IUndoAction CreateUndo(DocumentTreeNode parent, DocumentTreeNode child, int removeIndex);

  /// <summary>Отмена вставки элемента</summary>
  /// <param name="parent"></param>
  /// <param name="child"></param>
  /// <returns></returns>
  IUndoAction CreateUndo(DocumentTreeNode parent, DocumentTreeNode addNode);

  /// <summary>Создать действие смены позиции</summary>
  /// <param name="parent"></param>
  /// <param name="oldPos"></param>
  /// <param name="newPos"></param>
  /// <param name="exchanged">true - поменять элементы в позициях местами, false переместить с одной позиции на другую</param>
  /// <returns></returns>
  IUndoAction CreateUndo(DocumentTreeNode parent, int oldPos, int newPos, bool exchanged);

  bool DoUndo();

  bool DoRedo();
}
