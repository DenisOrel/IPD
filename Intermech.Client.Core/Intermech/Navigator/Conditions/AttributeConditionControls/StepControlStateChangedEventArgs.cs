
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.StepControlStateChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

/// <summary>
/// Аргументы события изменения состояния шага для главной формы
/// </summary>
public sealed class StepControlStateChangedEventArgs : EventArgs
{
  public bool NextEnable { get; private set; }

  public StepControlStateChangedEventArgs(bool nextEnable) => this.NextEnable = nextEnable;
}
