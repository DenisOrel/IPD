// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IAttachments
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IAttachments : IEnumerable<IAttachment>, IEnumerable
{
  int Count { get; }

  IAttachment this[int index] { get; }

  IAttachment Find(long objectid);

  int Add(long objectid);

  void RemoveAt(int index);
}
