
// Type: Intermech.Navigator.SelectionView.RelationOperatorItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для реляционных операторов условия (для реализации элементов в ComboBox)
/// </summary>
internal sealed class RelationOperatorItem
{
  public RelationalOperators relationalOperator;

  public RelationOperatorItem(RelationalOperators aRalOper) => this.relationalOperator = aRalOper;

  public override string ToString()
  {
    return RelationalOperatorsHelper.GetCaption(this.relationalOperator);
  }
}
