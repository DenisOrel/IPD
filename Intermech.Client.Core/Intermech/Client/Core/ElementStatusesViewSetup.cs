
// Type: Intermech.Client.Core.ElementStatusesViewSetup
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Настройка отображаемых статусов элементов</summary>
/// <summary>Настройка отображаемых статусов элементов</summary>
public class ElementStatusesViewSetup : Form
{
  /// <summary>Список Guid запрещённых плагинов</summary>
  private List<string> FPlugins;
  /// <summary>Служба по управлению статусами элементов</summary>
  private IElementStatusesClientService _elementStatusesClientService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private ListView listPlugins;
  private ColumnHeader columnPlugins;
  private ColumnHeader columnStatus;
  private ImageList imagesList;

  /// <summary>Конструктор</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="disabledPlugins">Список Guid плагинов, которые запрещены для выдачи статусов</param>
  public ElementStatusesViewSetup(string FormCaption, ref List<string> disabledPlugins)
  {
    this.InitializeComponent();
    if (disabledPlugins == null)
      disabledPlugins = new List<string>(0);
    this._elementStatusesClientService = ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    FormStorage.LoadLayout((Control) this);
    this.Text = FormCaption;
    this.FPlugins = disabledPlugins;
    this.FillPluginsList(disabledPlugins);
    this.UpdateControls();
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
    this.btnApply.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ElementStatusesViewSetup_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="selectedPlugins">Список Guid плагинов, которые выбраны для выдачи статусов</param>
  /// <returns>Результ вызова формы</returns>
  public static DialogResult Execute(string FormCaption, ref List<string> selectedPlugins)
  {
    using (ElementStatusesViewSetup statusesViewSetup = new ElementStatusesViewSetup(FormCaption, ref selectedPlugins))
      return statusesViewSetup.ShowDialog();
  }

  /// <summary>Очистка внутренних структур</summary>
  internal void Clear()
  {
  }

  /// <summary>Пересчёт ширины колонок в списке плагинов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void listPlugins_Resize(object sender, EventArgs e)
  {
    int num = this.listPlugins.ClientRectangle.Width - 30 - this.listPlugins.Columns[0].Width;
    if (num <= 0)
      return;
    this.listPlugins.Columns[1].Width = num;
  }

  /// <summary>
  /// Заполнить список плагинов, выделить в списке плагины, к которым идёт обращение за статусами
  /// </summary>
  /// <param name="disabledPlugins">Список Guid плагинов, которые запрещены для выдачи статусов</param>
  private void FillPluginsList(List<string> disabledPlugins)
  {
    try
    {
      this.listPlugins.BeginUpdate();
      this.listPlugins.Items.Clear();
      if (this._elementStatusesClientService == null)
        return;
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this._elementStatusesClientService.Plugins.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        ElementStatusesPluginDescription pluginDescription = enumerator.Value as ElementStatusesPluginDescription;
        ListViewItem listViewItem = this.listPlugins.Items.Add(pluginDescription.PluginName, 0);
        listViewItem.SubItems.Add(pluginDescription.StatusesDescription);
        listViewItem.Checked = disabledPlugins != null && disabledPlugins.IndexOf(pluginDescription.PluginGuid) < 0;
        listViewItem.Tag = (object) pluginDescription.PluginGuid;
      }
    }
    finally
    {
      this.listPlugins.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (this.FPlugins != null)
    {
      this.FPlugins.Clear();
      for (int index = 0; index < this.listPlugins.Items.Count; ++index)
      {
        ListViewItem listViewItem = this.listPlugins.Items[index];
        if (!listViewItem.Checked)
          this.FPlugins.Add((string) listViewItem.Tag);
      }
    }
    this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ElementStatusesViewSetup));
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.listPlugins = new ListView();
    this.columnPlugins = new ColumnHeader();
    this.columnStatus = new ColumnHeader();
    this.imagesList = new ImageList(this.components);
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Hand;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.listPlugins.CheckBoxes = true;
    this.listPlugins.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnPlugins,
      this.columnStatus
    });
    componentResourceManager.ApplyResources((object) this.listPlugins, "listPlugins");
    this.listPlugins.FullRowSelect = true;
    this.listPlugins.GridLines = true;
    this.listPlugins.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listPlugins.HideSelection = false;
    this.listPlugins.LargeImageList = this.imagesList;
    this.listPlugins.MultiSelect = false;
    this.listPlugins.Name = "listPlugins";
    this.listPlugins.SmallImageList = this.imagesList;
    this.listPlugins.UseCompatibleStateImageBehavior = false;
    this.listPlugins.View = View.Details;
    this.listPlugins.Resize += new EventHandler(this.listPlugins_Resize);
    componentResourceManager.ApplyResources((object) this.columnPlugins, "columnPlugins");
    componentResourceManager.ApplyResources((object) this.columnStatus, "columnStatus");
    this.imagesList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesList.ImageStream");
    this.imagesList.TransparentColor = Color.Transparent;
    this.imagesList.Images.SetKeyName(0, "plugin.ico");
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.listPlugins);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (ElementStatusesViewSetup);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.ElementStatusesViewSetup_FormClosed);
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
