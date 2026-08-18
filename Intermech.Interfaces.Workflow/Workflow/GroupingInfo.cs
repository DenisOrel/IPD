// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GroupingInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

/// <summary>
/// прицепляется к Attachment, когда идет expand
/// указывает, в какой группирующий объект входит это вложение
///  </summary>
public class GroupingInfo
{
  public readonly Attachment Parent;
  public readonly long CheckOutBy;

  public GroupingInfo(Attachment parent, long checkOutBy)
  {
    this.Parent = parent;
    this.CheckOutBy = checkOutBy;
  }
}
