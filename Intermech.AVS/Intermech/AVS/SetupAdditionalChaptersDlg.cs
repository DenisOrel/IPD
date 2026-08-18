// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupAdditionalChaptersDlg
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Диалог настройки частей спецификации </summary>
public class SetupAdditionalChaptersDlg : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private ImageList imageList1;
  private SetupAdditionalChaptersPanel additionalChaptersPanel;
  private ToolTipController _ReadModeToolTip;
  private bool inView;

  public SetupAdditionalChaptersDlg() => this.InitializeComponent();

  public SetupAdditionalChaptersDlg(bool readOnly)
  {
    this.InitializeComponent();
    this.ReadOnly = readOnly;
  }

  protected override void OnLoad(EventArgs e)
  {
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2717);
    base.OnLoad(e);
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._EditModeToolTip != null)
      {
        this._EditModeToolTip.Dispose();
        this._EditModeToolTip = (ToolTipController) null;
      }
      if (this._ReadModeToolTip != null)
      {
        this._ReadModeToolTip.Dispose();
        this._ReadModeToolTip = (ToolTipController) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модифицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetupAdditionalChaptersDlg));
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this.imageList1 = new ImageList(this.components);
    this.additionalChaptersPanel = new SetupAdditionalChaptersPanel();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(272, 401);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 1;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnOK.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(399, 401);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 2;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._BtnCancel.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.additionalChaptersPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.additionalChaptersPanel.Location = new Point(0, 0);
    this.additionalChaptersPanel.Name = "additionalChaptersPanel";
    this.additionalChaptersPanel.Size = new Size(533, 402);
    this.additionalChaptersPanel.TabIndex = 7;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(533, 435);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this.additionalChaptersPanel);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(292, 250);
    this.Name = nameof (SetupAdditionalChaptersDlg);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Части спецификации";
    this.FormClosing += new FormClosingEventHandler(this.SetupAdditionalChaptersDlg_FormClosing);
    this.ResumeLayout(false);
  }

  /// <summary>Установить форму в вьюшку</summary>
  public void SetInView()
  {
    this.AcceptButton = (IButtonControl) null;
    this.CancelButton = (IButtonControl) null;
    this.inView = true;
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this.ReadOnly |= this.additionalChaptersPanel.ReadOnly;
    this._BtnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._BtnOK.Enabled = !this.ReadOnly && this.additionalChaptersPanel.Changed;
    if (this.inView)
    {
      this._BtnCancel.Text = "Отмена";
      this._BtnOK.Text = "Применить";
      this._BtnCancel.Enabled = !this.ReadOnly;
    }
    if (this._EditModeToolTip == null)
      return;
    if (this.ReadOnly)
    {
      if (this._EditModeToolTip.Active)
      {
        this._EditModeToolTip.Active = false;
        this._ReadModeToolTip.Active = true;
      }
    }
    else if (this._ReadModeToolTip.Active)
    {
      this._ReadModeToolTip.Active = false;
      this._EditModeToolTip.Active = true;
    }
    if (!this.inView)
      return;
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения");
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения");
    this._ReadModeToolTip.Active = false;
  }

  /// <summary> Переинициализация выбранного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      this.additionalChaptersPanel.LoadData();
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void SetupAdditionalChaptersDlg_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.additionalChaptersPanel.SaveChanges();
    if (!this.inView)
      return;
    e.Cancel = true;
  }

  private void _BtnOK_MouseClick(object sender, MouseEventArgs e)
  {
    if (!(sender is Button button))
      return;
    this.DialogResult = button.DialogResult;
    this.Close();
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      Size size = this.Size;
      int width1 = size.Width;
      int x = this._BtnCancel.Location.X;
      size = this._BtnCancel.Size;
      int width2 = size.Width;
      int num = x + width2;
      return width1 - num;
    }
  }
}
