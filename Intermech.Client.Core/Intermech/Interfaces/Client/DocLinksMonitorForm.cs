
// Type: Intermech.Interfaces.Client.DocLinksMonitorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Extensions;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Interfaces.Client;

/// <summary>
/// Форма, отображающая в какие документы связью Состав документации входят документы, на которые надо поставить гриф
/// </summary>
public class DocLinksMonitorForm : Form
{
  private Dictionary<long, List<long>> _docsEntersIn;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer;
  private Panel btnPanel;
  private Button btnOK;
  private Button btnCancel;
  private TreeView treeView1;
  private ObjectsViewBase objectsViewBase1;

  public DocLinksMonitorForm()
  {
    this.InitializeComponent();
    this.treeView1.ImageList = ((ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService))).ImageList;
  }

  public void Init(Dictionary<long, List<long>> docsEntersIn)
  {
    this._docsEntersIn = docsEntersIn;
    this.InitTreeView();
  }

  private void InitTreeView()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.treeView1.SuspendDrawing();
      this.treeView1.Nodes.Clear();
      foreach (KeyValuePair<long, List<long>> keyValuePair in this._docsEntersIn)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(keyValuePair.Key, false);
        if (dbObject != null)
        {
          int num = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
          this.treeView1.Nodes.Add(dbObject.ObjectID.ToString(), dbObject.Caption, num, num).Tag = (object) dbObject.ObjectID;
        }
      }
      this.treeView1.ResumeDrawing();
    }
  }

  private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.ShowLinkedDocs((long) Convert.ToInt32(e.Node.Tag));
  }

  /// <summary>Показать в гриде связанные с объектом документы</summary>
  /// <param name="objectId"></param>
  private void ShowLinkedDocs(long objectId)
  {
    List<long> objectIDs = this._docsEntersIn.Where<KeyValuePair<long, List<long>>>((Func<KeyValuePair<long, List<long>>, bool>) (doc => doc.Key == objectId)).ToList<KeyValuePair<long, List<long>>>().First<KeyValuePair<long, List<long>>>().Value;
    this.objectsViewBase1.Initialize((IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545"), string.Empty, (IList) objectIDs), (System.IServiceProvider) this.objectsViewBase1.Services);
    this.objectsViewBase1.AutoScroll = true;
    this.objectsViewBase1.Activate((IView) null);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.splitContainer = new SplitContainer();
    this.treeView1 = new TreeView();
    this.objectsViewBase1 = new ObjectsViewBase();
    this.btnPanel = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.btnPanel.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer.Location = new Point(0, 0);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.AutoScroll = true;
    this.splitContainer.Panel1.Controls.Add((Control) this.treeView1);
    this.splitContainer.Panel2.AutoScroll = true;
    this.splitContainer.Panel2.Controls.Add((Control) this.objectsViewBase1);
    this.splitContainer.Size = new Size(817, 384);
    this.splitContainer.SplitterDistance = 270;
    this.splitContainer.TabIndex = 0;
    this.treeView1.Dock = DockStyle.Fill;
    this.treeView1.Location = new Point(0, 0);
    this.treeView1.Name = "treeView1";
    this.treeView1.Size = new Size(270, 384);
    this.treeView1.TabIndex = 0;
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
    this.objectsViewBase1.AllowCustomGroupValues = true;
    this.objectsViewBase1.AutoScroll = true;
    this.objectsViewBase1.Control = (object) this.objectsViewBase1;
    this.objectsViewBase1.DisableKeyDownEvents = false;
    this.objectsViewBase1.Dock = DockStyle.Fill;
    this.objectsViewBase1.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.objectsViewBase1.Font = new Font("Tahoma", 8.25f);
    this.objectsViewBase1.Location = new Point(0, 0);
    this.objectsViewBase1.Name = "objectsViewBase1";
    this.objectsViewBase1.Size = new Size(543, 384);
    this.objectsViewBase1.TabIndex = 0;
    this.objectsViewBase1.ViewContentType = ContentType.NonFolders;
    this.btnPanel.Controls.Add((Control) this.btnOK);
    this.btnPanel.Controls.Add((Control) this.btnCancel);
    this.btnPanel.Dock = DockStyle.Bottom;
    this.btnPanel.Location = new Point(0, 390);
    this.btnPanel.Name = "btnPanel";
    this.btnPanel.Size = new Size(817, 47);
    this.btnPanel.TabIndex = 1;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(468, 12);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(164, 23);
    this.btnOK.TabIndex = 21;
    this.btnOK.Text = "Назначить гриф документам";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(638, 12);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(167, 23);
    this.btnCancel.TabIndex = 20;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(817, 437);
    this.Controls.Add((Control) this.btnPanel);
    this.Controls.Add((Control) this.splitContainer);
    this.Name = nameof (DocLinksMonitorForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Анализ входимости документов связью 'Состав документации'";
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.btnPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
