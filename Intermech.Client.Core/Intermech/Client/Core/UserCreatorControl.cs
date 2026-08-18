
// Type: Intermech.Client.Core.UserCreatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Страничка "Роли" в мастере по созданию объектов типа "Пользователи"
/// </summary>
public class UserCreatorControl : ObjectCreatorControl
{
  /// <summary>Созданный объект</summary>
  private CreatedObjectItem _createdObject;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать экземпляр класса</summary>
  public UserCreatorControl() => this.InitializeComponent();

  /// <summary>Создать экземпляр класса, заполнить его информацией</summary>
  /// <param name="createdObject">Созданный объект</param>
  public UserCreatorControl(CreatedObjectItem createdObject)
    : this()
  {
    this._createdObject = createdObject;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserCreatorControl));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (UserCreatorControl);
    this.ResumeLayout(false);
  }
}
