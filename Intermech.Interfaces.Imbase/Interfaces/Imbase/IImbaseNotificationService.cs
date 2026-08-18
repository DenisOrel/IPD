// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseNotificationService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Сервис для генерации сообщений о действиях пользователя над
/// объектами IMBASE
/// </summary>
internal interface IImbaseNotificationService
{
  event TableEventHandler StartEdit;

  event TableEventHandler EndEdit;

  event TableEventHandler BeforeApplyUpdates;

  event TableEventHandler AfterApplyUpdates;

  event TableEventHandler CancelUpdates;
}
