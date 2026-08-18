// Decompiled with JetBrains decompiler
// Type: Intermech.ClientException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech;

/// <summary>
/// Предок для всех исключений, генерируемых клиентской частью системы
/// </summary>
[Serializable]
public class ClientException : Exception
{
  public ClientException(string message)
    : base(message)
  {
  }

  public ClientException()
  {
  }

  protected ClientException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
