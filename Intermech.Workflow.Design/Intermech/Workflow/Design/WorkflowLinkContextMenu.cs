// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowLinkContextMenu
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WorkflowNodeContextMenu.</summary>
public class WorkflowLinkContextMenu
{
  private static ContextMenuBarItem _menu;
  public static MenuButtonItem DelMI;

  public static ContextMenuBarItem Menu
  {
    get
    {
      if (WorkflowLinkContextMenu._menu == null)
      {
        WorkflowLinkContextMenu._menu = new ContextMenuBarItem();
        int index = WorkflowLinkContextMenu._menu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_138"));
        WorkflowLinkContextMenu.DelMI = WorkflowLinkContextMenu._menu.Items[index];
        WorkflowLinkContextMenu.DelMI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgDelete");
      }
      return WorkflowLinkContextMenu._menu;
    }
  }

  public static void InitMenu(WorkflowLink l, GraphView view)
  {
    if (WorkflowLinkContextMenu.Menu == null)
      return;
    WorkflowLinkContextMenu.DelMI.Enabled = !view.ReadOnly;
  }
}
