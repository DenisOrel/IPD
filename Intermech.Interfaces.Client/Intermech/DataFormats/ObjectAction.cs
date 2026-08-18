// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ObjectAction
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>Действия, выполняемые над объектами</summary>
[Flags]
[Serializable]
public enum ObjectAction
{
  /// <summary>Объект был создан</summary>
  Create = 1,
  /// <summary>Объект был взят на изменение</summary>
  CheckOut = 16, // 0x00000010
  /// <summary>Изменения в объекте были завершены</summary>
  CheckIn = 32, // 0x00000020
  /// <summary>Изменения в объекте были отменены</summary>
  CancelChanges = 64, // 0x00000040
  /// <summary>Изменения в объекте были сохранены</summary>
  SaveChanges = 128, // 0x00000080
  /// <summary>Объект был открыт в новом окне</summary>
  OpenInNewWindow = 256, // 0x00000100
  /// <summary>Объект был открыт</summary>
  Open = 4096, // 0x00001000
  /// <summary>Объект редактировался</summary>
  Edit = 8192, // 0x00002000
  /// <summary>Объект просматривался</summary>
  View = 16384, // 0x00004000
  /// <summary>Объект распечатывался</summary>
  Print = 32768, // 0x00008000
  /// <summary>Все действия запрещены</summary>
  DisabledActions = 0,
  /// <summary>Все допустимые действия</summary>
  AllActions = Print | View | Edit | Open | OpenInNewWindow | SaveChanges | CancelChanges | CheckIn | CheckOut | Create, // 0x0000F1F1
}
