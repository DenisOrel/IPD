// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.SelectedMessageAnalizer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.Controls;

#nullable disable
namespace Intermech.Forums;

public class SelectedMessageAnalizer : SelectedItemsAnalyzer
{
  /// <summary>
  /// Выполнить анализирование указанной коллекции элементов, выделенных в окне
  /// </summary>
  /// <param name="sender">Окно, в котором осуществляется выбор элементов</param>
  /// <param name="itemsHost">Служба окна, которая предоставляет коллекцию выделенных элементов</param>
  /// <returns>Результат проверки</returns>
  public override SelectedItemsAnalyzerResult Analyze(
    ISelectionWindow sender,
    ISelectedItemsHost itemsHost)
  {
    return sender == null || itemsHost == null || itemsHost.SelectedItems == null || itemsHost.SelectedItems.Count == 0 || itemsHost.SelectedItems.GetType() != typeof (UserMessageSelectedItems) ? SelectedItemsAnalyzerResult.Disabled : SelectedItemsAnalyzerResult.Enabled;
  }
}
