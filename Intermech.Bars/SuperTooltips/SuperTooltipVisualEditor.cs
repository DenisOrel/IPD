
// Type: SuperTooltips.SuperTooltipVisualEditor
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace SuperTooltips
{
    [ToolboxItem(false)]
    public class SuperTooltipVisualEditor : UserControl
    {
      private IContainer _container;
      private Panel mainPanel;
      private CheckBox cbFooter;
      private CheckBox cbHeader;
      private TextBox edFooter;
      private TextBox mainText;
      private TextBox edHeader;
      private Panel bodyImagePanel;
      private Button previewPanel;
      private ComboBox cbColors;
      private Label lbColors;
      private Panel footerImagePanel;
      private CheckBox cbCustomSize;
      private NumericUpDown szHeight;
      private NumericUpDown szWidth;
      private Label lbX;
      private SuperTooltipInfo _tooltipInfo;
      private bool _canceled;
      private IWindowsFormsEditorService _editorService;
      private bool _bodyImageAssigned;
      private bool _footerImageAssigned;
      private SuperTooltipControl _tooltipPreview;
      private Button resetImage;
      private Button btResetFooter;
      private SuperTooltip superToolTip;
      private Button button4;
      private Button button3;
      private CustomTypeEditorProvider _editorProvider;

      public SuperTooltipVisualEditor()
      {
        this._container = (IContainer) null;
        this.InitializeComponent();
        this.bodyImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder;
        this.footerImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder16x16;
        this.cbColors.Items.AddRange((object[]) Enum.GetNames(typeof (TooltipColorScheme)));
        this._canceled = true;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this._container != null)
          this._container.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SuperTooltipVisualEditor));
        this.mainPanel = new Panel();
        this.button4 = new Button();
        this.button3 = new Button();
        this.btResetFooter = new Button();
        this.resetImage = new Button();
        this.szWidth = new NumericUpDown();
        this.szHeight = new NumericUpDown();
        this.lbX = new Label();
        this.cbCustomSize = new CheckBox();
        this.footerImagePanel = new Panel();
        this.cbColors = new ComboBox();
        this.lbColors = new Label();
        this.previewPanel = new Button();
        this.bodyImagePanel = new Panel();
        this.edFooter = new TextBox();
        this.mainText = new TextBox();
        this.edHeader = new TextBox();
        this.cbFooter = new CheckBox();
        this.cbHeader = new CheckBox();
        this.superToolTip = new SuperTooltip();
        this.mainPanel.SuspendLayout();
        this.szWidth.BeginInit();
        this.szHeight.BeginInit();
        this.SuspendLayout();
        this.mainPanel.AccessibleDescription = (string) null;
        this.mainPanel.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.mainPanel, "mainPanel");
        this.mainPanel.BackColor = SystemColors.Control;
        this.mainPanel.BackgroundImage = (Image) null;
        this.mainPanel.Controls.Add((Control) this.button4);
        this.mainPanel.Controls.Add((Control) this.button3);
        this.mainPanel.Controls.Add((Control) this.btResetFooter);
        this.mainPanel.Controls.Add((Control) this.resetImage);
        this.mainPanel.Controls.Add((Control) this.szWidth);
        this.mainPanel.Controls.Add((Control) this.szHeight);
        this.mainPanel.Controls.Add((Control) this.lbX);
        this.mainPanel.Controls.Add((Control) this.cbCustomSize);
        this.mainPanel.Controls.Add((Control) this.footerImagePanel);
        this.mainPanel.Controls.Add((Control) this.cbColors);
        this.mainPanel.Controls.Add((Control) this.lbColors);
        this.mainPanel.Controls.Add((Control) this.previewPanel);
        this.mainPanel.Controls.Add((Control) this.bodyImagePanel);
        this.mainPanel.Controls.Add((Control) this.edFooter);
        this.mainPanel.Controls.Add((Control) this.mainText);
        this.mainPanel.Controls.Add((Control) this.edHeader);
        this.mainPanel.Controls.Add((Control) this.cbFooter);
        this.mainPanel.Controls.Add((Control) this.cbHeader);
        this.mainPanel.Font = (Font) null;
        this.mainPanel.Name = "mainPanel";
        this.button4.AccessibleDescription = (string) null;
        this.button4.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.button4, "button4");
        this.button4.BackgroundImage = (Image) null;
        this.button4.DialogResult = DialogResult.Cancel;
        this.button4.Font = (Font) null;
        this.button4.Name = "button4";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new EventHandler(this.OnCancelClick);
        this.button3.AccessibleDescription = (string) null;
        this.button3.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.button3, "button3");
        this.button3.BackgroundImage = (Image) null;
        this.button3.DialogResult = DialogResult.OK;
        this.button3.Font = (Font) null;
        this.button3.Name = "button3";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new EventHandler(this.OnOkClick);
        this.btResetFooter.AccessibleDescription = (string) null;
        this.btResetFooter.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.btResetFooter, "btResetFooter");
        this.btResetFooter.BackgroundImage = (Image) null;
        this.btResetFooter.Font = (Font) null;
        this.btResetFooter.Name = "btResetFooter";
        this.superToolTip.SetSuperTooltip((IComponent) this.btResetFooter, new SuperTooltipInfo("Reset footer image", "", "Click to reset footer image.", (Image) null, (Image) null, TooltipColorScheme.Orange, true, false, new Size(0, 0)));
        this.btResetFooter.Click += new EventHandler(this.ResetFooterImage);
        this.resetImage.AccessibleDescription = (string) null;
        this.resetImage.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.resetImage, "resetImage");
        this.resetImage.BackgroundImage = (Image) null;
        this.resetImage.Font = (Font) null;
        this.resetImage.Name = "resetImage";
        this.superToolTip.SetSuperTooltip((IComponent) this.resetImage, new SuperTooltipInfo("Reset Body Image", "", "Click to reset body image.", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
        this.resetImage.Click += new EventHandler(this.ResetMainImage);
        this.szWidth.AccessibleDescription = (string) null;
        this.szWidth.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.szWidth, "szWidth");
        this.szWidth.Font = (Font) null;
        this.szWidth.Maximum = new Decimal(new int[4]
        {
          10000,
          0,
          0,
          0
        });
        this.szWidth.Minimum = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.szWidth.Name = "szWidth";
        this.szWidth.Value = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.szHeight.AccessibleDescription = (string) null;
        this.szHeight.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.szHeight, "szHeight");
        this.szHeight.Font = (Font) null;
        this.szHeight.Maximum = new Decimal(new int[4]
        {
          10000,
          0,
          0,
          0
        });
        this.szHeight.Minimum = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.szHeight.Name = "szHeight";
        this.szHeight.Value = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.lbX.AccessibleDescription = (string) null;
        this.lbX.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.lbX, "lbX");
        this.lbX.Font = (Font) null;
        this.lbX.Name = "lbX";
        this.cbCustomSize.AccessibleDescription = (string) null;
        this.cbCustomSize.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.cbCustomSize, "cbCustomSize");
        this.cbCustomSize.BackgroundImage = (Image) null;
        this.cbCustomSize.Font = (Font) null;
        this.cbCustomSize.Name = "cbCustomSize";
        this.cbCustomSize.CheckedChanged += new EventHandler(this.Custom_CheckedChanged);
        this.footerImagePanel.AccessibleDescription = (string) null;
        this.footerImagePanel.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.footerImagePanel, "footerImagePanel");
        this.footerImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder16x16;
        this.footerImagePanel.Font = (Font) null;
        this.footerImagePanel.Name = "footerImagePanel";
        this.superToolTip.SetSuperTooltip((IComponent) this.footerImagePanel, new SuperTooltipInfo("Click to set footer image", "", "Allows you to choose footer image. Note that image displayed here is preview image. You can see tooltip preview below.", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(280, 170)));
        this.footerImagePanel.Click += new EventHandler(this.FooterImage_Click);
        this.cbColors.AccessibleDescription = (string) null;
        this.cbColors.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.cbColors, "cbColors");
        this.cbColors.BackgroundImage = (Image) null;
        this.cbColors.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cbColors.Font = (Font) null;
        this.cbColors.Name = "cbColors";
        this.cbColors.Sorted = true;
        this.lbColors.AccessibleDescription = (string) null;
        this.lbColors.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.lbColors, "lbColors");
        this.lbColors.Font = (Font) null;
        this.lbColors.Name = "lbColors";
        this.previewPanel.AccessibleDescription = (string) null;
        this.previewPanel.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.previewPanel, "previewPanel");
        this.previewPanel.BackColor = Color.OldLace;
        this.previewPanel.BackgroundImage = (Image) null;
        this.previewPanel.Font = (Font) null;
        this.previewPanel.Name = "previewPanel";
        this.previewPanel.UseVisualStyleBackColor = false;
        this.previewPanel.MouseLeave += new EventHandler(this.Preview_MouseLeave);
        this.previewPanel.MouseEnter += new EventHandler(this.Preview_MouseEnter);
        this.bodyImagePanel.AccessibleDescription = (string) null;
        this.bodyImagePanel.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.bodyImagePanel, "bodyImagePanel");
        this.bodyImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder;
        this.bodyImagePanel.BorderStyle = BorderStyle.FixedSingle;
        this.bodyImagePanel.Font = (Font) null;
        this.bodyImagePanel.Name = "bodyImagePanel";
        this.superToolTip.SetSuperTooltip((IComponent) this.bodyImagePanel, new SuperTooltipInfo("Click to set body image", "", "Allows you to choose body image. Note that image displayed here is preview image. You can see tooltip preview below", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(280, 170)));
        this.bodyImagePanel.Click += new EventHandler(this.BodyImage_Click);
        this.edFooter.AccessibleDescription = (string) null;
        this.edFooter.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.edFooter, "edFooter");
        this.edFooter.BackgroundImage = (Image) null;
        this.edFooter.Font = (Font) null;
        this.edFooter.Name = "edFooter";
        this.mainText.AcceptsReturn = true;
        this.mainText.AccessibleDescription = (string) null;
        this.mainText.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.mainText, "mainText");
        this.mainText.BackgroundImage = (Image) null;
        this.mainText.Font = (Font) null;
        this.mainText.Name = "mainText";
        this.edHeader.AccessibleDescription = (string) null;
        this.edHeader.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.edHeader, "edHeader");
        this.edHeader.BackgroundImage = (Image) null;
        this.edHeader.Font = (Font) null;
        this.edHeader.Name = "edHeader";
        this.cbFooter.AccessibleDescription = (string) null;
        this.cbFooter.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.cbFooter, "cbFooter");
        this.cbFooter.BackgroundImage = (Image) null;
        this.cbFooter.Font = (Font) null;
        this.cbFooter.Name = "cbFooter";
        this.cbHeader.AccessibleDescription = (string) null;
        this.cbHeader.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this.cbHeader, "cbHeader");
        this.cbHeader.BackgroundImage = (Image) null;
        this.cbHeader.Font = (Font) null;
        this.cbHeader.Name = "cbHeader";
        this.AccessibleDescription = (string) null;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.BackColor = Color.WhiteSmoke;
        this.BackgroundImage = (Image) null;
        this.Controls.Add((Control) this.mainPanel);
        this.Name = nameof (SuperTooltipVisualEditor);
        this.mainPanel.ResumeLayout(false);
        this.mainPanel.PerformLayout();
        this.szWidth.EndInit();
        this.szHeight.EndInit();
        this.ResumeLayout(false);
      }

      private void BodyImage_Click(object sender, EventArgs e)
      {
        Image image = (Image) null;
        if (this._editorProvider != null)
        {
          UITypeEditor editor = (UITypeEditor) TypeDescriptor.GetEditor(typeof (Image), typeof (UITypeEditor));
          this._editorProvider.SetInstance((object) this._tooltipInfo, TypeDescriptor.GetProperties((object) this._tooltipInfo)["BodyImage"]);
          CustomTypeEditorProvider editorProvider1 = this._editorProvider;
          CustomTypeEditorProvider editorProvider2 = this._editorProvider;
          Image bodyImage = this._tooltipInfo.BodyImage;
          image = editor.EditValue((ITypeDescriptorContext) editorProvider1, (System.IServiceProvider) editorProvider2, (object) bodyImage) as Image;
        }
        if (this._tooltipInfo.BodyImage == image)
          return;
        this._tooltipInfo.BodyImage = image;
        if (this._tooltipInfo.BodyImage != null)
          this.bodyImagePanel.BackgroundImage = this._tooltipInfo.BodyImage;
        else
          this.bodyImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder;
        this._bodyImageAssigned = true;
      }

      private void SetData()
      {
        if (this._tooltipInfo == null)
          return;
        this.edHeader.Text = this._tooltipInfo.HeaderText;
        this.cbHeader.Checked = this._tooltipInfo.HeaderVisible;
        this.mainText.Text = this._tooltipInfo.BodyText;
        if (this._tooltipInfo.BodyImage != null)
        {
          this.bodyImagePanel.BackgroundImage = this._tooltipInfo.BodyImage;
          this._bodyImageAssigned = true;
        }
        else
          this.bodyImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder;
        if (this._tooltipInfo.FooterImage != null)
        {
          this.footerImagePanel.BackgroundImage = this._tooltipInfo.FooterImage;
          this._footerImageAssigned = true;
        }
        else
          this.footerImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder16x16;
        this.edFooter.Text = this._tooltipInfo.FooterText;
        this.cbFooter.Checked = this._tooltipInfo.FooterVisible;
        this.cbColors.SelectedItem = (object) Enum.GetName(typeof (TooltipColorScheme), (object) this._tooltipInfo.Color);
        Size customSize = this._tooltipInfo.CustomSize;
        if (customSize.IsEmpty)
        {
          this.cbCustomSize.Checked = false;
        }
        else
        {
          this.cbCustomSize.Checked = true;
          NumericUpDown szWidth = this.szWidth;
          customSize = this._tooltipInfo.CustomSize;
          Decimal width = (Decimal) customSize.Width;
          szWidth.Value = width;
          NumericUpDown szHeight = this.szHeight;
          customSize = this._tooltipInfo.CustomSize;
          Decimal height = (Decimal) customSize.Height;
          szHeight.Value = height;
        }
        this.szHeight.Enabled = this.cbCustomSize.Checked;
        this.szWidth.Enabled = this.cbCustomSize.Checked;
      }

      private void GetData(SuperTooltipInfo tooltipInfo)
      {
        if (tooltipInfo == null)
          return;
        tooltipInfo.HeaderText = this.edHeader.Text;
        tooltipInfo.HeaderVisible = this.cbHeader.Checked;
        tooltipInfo.BodyText = this.mainText.Text;
        tooltipInfo.BodyImage = !this._bodyImageAssigned ? (Image) null : this.bodyImagePanel.BackgroundImage;
        tooltipInfo.FooterImage = !this._footerImageAssigned ? (Image) null : this.footerImagePanel.BackgroundImage;
        tooltipInfo.FooterText = this.edFooter.Text;
        tooltipInfo.FooterVisible = this.cbFooter.Checked;
        tooltipInfo.Color = (TooltipColorScheme) Enum.Parse(typeof (TooltipColorScheme), this.cbColors.SelectedItem.ToString());
        if (this.cbCustomSize.Checked)
          tooltipInfo.CustomSize = new Size((int) this.szWidth.Value, (int) this.szHeight.Value);
        else
          tooltipInfo.CustomSize = Size.Empty;
      }

      private void OnCancelClick(object sender, EventArgs e)
      {
        this.DisposeTooltip();
        this._canceled = true;
        ((Form) this.Parent).Close();
      }

      private void OnOkClick(object sender, EventArgs e)
      {
        this.DisposeTooltip();
        this._canceled = false;
        ((Form) this.Parent).Close();
      }

      private void DisposeTooltip()
      {
        if (this._tooltipPreview == null)
          return;
        this._tooltipPreview.Hide();
        this._tooltipPreview.Dispose();
        this._tooltipPreview = (SuperTooltipControl) null;
      }

      private void Preview_MouseEnter(object dender, EventArgs e)
      {
        this.DisposeTooltip();
        this._tooltipPreview = new SuperTooltipControl();
        SuperTooltipInfo superTooltipInfo = new SuperTooltipInfo();
        this.GetData(superTooltipInfo);
        Point screen = this.previewPanel.PointToScreen(new Point(0, this.previewPanel.Height + 1));
        this._tooltipPreview.ShowTooltip(superTooltipInfo, screen.X, screen.Y, true);
      }

      private void Preview_MouseLeave(object sender, EventArgs e) => this.DisposeTooltip();

      private void FooterImage_Click(object sender, EventArgs e)
      {
        Image image = (Image) null;
        if (this._editorProvider != null)
        {
          UITypeEditor editor = (UITypeEditor) TypeDescriptor.GetEditor(typeof (Image), typeof (UITypeEditor));
          this._editorProvider.SetInstance((object) this._tooltipInfo, TypeDescriptor.GetProperties((object) this._tooltipInfo)["FooterImage"]);
          CustomTypeEditorProvider editorProvider1 = this._editorProvider;
          CustomTypeEditorProvider editorProvider2 = this._editorProvider;
          Image footerImage = this._tooltipInfo.FooterImage;
          image = editor.EditValue((ITypeDescriptorContext) editorProvider1, (System.IServiceProvider) editorProvider2, (object) footerImage) as Image;
        }
        if (this._tooltipInfo.FooterImage == image)
          return;
        this._tooltipInfo.FooterImage = image;
        if (this._tooltipInfo.FooterImage != null)
          this.footerImagePanel.BackgroundImage = this._tooltipInfo.FooterImage;
        else
          this.footerImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder16x16;
        this._footerImageAssigned = true;
      }

      private void Custom_CheckedChanged(object sender, EventArgs e)
      {
        this.szWidth.Enabled = this.cbCustomSize.Checked;
        this.szHeight.Enabled = this.cbCustomSize.Checked;
        if (!this.cbCustomSize.Checked)
          return;
        SuperTooltipInfo superTooltipInfo = new SuperTooltipInfo();
        this.GetData(superTooltipInfo);
        SuperTooltipControl superTooltipControl = new SuperTooltipControl();
        superTooltipControl.UpdateWithSuperTooltipInfo(superTooltipInfo);
        superTooltipControl.RecalcSize();
        this.szWidth.Value = (Decimal) superTooltipControl.Width;
        this.szHeight.Value = (Decimal) superTooltipControl.Height;
        superTooltipControl.Dispose();
      }

      private void ResetMainImage(object sender, EventArgs e)
      {
        this._tooltipInfo.BodyImage = (Image) null;
        this._bodyImageAssigned = false;
        this.bodyImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder;
      }

      private void ResetFooterImage(object sender, EventArgs e)
      {
        this._tooltipInfo.FooterImage = (Image) null;
        this._footerImageAssigned = false;
        this.footerImagePanel.BackgroundImage = (Image) Resources.ImagePlaceHolder16x16;
      }

      protected override void OnVisibleChanged(EventArgs e)
      {
        base.OnVisibleChanged(e);
        if (this.Visible)
          return;
        this.DisposeTooltip();
      }

      public bool Canceled => this._canceled;

      public CustomTypeEditorProvider EditorProvider
      {
        get => this._editorProvider;
        set => this._editorProvider = value;
      }

      public IWindowsFormsEditorService EditorService
      {
        get => this._editorService;
        set => this._editorService = value;
      }

      public SuperTooltipInfo SuperTooltipInfo
      {
        get
        {
          SuperTooltipInfo tooltipInfo = new SuperTooltipInfo();
          this.GetData(tooltipInfo);
          return tooltipInfo;
        }
        set
        {
          this._tooltipInfo = value;
          this.SetData();
        }
      }
    }
}
