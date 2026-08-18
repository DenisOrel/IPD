// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IOEventType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Событие</summary>
[Serializable]
public enum IOEventType
{
  /// <summary>Нет события</summary>
  evNone = 0,
  /// <summary>Нажата клавиша</summary>
  evKeyDown = 1,
  /// <summary>Отпущена клавиша</summary>
  evKeyUp = 2,
  /// <summary>Нажата кнопка мыши</summary>
  evMouseClick = 65536, // 0x00010000
  /// <summary>Двойной клик мышью</summary>
  evMouseDoubleClick = 131072, // 0x00020000
  /// <summary>
  /// Событие drag'n'drop - пришло в клиентскую область контрола
  /// </summary>
  evDragDropEnter = 16777216, // 0x01000000
  /// <summary>
  /// Событие drag'n'drop - выполняется перетаскивание в клиентской области контрола
  /// </summary>
  evDragDropOver = 33554432, // 0x02000000
  /// <summary>
  /// Событие drag'n'drop - перетаскивание ушло из клиентской области контрола
  /// </summary>
  evDragDropLeave = 67108864, // 0x04000000
  /// <summary>
  /// Событие drag'n'drop - перетаскивание завершилось в клиентской области контрола
  /// </summary>
  evDragDrop = 134217728, // 0x08000000
}
