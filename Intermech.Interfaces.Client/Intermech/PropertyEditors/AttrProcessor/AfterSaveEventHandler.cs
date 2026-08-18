// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AfterSaveEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Делегат индикации сохранения атрибутов в AttributeProcessor
/// </summary>
/// <param name="sender"></param>
/// <param name="args">список значений, измененный сервером после сохранения</param>
public delegate void AfterSaveEventHandler(object sender, AfterSaveEventEventArgs args);
