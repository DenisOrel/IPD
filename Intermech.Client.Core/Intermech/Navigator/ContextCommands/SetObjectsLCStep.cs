
// Type: Intermech.Navigator.ContextCommands.SetObjectsLCStep
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextCommands;

/// <summary>Summary description for SetObjectsLCStep.</summary>
public class SetObjectsLCStep : Form
{
  private ObjectSteps[] _Os;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private Panel panel3;
  private Button btnCancel;
  private Button buttonOk;
  private Panel panel1;
  private Panel panel2;
  private Label label1;
  private Label label2;
  private IContainer components;
  public int StepSelected = -1;

  public SetObjectsLCStep(ObjectSteps[] os)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 704);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 50, workingArea.Height / 100 * 35);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this._Os = os;
    for (int index = 0; index < this._Os.Length; ++index)
    {
      if (this._Os[index].Atribute == 0)
      {
        this.label1.Text = this._Os[index].StepName;
        try
        {
          using (Icon icon = new Icon((Stream) new MemoryStream(this._Os[index].Icon)))
          {
            this.label2.Image = (Image) icon.ToBitmap();
            break;
          }
        }
        catch
        {
          this.label2.Image = (Image) null;
          break;
        }
      }
    }
    this.FillList();
  }

  private void FillList()
  {
    this.treeList1.Nodes.Clear();
    int num = -1;
    ImageList imageList = new ImageList();
    imageList.ColorDepth = ColorDepth.Depth24Bit;
    this.treeList1.StateImageList = imageList;
    for (int index = 0; index < this._Os.Length; ++index)
    {
      if (this._Os[index].Atribute != 0)
      {
        bool flag = true;
        try
        {
          if (this._Os[index].Icon != null)
          {
            if (this._Os[index].Icon.Length != 0)
            {
              using (MemoryStream memoryStream = new MemoryStream(this._Os[index].Icon))
              {
                Icon icon = new Icon((Stream) memoryStream);
                imageList.Images.Add(icon);
              }
              ++num;
            }
          }
        }
        catch
        {
          flag = false;
        }
        TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[1]
        {
          (object) this._Os[index].StepName
        }, (TreeListNode) null);
        treeListNode.Tag = (object) this._Os[index].LCStep;
        if (flag)
          treeListNode.StateImageIndex = num;
      }
    }
  }

  private void RefreshList()
  {
    this.buttonOk.Enabled = false;
    this.FillList();
  }

  private void buttonOk_Click(object sender, EventArgs e)
  {
    if (this.treeList1.Selection.Count != 1)
      return;
    this.StepSelected = Convert.ToInt32(this.treeList1.Selection[0].Tag);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetObjectsLCStep));
    this.groupBox1 = new GroupBox();
    this.groupBox2 = new GroupBox();
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.label2 = new Label();
    this.treeList1 = new TreeList();
    this.panel3 = new Panel();
    this.btnCancel = new Button();
    this.buttonOk = new Button();
    this.treeListColumn1 = new TreeListColumn();
    this.groupBox2.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.treeList1.BeginInit();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.Add((Control) this.panel2);
    this.groupBox2.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.panel2.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel1.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.treeList1.MouseDoubleClick += new MouseEventHandler(this.treeList1_MouseDoubleClick);
    this.panel3.Controls.Add((Control) this.btnCancel);
    this.panel3.Controls.Add((Control) this.buttonOk);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.Cursor = Cursors.Hand;
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.AcceptButton = (IButtonControl) this.buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this.groupBox2);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SetObjectsLCStep);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Closed += new EventHandler(this.SetObjectsLCStep_Closed);
    this.Load += new EventHandler(this.SetObjectsLCStep_Load);
    this.groupBox2.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.treeList1.EndInit();
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.buttonOk.Enabled = this.treeList1.Selection.Count > 0;
  }

  private void SetObjectsLCStep_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void SetObjectsLCStep_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.treeList1.Selection.Count <= 0 || e.Button != MouseButtons.Left)
      return;
    this.buttonOk_Click(sender, (EventArgs) null);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }
}
