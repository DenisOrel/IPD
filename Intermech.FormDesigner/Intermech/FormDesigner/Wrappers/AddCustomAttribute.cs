// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AddCustomAttribute
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// Сорбытие для добавления некоторых пользовательских атрибутов
/// </summary>
/// <param name="component">объект</param>
/// <param name="pd">PropertyDescriptor</param>
public delegate void AddCustomAttribute(object component, PropertyDescriptor pd);
