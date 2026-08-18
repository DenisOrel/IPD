// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.EntityNotFoundException
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Ошибка при получении сущности в Altium Designer</summary>
[Serializable]
public sealed class EntityNotFoundException : Exception
{
  /// <summary>Создать объект</summary>
  public EntityNotFoundException()
  {
  }

  public EntityNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Создать объект</summary>
  /// <param name="entityType">Тип получаемой сущности</param>
  public EntityNotFoundException(Type entityType) => this.EntityType = entityType;

  public Type EntityType { get; private set; }
}
