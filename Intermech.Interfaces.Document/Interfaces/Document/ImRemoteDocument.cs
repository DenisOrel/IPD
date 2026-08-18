// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImRemoteDocument
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.IO;
using System;
using System.IO;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Document;

[Serializable]
public class ImRemoteDocument : ISerializable
{
  private ImDocumentData document;

  public ImRemoteDocument(ImDocumentData doc) => this.document = doc;

  protected ImRemoteDocument(SerializationInfo info, StreamingContext context)
  {
    Stream stream = (Stream) new MemoryStream((byte[]) info.GetValue("Stream", typeof (byte[])));
    stream.Position = 0L;
    this.document = ImDocumentData.LoadFromXml(stream);
  }

  public ImDocumentData Document
  {
    get => this.document;
    set => this.document = value;
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
