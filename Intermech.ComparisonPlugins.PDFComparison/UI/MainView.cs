
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.MainView




using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public class MainView : Form, IMainView
    {
      private bool _moving;
      private PointF _mouseOffset;
      private Point _startMouseDragged;
      private IContainer components;
      private TableLayoutPanel tableLayoutPanel1;
      private Panel panel1;
      private LayerView lowLayerView;
      private LayerView topLayerView;
      private ComboBox comboBoxMergeType;
      private Label label7;
      private Label label6;
      private Label label5;
      private GroupBox groupBox1;
      private CustomUpDown customUpDownAngle;
      private CustomUpDown customUpDownOffsetY;
      private CustomUpDown customUpDownOffsetX;
      private Button buttonResetTransform;
      private Button buttonEnableMooving;
      private Label label4;
      private Label label3;
      private Label label2;
      private ComboBox comboBoxZoom;
      private Label label1;
      private ZoomPictureBox zoomPictureBox;

      public event EventHandler ChangedView;

      public float Angle { get; private set; }

      public Point Offset { get; private set; }

      public double Zoom { get; private set; }

      public int ViewType { get; set; } = 2;

      public ILayerView TopLayerView => (ILayerView) this.topLayerView;

      public ILayerView LowLayerView => (ILayerView) this.lowLayerView;

      public void SetImage(Image image)
      {
        this.zoomPictureBox.Image = image;
        this.ResetTransform();
      }

      public void UpdateImage(Image image) => this.zoomPictureBox.UpdateImage(image);

      public MainView(ComparisonProvider comparisonProvider)
      {
        this.InitializeComponent();
        this.TopLayerView.SetColor(Color.Red);
        this.LowLayerView.SetColor(Color.Aqua);
        this.comboBoxMergeType.SelectedIndexChanged += new EventHandler(this.comboBoxMergeType_SelectedIndexChanged);
        MainPresenter mainPresenter = new MainPresenter((IMainView) this, comparisonProvider);
      }

      private void updateControls()
      {
        this.customUpDownOffsetX.Value = (double) this.Offset.X;
        this.customUpDownOffsetY.Value = (double) this.Offset.Y;
        this.customUpDownAngle.Value = (double) this.Angle;
        this.comboBoxZoom.Text = this.Zoom.ToString();
        this.comboBoxMergeType.SelectedIndex = this.ViewType;
      }

      private void ResetTransform()
      {
        this.Angle = 0.0f;
        this.Offset = Point.Empty;
        this.Zoom = 100.0;
        this._mouseOffset = PointF.Empty;
        this.buttonEnableMooving.BackColor = Color.Gainsboro;
        this.zoomPictureBox.AllowUserDrag = true;
        this._moving = false;
        this.updateControls();
      }

      private void comboBoxZoom_Enter(object sender, EventArgs e)
      {
        this.comboBoxZoom.Text = string.Empty;
      }

      private void comboBoxZoom_KeyDown(object sender, KeyEventArgs e)
      {
        double result;
        if (e.KeyData != Keys.Return || !double.TryParse(this.comboBoxZoom.Text, out result))
          return;
        this.Zoom = result;
      }

      private void comboBoxZoom_KeyPress(object sender, KeyPressEventArgs e)
      {
        FormHelper.CheckEnterFormat(e);
      }

      private void comboBoxZoom_Leave(object sender, EventArgs e)
      {
        this.comboBoxZoom.Text = this.Zoom.ToString();
      }

      private void comboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
      {
        double result;
        if (this.comboBoxZoom.Focused && double.TryParse(this.comboBoxZoom.Text, out result))
        {
          this.Zoom = result;
          EventHandler changedView = this.ChangedView;
          if (changedView == null)
            return;
          changedView((object) null, EventArgs.Empty);
        }
        else
          this.comboBoxZoom.Text = this.Zoom.ToString();
      }

      private void customUpDownAngle_ValueChanged(object sender, EventArgs e)
      {
        if (!this.customUpDownAngle.Focused)
          return;
        this.Angle = (float) this.customUpDownAngle.Value;
        EventHandler changedView = this.ChangedView;
        if (changedView == null)
          return;
        changedView((object) null, EventArgs.Empty);
      }

      private void customUpDownOffsetX_ValueChanged(object sender, EventArgs e)
      {
        if (!this.customUpDownOffsetX.Focused)
          return;
        this.Offset = new Point((int) this.customUpDownOffsetX.Value, this.Offset.Y);
        EventHandler changedView = this.ChangedView;
        if (changedView == null)
          return;
        changedView((object) null, EventArgs.Empty);
      }

      private void customUpDownOffsetY_ValueChanged(object sender, EventArgs e)
      {
        if (!this.customUpDownOffsetY.Focused)
          return;
        this.Offset = new Point(this.Offset.X, (int) this.customUpDownOffsetY.Value);
        EventHandler changedView = this.ChangedView;
        if (changedView == null)
          return;
        changedView((object) null, EventArgs.Empty);
      }

      private void comboBoxMergeType_SelectedIndexChanged(object sender, EventArgs e)
      {
        if (this.comboBoxMergeType.Focused)
        {
          this.ViewType = this.comboBoxMergeType.SelectedIndex;
          EventHandler changedView = this.ChangedView;
          if (changedView == null)
            return;
          changedView((object) null, EventArgs.Empty);
        }
        else
          this.comboBoxMergeType.SelectedIndex = this.ViewType;
      }

      private void buttonResetTransform_Click(object sender, EventArgs e)
      {
        this.ResetTransform();
        EventHandler changedView = this.ChangedView;
        if (changedView == null)
          return;
        changedView((object) null, EventArgs.Empty);
      }

      private void buttonEnableMooving_Click(object sender, EventArgs e)
      {
        if (this.zoomPictureBox.AllowUserDrag)
        {
          this.buttonEnableMooving.BackColor = Color.CornflowerBlue;
          this.zoomPictureBox.AllowUserDrag = false;
        }
        else
        {
          this.buttonEnableMooving.BackColor = Color.Gainsboro;
          this.zoomPictureBox.AllowUserDrag = true;
        }
      }

      private void zoomPictureBox_MouseDown(object sender, MouseEventArgs e)
      {
        if (e.Button != MouseButtons.Left || this.zoomPictureBox.AllowUserDrag)
          return;
        this.zoomPictureBox.Cursor = Cursors.SizeAll;
        this.zoomPictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
        this._startMouseDragged = e.Location;
        this._moving = true;
      }

      private void zoomPictureBox_MouseMove(object sender, MouseEventArgs e)
      {
        if (e.Button != MouseButtons.Left || !this._moving)
          return;
        ref PointF local1 = ref this._mouseOffset;
        double x = (double) local1.X;
        Point location = e.Location;
        double num1 = (double) (location.X - this._startMouseDragged.X) / (double) this.zoomPictureBox.Zoom;
        local1.X = (float) (x + num1);
        ref PointF local2 = ref this._mouseOffset;
        double y = (double) local2.Y;
        location = e.Location;
        double num2 = (double) (location.Y - this._startMouseDragged.Y) / (double) this.zoomPictureBox.Zoom;
        local2.Y = (float) (y + num2);
        this.Offset = Point.Round(this._mouseOffset);
        this._startMouseDragged = e.Location;
        EventHandler changedView = this.ChangedView;
        if (changedView != null)
          changedView((object) null, EventArgs.Empty);
        this.updateControls();
      }

      private void zoomPictureBox_MouseUp(object sender, MouseEventArgs e)
      {
        if (e.Button != MouseButtons.Left)
          return;
        this.zoomPictureBox.InterpolationMode = InterpolationMode.Bicubic;
        this._moving = false;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainView));
        this.tableLayoutPanel1 = new TableLayoutPanel();
        this.panel1 = new Panel();
        this.comboBoxMergeType = new ComboBox();
        this.label7 = new Label();
        this.label6 = new Label();
        this.label5 = new Label();
        this.groupBox1 = new GroupBox();
        this.buttonResetTransform = new Button();
        this.buttonEnableMooving = new Button();
        this.label4 = new Label();
        this.label3 = new Label();
        this.label2 = new Label();
        this.comboBoxZoom = new ComboBox();
        this.label1 = new Label();
        this.lowLayerView = new LayerView();
        this.topLayerView = new LayerView();
        this.customUpDownAngle = new CustomUpDown();
        this.customUpDownOffsetY = new CustomUpDown();
        this.customUpDownOffsetX = new CustomUpDown();
        this.zoomPictureBox = new ZoomPictureBox();
        this.tableLayoutPanel1.SuspendLayout();
        this.panel1.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        this.tableLayoutPanel1.ColumnCount = 1;
        this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 0);
        this.tableLayoutPanel1.Controls.Add((Control) this.zoomPictureBox, 0, 2);
        this.tableLayoutPanel1.Dock = DockStyle.Fill;
        this.tableLayoutPanel1.Location = new Point(0, 0);
        this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount = 3;
        this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f));
        this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
        this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
        this.tableLayoutPanel1.Size = new Size(1008, 729);
        this.tableLayoutPanel1.TabIndex = 0;
        this.panel1.BorderStyle = BorderStyle.FixedSingle;
        this.panel1.Controls.Add((Control) this.lowLayerView);
        this.panel1.Controls.Add((Control) this.topLayerView);
        this.panel1.Controls.Add((Control) this.comboBoxMergeType);
        this.panel1.Controls.Add((Control) this.label7);
        this.panel1.Controls.Add((Control) this.label6);
        this.panel1.Controls.Add((Control) this.label5);
        this.panel1.Controls.Add((Control) this.groupBox1);
        this.panel1.Dock = DockStyle.Fill;
        this.panel1.Location = new Point(0, 0);
        this.panel1.Margin = new Padding(0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(1008, 100);
        this.panel1.TabIndex = 5;
        this.comboBoxMergeType.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxMergeType.FormattingEnabled = true;
        this.comboBoxMergeType.Items.AddRange(new object[3]
        {
          (object) "Верхний слой",
          (object) "Нижний слой",
          (object) "Объединение слоев"
        });
        this.comboBoxMergeType.Location = new Point(88, 67);
        this.comboBoxMergeType.Name = "comboBoxMergeType";
        this.comboBoxMergeType.Size = new Size(252, 21);
        this.comboBoxMergeType.TabIndex = 0;
        this.label7.AutoSize = true;
        this.label7.Location = new Point(3, 41);
        this.label7.Name = "label7";
        this.label7.Size = new Size(77, 13);
        this.label7.TabIndex = 4;
        this.label7.Text = "Нижний слой:";
        this.label6.AutoSize = true;
        this.label6.Location = new Point(3, 13);
        this.label6.Name = "label6";
        this.label6.Size = new Size(79, 13);
        this.label6.TabIndex = 4;
        this.label6.Text = "Верхний слой:";
        this.label5.AutoSize = true;
        this.label5.Location = new Point(3, 69);
        this.label5.Name = "label5";
        this.label5.Size = new Size(79, 13);
        this.label5.TabIndex = 4;
        this.label5.Text = "Отображение:";
        this.groupBox1.Controls.Add((Control) this.customUpDownAngle);
        this.groupBox1.Controls.Add((Control) this.customUpDownOffsetY);
        this.groupBox1.Controls.Add((Control) this.customUpDownOffsetX);
        this.groupBox1.Controls.Add((Control) this.buttonResetTransform);
        this.groupBox1.Controls.Add((Control) this.buttonEnableMooving);
        this.groupBox1.Controls.Add((Control) this.label4);
        this.groupBox1.Controls.Add((Control) this.label3);
        this.groupBox1.Controls.Add((Control) this.label2);
        this.groupBox1.Controls.Add((Control) this.comboBoxZoom);
        this.groupBox1.Controls.Add((Control) this.label1);
        this.groupBox1.Location = new Point(662, 3);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new Size(341, 94);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Управление верхним слоем";
        this.buttonResetTransform.Location = new Point(259, 62);
        this.buttonResetTransform.Name = "buttonResetTransform";
        this.buttonResetTransform.Size = new Size(75, 23);
        this.buttonResetTransform.TabIndex = 6;
        this.buttonResetTransform.Text = "Сброс";
        this.buttonResetTransform.UseVisualStyleBackColor = true;
        this.buttonResetTransform.Click += new EventHandler(this.buttonResetTransform_Click);
        this.buttonEnableMooving.Location = new Point(178, 62);
        this.buttonEnableMooving.Name = "buttonEnableMooving";
        this.buttonEnableMooving.Size = new Size(75, 23);
        this.buttonEnableMooving.TabIndex = 6;
        this.buttonEnableMooving.Text = "Смещение";
        this.buttonEnableMooving.UseVisualStyleBackColor = true;
        this.buttonEnableMooving.Click += new EventHandler(this.buttonEnableMooving_Click);
        this.label4.AutoSize = true;
        this.label4.Location = new Point(6, 72);
        this.label4.Name = "label4";
        this.label4.Size = new Size(78, 13);
        this.label4.TabIndex = 4;
        this.label4.Text = "Положение Y:";
        this.label3.AutoSize = true;
        this.label3.Location = new Point(6, 46);
        this.label3.Name = "label3";
        this.label3.Size = new Size(78, 13);
        this.label3.TabIndex = 4;
        this.label3.Text = "Положение X:";
        this.label2.AutoSize = true;
        this.label2.Location = new Point(168, 20);
        this.label2.Name = "label2";
        this.label2.Size = new Size(85, 13);
        this.label2.TabIndex = 2;
        this.label2.Text = "Угол поворота:";
        this.comboBoxZoom.FormattingEnabled = true;
        this.comboBoxZoom.Items.AddRange(new object[8]
        {
          (object) "25",
          (object) "50",
          (object) "75",
          (object) "100",
          (object) "125",
          (object) "150",
          (object) "175",
          (object) "200"
        });
        this.comboBoxZoom.Location = new Point(68, 16 /*0x10*/);
        this.comboBoxZoom.Name = "comboBoxZoom";
        this.comboBoxZoom.Size = new Size(64 /*0x40*/, 21);
        this.comboBoxZoom.TabIndex = 1;
        this.comboBoxZoom.SelectedIndexChanged += new EventHandler(this.comboBoxZoom_SelectedIndexChanged);
        this.comboBoxZoom.Enter += new EventHandler(this.comboBoxZoom_Enter);
        this.comboBoxZoom.Leave += new EventHandler(this.comboBoxZoom_Leave);
        this.label1.AutoSize = true;
        this.label1.Location = new Point(6, 20);
        this.label1.Name = "label1";
        this.label1.Size = new Size(56, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Масштаб:";
        this.lowLayerView.Location = new Point(88, 36);
        this.lowLayerView.Name = "lowLayerView";
        this.lowLayerView.Size = new Size(568, 26);
        this.lowLayerView.TabIndex = 6;
        this.topLayerView.Location = new Point(88, 8);
        this.topLayerView.Name = "topLayerView";
        this.topLayerView.Size = new Size(568, 26);
        this.topLayerView.TabIndex = 5;
        this.customUpDownAngle.DigitsCount = 2;
        this.customUpDownAngle.Increment = 0.1f;
        this.customUpDownAngle.Location = new Point(258, 17);
        this.customUpDownAngle.Name = "customUpDownAngle";
        this.customUpDownAngle.Size = new Size(75, 20);
        this.customUpDownAngle.TabIndex = 7;
        this.customUpDownAngle.Value = 0.0;
        this.customUpDownAngle.ValueChanged += new EventHandler(this.customUpDownAngle_ValueChanged);
        this.customUpDownOffsetY.Increment = 1f;
        this.customUpDownOffsetY.Location = new Point(90, 69);
        this.customUpDownOffsetY.Name = "customUpDownOffsetY";
        this.customUpDownOffsetY.Size = new Size(75, 20);
        this.customUpDownOffsetY.TabIndex = 7;
        this.customUpDownOffsetY.Value = 0.0;
        this.customUpDownOffsetY.ValueChanged += new EventHandler(this.customUpDownOffsetY_ValueChanged);
        this.customUpDownOffsetX.Increment = 1f;
        this.customUpDownOffsetX.Location = new Point(90, 43);
        this.customUpDownOffsetX.Name = "customUpDownOffsetX";
        this.customUpDownOffsetX.Size = new Size(75, 20);
        this.customUpDownOffsetX.TabIndex = 7;
        this.customUpDownOffsetX.Value = 0.0;
        this.customUpDownOffsetX.ValueChanged += new EventHandler(this.customUpDownOffsetX_ValueChanged);
        this.zoomPictureBox.BackColor = Color.Gray;
        this.zoomPictureBox.Dock = DockStyle.Fill;
        this.zoomPictureBox.InterpolationMode = InterpolationMode.Bicubic;
        this.zoomPictureBox.InterpolationModeZoomOut = InterpolationMode.Bilinear;
        this.zoomPictureBox.Location = new Point(0, 100);
        this.zoomPictureBox.Margin = new Padding(0);
        this.zoomPictureBox.Name = "zoomPictureBox";
        this.zoomPictureBox.PixelOffsetMode = PixelOffsetMode.HighQuality;
        this.zoomPictureBox.Size = new Size(1008, 629);
        this.zoomPictureBox.TabIndex = 6;
        this.zoomPictureBox.VisibleCenter = (PointF) componentResourceManager.GetObject("zoomPictureBox.VisibleCenter");
        this.zoomPictureBox.MouseDown += new MouseEventHandler(this.zoomPictureBox_MouseDown);
        this.zoomPictureBox.MouseMove += new MouseEventHandler(this.zoomPictureBox_MouseMove);
        this.zoomPictureBox.MouseUp += new MouseEventHandler(this.zoomPictureBox_MouseUp);
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1008, 729);
        this.Controls.Add((Control) this.tableLayoutPanel1);
        this.MinimumSize = new Size(1024 /*0x0400*/, 768 /*0x0300*/);
        this.Name = nameof (MainView);
        this.Text = "Сравнение PDF";
        this.tableLayoutPanel1.ResumeLayout(false);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
      }
    }
}
