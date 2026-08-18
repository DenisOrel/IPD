// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ResourceCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class ResourceCollection : Collection<Resource>, ISerializable
{
  public ResourceCollection()
  {
  }

  protected ResourceCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<Resource>) info.GetValue<Resource[]>("Items"));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    char ch = ',';
    int length = str.IndexOf(ch);
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    info.AddValue("Items", (object) this.ToArray<Resource>(this.Count));
  }

  [NotNull]
  [ItemNotNull]
  public List<string> Functions
  {
    get
    {
      List<string> functions = new List<string>();
      foreach (Resource resource in (System.Collections.ObjectModel.Collection<Resource>) this)
      {
        if (!functions.Contains(resource.Name))
          functions.Add(resource.Name);
      }
      return functions;
    }
  }
}
