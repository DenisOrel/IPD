// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.PropertySetValue
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Делегат для просмотра значений</summary>
/// <param name="component">объект для которого пришло значение</param>
/// <param name="e">параметры</param>
public delegate void PropertySetValue(object component, SetValueEventArgs e);
