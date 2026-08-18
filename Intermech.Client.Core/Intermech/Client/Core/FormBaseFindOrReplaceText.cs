
// Type: Intermech.Client.Core.FormBaseFindOrReplaceText
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Класс для поиска текста в чём-либо </summary>
public class FormBaseFindOrReplaceText : FormBaseFindOrReplace
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private UserControlFindReplaceText _userControlFindReplaceText;

  public FormBaseFindOrReplaceText() => this.InitializeComponent();

  /// <summary>
  /// Получение ссылки на объект, который реализует всю функциональность по настройке поиска
  /// </summary>
  public override object InterfaceObject => (object) this._userControlFindReplaceText;

  /// <summary> Сохранить выбранные пользователем настройки поиска для последующего востановления </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public override void SaveConfiguration(IConfiguration iConfiguration)
  {
    this._userControlFindReplaceText.SaveConfiguration(iConfiguration);
  }

  /// <summary> Востановление настроек поиска из ранее сохнанённых </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public override void LoadConfiguration(IConfiguration iConfiguration)
  {
    this._userControlFindReplaceText.LoadConfiguration(iConfiguration);
  }

  /// <summary> </summary>
  protected override void AfterShow()
  {
    this._userControlFindReplaceText.Location = new Point(11, 28);
    UserControlFindReplaceText controlFindReplaceText = this._userControlFindReplaceText;
    Size clientSize = this.ClientSize;
    int width = clientSize.Width - 12;
    clientSize = this.ClientSize;
    int height = clientSize.Height - 28;
    Size size = new Size(width, height);
    controlFindReplaceText.Size = size;
    base.AfterShow();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBaseFindOrReplaceText));
    this._userControlFindReplaceText = new UserControlFindReplaceText();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._tabControlFindOrReplace, "_tabControlFindOrReplace");
    componentResourceManager.ApplyResources((object) this._userControlFindReplaceText, "_userControlFindReplaceText");
    this._userControlFindReplaceText.BackColor = SystemColors.Window;
    this._userControlFindReplaceText.FindWhat = "";
    this._userControlFindReplaceText.MatchCase = false;
    this._userControlFindReplaceText.MatchWholeWord = false;
    this._userControlFindReplaceText.MinimumSize = new Size(513, 295);
    this._userControlFindReplaceText.Name = "_userControlFindReplaceText";
    this._userControlFindReplaceText.PossibleSearchPlaces = new string[0];
    this._userControlFindReplaceText.ReplaceWith = "";
    this._userControlFindReplaceText.SearchDirrection = SearchDirrection.EntireDocSearch;
    this._userControlFindReplaceText.SelectedSearchPlace = -1;
    this._userControlFindReplaceText.Tag = (object) "  ";
    this._userControlFindReplaceText.UseRegularExpressions = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._userControlFindReplaceText);
    this.Name = nameof (FormBaseFindOrReplaceText);
    this.Controls.SetChildIndex((Control) this._tabControlFindOrReplace, 0);
    this.Controls.SetChildIndex((Control) this._userControlFindReplaceText, 0);
    this.ResumeLayout(false);
  }
}
