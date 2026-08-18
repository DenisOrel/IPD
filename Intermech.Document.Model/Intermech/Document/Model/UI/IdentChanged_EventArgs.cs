// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.IdentChanged_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Аргументы события IdentChanged</summary>
public class IdentChanged_EventArgs : EventArgs
{
  /// <summary>Тип измененного отступа</summary>
  public enumTypeDrag Type;

  /// <summary>Конструктор</summary>
  /// <param name="newName">Тип измененного отступа</param>
  public IdentChanged_EventArgs(enumTypeDrag type) => this.Type = type;
}
