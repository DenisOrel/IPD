// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImRemoteClientDocument
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.IO;
using System;
using System.IO;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Document;

[Serializable]
public class ImRemoteClientDocument : ISerializable
{
  private ImDocument _document;

  public ImRemoteClientDocument(ImDocument doc)
  {
    DocumentPlugin.InitDocumentPlugin();
    this._document = doc;
  }

  protected ImRemoteClientDocument(SerializationInfo info, StreamingContext context)
  {
    DocumentPlugin.InitDocumentPlugin();
    Stream stream = (Stream) new MemoryStream((byte[]) info.GetValue("Stream", typeof (byte[])));
    stream.Position = 0L;
    this._document = ImDocument.LoadFromXml(stream, true, false);
  }

  public ImDocument Document
  {
    get => this._document;
    set => this._document = value;
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      this.Document.SaveToXml((Stream) imChunkedStream);
      byte[] array = imChunkedStream.ToArray();
      info.AddValue("Stream", (object) array, typeof (byte[]));
    }
  }
}
