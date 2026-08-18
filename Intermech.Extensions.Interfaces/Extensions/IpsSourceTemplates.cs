// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IpsSourceTemplates
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System.ComponentModel;

#nullable disable
namespace Intermech.Extensions;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IpsSourceTemplates
{
  [SourceTemplate]
  public static void AttrArgNotEmpty(this int attributeID)
  {
  }

  [SourceTemplate]
  public static void AttrNotEmpty(this int attributeID)
  {
  }

  [SourceTemplate]
  public static void AttrArgNotEmpty(this ObligatoryObjectAttributes attributeID)
  {
  }

  [SourceTemplate]
  public static void AttrNotEmpty(this ObligatoryObjectAttributes attributeID)
  {
  }

  [SourceTemplate]
  public static void ObjTypeArgNotEmpty(this int objectTypeID)
  {
  }

  [SourceTemplate]
  public static void ObjTypeNotEmpty(this int objectTypeID)
  {
  }

  [SourceTemplate]
  public static void RelTypeArgNotEmpty(this int relationTypeID)
  {
  }

  [SourceTemplate]
  public static void RelTypeNotEmpty(this int relationTypeID)
  {
  }

  [SourceTemplate]
  public static void ObjArgNotEmpty(this long objectID)
  {
  }

  [SourceTemplate]
  public static void ObjNotEmpty(this long objectID)
  {
  }

  [SourceTemplate]
  public static void RelArgNotEmpty(this long relationID)
  {
  }

  [SourceTemplate]
  public static void RelNotEmpty(this long relationID)
  {
  }
}
