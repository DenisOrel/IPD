// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MultiCAD.JTCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.MultiCAD;

internal sealed class JTCommandsProvider : ICommandsProvider
{
  private static readonly string AlternativeRepresentationsCommandName = "AlternativeRepresentations";
  private static readonly string MakeJTDocumentsCommandName = "MakeJTDocuments";
  private IOutputView outputViewService;
  private JTSourceDocumentTypesHelper jtSourceDocumentTypesHelper;
  private MenuTemplateNode rootMenuNode;

  public JTCommandsProvider(
    IOutputView outputViewService,
    JTSourceDocumentTypesHelper jtSourceDocumentTypesHelper)
  {
    this.outputViewService = outputViewService;
    this.jtSourceDocumentTypesHelper = jtSourceDocumentTypesHelper;
  }

  public void AddCommandsToMenuTemplate(MenuTemplate menuTemplate)
  {
    this.rootMenuNode = new MenuTemplateNode(JTCommandsProvider.AlternativeRepresentationsCommandName, "Представления документов", -1, 200, 0);
    this.rootMenuNode.Nodes.Add(new MenuTemplateNode(JTCommandsProvider.MakeJTDocumentsCommandName, "Создать/обновить JT-представление", -1, 10, 0));
    menuTemplate.BeginUpdate();
    try
    {
      menuTemplate.Nodes.Add(this.rootMenuNode);
    }
    finally
    {
      menuTemplate.EndUpdate();
    }
  }

  public void RemoveCommandsFromMenuTemplate(MenuTemplate menuTemplate)
  {
    if (this.rootMenuNode == null)
      return;
    menuTemplate.BeginUpdate();
    try
    {
      menuTemplate.Nodes.Remove(this.rootMenuNode);
      this.rootMenuNode = (MenuTemplateNode) null;
    }
    finally
    {
      menuTemplate.EndUpdate();
    }
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    try
    {
      if (!this.jtSourceDocumentTypesHelper.IsSourceDocumentType(items.GetItemID(0).TypeID))
        return CommandsInfo.Empty;
      CommandsInfo mergedCommands = new CommandsInfo();
      mergedCommands.Add(JTCommandsProvider.MakeJTDocumentsCommandName, new CommandInfo(0, new ClickEventHandler(this.MakeJTDocumentsHandler)));
      return mergedCommands;
    }
    catch (BadIntegratorSettingsException ex)
    {
      this.outputViewService.WriteString("Ошибки", $"Контекстному меню не удалось отобразить команды для управления JT-представлениями для технологии MultiCAD из-за ошибки. {ex.IntegratorName}: {ex.Message}");
      return CommandsInfo.Empty;
    }
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    return CommandsInfo.Empty;
  }

  private void MakeJTDocumentsHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    int errorCount = 0;
    Exception firstError = (Exception) null;
    ProgressSinks.DialogService.Invoke("Создание/обновление JT-представлений для выбранных документов", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
    {
      IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, items.Count);
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        progressSink.SetState(itemData.Caption);
        using (UIReport.CreateScope())
        {
          UIReportBuilder uiReportBuilder = new UIReportBuilder();
          uiReportBuilder.ReportStart($"Создание/обновление JT-представлений для '{itemData.Caption}'");
          try
          {
            MakeJTDocumentsAction jtDocumentsAction = new MakeJTDocumentsAction(itemData.ObjectID, itemData.ObjectType);
            jtDocumentsAction.Perform();
            foreach (Exception error in jtDocumentsAction.Errors)
              UIReport.ReportEvent(error.Message, TraceLevel.Warning);
            uiReportBuilder.ReportSuccess();
          }
          catch (Exception ex)
          {
            uiReportBuilder.ReportFail(ex);
            ++errorCount;
            if (errorCount == 1)
              firstError = ex;
          }
        }
        progressUpdater.AddCompletedTasks(1);
      }
    }));
    if (errorCount == 0)
      return;
    if (errorCount == 1)
    {
      ExceptionHelper.ExceptionService.ShowException(firstError);
    }
    else
    {
      int num = (int) MessageBox.Show("В процессе создания/обновления JT-представлений возникли ошибки. Более подробные сведения можно получить в окне 'Вывод'.", "Создание/обновление JT-представлений", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this.outputViewService.Activate("Вывод");
    this.outputViewService.ShowView();
  }
}
