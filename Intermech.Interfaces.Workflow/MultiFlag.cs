// Decompiled with JetBrains decompiler
// Type: Intermech.MultiFlag
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech;

/// <summary>
/// Помечает флаг как используемый несколькими свойствами.
/// Означает, что флаг не будет сбрасываться при записи значения по умолчанию в какое-то из свойств.
/// </summary>
public class MultiFlag : Attribute
{
}
