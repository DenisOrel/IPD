// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionUserRoles
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Кем приходится пользователь поручению</summary>
[Flags]
public enum ResolutionUserRoles
{
  /// <summary>Никто</summary>
  None = 0,
  /// <summary>Администратор IPS. Нужен при проверке прав доступа и видимости</summary>
  Admin = 1,
  /// <summary>Создатель. Пользователь создавший поручение</summary>
  Creator = 2,
  /// <summary>Автор. Пользователь, которого назначили автором поручения</summary>
  Author = 4,
  /// <summary>Контроллёр</summary>
  Controller = 8,
  /// <summary>Исполнитель</summary>
  Executor = 16, // 0x00000010
  /// <summary>Кто-нибудь из всех ролей</summary>
  AnyRole = Executor | Controller | Author | Creator | Admin, // 0x0000001F
}
