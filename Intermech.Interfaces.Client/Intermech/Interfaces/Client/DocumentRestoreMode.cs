// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DocumentRestoreMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Способ восстановления сохранённых окон</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum DocumentRestoreMode
{
  /// <summary>Не восстанавливать</summary>
  [CustomDescription("Attribute.Client.Core_210")] None,
  /// <summary>Создавать прокси</summary>
  [CustomDescription("Attribute.Client.Core_211")] CreateProxy,
  /// <summary>Восстанавливать</summary>
  [CustomDescription("Attribute.Client.Core_212")] Restore,
}
