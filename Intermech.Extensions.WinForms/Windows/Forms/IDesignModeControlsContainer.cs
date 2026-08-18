// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.IDesignModeControlsContainer
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Windows.Forms;

public interface IDesignModeControlsContainer
{
  [CanBeNull]
  List<(Control DesignModeControl, string FieldName)> GetDesignModeChildControls();
}
