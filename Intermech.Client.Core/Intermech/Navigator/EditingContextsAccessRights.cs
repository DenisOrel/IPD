
// Type: Intermech.Navigator.EditingContextsAccessRights
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator;

/// <summary>Права доступа к контексту редактирования</summary>
[Flags]
[Serializable]
public enum EditingContextsAccessRights
{
  /// <summary>Только просмотр</summary>
  ReadOnly = 0,
  /// <summary>Полный доступ к контексту редактирования</summary>
  FullAccess = 1,
}
