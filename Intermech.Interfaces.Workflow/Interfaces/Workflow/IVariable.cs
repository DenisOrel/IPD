// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IVariable
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IVariable
{
  string Name { get; }

  VarType Type { get; }

  string Value { get; set; }

  object TypedValue { get; set; }

  int AttributeID { get; }
}
