// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ColumnDescriptorExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Kernel.Search;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ColumnDescriptorExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetAttributeID(
    in this ColumnDescriptor columnDescriptor,
    bool throwExceptionIfCantGet = true)
  {
    return DB.GetAttributeID(columnDescriptor.AttributeID, throwExceptionIfCantGet);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttributeID(
    in this ColumnDescriptor columnDescriptor,
    out int attributeID)
  {
    return DB.TryGetAttributeID(columnDescriptor.AttributeID, out attributeID);
  }
}
