// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ContentType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Данный перечислитель (набор битовых флажков, [Flags]) позволяет указать,
/// какое содержимое находится в составе элемента пространства навигации.
/// </summary>
[Flags]
public enum ContentType
{
  /// <summary>Содержимое узла неизвестно.</summary>
  None = 0,
  /// <summary>В содержимое узла могут входить папки.</summary>
  Folders = 1,
  /// <summary>
  /// В содержимое узла могут входить не папки (аналог файлов в файловой системе).
  /// </summary>
  NonFolders = 2,
  /// <summary>В содержимое узла могут скрытые узлы.</summary>
  Hidden = 4,
}
