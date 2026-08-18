// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.NotifyOptions
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Workflow;

/// <summary>
/// Опции, регулирующие поведение уведомления.
/// !!!!!!!!!!!!
/// При добавлении обязательно внести изменения в NotifyOptionsHelper
/// !!!!!!!!!!!!!
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Workflow_1")]
[Category("Misc")]
[Flags]
public enum NotifyOptions
{
  None = 0,
  [CustomDescription("Attribute.Interfaces.Workflow_2"), NotifyOptions(NotifyOptions.CheckOut)] CheckOut = 1,
  [CustomDescription("Attribute.Interfaces.Workflow_3"), NotifyOptions(NotifyOptions.UndoCheckOut)] UndoCheckOut = 2,
  [CustomDescription("Attribute.Interfaces.Workflow_4"), NotifyOptions(NotifyOptions.Delete)] Delete = 4,
  [CustomDescription("Attribute.Interfaces.Workflow_5"), NotifyOptions(NotifyOptions.Version)] Version = 8,
  [CustomDescription("ForumChanged"), NotifyOptions(NotifyOptions.Forum)] Forum = 16, // 0x00000010
  [CustomDescription("Attribute.Interfaces.Workflow_9"), NotifyOptions(NotifyOptions.AttributeValueChanged)] AttributeValueChanged = 32, // 0x00000020
}
