// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SelectionOptionsHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс, в котором хранятся опции для окна по выбору объектов
/// </summary>
public sealed class SelectionOptionsHolder
{
  /// <summary>Опции</summary>
  public SelectionOptions Options = SelectionOptions.Default;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="options">Опции</param>
  public SelectionOptionsHolder(SelectionOptions options) => this.Options = options;
}
