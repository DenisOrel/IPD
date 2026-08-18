// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBGuidAttribute
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

public abstract class DBGuidAttribute : Attribute
{
  private Guid guid;

  public DBGuidAttribute(string guid) => this.guid = new Guid(guid);

  public Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }
}
