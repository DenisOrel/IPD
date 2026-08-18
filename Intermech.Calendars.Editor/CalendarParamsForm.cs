
// Type: Intermech.Calendars.Editor.CalendarParamsForm
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Calendars.Editor;

public class CalendarParamsForm : Form
{
  private IContainer components;
  private Button button1;
  private Button button2;
  private MonthCalendar monthCalendar1;

  public CalendarParamsForm() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CalendarParamsForm));
    this.button1 = new Button();
    this.button2 = new Button();
    this.monthCalendar1 = new MonthCalendar();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.monthCalendar1, "monthCalendar1");
    this.monthCalendar1.Name = "monthCalendar1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.monthCalendar1);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (CalendarParamsForm);
    this.ResumeLayout(false);
  }
}
