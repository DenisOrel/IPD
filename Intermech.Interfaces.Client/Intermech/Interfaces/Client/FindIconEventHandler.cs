// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FindIconEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат для поиска и автоматического добавления в список икон
/// для типа и/или категории.
/// </summary>
public delegate Icon FindIconEventHandler(int category, int type, object data);
