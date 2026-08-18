// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DBCResourcesAccess
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.IO;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal static class DBCResourcesAccess
{
  internal static string nameSpace = "Intermech.DatabaseConfigurator.Resources.";

  internal static MemoryStream LoadResurce(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (DatabaseConfiguratorConsts).Assembly.GetManifestResourceStream(ResourceName);
      if (stream == null)
        return new MemoryStream();
      byte[] buffer = new byte[stream.Length];
      MemoryStream memoryStream = new MemoryStream(buffer.Length);
      stream.Read(buffer, 0, buffer.Length);
      memoryStream.Write(buffer, 0, buffer.Length);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      return memoryStream;
    }
    finally
    {
      stream?.Close();
    }
  }
}
