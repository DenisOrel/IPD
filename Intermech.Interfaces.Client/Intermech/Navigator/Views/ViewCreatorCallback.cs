// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ViewCreatorCallback
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>Делегат метода, который создает закладку.</summary>
public delegate Control ViewCreatorCallback(
  ISelectedItems items,
  System.IServiceProvider services,
  object additionalInfo);
