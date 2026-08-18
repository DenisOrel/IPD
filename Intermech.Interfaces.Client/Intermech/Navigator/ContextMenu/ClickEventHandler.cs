// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.ClickEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Делегат метода, который вызывается для выполнения команды контекстного меню.
/// </summary>
/// <param name="items">Коллекция выделенных элементов пространства навигации</param>
/// <param name="viewServices">Контейнер сервисов для выделенных элементов пространства навигации</param>
/// <param name="additionalInfo"></param>
public delegate void ClickEventHandler(
  ISelectedItems items,
  IServiceProvider viewServices,
  object additionalInfo);
