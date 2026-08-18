
// Type: Intermech.Search.ObjectLinkBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class ObjectLinkBox : Box<long>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ObjectLinkBox()
  {
    this.InitializeComponent();
    this.ObjectTypeID = -1;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ObjectTypeID { get; set; }

  protected override long DefaultValue => 0;

  protected override bool SupportedTextInput => false;

  protected override string GetTextBoxText()
  {
    if (this.IsEmpty || ObjectHelper.IsUnknownObjectVersionID(this.TypedValue))
      return string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.TypedValue, false);
      return dbObject != null ? dbObject.Caption : string.Empty;
    }
  }

  protected override void Edit()
  {
    long[] numArray = SelectionWindow.SelectObjects("Выберите объект", "", ObjectTypeHelper.IsUnknownObjectTypeID(this.ObjectTypeID) ? (IDescriptor) new AllObjectTypesDescriptor() : (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(this.ObjectTypeID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    this.Value = (object) numArray[0];
    this.HandleKeyUp(Keys.Return);
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ControlLightLight;
    this.Name = nameof (ObjectLinkBox);
    this.Size = new Size(250, 20);
    this.ResumeLayout(false);
  }
}
