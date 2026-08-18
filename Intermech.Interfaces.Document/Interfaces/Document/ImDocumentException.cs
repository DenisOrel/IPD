// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImDocumentException
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Исключение для редактора документов Интермех</summary>
[Serializable]
public class ImDocumentException : Exception, ISerializable
{
  public ImDocumentException(string message)
    : base(message)
  {
  }

  public ImDocumentException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  protected ImDocumentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
  }
}
