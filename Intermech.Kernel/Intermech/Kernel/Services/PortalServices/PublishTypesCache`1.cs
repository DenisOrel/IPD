// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishTypesCache`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.IO;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class PublishTypesCache<TCache> where TCache : ICollection
{
  private readonly string _configName;

  public PublishTypesCache(string configName) => this._configName = configName;

  protected abstract TCache Check(IUserSession session, TCache cache);

  public TCache LoadCache(IUserSession session)
  {
    IBlobReader configAttribute = session.Configurations.GetConfigAttribute(this._configName) as IBlobReader;
    try
    {
      if (configAttribute.OpenBlob(0).RealFileSize > 0L)
      {
        byte[] buffer = configAttribute.ReadDataBlock(0);
        if (buffer != null && buffer.Length != 0)
        {
          using (MemoryStream serializationStream = new MemoryStream(buffer))
          {
            serializationStream.Position = 0L;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            TCache cache = default (TCache);
            try
            {
              cache = (TCache) binaryFormatter.Deserialize((Stream) serializationStream);
            }
            catch
            {
            }
            return this.Check(session, cache);
          }
        }
      }
      return this.Check(session, default (TCache));
    }
    finally
    {
      configAttribute.CloseBlob();
    }
  }

  public void SaveCache(IUserSession session, TCache cache)
  {
    IDBAttribute configAttribute = session.Configurations.GetConfigAttribute(this._configName);
    if ((object) cache == null || cache.Count == 0)
    {
      (configAttribute as IBlobWriter).OpenBlob(new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty), true);
    }
    else
    {
      using (ImChunkedStream serializationStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) cache);
        IBlobWriter blobWriter = configAttribute as IBlobWriter;
        blobWriter.OpenBlob(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, this._configName, ArcMethods.NotPacked, string.Empty), false);
        blobWriter.WriteDataBlock(serializationStream.ToArray());
      }
    }
  }
}
