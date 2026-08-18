// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.NotificationEventType
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Тип события срабатывания уведомления для настроек автоуведомления.
/// Фактически тут дублируется часть перечисления ActionType, кроме разветвления события NextLCStep на NextLCStep и NextLCLevel.
/// Жизненно необходимо, чтобы  текстовое представление остальных событий было таким же, как и у ActionType.
/// </summary>
public enum NotificationEventType
{
  None,
  [CustomDescription("Attribute.Interfaces.Workflow_23")] AddLink,
  [CustomDescription("Attribute.Interfaces.Workflow_24")] DeleteLink,
  [CustomDescription("Attribute.Interfaces.Workflow_25")] Create,
  [CustomDescription("Attribute.Interfaces.Workflow_35")] CreateVersion,
  [CustomDescription("Attribute.Interfaces.Workflow_26")] Delete,
  [CustomDescription("Attribute.Interfaces.Workflow_27")] NextLCStep,
  [CustomDescription("Attribute.Interfaces.Workflow_28")] NextLCLevel,
  [CustomDescription("Attribute.Interfaces.Workflow_29")] Cancel,
  [CustomDescription("Attribute.Interfaces.Workflow_30")] CheckIn,
  [CustomDescription("Attribute.Interfaces.Workflow_31")] CheckOut,
  [CustomDescription("Attribute.Interfaces.Workflow_32")] Restore,
  [CustomDescription("Attribute.Interfaces.Workflow_33")] Write,
  [CustomDescription("Attribute.Interfaces.Workflow_34")] GetAccess,
}
