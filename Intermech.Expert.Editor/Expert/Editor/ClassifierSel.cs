// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ClassifierSel
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class ClassifierSel : Form
{
  private long curId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private ClassifyingControl classif;

  public ClassifierSel() => this.InitializeComponent();

  public bool Execute(ref long classifId)
  {
    this.FillTreeList();
    if (classifId != 0L && classifId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.classif.SelectClassifier(sessionKeeper.Session, classifId);
    }
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    classifId = this.curId;
    return true;
  }

  private void FillTreeList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] classifierForObjType = (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetClassifierForObjType((object) sessionKeeper.Session.SessionGUID, -1);
      if (classifierForObjType == null || classifierForObjType.Length == 0)
        return;
      this.classif.RootClassifiers = classifierForObjType;
    }
  }

  private void classif_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (!(this.classif.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData) || itemData.Value == this.curId)
      return;
    this.curId = itemData.Value;
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
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.classif = new ClassifyingControl();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.classif).BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 487);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(775, 29);
    this.panel1.TabIndex = 0;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.Location = new Point(607, 3);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "Да";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.Location = new Point(688, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.classif.Dock = DockStyle.Fill;
    this.classif.Location = new Point(0, 0);
    this.classif.Margin = new Padding(4);
    this.classif.Name = "classif";
    this.classif.Size = new Size(775, 487);
    this.classif.SupportedEvents = IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    this.classif.TabIndex = 1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(775, 516);
    this.Controls.Add((Control) this.classif);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ClassifierSel);
    this.Text = "Выбор папки классификатора";
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.classif).EndInit();
    this.ResumeLayout(false);
  }
}
