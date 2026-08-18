// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CacheChangedEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="objectId"></param>
public delegate void CacheChangedEventHandler(object sender, long objectId);
