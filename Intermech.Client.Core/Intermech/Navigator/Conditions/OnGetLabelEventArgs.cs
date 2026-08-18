
// Type: Intermech.Navigator.Conditions.OnGetLabelEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.SelectionView;


namespace Intermech.Navigator.Conditions;

public sealed class OnGetLabelEventArgs
{
  public SelectionParameterTypes ParamType { get; private set; }

  public ShowValueMode ValueMode { get; private set; }

  public RelationalOperators RelationalOperator { get; private set; }

  public bool Handled { get; set; }

  public LabelsForControl LabelsForControl { get; set; }

  public OnGetLabelEventArgs(
    SelectionParameterTypes paramType,
    ShowValueMode valueMode,
    RelationalOperators relationalOperator)
  {
    this.ParamType = paramType;
    this.ValueMode = valueMode;
    this.RelationalOperator = relationalOperator;
    this.LabelsForControl = new LabelsForControl();
    this.Handled = false;
  }
}
