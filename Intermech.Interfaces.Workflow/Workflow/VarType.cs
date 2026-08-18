// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.VarType
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow;

public enum VarType
{
  [FType(FieldTypes.ftString)] String,
  [FType(FieldTypes.ftInteger)] Integer,
  [FType(FieldTypes.ftDouble)] Float,
  [FType(FieldTypes.ftDateTime)] DateTime,
  [CustomDescription("Attribute.Interfaces.Workflow_6"), FType(FieldTypes.ftString, "list.bmp", MultiValueModes = MultiValueModes.SingleValueFromList)] StringList,
  [CustomDescription("Attribute.Interfaces.Workflow_7"), FType(FieldTypes.ftMemo, "users.bmp")] ParticipantList,
  [FType(FieldTypes.ftBoolean)] Boolean,
  [CustomDescription("Attribute.Interfaces.Workflow_8"), FType(FieldTypes.ftObjectLink, "cad0011e-306c-11d8-b4e9-00304f19f545", "archive.bmp")] Archive,
  [CustomDescription("Attribute.Interfaces.Workflow_10"), FType(FieldTypes.ftMemo, MultiValueModes = MultiValueModes.MultiValues)] Text,
  [FType(FieldTypes.ftUnknown)] Unknown,
}
