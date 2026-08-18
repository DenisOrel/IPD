// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AfterObjectCreatedEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат для события, возникающего после успешного завершения создания нового объекта
/// </summary>
/// <param name="sender">Ссылка на экземпляр создателя cобытия</param>
/// <param name="ea">Аргументы события</param>
public delegate void AfterObjectCreatedEventHandler(object sender, AfterObjectCreatedEventArgs e);
