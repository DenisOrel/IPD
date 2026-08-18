// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectsSelectionOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Опции, применяемые при работе с коллекциями объектов</summary>
[Flags]
[Serializable]
public enum ObjectsSelectionOptions : long
{
  /// <summary>
  /// Отключить фильтрацию версий объектов, применяемых в контекстах редактирования
  /// </summary>
  None = 0,
  ShowAllModifications = 1,
  LocalTypesMode = 2,
  ShowNotOwnedWorkCopies = 4,
  TrashMode = ShowNotOwnedWorkCopies | ShowAllModifications, // 0x0000000000000005
}
