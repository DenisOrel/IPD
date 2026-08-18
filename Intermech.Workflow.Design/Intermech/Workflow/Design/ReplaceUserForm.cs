// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ReplaceUserForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ReplaceUserForm : Form
{
  private static IUserNamesCache _userNamesCache;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Button userButton;
  private Button toUserButton;
  private Label label2;
  private Button CancButton;
  private Button OkButton;
  private Button swapButton;
  private ToolTip toolTip;

  public ReplaceUserForm() => this.InitializeComponent();

  protected static IUserNamesCache UserNamesCache
  {
    get
    {
      if (ReplaceUserForm._userNamesCache == null)
        ReplaceUserForm._userNamesCache = CacheManager.Cache(nameof (UserNamesCache)) as IUserNamesCache;
      return ReplaceUserForm._userNamesCache != null ? ReplaceUserForm._userNamesCache : throw new Exception("UserNamesCache needed!");
    }
  }

  private void UpdateButtons() => this.OkButton.Enabled = this.UserID > 0L && this.ToUserID > 0L;

  private void userButton_Click(object sender, EventArgs e)
  {
    if (!(sender is Button button))
      return;
    long[] objects = new long[1]
    {
      button.Tag != null ? Convert.ToInt64(button.Tag) : 0L
    };
    IDBObjectID[] dbObjectIdArray;
    if (sender == this.userButton)
    {
      List<long> objectIDs = new List<long>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-9, RelationalOperators.Equal, (object) sessionKeeper.Session.IdentHelper.AnnulmentLevelID, LogicalOperators.NONE, 0, false)
        };
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.UserTypeID);
        objectCollection.TrashMode = true;
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(new DBRecordSetParams(conditions, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        })).Rows)
        {
          object obj = row.ItemArray[0];
          objectIDs.Add(Convert.ToInt64(obj));
        }
      }
      ListDescriptor listDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, "Уволенные пользователи", (IList) objectIDs);
      dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
      {
        wfConsts.UserTypeID
      }, objects, false, true, true, true, (IDescriptor) listDescriptor);
    }
    else
      dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
      {
        wfConsts.UserTypeID
      }, objects, false, true);
    if (dbObjectIdArray != null)
    {
      button.Text = dbObjectIdArray[0].Caption;
      button.Tag = (object) dbObjectIdArray[0].Value;
    }
    this.UpdateButtons();
  }

  public long UserID
  {
    get => this.userButton.Tag == null ? 0L : Convert.ToInt64(this.userButton.Tag);
    set
    {
      if (value == 0L)
        return;
      this.userButton.Tag = (object) value;
      this.userButton.Text = ReplaceUserForm.UserNamesCache.GetUserName(value);
    }
  }

  public long ToUserID
  {
    get => this.toUserButton.Tag == null ? 0L : Convert.ToInt64(this.toUserButton.Tag);
    set
    {
      if (value == 0L)
        return;
      this.toUserButton.Tag = (object) value;
      this.toUserButton.Text = ReplaceUserForm.UserNamesCache.GetUserName(value);
    }
  }

  private void ReplaceUserForm_Load(object sender, EventArgs e)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    dictionary.Add("UserID", (object) 0);
    dictionary.Add("ToUserID", (object) 0);
    try
    {
      FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    }
    catch
    {
    }
    this.UserID = Convert.ToInt64(dictionary["UserID"]);
    this.ToUserID = Convert.ToInt64(dictionary["ToUserID"]);
    this.UpdateButtons();
  }

  private void ReplaceUserForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, object>()
    {
      {
        "UserID",
        (object) this.UserID
      },
      {
        "ToUserID",
        (object) this.ToUserID
      }
    });
  }

  private void swapButton_Click(object sender, EventArgs e)
  {
    object tag = this.userButton.Tag;
    string text = this.userButton.Text;
    this.userButton.Tag = this.toUserButton.Tag;
    this.userButton.Text = this.toUserButton.Text;
    this.toUserButton.Tag = tag;
    this.toUserButton.Text = text;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReplaceUserForm));
    this.label1 = new Label();
    this.userButton = new Button();
    this.toUserButton = new Button();
    this.label2 = new Label();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.swapButton = new Button();
    this.toolTip = new ToolTip(this.components);
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(10, 25);
    this.label1.Name = "label1";
    this.label1.Size = new Size(134, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Заменить пользователя:";
    this.userButton.Location = new Point(173, 20);
    this.userButton.Name = "userButton";
    this.userButton.Size = new Size(278, 23);
    this.userButton.TabIndex = 1;
    this.userButton.Text = "...";
    this.userButton.UseVisualStyleBackColor = true;
    this.userButton.Click += new EventHandler(this.userButton_Click);
    this.toUserButton.Location = new Point(173, 53);
    this.toUserButton.Name = "toUserButton";
    this.toUserButton.Size = new Size(278, 23);
    this.toUserButton.TabIndex = 3;
    this.toUserButton.Text = "...";
    this.toUserButton.UseVisualStyleBackColor = true;
    this.toUserButton.Click += new EventHandler(this.userButton_Click);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(10, 58);
    this.label2.Name = "label2";
    this.label2.Size = new Size(98, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "На пользователя:";
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.ImeMode = ImeMode.NoControl;
    this.CancButton.Location = new Point(376, 96 /*0x60*/);
    this.CancButton.Name = "CancButton";
    this.CancButton.Size = new Size(75, 23);
    this.CancButton.TabIndex = 6;
    this.CancButton.Text = "Отмена";
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.ImeMode = ImeMode.NoControl;
    this.OkButton.Location = new Point(295, 96 /*0x60*/);
    this.OkButton.Name = "OkButton";
    this.OkButton.Size = new Size(75, 23);
    this.OkButton.TabIndex = 5;
    this.OkButton.Text = "OK";
    this.swapButton.FlatAppearance.BorderSize = 0;
    this.swapButton.FlatStyle = FlatStyle.Flat;
    this.swapButton.ForeColor = SystemColors.ControlText;
    this.swapButton.Image = (Image) componentResourceManager.GetObject("swapButton.Image");
    this.swapButton.Location = new Point(457, 39);
    this.swapButton.Name = "swapButton";
    this.swapButton.Size = new Size(18, 18);
    this.swapButton.TabIndex = 7;
    this.toolTip.SetToolTip((Control) this.swapButton, "Поменять местами");
    this.swapButton.UseVisualStyleBackColor = true;
    this.swapButton.Click += new EventHandler(this.swapButton_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(485, 136);
    this.Controls.Add((Control) this.swapButton);
    this.Controls.Add((Control) this.CancButton);
    this.Controls.Add((Control) this.OkButton);
    this.Controls.Add((Control) this.toUserButton);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.userButton);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReplaceUserForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Замена исполнителя";
    this.FormClosed += new FormClosedEventHandler(this.ReplaceUserForm_FormClosed);
    this.Load += new EventHandler(this.ReplaceUserForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
