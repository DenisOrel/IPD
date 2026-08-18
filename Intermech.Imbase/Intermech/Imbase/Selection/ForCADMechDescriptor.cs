// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ForCADMechDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator;
using Intermech.Navigator.CustomNode;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ForCADMechDescriptor : Descriptor
{
  public List<int> TypeIDs { get; private set; }

  public ForCADMechDescriptor(string caption, DescriptorCollection descriptors, List<int> typeIDs)
    : base(caption, descriptors)
  {
    this.TypeIDs = typeIDs != null ? typeIDs : new List<int>(0);
  }
}
