// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IOEventFlags
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Флажки события</summary>
[Flags]
[Serializable]
public enum IOEventFlags
{
  /// <summary>Никаких флажков нет</summary>
  efNone = 0,
  /// <summary>Событие уже обработано</summary>
  efProcessed = 1,
  /// <summary>Событие является широковещательным</summary>
  efBroadcast = 2,
}
