
// Type: Intermech.Navigator.DBObjects.ClientCoreResourcesAccess
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.IO;


namespace Intermech.Navigator.DBObjects;

/// <summary>Чтение ресурсов</summary>
internal static class ClientCoreResourcesAccess
{
  /// <summary>Путь к ресурсам</summary>
  internal static string nameSpace = "Intermech.Client.Core.Navigator.Resources.";

  /// <summary>Считать ресурс в массив байт</summary>
  /// <param name="ResourceName">Имя ресурса</param>
  /// <returns>Иконка в потоке</returns>
  internal static MemoryStream LoadResurce(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (ClientCoreResourcesAccess).Assembly.GetManifestResourceStream(ResourceName);
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
