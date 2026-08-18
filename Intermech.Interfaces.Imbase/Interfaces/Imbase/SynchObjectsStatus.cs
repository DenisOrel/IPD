// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.SynchObjectsStatus
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public enum SynchObjectsStatus
{
  /// <summary>Не нуждается в обновлении</summary>
  [Description("не нуждается в обновлении.")] NotNeedToModified,
  /// <summary>Синхронизировано</summary>
  [Description("синхронизирован.")] Synchronized,
  /// <summary>Объект не связан с IMBASE</summary>
  [Description("не связан с IMBASE.")] DontLinkedWithIMBASE,
  /// <summary>
  /// Не удалось синхронизировать, по причине возникшней ошибки
  /// </summary>
  [Description("не удалось синхронизировать, по причине возникшней ошибки:")] NotSynchronized,
}
