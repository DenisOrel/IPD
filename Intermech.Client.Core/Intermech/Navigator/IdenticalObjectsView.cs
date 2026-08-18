
// Type: Intermech.Navigator.IdenticalObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

public sealed class IdenticalObjectsView : ObjectsViewBase
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public IdenticalObjectsView()
  {
    this.InitializeComponent();
    this._services.AddService(typeof (IdenticalObjectsView), (object) this);
    if (Holder.NotificationService != null)
      this._services.AddService(typeof (INotificationService), (object) Holder.NotificationService);
    this.DisableFiltration = true;
  }

  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    base.NotificationEventFired(sender, e);
    switch (e)
    {
    }
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
