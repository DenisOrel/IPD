// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OrderProcessTemplates
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Шаблоны поручений.</summary>
[Serializable]
public class OrderProcessTemplates
{
  private Guid _control = Guid.Empty;
  private Guid _noControl = Guid.Empty;
  private Guid _successiveControl = Guid.Empty;
  private Guid _successiveNoControl = Guid.Empty;

  /// <summary>Шаблон процесса для контрольного поручения.</summary>
  public Guid Control
  {
    [DebuggerStepThrough] get => this._control;
    set
    {
      this._control = value;
      this.Changed = true;
    }
  }

  /// <summary>Шаблон процесса для неконтрольного поручения.</summary>
  public Guid NoControl
  {
    [DebuggerStepThrough] get => this._noControl;
    set
    {
      this._noControl = value;
      this.Changed = true;
    }
  }

  /// <summary>Шаблон процесса для контрольного поручения.</summary>
  public Guid SuccessiveControl
  {
    [DebuggerStepThrough] get => this._successiveControl;
    set
    {
      this._successiveControl = value;
      this.Changed = true;
    }
  }

  /// <summary>Шаблон процесса для неконтрольного поручения.</summary>
  public Guid SuccessiveNoControl
  {
    [DebuggerStepThrough] get => this._successiveNoControl;
    set
    {
      this._successiveNoControl = value;
      this.Changed = true;
    }
  }

  public bool FromParent { get; set; }

  public bool Changed { get; private set; }

  public bool Empty => this._control == Guid.Empty && this._noControl == Guid.Empty;

  public override bool Equals(object obj)
  {
    OrderProcessTemplates processTemplates = (OrderProcessTemplates) obj;
    return processTemplates != null && this.Control == processTemplates.Control && this.NoControl == processTemplates.NoControl && this.SuccessiveControl == processTemplates.SuccessiveControl && this.SuccessiveNoControl == processTemplates.SuccessiveNoControl;
  }

  public override int GetHashCode()
  {
    return this._control.GetHashCode() << 24 ^ this._noControl.GetHashCode() << 16 /*0x10*/ ^ this._successiveControl.GetHashCode() << 8 ^ this._successiveNoControl.GetHashCode();
  }
}
