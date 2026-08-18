// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISortedPropertyGrid
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Управление сортировкой в property grid настроек</summary>
public interface ISortedPropertyGrid
{
  PropertySort Sort { get; }
}
