// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.BackgroundTaskResult
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Информация о выполненной задаче.</summary>
[Serializable]
public class BackgroundTaskResult
{
  /// <summary>Список сообщений пользователю.</summary>
  public List<BackgroundTaskMessage> Messages { get; set; }

  /// <summary>Список идентификаторов измененных объектов.</summary>
  public List<long> ChangedObjects { get; set; }

  /// <summary>Список идентификаторов созданных объектов.</summary>
  public List<long> CreatedObjects { get; set; }

  /// <summary>Конструктор.</summary>
  public BackgroundTaskResult()
  {
    this.Messages = new List<BackgroundTaskMessage>();
    this.ChangedObjects = new List<long>();
    this.CreatedObjects = new List<long>();
  }
}
