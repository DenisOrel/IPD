// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.BeforeDoDragDrop_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

public class BeforeDoDragDrop_EventArgs : EventArgs
{
  private object objectToDrag;
  private DragDropEffects effect;
  private bool doDragDrop;

  /// <summary>Объект для перемещения</summary>
  public object ObjectToDrag
  {
    get => this.objectToDrag;
    set => this.objectToDrag = value;
  }

  /// <summary>Эффект drag drop</summary>
  public DragDropEffects Effect
  {
    get => this.effect;
    set => this.effect = value;
  }

  /// <summary>Требуется ли начинать процедуру перемещения</summary>
  public bool DoDragDrop
  {
    get => this.doDragDrop;
    set => this.doDragDrop = value;
  }

  public BeforeDoDragDrop_EventArgs(bool DoDragDrop) => this.DoDragDrop = DoDragDrop;
}
