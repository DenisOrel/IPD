
// Type: Intermech.Navigator.Conditions.DateTimeTextBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class DateTimeTextBox : TextBoxButton
{
  public DateTimeTextBox() => this.InitializeComponent();

  public override void OpenDialog_Click(object sender, OnOpenDialogEventArgs e)
  {
    Control parent = this.Parent;
    Point location = this.bOpenDialog.Location;
    int x1 = location.X;
    location = this.bOpenDialog.Location;
    int y1 = location.Y + this.bOpenDialog.Height + 5;
    Point p = new Point(x1, y1);
    Point screen = parent.PointToScreen(p);
    int num1 = screen.X + (int) byte.MaxValue;
    Rectangle workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int x2 = workingArea1.X;
    workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int width = workingArea1.Width;
    int num2 = x2 + width;
    if (num1 > num2)
      screen.X = Screen.PrimaryScreen.WorkingArea.X + Screen.PrimaryScreen.WorkingArea.Width - (int) byte.MaxValue;
    int x3 = screen.X;
    Rectangle workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int x4 = workingArea2.X;
    if (x3 < x4)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int x5 = workingArea2.X;
      local.X = x5;
    }
    int num3 = screen.Y + 205;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y2 = workingArea2.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int height1 = workingArea2.Height;
    int num4 = y2 + height1;
    if (num3 > num4)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y3 = workingArea2.Y;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int height2 = workingArea2.Height;
      int num5 = y3 + height2 - 205;
      local.Y = num5;
    }
    int y4 = screen.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y5 = workingArea2.Y;
    if (y4 < y5)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y6 = workingArea2.Y;
      local.Y = y6;
    }
    using (DateTimePopupControl timePopupControl = new DateTimePopupControl())
    {
      if (timePopupControl.Execute(screen, new Size(0, 0), (System.IServiceProvider) null, this.Value != null ? this.Value : (object) DateTime.Now) == DialogResult.OK)
      {
        e.SelectedValues = this.Value = (object) ((DateTime) timePopupControl.Value).Date;
        EventHandler valueChanged = this.ValueChanged;
        if (valueChanged != null)
          valueChanged((object) this, new EventArgs());
        this.ValueChangedFromDialog = true;
      }
      this.ValueChangedFromDialog = false;
    }
  }

  public object Value
  {
    get
    {
      DateTime result;
      return !string.IsNullOrEmpty(this.Text) && DateTime.TryParse(this.Text, out result) ? (object) result : (object) null;
    }
    set
    {
      if (value is DateTime dateTime)
        this.SetText(dateTime.ToString(this.FormatString));
      else
        this.SetText(string.Empty);
    }
  }

  public string FormatString { get; set; }

  public EventHandler ValueChanged { get; internal set; }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.bOpenDialog.Image = (Image) Resources.Calendar;
    this.bOpenDialog.Text = "";
    this.tbText.ValidatingType = typeof (DateTime);
    this.tbText.TypeValidationCompleted += new TypeValidationEventHandler(this.Text_TypeValidationCompleted);
    this.tbText.TextChanged += new EventHandler(this.TbText_TextChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.Name = nameof (DateTimeTextBox);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void TbText_TextChanged(object sender, EventArgs e)
  {
    EventHandler valueChanged = this.ValueChanged;
    if (valueChanged == null)
      return;
    valueChanged((object) this, new EventArgs());
  }

  private void Text_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
  {
    if (!string.IsNullOrEmpty(this.tbText.Text) && !e.IsValidInput)
    {
      int num = (int) MessageBox.Show(e.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      e.Cancel = true;
    }
    else
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged((object) this, new EventArgs());
    }
  }
}
