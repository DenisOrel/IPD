// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerControlToolBoxUpdateEvent
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Делегат на имзменение списка ToolBoxItems.</summary>
/// <param name="sender">Контрол-редактор</param>
/// <param name="e">Параметры</param>
public delegate void FormDesignerControlToolBoxUpdateEvent(
  FormDesignerControl sender,
  FormDesignerControlToolBoxUpdateEventArgs e);
