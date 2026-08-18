// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.GetCategoryTypeParentEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Делегат события "Найти родительскую категорию и тип"</summary>
/// <param name="sender">Отправитель</param>
/// <param name="e">Аргументы события</param>
public delegate void GetCategoryTypeParentEventHandler(
  object sender,
  GetCategoryTypeParentEventArgs e);
