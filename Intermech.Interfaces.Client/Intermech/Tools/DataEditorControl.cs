// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataEditorControl
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools;

/// <summary>
/// Базовый класс для редакторов настроек, представленных в форме xml-документа.
/// </summary>
public class DataEditorControl : UserControl
{
  private XmlDocument originalData;
  private bool readOnly;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создает объект.</summary>
  public DataEditorControl() => this.InitializeComponent();

  /// <summary>Передает редактору объект с настройками.</summary>
  /// <param name="data">Настройки</param>
  /// <param name="readOnly">Признак режима отображения настроек без возможности редактирования</param>
  public virtual void SetData(XmlDocument data, bool readOnly)
  {
    this.originalData = data;
    this.readOnly = readOnly;
  }

  /// <summary>
  /// Редактор возвращает новый объект настроек, содержащий все сделанные пользователем изменения.
  /// </summary>
  /// <returns>Объект с настройками</returns>
  public virtual XmlDocument GetData() => this.originalData;

  [Browsable(false)]
  public XmlDocument OriginalData => this.originalData;

  [Browsable(false)]
  public bool ReadOnly => this.readOnly;

  protected void RaiseDataChanged()
  {
    if (this.DataChanged == null)
      return;
    this.DataChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler DataChanged;

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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.Transparent;
    this.Name = nameof (DataEditorControl);
    this.Size = new Size(233, 191);
    this.ResumeLayout(false);
  }
}
