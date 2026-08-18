// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.EnabledNodesActions
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Перечислитель позволяет указать, какие действия допустимы над коллекцией выделенных в дереве типов узлов
/// </summary>
[Flags]
public enum EnabledNodesActions
{
  /// <summary>Никаких действий не разрешено</summary>
  None = 0,
  /// <summary>Разрешено перемещать узлы вверх по дереву</summary>
  MoveUp = 1,
  /// <summary>Разрешено перемещать узлы вниз по дереву</summary>
  MoveDown = 2,
  /// <summary>Разрешено перемещать узлы в начало списка</summary>
  MoveTop = 4,
  /// <summary>Разрешено перемещать узлы в конец списка</summary>
  MoveBottom = 8,
  /// <summary>Разрешено добавлять</summary>
  Add = 16, // 0x00000010
  /// <summary>Разрешено удалять</summary>
  Remove = 32, // 0x00000020
}
