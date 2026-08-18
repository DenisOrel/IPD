// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.ParameterAlreadyPresentException
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>
/// Ошибка при добавлении в коллекцию параметра, который уже представлен в коллекции
/// </summary>
[Serializable]
public sealed class ParameterAlreadyPresentException : Exception
{
  /// <summary>Создать объект</summary>
  public ParameterAlreadyPresentException()
  {
  }

  public ParameterAlreadyPresentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Создать объект</summary>
  /// <param name="parameterName">Имя параметра</param>
  public ParameterAlreadyPresentException(string parameterName)
  {
    this.ParameterName = parameterName;
  }

  public string ParameterName { get; private set; }
}
