// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.DataPacketProducer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Imbase.API;

internal static class DataPacketProducer
{
  internal static byte[] PackDataTable(DataTable dataTable, FieldInfo[] fieldsInfo)
  {
    using (MemoryStream memoryStream = new MemoryStream(4096 /*0x1000*/))
      return memoryStream.ToArray();
  }

  private static void AnalizeSizes(DataTable dataTable, FieldInfo[] fieldsInfo)
  {
  }
}
