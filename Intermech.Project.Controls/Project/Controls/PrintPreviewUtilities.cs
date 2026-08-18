// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.PrintPreviewUtilities
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Project.Controls.Properties;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal static class PrintPreviewUtilities
{
  public static void ApplyResources([NotNull] PrintPreviewDialog printPreviewDialog)
  {
    printPreviewDialog.Text = Resources.PrintPreview;
    if (!(printPreviewDialog.Controls[1] is ToolStrip control))
      return;
    ToolStripItem toolStripItem1 = control.Items[0];
    toolStripItem1.Text = Resources.Print;
    toolStripItem1.ToolTipText = Resources.Print;
    // ISSUE: variable of the null type
    __Null local1;
    string str1 = (string) (local1 = null);
    toolStripItem1.AccessibleDescription = (string) local1;
    toolStripItem1.AccessibleName = str1;
    ToolStripItem toolStripItem2 = control.Items[1];
    toolStripItem2.Text = Resources.Zoom;
    toolStripItem2.ToolTipText = Resources.Zoom;
    // ISSUE: variable of the null type
    __Null local2;
    string str2 = (string) (local2 = null);
    toolStripItem2.AccessibleDescription = (string) local2;
    toolStripItem2.AccessibleName = str2;
    ToolStripItem toolStripItem3 = control.Items[3];
    toolStripItem3.Text = Resources.OnePage;
    toolStripItem3.ToolTipText = Resources.OnePage;
    // ISSUE: variable of the null type
    __Null local3;
    string str3 = (string) (local3 = null);
    toolStripItem3.AccessibleDescription = (string) local3;
    toolStripItem3.AccessibleName = str3;
    ToolStripItem toolStripItem4 = control.Items[4];
    toolStripItem4.Text = Resources.TwoPages;
    toolStripItem4.ToolTipText = Resources.TwoPages;
    // ISSUE: variable of the null type
    __Null local4;
    string str4 = (string) (local4 = null);
    toolStripItem4.AccessibleDescription = (string) local4;
    toolStripItem4.AccessibleName = str4;
    ToolStripItem toolStripItem5 = control.Items[5];
    toolStripItem5.Text = Resources.ThreePages;
    toolStripItem5.ToolTipText = Resources.ThreePages;
    // ISSUE: variable of the null type
    __Null local5;
    string str5 = (string) (local5 = null);
    toolStripItem5.AccessibleDescription = (string) local5;
    toolStripItem5.AccessibleName = str5;
    ToolStripItem toolStripItem6 = control.Items[6];
    toolStripItem6.Text = Resources.FourPages;
    toolStripItem6.ToolTipText = Resources.FourPages;
    // ISSUE: variable of the null type
    __Null local6;
    string str6 = (string) (local6 = null);
    toolStripItem6.AccessibleDescription = (string) local6;
    toolStripItem6.AccessibleName = str6;
    ToolStripItem toolStripItem7 = control.Items[7];
    toolStripItem7.Text = Resources.SixPages;
    toolStripItem7.ToolTipText = Resources.SixPages;
    // ISSUE: variable of the null type
    __Null local7;
    string str7 = (string) (local7 = null);
    toolStripItem7.AccessibleDescription = (string) local7;
    toolStripItem7.AccessibleName = str7;
    ToolStripItem toolStripItem8 = control.Items[9];
    toolStripItem8.Text = Resources.Close;
    // ISSUE: variable of the null type
    __Null local8;
    string str8 = (string) (local8 = null);
    toolStripItem8.AccessibleDescription = (string) local8;
    toolStripItem8.AccessibleName = str8;
    ToolStripItem toolStripItem9 = control.Items[11];
    toolStripItem9.Text = Resources.Page;
    toolStripItem9.ToolTipText = Resources.Page;
    // ISSUE: variable of the null type
    __Null local9;
    string str9 = (string) (local9 = null);
    toolStripItem9.AccessibleDescription = (string) local9;
    toolStripItem9.AccessibleName = str9;
  }
}
