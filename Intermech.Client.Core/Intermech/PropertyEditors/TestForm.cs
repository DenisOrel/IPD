
// Type: Intermech.PropertyEditors.TestForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for TestForm.</summary>
public class TestForm : Form
{
  private Button button1;
  private ContextMenu contextMenu1;
  private MenuItem menuItem1;
  private MenuItem menuItem2;
  private Button button2;
  private Button button3;
  private Button button4;
  private OpenFileDialog openFileDialog;
  private Button button5;
  private Label label;
  private ObjectPropertyGrid opg;
  private Panel panel1;
  private TextBox textBox;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Button button8;
  private Button button9;
  private Button button10;
  private FileAttributeEditForm fileAttributeEditForm = new FileAttributeEditForm();
  private Button button7;
  private Button button11;
  private TextBox exEditor;
  private Button button12;
  private Button button13;
  private Button button6;
  private Button button14;
  private Button button15;
  private Panel panel0;
  private ObjectPropertyGrid objPG;
  public CategoryPropsClass cpc;

  public TestForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TestForm));
    this.contextMenu1 = new ContextMenu();
    this.menuItem1 = new MenuItem();
    this.menuItem2 = new MenuItem();
    this.button1 = new Button();
    this.button2 = new Button();
    this.button3 = new Button();
    this.button4 = new Button();
    this.openFileDialog = new OpenFileDialog();
    this.button5 = new Button();
    this.label = new Label();
    this.panel1 = new Panel();
    this.textBox = new TextBox();
    this.button8 = new Button();
    this.button9 = new Button();
    this.button10 = new Button();
    this.button7 = new Button();
    this.button11 = new Button();
    this.exEditor = new TextBox();
    this.button12 = new Button();
    this.button13 = new Button();
    this.button6 = new Button();
    this.button14 = new Button();
    this.button15 = new Button();
    this.panel0 = new Panel();
    this.objPG = new ObjectPropertyGrid();
    this.SuspendLayout();
    this.contextMenu1.MenuItems.AddRange(new MenuItem[2]
    {
      this.menuItem1,
      this.menuItem2
    });
    componentResourceManager.ApplyResources((object) this.contextMenu1, "contextMenu1");
    this.contextMenu1.Popup += new EventHandler(this.contextMenu1_Popup);
    componentResourceManager.ApplyResources((object) this.menuItem1, "menuItem1");
    this.menuItem1.Index = 0;
    componentResourceManager.ApplyResources((object) this.menuItem2, "menuItem2");
    this.menuItem2.Index = 1;
    this.button1.AccessibleDescription = (string) null;
    this.button1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.BackgroundImage = (Image) null;
    this.button1.Font = (Font) null;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    this.button2.AccessibleDescription = (string) null;
    this.button2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.BackgroundImage = (Image) null;
    this.button2.Font = (Font) null;
    this.button2.Name = "button2";
    this.button2.Click += new EventHandler(this.button2_Click);
    this.button3.AccessibleDescription = (string) null;
    this.button3.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.BackgroundImage = (Image) null;
    this.button3.Font = (Font) null;
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click);
    this.button4.AccessibleDescription = (string) null;
    this.button4.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.BackgroundImage = (Image) null;
    this.button4.Font = (Font) null;
    this.button4.Name = "button4";
    this.button4.Click += new EventHandler(this.button4_Click);
    componentResourceManager.ApplyResources((object) this.openFileDialog, "openFileDialog");
    this.openFileDialog.FileOk += new CancelEventHandler(this.openFileDialog_FileOk);
    this.openFileDialog.RestoreDirectory = true;
    this.button5.AccessibleDescription = (string) null;
    this.button5.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button5, "button5");
    this.button5.BackgroundImage = (Image) null;
    this.button5.Font = (Font) null;
    this.button5.Name = "button5";
    this.button5.Click += new EventHandler(this.button5_Click);
    this.label.AccessibleDescription = (string) null;
    this.label.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label, "label");
    this.label.Font = (Font) null;
    this.label.Name = "label";
    this.panel1.AccessibleDescription = (string) null;
    this.panel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackgroundImage = (Image) null;
    this.panel1.Font = (Font) null;
    this.panel1.Name = "panel1";
    this.textBox.AccessibleDescription = (string) null;
    this.textBox.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textBox, "textBox");
    this.textBox.BackgroundImage = (Image) null;
    this.textBox.Font = (Font) null;
    this.textBox.Name = "textBox";
    this.button8.AccessibleDescription = (string) null;
    this.button8.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button8, "button8");
    this.button8.BackgroundImage = (Image) null;
    this.button8.Font = (Font) null;
    this.button8.Name = "button8";
    this.button8.Click += new EventHandler(this.button8_Click);
    this.button9.AccessibleDescription = (string) null;
    this.button9.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button9, "button9");
    this.button9.BackgroundImage = (Image) null;
    this.button9.Font = (Font) null;
    this.button9.Name = "button9";
    this.button9.Click += new EventHandler(this.button9_Click);
    this.button10.AccessibleDescription = (string) null;
    this.button10.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button10, "button10");
    this.button10.BackgroundImage = (Image) null;
    this.button10.Font = (Font) null;
    this.button10.Name = "button10";
    this.button10.Click += new EventHandler(this.button10_Click);
    this.button7.AccessibleDescription = (string) null;
    this.button7.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button7, "button7");
    this.button7.BackgroundImage = (Image) null;
    this.button7.Font = (Font) null;
    this.button7.Name = "button7";
    this.button7.Click += new EventHandler(this.button7_Click);
    this.button11.AccessibleDescription = (string) null;
    this.button11.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button11, "button11");
    this.button11.BackgroundImage = (Image) null;
    this.button11.Font = (Font) null;
    this.button11.Name = "button11";
    this.button11.Click += new EventHandler(this.button11_Click);
    this.exEditor.AccessibleDescription = (string) null;
    this.exEditor.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.exEditor, "exEditor");
    this.exEditor.BackgroundImage = (Image) null;
    this.exEditor.Font = (Font) null;
    this.exEditor.Name = "exEditor";
    this.button12.AccessibleDescription = (string) null;
    this.button12.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button12, "button12");
    this.button12.BackgroundImage = (Image) null;
    this.button12.Font = (Font) null;
    this.button12.Name = "button12";
    this.button12.Click += new EventHandler(this.button12_Click);
    this.button13.AccessibleDescription = (string) null;
    this.button13.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button13, "button13");
    this.button13.BackgroundImage = (Image) null;
    this.button13.Font = (Font) null;
    this.button13.Name = "button13";
    this.button13.Click += new EventHandler(this.button13_Click);
    this.button6.AccessibleDescription = (string) null;
    this.button6.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button6, "button6");
    this.button6.BackgroundImage = (Image) null;
    this.button6.Font = (Font) null;
    this.button6.Name = "button6";
    this.button6.Click += new EventHandler(this.button6_Click);
    this.button14.AccessibleDescription = (string) null;
    this.button14.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button14, "button14");
    this.button14.BackgroundImage = (Image) null;
    this.button14.Font = (Font) null;
    this.button14.Name = "button14";
    this.button14.UseVisualStyleBackColor = true;
    this.button14.Click += new EventHandler(this.button14_Click);
    this.button15.AccessibleDescription = (string) null;
    this.button15.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button15, "button15");
    this.button15.BackgroundImage = (Image) null;
    this.button15.Font = (Font) null;
    this.button15.Name = "button15";
    this.button15.UseVisualStyleBackColor = true;
    this.button15.Click += new EventHandler(this.button15_Click);
    this.panel0.AccessibleDescription = (string) null;
    this.panel0.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel0, "panel0");
    this.panel0.BackgroundImage = (Image) null;
    this.panel0.Font = (Font) null;
    this.panel0.Name = "panel0";
    this.objPG.AccessibleDescription = (string) null;
    this.objPG.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.objPG, "objPG");
    this.objPG.BackgroundImage = (Image) null;
    this.objPG.Font = (Font) null;
    this.objPG.InternalMenuEnabled = true;
    this.objPG.LockTypeChange = false;
    this.objPG.Name = "objPG";
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.objPG);
    this.Controls.Add((Control) this.panel0);
    this.Controls.Add((Control) this.button15);
    this.Controls.Add((Control) this.button14);
    this.Controls.Add((Control) this.button6);
    this.Controls.Add((Control) this.button13);
    this.Controls.Add((Control) this.button12);
    this.Controls.Add((Control) this.exEditor);
    this.Controls.Add((Control) this.button11);
    this.Controls.Add((Control) this.button7);
    this.Controls.Add((Control) this.button10);
    this.Controls.Add((Control) this.button9);
    this.Controls.Add((Control) this.button8);
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.label);
    this.Controls.Add((Control) this.button5);
    this.Controls.Add((Control) this.button4);
    this.Controls.Add((Control) this.button3);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Font = (Font) null;
    this.Icon = (Icon) null;
    this.Name = nameof (TestForm);
    this.Tag = (object) "   ";
    this.Closed += new EventHandler(this.TestForm_Closed);
    this.Load += new EventHandler(this.TestForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void TestForm_Load(object sender, EventArgs e) => FormStorage.LoadLayout((Control) this);

  private void button1_Click(object sender, EventArgs e) => this.objPG.Save();

  private void contextMenu1_Popup(object sender, EventArgs e)
  {
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.opg.ContextMenu = (ContextMenu) null;
  }

  private void opg_ContextMenuChanged(object sender, EventArgs e)
  {
  }

  private void opg_CursorChanged(object sender, EventArgs e)
  {
  }

  private void opg_EnabledChanged(object sender, EventArgs e)
  {
  }

  private void opg_Validating(object sender, CancelEventArgs e)
  {
  }

  private void button3_Click(object sender, EventArgs e)
  {
    this.opg.ContextMenu = this.contextMenu1;
  }

  private void button4_Click(object sender, EventArgs e)
  {
    BlobProcWriter blobProcWriter;
    using (MemoryStream aSourceStream = new MemoryStream(new byte[10]
    {
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 3,
      (byte) 4,
      (byte) 5,
      (byte) 6,
      (byte) 7,
      (byte) 8,
      (byte) 9
    }))
      blobProcWriter = new BlobProcWriter(26011L, AttributableElements.Object, 1013, 0, 0, new BlobInformation()
      {
        ArcMethod = ArcMethods.ZLibPacked,
        FileName = "asdf.txt",
        ModifyDate = DateTime.Now
      }, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
    blobProcWriter.WriteData();
    BlobProcReader blobProcReader;
    using (MemoryStream aDestStream = new MemoryStream())
      blobProcReader = new BlobProcReader(26011L, AttributableElements.Object, 1013, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
    blobProcReader.ReadData();
  }

  private void opg_GridChanged(object sender, EventArgs e)
  {
  }

  private void opg_Click(object sender, EventArgs e)
  {
  }

  private void openFileDialog_FileOk(object sender, CancelEventArgs e)
  {
  }

  private void button5_Click(object sender, EventArgs e)
  {
    string path = "d:\\video\\mult\\ezik_v_tumane.avi";
    new BlobProcWriter(26011L, AttributableElements.Object, 1013, 0, 0, new BlobInformation()
    {
      ArcMethod = ArcMethods.ZLibPacked,
      FileName = path,
      ModifyDate = DateTime.Now
    }, (Stream) new FileStream(path, FileMode.Open, FileAccess.Read), new BlobProcCustomClass.ProgressEventHandler(this.ProgressEvent), new BlobProcCustomClass.ThreadFinishEventHandler(this.ThreadTermEvent)).WriteData();
  }

  private void ThreadTermEvent(
    BlobProcCustomClass sender,
    bool result,
    object message,
    Exception exception,
    BlobInformation bi)
  {
    int num = 0;
    while (num < 100)
      ++num;
  }

  private void ProgressEvent(BlobProcCustomClass sender, BlobProcessorMode mode, int progress)
  {
    this.label.Text = progress.ToString();
    this.label.Refresh();
  }

  private void button8_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IIconReader)) is IIconReader service))
      return;
    service.GetIconByFileExt("avi");
  }

  private void button9_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service))
      return;
    this.cpc = new CategoryPropsClass();
    service.RegisterCategoryProps(7, (ICategoryProps) this.cpc);
  }

  private void button10_Click(object sender, EventArgs e) => this.cpc.SetVal("ttttt");

  private void TestForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void button11_Click(object sender, EventArgs e)
  {
    this.objPG.Load(100392L, AttributableElements.Object, GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions, false, typeof (ObjectAllAttributesGridTab));
  }

  private void button7_Click(object sender, EventArgs e) => this.objPG.Save();

  private void button12_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure[] conditionStructures = ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, 119755L, 0L);
      List<IDBAttributeType> dbAttributeTypeList = new List<IDBAttributeType>();
      for (int index = 0; index < conditionStructures.Length; ++index)
      {
        IDBAttributeType dbAttributeType = (IDBAttributeType) null;
        if (conditionStructures[index].Attribute is Guid)
          dbAttributeType = sessionKeeper.Session.GetAttributeType((Guid) conditionStructures[index].Attribute);
        if (conditionStructures[index].Attribute is int)
          dbAttributeType = sessionKeeper.Session.GetAttributeType((int) conditionStructures[index].Attribute);
        if (dbAttributeType != null)
          dbAttributeTypeList.Add(dbAttributeType);
      }
      DataSetProcessor.ConstructFilter(conditionStructures, dbAttributeTypeList.ToArray());
      string empty = string.Empty;
    }
  }

  private void button13_Click(object sender, EventArgs e)
  {
    this.GetBlobFileName(5L, 44, 555, "", true);
  }

  public string GetBlobFileName(
    long id,
    int attrId,
    int blobId,
    string baseFolder,
    bool createMissingFolders)
  {
    return $"{id.ToString("X")}  {attrId.ToString("X")}  {blobId.ToString("X")}";
  }

  private void button6_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService))
        return;
      customService.GetDocumentTypesByFileExt(sessionKeeper.Session.SessionGUID, ".txt");
    }
  }

  public string ExText
  {
    get => this.exEditor.Text;
    set => this.exEditor.Text = value;
  }

  private void button14_Click(object sender, EventArgs e)
  {
    this.objPG.Load(58752L, AttributableElements.Object, GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions, false, typeof (ObjectAllAttributesGridTab));
  }

  private void button15_Click(object sender, EventArgs e)
  {
    AttributeProcessor attributeProcessor = new AttributeProcessor();
    attributeProcessor.Load(63024L, AttributableElements.Object, ClientConsts.GetAttributeValuesModes);
    AttributeValuesList actualAttributeValues = attributeProcessor.ActualAttributeValues;
    IAttributeEditorControl editorControl = attributeProcessor.GetEditorControl(1256, new int?(), UITypeEditorEditStyle.Modal);
    if (editorControl != null && editorControl is Form)
    {
      int num = (int) ((Form) editorControl).ShowDialog();
    }
    attributeProcessor.Save();
  }
}
