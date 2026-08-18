// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.BriefcaseAccessor
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Briefcase;

#nullable disable
namespace Intermech.Interfaces;

public class BriefcaseAccessor : IBriefcaseContext
{
  public static SimpleBriefcase GlobalBriefcase;

  public SimpleBriefcase Briefcase => BriefcaseAccessor.GlobalBriefcase;
}
