// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.NotifyRules
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class NotifyRules : Form
{
  public NotifyOptions Options;
  public List<int> Attributes;
  public NotifyOptions OptionsBackup;
  public List<int> AttributesBackup;
  public string Comment;
  private long _objectID;
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private Panel panel2;
  private Label label1;
  private Panel panel1;
  private Panel panel3;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private Panel rtbPanel;
  private Label label2;
  private RichTextBox rtbComment;

  public NotifyRules(
    NotifyOptions options,
    List<int> attributes,
    long[] objectIDs,
    string comment)
  {
    this.Options = options;
    this.OptionsBackup = options;
    this.Attributes = attributes != null ? new List<int>((IEnumerable<int>) attributes) : new List<int>();
    this.AttributesBackup = attributes != null ? new List<int>((IEnumerable<int>) attributes) : new List<int>();
    this.Comment = comment;
    INamedImageList service = (INamedImageList) ApplicationServices.Container.GetService(typeof (INamedImageList));
    this.InitializeComponent();
    this.treeList1.StateImageList = service.ImageList;
    this.treeList1.CheckedStateIndex = service.ImageIndex("imgChecked");
    this.treeList1.UncheckedStateIndex = service.ImageIndex("imgUnchecked");
    this.treeList1.GrayedStateIndex = service.ImageIndex("imgGrayed");
    this.rtbComment.Text = this.Comment;
    foreach (NotifyOptions notifyOptions in Enum.GetValues(typeof (NotifyOptions)))
    {
      if (notifyOptions != NotifyOptions.None)
      {
        TreeListNode treeListNode1 = this.treeList1.AppendNode((object) new object[1]
        {
          (object) EnumDescConverter.GetEnumDescription((Enum) notifyOptions)
        }, (TreeListNode) null);
        treeListNode1.Tag = (object) notifyOptions;
        treeListNode1.CheckState = (options & notifyOptions) == notifyOptions ? CheckState.Checked : CheckState.Unchecked;
        if (notifyOptions == NotifyOptions.AttributeValueChanged)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            List<int> intList1 = new List<int>();
            List<int> first = new List<int>();
            for (int index1 = 0; index1 < objectIDs.Length; ++index1)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectIDs[index1]);
              if (!intList1.Contains(objectInfo.ObjectTypeID))
              {
                intList1.Add(objectInfo.ObjectTypeID);
                DataTable dataTable = sessionKeeper.Session.GetObjectType(objectInfo.ObjectTypeID).Attributes.Select(string.Empty);
                List<int> intList2 = new List<int>();
                for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                {
                  int int32_1 = Convert.ToInt32(dataTable.Rows[index2]["F_ATTRIBUTE_ID"]);
                  FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(dataTable.Rows[index2]["F_ATTRIBUTE_TYPE"]);
                  if (int32_1 >= 0 && int32_2 != FieldTypes.ftBlob && int32_2 != FieldTypes.ftMemo && int32_2 != FieldTypes.ftFile && int32_2 != FieldTypes.ftShortBlob)
                    intList2.Add(int32_1);
                }
                if (first.Count == 0)
                  first.AddRange((IEnumerable<int>) intList2);
                else
                  first = first.Intersect<int>((IEnumerable<int>) intList2).ToList<int>();
              }
            }
            List<MyElement> myElementList = new List<MyElement>();
            for (int index = 0; index < first.Count; ++index)
              myElementList.Add(new MyElement((object) first[index], MetaDataHelper.GetAttributeTypeName(first[index]), (object) null));
            myElementList.Sort();
            for (int index = 0; index < myElementList.Count; ++index)
            {
              TreeListNode treeListNode2 = this.treeList1.AppendNode((object) new object[1]
              {
                (object) myElementList[index].Caption
              }, treeListNode1);
              treeListNode2.Tag = (object) Convert.ToInt32(myElementList[index].Value);
              treeListNode2.CheckState = attributes == null || !attributes.Contains(Convert.ToInt32(myElementList[index].Value)) ? CheckState.Unchecked : CheckState.Checked;
            }
            if (treeListNode1.Nodes.Count == 0)
              this.treeList1.Nodes.Remove(treeListNode1);
          }
          if (this.Attributes.Count == 0)
            treeListNode1.CheckState = CheckState.Unchecked;
        }
      }
    }
    this.CheckBtnOkEnable();
  }

  private void CheckBtnOkEnable()
  {
    bool flag = false;
    for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
    {
      if (this.treeList1.Nodes[index].CheckState != CheckState.Unchecked)
      {
        flag = true;
        break;
      }
    }
    this.bOK.Enabled = flag;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    NotifyOptions notifyOptions = NotifyOptions.None;
    for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
    {
      if (this.treeList1.Nodes[index].CheckState != CheckState.Unchecked)
        notifyOptions |= (NotifyOptions) this.treeList1.Nodes[index].Tag;
    }
    this.Options = notifyOptions;
    if ((notifyOptions & NotifyOptions.AttributeValueChanged) == NotifyOptions.AttributeValueChanged)
    {
      TreeListNode treeListNode = (TreeListNode) null;
      for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
      {
        if ((NotifyOptions) this.treeList1.Nodes[index].Tag == NotifyOptions.AttributeValueChanged)
        {
          treeListNode = this.treeList1.Nodes[index];
          break;
        }
      }
      this.Attributes = new List<int>();
      int num1 = 0;
      for (int index = 0; index < treeListNode.Nodes.Count; ++index)
      {
        if (treeListNode.Nodes[index].CheckState == CheckState.Checked)
        {
          this.Attributes.Add((int) treeListNode.Nodes[index].Tag);
          ++num1;
        }
      }
      if (num1 > 12)
      {
        int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Workflow.Client_85"), (object) 12), LocalizationHolder.rm.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
    }
    else
      this.Attributes = (List<int>) null;
    this.Comment = this.rtbComment.Text;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void treeList1_CheckStateChanged(object sender, NodeEventArgs e)
  {
    this.CheckBtnOkEnable();
  }

  private void bCancel_Click(object sender, EventArgs e)
  {
    this.Attributes = this.AttributesBackup;
    this.Options = this.OptionsBackup;
    this.DialogResult = DialogResult.None;
    this.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NotifyRules));
    this.bOK = new Button();
    this.bCancel = new Button();
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.panel3 = new Panel();
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.rtbPanel = new Panel();
    this.rtbComment = new RichTextBox();
    this.label2 = new Label();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel3.SuspendLayout();
    this.treeList1.BeginInit();
    this.rtbPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    this.panel2.Controls.Add((Control) this.bOK);
    this.panel2.Controls.Add((Control) this.bCancel);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel3.Controls.Add((Control) this.rtbPanel);
    this.panel3.Controls.Add((Control) this.treeList1);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.CheckBoxes = CheckBoxesStyle.ThreeState;
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.CheckStateChanged += new NodeEventHandler(this.treeList1_CheckStateChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.rtbPanel.Controls.Add((Control) this.label2);
    this.rtbPanel.Controls.Add((Control) this.rtbComment);
    componentResourceManager.ApplyResources((object) this.rtbPanel, "rtbPanel");
    this.rtbPanel.Name = "rtbPanel";
    componentResourceManager.ApplyResources((object) this.rtbComment, "rtbComment");
    this.rtbComment.Name = "rtbComment";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NotifyRules);
    this.ShowInTaskbar = false;
    this.Tag = (object) "   ";
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.treeList1.EndInit();
    this.rtbPanel.ResumeLayout(false);
    this.rtbPanel.PerformLayout();
    this.ResumeLayout(false);
  }
}
