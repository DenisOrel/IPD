// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.Step3
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>
/// 
/// </summary>
internal class Step3 : UserControl
{
  private Button _next;
  private Button _prev;
  private object _host;
  private bool _useButtons;
  private ArrayList _attributeModels;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox textBox1;

  /// <summary>
  /// 
  /// </summary>
  public ArrayList AttributeModels
  {
    set
    {
      this._attributeModels = value;
      this.Place();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int OriginBetween { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Point OriginLocation { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public bool UseButtons
  {
    get => this._useButtons;
    set
    {
      this._useButtons = this.Visible = value;
      if (!value)
        return;
      this._next.Text = LocalizationHolder.rm.GetString("FormDesigner_7");
      this._next.Visible = true;
      this._prev.Enabled = false;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="next">Кнопка "Далее"</param>
  /// <param name="prev">Кнопка "Назад"</param>
  public Step3(object host, Button next, Button prev)
  {
    this.InitializeComponent();
    this._host = host;
    this._next = next;
    this._prev = prev;
    this.OriginLocation = new Point(8, 8);
    this.OriginBetween = 8;
  }

  /// <summary>
  /// 
  /// </summary>
  private void Place()
  {
    this._next.Enabled = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Control rootComponent = (this._host as IDesignerHost).RootComponent as Control;
      using ((this._host as IDesignerHost).CreateTransaction())
      {
        Point originLocation = this.OriginLocation;
        int y = originLocation.Y;
        Size size1 = rootComponent.ClientSize;
        int height = size1.Height;
        size1 = rootComponent.ClientSize;
        int width = size1.Width;
        string format1 = LocalizationHolder.rm.GetString("FormDesigner_3");
        string format2 = LocalizationHolder.rm.GetString("FormDesigner_4");
        string format3 = LocalizationHolder.rm.GetString("FormDesigner_5");
        string format4 = LocalizationHolder.rm.GetString("FormDesigner_6");
        foreach (AttributeModel attributeModel in this._attributeModels)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeModel.Name, false);
          if (attributeType == null)
            this.textBox1.AppendText(string.Format(format1, (object) attributeModel.Name));
          else if (attributeModel.ControlType == (System.Type) null)
          {
            this.textBox1.AppendText(string.Format(format2, (object) attributeModel.Name));
          }
          else
          {
            originLocation = this.OriginLocation;
            int x = originLocation.X;
            Label label1 = (Label) null;
            Control control = (Control) null;
            switch (attributeModel.Arrange)
            {
              case LabelArrange.laNone:
                control = this.CreateControl(attributeModel, x, y, rootComponent.Width);
                break;
              case LabelArrange.laLeft:
                IMLabel imLabel1 = new IMLabel();
                imLabel1.Location = new Point(x, y);
                imLabel1.TextAlign = ContentAlignment.MiddleLeft;
                imLabel1.Text = attributeModel.Name;
                label1 = (Label) imLabel1;
                x = 150;
                control = this.CreateControl(attributeModel, x, y, rootComponent.Width);
                label1.MaximumSize = new Size(x - this.OriginBetween * 2, 0);
                Label label2 = label1;
                size1 = label1.MaximumSize;
                Size size2 = new Size(size1.Width, control.Height);
                label2.MinimumSize = size2;
                label1.AutoSize = true;
                (this._host as IContainer).Add((IComponent) label1);
                rootComponent.Controls.Add((Control) label1);
                break;
              case LabelArrange.laTop:
                IMLabel imLabel2 = new IMLabel();
                imLabel2.Location = new Point(x, y);
                imLabel2.TextAlign = ContentAlignment.MiddleLeft;
                imLabel2.Text = attributeModel.Name;
                label1 = (Label) imLabel2;
                y += label1.Height;
                control = this.CreateControl(attributeModel, x, y, rootComponent.Width);
                Label label3 = label1;
                label1.MinimumSize = size1 = new Size(control.Width, 0);
                Size size3 = size1;
                label3.MaximumSize = size3;
                label1.AutoSize = true;
                (this._host as IContainer).Add((IComponent) label1);
                rootComponent.Controls.Add((Control) label1);
                break;
            }
            y += control.Height + this.OriginBetween;
            width = width > x + control.Width ? width : x + control.Width + 5;
            (control as IAttributeEditor).AttributeInfo = new AttributeInfo(attributeType.PropertiesStructure.AttributeGuid, Guid.Empty);
            (this._host as IContainer).Add((IComponent) control);
            rootComponent.Controls.Add(control);
            if (label1 != null)
              this.textBox1.AppendText(string.Format(format3, (object) label1.Name, (object) label1.Location));
            this.textBox1.AppendText(string.Format(format4, (object) control.Name, (object) control.GetType(), (object) control.Location));
          }
        }
        rootComponent.ClientSize = new Size(width, height > y ? height : y);
      }
    }
    this._next.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <param name="rootWidth"></param>
  /// <returns></returns>
  private Control CreateControl(AttributeModel model, int x, int y, int rootWidth)
  {
    Control instance = Activator.CreateInstance(model.ControlType) as Control;
    instance.Location = new Point(x, y);
    if (model.Width > 0)
      instance.Width = model.Width > rootWidth - x ? rootWidth - x - 5 : model.Width;
    return instance;
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
    this.textBox1 = new TextBox();
    this.SuspendLayout();
    this.textBox1.Dock = DockStyle.Fill;
    this.textBox1.Location = new Point(0, 0);
    this.textBox1.Multiline = true;
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    this.textBox1.ScrollBars = ScrollBars.Both;
    this.textBox1.Size = new Size(660, 430);
    this.textBox1.TabIndex = 0;
    this.Controls.Add((Control) this.textBox1);
    this.Name = nameof (Step3);
    this.Size = new Size(660, 430);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
