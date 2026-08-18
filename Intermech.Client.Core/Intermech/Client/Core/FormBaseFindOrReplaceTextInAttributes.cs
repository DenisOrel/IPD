
// Type: Intermech.Client.Core.FormBaseFindOrReplaceTextInAttributes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Configuration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Диалог для поиска текста в атрибутах чего-либо </summary>
public class FormBaseFindOrReplaceTextInAttributes : FormBaseFindOrReplace
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private UserControlFindReplaceTextInAttributes userControlFindReplaceTextInAttributes1;
  protected UserControlFindReplaceTextInAttributes _userControlFindReplaceTextInAttributes;

  public FormBaseFindOrReplaceTextInAttributes() => this.InitializeComponent();

  /// <summary>
  /// Получение ссылки на объект, который реализует всю функциональность по настройке поиска
  /// </summary>
  public override object InterfaceObject => (object) this._userControlFindReplaceTextInAttributes;

  /// <summary> Сохранить выбранные пользователем настройки поиска для последующего востановления </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public override void SaveConfiguration(IConfiguration iConfiguration)
  {
    this._userControlFindReplaceTextInAttributes.SaveConfiguration(iConfiguration);
  }

  /// <summary> Востановление настроек поиска из ранее сохнанённых </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public override void LoadConfiguration(IConfiguration iConfiguration)
  {
    this._userControlFindReplaceTextInAttributes.LoadConfiguration(iConfiguration);
  }

  /// <summary> </summary>
  protected override void AfterShow()
  {
    base.AfterShow();
    if (Application.RenderWithVisualStyles)
      this._userControlFindReplaceTextInAttributes.BackColor = SystemColors.Window;
    else
      this._userControlFindReplaceTextInAttributes.BackColor = SystemColors.Control;
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.AutoScrollMinSize = new Size(0, 0);
    this.MinimumSize = new Size(0, 0);
    UserControlFindReplaceTextInAttributes textInAttributes = this._userControlFindReplaceTextInAttributes;
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = base.ProcessCmdKey(ref msg, keyData);
    if (keyData == Keys.Escape)
    {
      this.Close();
      flag = true;
    }
    return flag;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBaseFindOrReplaceTextInAttributes));
    this.userControlFindReplaceTextInAttributes1 = new UserControlFindReplaceTextInAttributes();
    this._userControlFindReplaceTextInAttributes = new UserControlFindReplaceTextInAttributes();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._tabControlFindOrReplace, "_tabControlFindOrReplace");
    componentResourceManager.ApplyResources((object) this.userControlFindReplaceTextInAttributes1, "userControlFindReplaceTextInAttributes1");
    this.userControlFindReplaceTextInAttributes1.BackColor = SystemColors.Window;
    this.userControlFindReplaceTextInAttributes1.FindWhat = "";
    this.userControlFindReplaceTextInAttributes1.MatchCase = false;
    this.userControlFindReplaceTextInAttributes1.MatchWholeWord = false;
    this.userControlFindReplaceTextInAttributes1.Name = "userControlFindReplaceTextInAttributes1";
    this.userControlFindReplaceTextInAttributes1.PossibleSearchPlaces = new string[0];
    this.userControlFindReplaceTextInAttributes1.ReplaceWith = "";
    this.userControlFindReplaceTextInAttributes1.SearchDirrection = SearchDirrection.EntireDocSearch;
    this.userControlFindReplaceTextInAttributes1.SelectedSearchPlace = -1;
    this.userControlFindReplaceTextInAttributes1.Tag = (object) "   ";
    this.userControlFindReplaceTextInAttributes1.UseRegularExpressions = false;
    componentResourceManager.ApplyResources((object) this._userControlFindReplaceTextInAttributes, "_userControlFindReplaceTextInAttributes");
    this._userControlFindReplaceTextInAttributes.BackColor = SystemColors.Window;
    this._userControlFindReplaceTextInAttributes.FindWhat = "";
    this._userControlFindReplaceTextInAttributes.MatchCase = false;
    this._userControlFindReplaceTextInAttributes.MatchWholeWord = false;
    this._userControlFindReplaceTextInAttributes.Name = "_userControlFindReplaceTextInAttributes";
    this._userControlFindReplaceTextInAttributes.PossibleSearchPlaces = new string[0];
    this._userControlFindReplaceTextInAttributes.ReplaceWith = "";
    this._userControlFindReplaceTextInAttributes.SearchDirrection = SearchDirrection.EntireDocSearch;
    this._userControlFindReplaceTextInAttributes.SelectedSearchPlace = -1;
    this._userControlFindReplaceTextInAttributes.Tag = (object) "   ";
    this._userControlFindReplaceTextInAttributes.UseRegularExpressions = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._userControlFindReplaceTextInAttributes);
    this.Name = nameof (FormBaseFindOrReplaceTextInAttributes);
    this.Controls.SetChildIndex((Control) this._tabControlFindOrReplace, 0);
    this.Controls.SetChildIndex((Control) this._userControlFindReplaceTextInAttributes, 0);
    this.ResumeLayout(false);
  }
}
