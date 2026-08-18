// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.NotAllSignedException
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class NotAllSignedException : WorkflowException
{
  public NotAllSignedException(string message)
    : base(message)
  {
  }

  protected NotAllSignedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
