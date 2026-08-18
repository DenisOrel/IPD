// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelConfigurationBuilderResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBModelConfigurationBuilderResult
{
  public DBModelConfigurationBuilderResult(int capacity)
  {
    this.InternalDescriptors = new List<DBEntityTypeDescriptor>(capacity);
    this.ChangeTrackerDescriptors = new List<EntityChangeTrackerDescriptor>(capacity);
  }

  public List<DBEntityTypeDescriptor> InternalDescriptors { get; private set; }

  public List<EntityChangeTrackerDescriptor> ChangeTrackerDescriptors { get; private set; }
}
