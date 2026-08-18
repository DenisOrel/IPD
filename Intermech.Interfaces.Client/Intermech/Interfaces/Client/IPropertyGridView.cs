// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyGridView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Окно для отображения свойств свбранного объекта для редакторов
/// документов
/// </summary>
public interface IPropertyGridView
{
  event PropertyValueChangedEventHandler PropertyValueChanged;

  event EventHandler SelectedObjectChanged;

  PropertyGrid PropertyGrid { get; }

  object DesignableObject { get; set; }

  object[] DesignableObjects { get; set; }

  void SetDesignableObjects(params object[] objects);

  object SelectedObject { get; set; }

  object[] SelectedObjects { get; set; }
}
