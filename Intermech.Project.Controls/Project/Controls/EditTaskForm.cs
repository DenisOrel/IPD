// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EditTaskForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.UI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Карточка задачи</summary>
public class EditTaskForm : ProjectDialogBase
{
  private readonly TaskView.Page _initialPage;
  private TaskView _taskView;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TaskView TaskView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._taskView.CheckInitializedIn<TaskView>((object) this);
    }
  }

  public EditTaskForm()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    Intermech.Client.Core.FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.TaskView.Layout = dictionary;
  }

  /// <summary>Конструктор</summary>
  /// <param name="initialParamName">Имя параметра задачи, для которого подбирается закладка, которая сразу будет показана
  /// пользователю. Изначально делалось для того, чтобы по двойному клику в гриде по параметру, напр. "Ресурсы",
  /// открывалась карточка задачи с нужной активированной закладкой, напр. "Ресурсы"</param>
  public EditTaskForm([CanBeNull] string initialParamName)
    : this()
  {
    if (this.DesignMode || string.IsNullOrWhiteSpace(initialParamName))
      return;
    this._initialPage = TaskView.ParamsBinding2Page.GetOrDefaultReadOnly<string, TaskView.Page>(initialParamName);
  }

  public bool EditTask([NotNull] Task task, bool readOnly)
  {
    this.TaskView.LoadFromTask(task, readOnly);
    if (task is Intermech.Project.Project)
      this.Text = Localization.GetString("ProjectProps");
    string str = task.Name;
    if (str != string.Empty)
      str = $" \"{str}\"";
    this.Text += str;
    if (readOnly)
      this.ActiveControl = (Control) this._okButton;
    else
      this.FormClosing += new FormClosingEventHandler(this.EditTaskForm_FormClosing);
    if (this._initialPage != TaskView.Page.None)
    {
      this.TaskView.ActivatePage(this._initialPage);
      this.TaskView.LoadSelectedPage();
    }
    bool flag = this.ShowDialog() == DialogResult.OK;
    if (flag & readOnly)
      flag = false;
    return flag;
  }

  private void EditTaskForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this, (IDictionary) this.TaskView.Layout);
    if (this.DialogResult != DialogResult.OK)
      return;
    this.TaskView.SaveToTask(this.TaskView.Task);
  }

  /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this._taskView = new TaskView();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 308);
    this._pnlDialogButtons.Size = new Size(658, 36);
    this._cancelButton.TabIndex = 2;
    this._okButton.TabIndex = 1;
    this._bevelDialogButtons.Location = new Point(0, 306);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(658, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(485, 0);
    this._taskView.Dock = DockStyle.Fill;
    this._taskView.Location = new Point(0, 0);
    this._taskView.Name = "_taskView";
    this._taskView.Size = new Size(658, 344);
    this._taskView.TabIndex = 0;
    this._taskView.Tag = (object) "";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(658, 344);
    this.Controls.Add((Control) this._taskView);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    this.MinimumSize = new Size(674, 382);
    this.Name = nameof (EditTaskForm);
    this.Text = "Свойства";
    this.Controls.SetChildIndex((Control) this._taskView, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
