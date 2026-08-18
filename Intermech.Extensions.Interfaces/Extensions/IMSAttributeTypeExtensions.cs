// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IMSAttributeTypeExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

internal static class IMSAttributeTypeExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnContents GetDefaultContent([NotNull] this IMSAttributeType attributeType)
  {
    switch (attributeType.FieldType)
    {
      case FieldTypes.ftDateTime:
        return ColumnContents.Date;
      case FieldTypes.ftObjectLink:
        return ColumnContents.ID;
      case FieldTypes.ftBlob:
      case FieldTypes.ftMeasured:
        return ColumnContents.Value;
      default:
        return ColumnContents.Text;
    }
  }
}
