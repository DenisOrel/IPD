
// Type: Intermech.Client.Core.AllowedAttrsSourceTypesEnum
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

[Flags]
public enum AllowedAttrsSourceTypesEnum
{
  /// <summary>Все типы атрибутов</summary>
  All = 1,
  /// <summary>Атрибуты объектов</summary>
  Objects = 2,
  /// <summary>Атрибуты связей</summary>
  Relations = 4,
}
