// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AutoPlaceInArchiveView.WrongIdForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.AutoPlaceInArchiveView;

/// <summary>
/// Форма, информирующая о типах, не добавленных в списки авторазмещения
/// </summary>
public class WrongIdForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView lvDocTypes;
  private ColumnHeader docTypes;
  private Label label1;
  private Button button1;

  /// <summary>Конструктор</summary>
  public WrongIdForm(List<int> wrongIDs)
  {
    this.InitializeComponent();
    this.lvDocTypes.SmallImageList = Statics.IconSrv == null ? (ImageList) null : Statics.IconSrv.ImageList;
    this.lvDocTypes.BeginUpdate();
    this.lvDocTypes.Items.Clear();
    foreach (int wrongId in wrongIDs)
    {
      if (wrongId != -1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(wrongId);
        if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
        {
          ListViewItem listViewItem = new ListViewItem(objectType.ObjectTypeName);
          listViewItem.Tag = (object) wrongId;
          if (Statics.IconSrv != null)
          {
            int num = Statics.IconSrv.IndexOf(4, wrongId);
            listViewItem.ImageIndex = num;
          }
          this.lvDocTypes.Items.Add(listViewItem);
        }
      }
    }
    this.lvDocTypes.EndUpdate();
    this.lvDocTypes.Refresh();
  }

  private void button1_Click(object sender, EventArgs e) => this.Close();

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
    this.lvDocTypes = new ListView();
    this.docTypes = new ColumnHeader();
    this.label1 = new Label();
    this.button1 = new Button();
    this.SuspendLayout();
    this.lvDocTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.docTypes
    });
    this.lvDocTypes.Location = new Point(11, 56);
    this.lvDocTypes.Name = "lvDocTypes";
    this.lvDocTypes.Size = new Size(303, 256 /*0x0100*/);
    this.lvDocTypes.TabIndex = 2;
    this.lvDocTypes.UseCompatibleStateImageBehavior = false;
    this.lvDocTypes.View = View.Details;
    this.docTypes.Text = "Типы документов";
    this.docTypes.Width = 327;
    this.label1.Dock = DockStyle.Top;
    this.label1.Location = new Point(0, 0);
    this.label1.Name = "label1";
    this.label1.Padding = new Padding(10, 12, 5, 0);
    this.label1.Size = new Size(326, 53);
    this.label1.TabIndex = 3;
    this.label1.Text = "Следующие типы не добавлены в список из-за конфликта с настройкой Разрешенные типы документов:";
    this.button1.Location = new Point(225, 329);
    this.button1.Name = "button1";
    this.button1.Size = new Size(90, 27);
    this.button1.TabIndex = 4;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(326, 368);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.lvDocTypes);
    this.MaximumSize = new Size(342, 406);
    this.Name = nameof (WrongIdForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Конфликтные типы";
    this.ResumeLayout(false);
  }
}
