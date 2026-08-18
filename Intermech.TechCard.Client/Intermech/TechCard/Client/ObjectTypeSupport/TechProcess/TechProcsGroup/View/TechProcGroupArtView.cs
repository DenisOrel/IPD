// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup.View.TechProcGroupArtView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.View;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup.View;

/// <summary>Закладка для привязки ЕТП к ГТП</summary>
public class TechProcGroupArtView : TechCardBaseGroupArtView
{
  /// <summary>
  /// 
  /// </summary>
  internal new static readonly string IconImageName = "imgGroupArtView";
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeComponent();
    base.InitializeCustomControls();
    this.mbiOpenLinkedObject.Text = "Открыть единичный техпроцесс";
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
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
