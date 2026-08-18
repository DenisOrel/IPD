// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumExtend
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Forums;

public class ForumExtend : IForumExtend
{
  public event ForumExtendEventHandler Extend;

  public List<long> GetObjects(long objectID, Guid session)
  {
    List<long> objects = new List<long>();
    if (this.Extend != null)
    {
      ForumEventArgs eventArgs = new ForumEventArgs(objectID, session);
      foreach (ForumExtendEventHandler invocation in this.Extend.GetInvocationList())
      {
        try
        {
          invocation(eventArgs);
          objects.AddRange((IEnumerable<long>) eventArgs.ResultIDs);
        }
        catch
        {
        }
      }
    }
    return objects;
  }
}
