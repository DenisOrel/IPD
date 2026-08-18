// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowNodeContextMenu
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WorkflowNodeContextMenu.</summary>
public class WorkflowNodeContextMenu
{
  private static ContextMenuBarItem _menu = (ContextMenuBarItem) null;
  private static readonly string cStrCreateBackLinkCaption = LocalizationHolder.rm.GetString("Workflow.Design_129");
  public static volatile MenuButtonItem AddLinkMI = (MenuButtonItem) null;
  public static MenuButtonItem AddBLinkMI = (MenuButtonItem) null;
  public static MenuButtonItem AddPBlockMI = (MenuButtonItem) null;
  public static MenuButtonItem EditFormMI = (MenuButtonItem) null;
  public static MenuButtonItem ViewFormMI = (MenuButtonItem) null;
  public static MenuButtonItem DelFormMI = (MenuButtonItem) null;
  public static MenuButtonItem CutMI = (MenuButtonItem) null;
  public static MenuButtonItem CopyMI = (MenuButtonItem) null;
  public static MenuButtonItem DelMI = (MenuButtonItem) null;
  public static MenuButtonItem PropsMI = (MenuButtonItem) null;
  public static MenuButtonItem VarsMI = (MenuButtonItem) null;
  public static MenuButtonItem SubProcMI = (MenuButtonItem) null;
  public static MenuButtonItem StartProcessFromThisMI = (MenuButtonItem) null;
  public static bool IsVisibleStartProcessFromThis = false;

  public static ContextMenuBarItem Menu
  {
    get
    {
      if (WorkflowNodeContextMenu._menu == null)
      {
        WorkflowNodeContextMenu._menu = new ContextMenuBarItem();
        int index1 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_130"));
        WorkflowNodeContextMenu.AddLinkMI = WorkflowNodeContextMenu._menu.Items[index1];
        int index2 = WorkflowNodeContextMenu._menu.Items.Add(WorkflowNodeContextMenu.cStrCreateBackLinkCaption);
        WorkflowNodeContextMenu.AddBLinkMI = WorkflowNodeContextMenu._menu.Items[index2];
        int index3 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("CreateParallelBlock"));
        WorkflowNodeContextMenu.AddPBlockMI = WorkflowNodeContextMenu._menu.Items[index3];
        int index4 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_131"));
        WorkflowNodeContextMenu.ViewFormMI = WorkflowNodeContextMenu._menu.Items[index4];
        WorkflowNodeContextMenu.ViewFormMI.BeginGroup = true;
        int index5 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_132"));
        WorkflowNodeContextMenu.EditFormMI = WorkflowNodeContextMenu._menu.Items[index5];
        int index6 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_133"));
        WorkflowNodeContextMenu.DelFormMI = WorkflowNodeContextMenu._menu.Items[index6];
        int index7 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_134"));
        WorkflowNodeContextMenu.SubProcMI = WorkflowNodeContextMenu._menu.Items[index7];
        WorkflowNodeContextMenu.SubProcMI.BeginGroup = true;
        int index8 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Variables_Cmd"));
        WorkflowNodeContextMenu.VarsMI = WorkflowNodeContextMenu._menu.Items[index8];
        WorkflowNodeContextMenu.VarsMI.BeginGroup = true;
        int index9 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_136"));
        WorkflowNodeContextMenu.CutMI = WorkflowNodeContextMenu._menu.Items[index9];
        WorkflowNodeContextMenu.CutMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgCut");
        WorkflowNodeContextMenu.CutMI.BeginGroup = true;
        int index10 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_137"));
        WorkflowNodeContextMenu.CopyMI = WorkflowNodeContextMenu._menu.Items[index10];
        WorkflowNodeContextMenu.CopyMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgCopy");
        int index11 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_138"));
        WorkflowNodeContextMenu.DelMI = WorkflowNodeContextMenu._menu.Items[index11];
        WorkflowNodeContextMenu.DelMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgDelete");
        int index12 = WorkflowNodeContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_139"));
        WorkflowNodeContextMenu.PropsMI = WorkflowNodeContextMenu._menu.Items[index12];
        WorkflowNodeContextMenu.PropsMI.BeginGroup = true;
        WorkflowNodeContextMenu.PropsMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgProp");
        int index13 = WorkflowNodeContextMenu._menu.Items.Add("Запустить процесс отсюда");
        WorkflowNodeContextMenu.StartProcessFromThisMI = WorkflowNodeContextMenu._menu.Items[index13];
        WorkflowNodeContextMenu.StartProcessFromThisMI.Visible = WorkflowNodeContextMenu.IsVisibleStartProcessFromThis;
        WorkflowNodeContextMenu.StartProcessFromThisMI.BeginGroup = true;
      }
      return WorkflowNodeContextMenu._menu;
    }
  }

  private static void UpdateMenuGroups(ContextMenuBarItem mb)
  {
    object obj = (object) null;
    for (int index = 0; index < mb.Items.Count; ++index)
    {
      if (mb.Items[index].Tag != null)
      {
        mb.Items[index].BeginGroup = false;
        mb.Items[index].Tag = (object) null;
      }
    }
    for (int index = 0; index < mb.Items.Count; ++index)
    {
      MenuButtonItem menuButtonItem = mb.Items[index];
      if (!menuButtonItem.Visible)
      {
        if (menuButtonItem.BeginGroup)
          obj = (object) true;
      }
      else if (obj != null)
      {
        if (!menuButtonItem.BeginGroup)
          menuButtonItem.Tag = (object) 1;
        menuButtonItem.BeginGroup = true;
        obj = (object) null;
      }
    }
  }

  public static void InitMenu(WorkflowNode w)
  {
    if (WorkflowNodeContextMenu.Menu == null)
      return;
    GraphView view = w.View;
    WorkflowNodeContextMenu.AddLinkMI.Visible = !view.ReadOnly;
    WorkflowNodeContextMenu.AddBLinkMI.Visible = WorkflowNodeContextMenu.AddLinkMI.Visible && wfConsts.RollbackActivityKinds.Contains(w.ActivityKind) && !w.IsParallelBlockFinish;
    WorkflowNodeContextMenu.AddPBlockMI.Visible = !view.ReadOnly;
    WorkflowNodeContextMenu.EditFormMI.Visible = WorkflowNodeContextMenu.AddLinkMI.Visible && w.FormID >= 0L;
    WorkflowNodeContextMenu.CutMI.Visible = w.CanDelete();
    WorkflowNodeContextMenu.CopyMI.Visible = w.CanCopy();
    WorkflowNodeContextMenu.DelMI.Visible = WorkflowNodeContextMenu.AddLinkMI.Visible;
    WorkflowNodeContextMenu.ViewFormMI.Visible = w.FormID > 0L;
    WorkflowNodeContextMenu.DelFormMI.Visible = WorkflowNodeContextMenu.EditFormMI.Visible && WorkflowNodeContextMenu.ViewFormMI.Visible;
    WorkflowNodeContextMenu.VarsMI.Visible = view.IsProcess;
    WorkflowNodeContextMenu.SubProcMI.Visible = w.ActivityType == wfConsts.SubProcessTypeID;
    WorkflowNodeContextMenu.UpdateMenuGroups(WorkflowNodeContextMenu._menu);
  }
}
