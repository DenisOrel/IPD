// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IndexNotFoundException
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Serializable]
public class IndexNotFoundException : ApplicationException
{
  /// <summary>Конструктор.</summary>
  /// <param name="msg"></param>
  public IndexNotFoundException(string msg)
    : base(msg)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="msg"></param>
  /// <param name="innerException"></param>
  public IndexNotFoundException(string msg, Exception innerException)
    : base(msg, innerException)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public IndexNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
