// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeColumnRenameEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Делегат для вызова</summary>
/// <param name="sender">Отправитель</param>
/// <param name="e">Аргументы события</param>
[Serializable]
public delegate void NodeColumnRenameEventHandler(object sender, NodeColumnRenameEventArgs e);
