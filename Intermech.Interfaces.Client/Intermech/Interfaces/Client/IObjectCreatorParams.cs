// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectCreatorParams
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс с параметрами для управления кастом креаторами
/// </summary>
public interface IObjectCreatorParams
{
  /// <summary>
  /// Режим создания заготовки объекта( как правило не требуется показывать диалоги создания)
  /// </summary>
  /// <returns></returns>
  bool RawMode { get; }
}
