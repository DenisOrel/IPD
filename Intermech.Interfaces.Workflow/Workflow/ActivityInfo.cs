// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

/// <summary>Summary description for ActivityInfo.</summary>
public class ActivityInfo
{
  public int Type;
  public string ObjectName;
  public string TypeName;
  public int ImageIndex = -2;
  public ActivityKind Kind;
  private Guid _typeGuid;

  public Guid TypeGuid
  {
    get => this._typeGuid;
    set
    {
      this._typeGuid = value;
      this.Kind = ActGuidMapper.GuidToKind(value);
    }
  }
}
