// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSCheckInException
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Вид ошибка возникающий при чекине спецификации</summary>
[Serializable]
public class AVSCheckInException : KernelException
{
  public AVSCheckInException(string message)
    : base(message)
  {
  }

  public AVSCheckInException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  public AVSCheckInException()
  {
  }

  protected AVSCheckInException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
