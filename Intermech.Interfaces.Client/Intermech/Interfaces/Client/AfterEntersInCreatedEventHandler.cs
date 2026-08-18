// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AfterEntersInCreatedEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат для события, возникающего при успешном включении в какой-либо состав создаваемого объекта
/// </summary>
/// <param name="sender">Ссылка на экземпляр создателя cобытия</param>
/// <param name="e">Аргументы события</param>
public delegate void AfterEntersInCreatedEventHandler(
  object sender,
  AfterEntersInCreatedEventArgs e);
