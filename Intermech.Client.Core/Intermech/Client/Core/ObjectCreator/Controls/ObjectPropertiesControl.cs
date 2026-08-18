
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectPropertiesControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// Summary description for ObjectCreatorControlFileAttrs.
/// </summary>
internal class ObjectPropertiesControl : ObjectCreatorControl
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Panel panel2;
  private Label label1;
  private PictureBox pictureBox1;
  private ObjectPropertyGrid objPropGrid;
  private const GetAttributeValuesModes _gridMode = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility;
  private static readonly System.Type[] tabTypes = new System.Type[1]
  {
    typeof (ObjectAllAttributesGridTab)
  };
  /// <summary>Признак того что работаем с заготовкой</summary>
  internal bool blankMode;

  public event PropertyValueChangedHendler PropertyValueChangedEvent;

  public event GridChangedHandler GridChangedEvent;

  public ObjectPropertiesControl(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this._SaveInTransaction = false;
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectPropertiesControl));
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.objPropGrid = new ObjectPropertyGrid();
    this.panel2.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    this.panel2.AccessibleDescription = (string) null;
    this.panel2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.BackgroundImage = (Image) null;
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.pictureBox1);
    this.panel2.Font = (Font) null;
    this.panel2.Name = "panel2";
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.GrayText;
    this.label1.Name = "label1";
    this.pictureBox1.AccessibleDescription = (string) null;
    this.pictureBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.BackgroundImage = (Image) null;
    this.pictureBox1.Font = (Font) null;
    this.pictureBox1.ImageLocation = (string) null;
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.objPropGrid.AccessibleDescription = (string) null;
    this.objPropGrid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.objPropGrid, "objPropGrid");
    this.objPropGrid.BackgroundImage = (Image) null;
    this.objPropGrid.Font = (Font) null;
    this.objPropGrid.InternalMenuEnabled = true;
    this.objPropGrid.LineColor = SystemColors.ScrollBar;
    this.objPropGrid.LockTypeChange = true;
    this.objPropGrid.Name = "objPropGrid";
    this.objPropGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.objPropGrid_PropertyValueChanged);
    this.objPropGrid.GridChanged += new ObjectPropertyGrid.GridChangedDelegate(this.objPropGrid_GridChanged);
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.objPropGrid);
    this.Controls.Add((Control) this.panel2);
    this.Font = (Font) null;
    this.Name = nameof (ObjectPropertiesControl);
    this.panel2.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Обновление данных (обновление и заполнение элементов управления)
  /// </summary>
  /// <param name="args"></param>
  /// <returns>Если обновление прошло успешно - true, иначе - false</returns>
  public override bool Refresh(PageRefreshArgs args)
  {
    this.pictureBox1.Image = this.CreatedObject.ObjectTypeImage;
    this.label1.Text = this.CreatedObject.ObjectTypeCaption;
    Control parent = this.objPropGrid.Parent;
    this.objPropGrid.Parent = (Control) null;
    this.objPropGrid.Load(this.CreatedObject.ObjectID, AttributableElements.Object, GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility, true, ObjectPropertiesControl.tabTypes);
    this.objPropGrid.Parent = parent;
    return base.Refresh(args);
  }

  /// <summary>Сохранение данных</summary>
  /// <param name="args"></param>
  /// <returns>Если сохранение прошло успешно - true, иначе - false</returns>
  public override bool Save(PageSaveArgs args)
  {
    this.objPropGrid.Save(this.blankMode);
    return base.Save(args);
  }

  private void objPropGrid_GridChanged(object sender, GridChangedEventArgs e)
  {
    GridChangedHandler gridChangedEvent = this.GridChangedEvent;
    if (gridChangedEvent == null)
      return;
    gridChangedEvent(sender, e);
  }

  private void objPropGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (this.PropertyValueChangedEvent == null)
      return;
    this.PropertyValueChangedEvent(s, e);
  }

  public override int HelpTopicID => 697;
}
