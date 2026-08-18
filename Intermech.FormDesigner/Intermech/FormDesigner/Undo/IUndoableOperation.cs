// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.IUndoableOperation
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// This Interface describes a the basic Undo/Redo operation all Undo Operations must implement this interface.
/// </summary>
public interface IUndoableOperation
{
  /// <summary>Отменить изменения.</summary>
  void Undo();

  /// <summary>Вернуть изменения.</summary>
  void Redo();
}
