// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AutoPlaceInArchiveView.ArchiveAutoPlaceSettingsIntersection
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
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
/// Форма отображения пересечений настроек с другими архивами.
/// </summary>
public class ArchiveAutoPlaceSettingsIntersection : Form
{
  /// <summary>Словарь с найденными пересечениями</summary>
  private Dictionary<long, TypesAndUsers> _archIntersectionDict;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Label label1;
  private TreeView _intersectTree;

  /// <summary>Конструктор</summary>
  public ArchiveAutoPlaceSettingsIntersection(
    Dictionary<long, TypesAndUsers> archIntersectionDict)
  {
    this.InitializeComponent();
    this._archIntersectionDict = archIntersectionDict;
  }

  /// <summary>Кнопка ОК - закрываем форму.</summary>
  private void btnOK_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Загрузка формы</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e"></param>
  private void ArchiveAutoPlaceSettingsIntersection_Load(object sender, EventArgs e)
  {
    this.CreateIntersectionTree();
    this._intersectTree.ImageList = Statics.IconSrv == null ? (ImageList) null : Statics.IconSrv.ImageList;
    this._intersectTree.ExpandAll();
  }

  /// <summary>
  /// Создает дерево, отображающее пересечения настроек архивов.
  /// </summary>
  private void CreateIntersectionTree()
  {
    IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    foreach (KeyValuePair<long, TypesAndUsers> keyValuePair in this._archIntersectionDict)
    {
      long key = keyValuePair.Key;
      QuickObjectInfo objectInfo = service.GetObjectInfo(key);
      int num = 0;
      if (Statics.IconSrv != null)
        num = Statics.IconSrv.IndexOf(4, ConstsHolder.ArcTypeID);
      TreeNode treeNode = new TreeNode(objectInfo.Caption, num, num);
      this._intersectTree.Nodes.Add(treeNode);
      this.FillArchiveNode(treeNode, keyValuePair.Value);
    }
  }

  /// <summary>Заполняет узел архива типами и пользователями.</summary>
  /// <param name="archiveNode">Узел архива.</param>
  /// <param name="typesAndUsers">Типы и пользователи.</param>
  /// <param name="sk">Сессия.</param>
  private void FillArchiveNode(TreeNode archiveNode, TypesAndUsers typesAndUsers)
  {
    int num1 = 0;
    int num2 = 0;
    if (Statics.IconSrv != null)
    {
      num1 = Statics.IconSrv.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545")));
      num2 = Statics.IconSrv.IndexOf(4, ConstsHolder.DocTypeID);
    }
    TreeNode node1 = new TreeNode(ServiceHolder.rm.GetString("Archives_186"), num1, num1);
    archiveNode.Nodes.Add(node1);
    TreeNode node2 = new TreeNode(ServiceHolder.rm.GetString("Archives_185"), num2, num2);
    archiveNode.Nodes.Add(node2);
    IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    foreach (long userId in typesAndUsers.UserIDs)
    {
      QuickObjectInfo objectInfo = service.GetObjectInfo(userId);
      int num3 = 0;
      if (Statics.IconSrv != null)
        num3 = Statics.IconSrv.IndexOf(4, objectInfo.ObjectTypeID);
      TreeNode node3 = new TreeNode(objectInfo.Caption, num3, num3);
      node1.Nodes.Add(node3);
    }
    foreach (int docTypeId in typesAndUsers.DocTypeIDs)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(docTypeId);
      if (objectType != null)
      {
        int num4 = 0;
        if (Statics.IconSrv != null)
          num4 = Statics.IconSrv.IndexOf(4, docTypeId);
        TreeNode node4 = new TreeNode(objectType.ObjectTypeName, num4, num4);
        node2.Nodes.Add(node4);
      }
    }
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
    this.btnOK = new Button();
    this.label1 = new Label();
    this._intersectTree = new TreeView();
    this.SuspendLayout();
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.Location = new Point(363, 246);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(90, 27);
    this.btnOK.TabIndex = 0;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(13, 13);
    this.label1.Name = "label1";
    this.label1.Size = new Size(441, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Настройки, которые уже есть в системе, не могут быть добавлены в текущий архив.";
    this._intersectTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._intersectTree.Location = new Point(16 /*0x10*/, 40);
    this._intersectTree.Name = "_intersectTree";
    this._intersectTree.Size = new Size(436, 193);
    this._intersectTree.TabIndex = 2;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(471, 285);
    this.Controls.Add((Control) this._intersectTree);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnOK);
    this.MinimumSize = new Size(487, 323);
    this.Name = nameof (ArchiveAutoPlaceSettingsIntersection);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Пересечение настроек авторазмещения с другими архивами";
    this.Load += new EventHandler(this.ArchiveAutoPlaceSettingsIntersection_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
