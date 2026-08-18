// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.WayOfNotificationEnum
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Способ уведомления подписчика о произошедшем событии</summary>
public enum WayOfNotificationEnum
{
  /// <summary>Внутренняя почта</summary>
  InternalMail,
  /// <summary>Внешняя почта</summary>
  ExternalMail,
  /// <summary>Внутренняя и внешняя почта</summary>
  InternalAndExternalMail,
}
