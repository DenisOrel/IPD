// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IUndo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Данный интерфейс должно поддерживать окно, которое управляет кнопками отменить и вернуть
/// </summary>
public interface IUndo
{
  List<UndoItem> GetUndoItems();

  List<UndoItem> GetRedoItems();

  /// <summary>Отменить все действия до указанного</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  bool Undo(UndoItem item);

  /// <summary>Вернуть все действия до указанного</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  bool Redo(UndoItem item);
}
