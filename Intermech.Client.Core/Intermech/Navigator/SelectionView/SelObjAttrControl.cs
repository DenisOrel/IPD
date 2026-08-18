
// Type: Intermech.Navigator.SelectionView.SelObjAttrControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>Control for selecting attribute and object type</summary>
public class SelObjAttrControl : UserControl
{
  /// <summary>First part of the result - attribute data</summary>
  public SelFormResult selAttr;
  /// <summary>Second part of the result - object type data</summary>
  public SelFormResult selObjType;
  /// <summary>Attribute field type</summary>
  public FieldTypes attrType;
  internal bool multi;
  internal SelForm sForm;
  public bool sortByShort = true;
  public bool attrChanged = true;
  private DataTable attrData;
  private DataTable objTypeData;
  public bool checkMultiValAttr;
  public bool allowEmptyAttr;
  public bool allowEmptyObj = true;
  private bool _FullNames = true;
  private bool lockCheck;
  private Button btnClearAttr;
  private ButtonEdit textObjName;
  private ButtonEdit textAttName;
  private Label label1;
  private ImageList selAttrIL;
  private Label AttrTypeLbl;
  private Label AttrNameLbl;
  private Panel panelBottom;
  private GroupBox groupBox1;
  private Button buttonCancel;
  private Button buttonAccept;
  private CheckBox checkObjType;
  private IContainer components;

  /// <summary>
  /// Свойство для изменения видимости кнопок "Применить" и "Отмена"
  /// </summary>
  public bool ShowButtons
  {
    get => this.panelBottom.Visible;
    set
    {
      if (this.panelBottom.Visible == value)
        return;
      this.panelBottom.Visible = value;
    }
  }

  public Button CancelButton => this.buttonCancel;

  public Button AcceptButton => this.buttonAccept;

  public event EventHandler Changed;

  protected virtual void OnChanged(EventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, e);
  }

  /// <summary>Just a standard constructor</summary>
  public SelObjAttrControl()
  {
    this.InitializeComponent();
    this.sForm = new SelForm();
    this.LoadBitmaps(this.selAttrIL);
    this.textObjName.Text = "";
    this.textAttName.Text = "";
    this.AttrTypeLbl.Text = "";
    this.AttrNameLbl.Text = "";
    this.attrType = FieldTypes.ftUnknown;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelObjAttrControl));
    this.btnClearAttr = new Button();
    this.selAttrIL = new ImageList(this.components);
    this.textObjName = new ButtonEdit();
    this.textAttName = new ButtonEdit();
    this.label1 = new Label();
    this.AttrTypeLbl = new Label();
    this.AttrNameLbl = new Label();
    this.panelBottom = new Panel();
    this.groupBox1 = new GroupBox();
    this.buttonCancel = new Button();
    this.buttonAccept = new Button();
    this.checkObjType = new CheckBox();
    this.textObjName.Properties.BeginInit();
    this.textAttName.Properties.BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnClearAttr, "btnClearAttr");
    this.btnClearAttr.ImageList = this.selAttrIL;
    this.btnClearAttr.Name = "btnClearAttr";
    this.btnClearAttr.Click += new EventHandler(this.btnClearAttr_Click);
    this.selAttrIL.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.selAttrIL, "selAttrIL");
    this.selAttrIL.TransparentColor = Color.White;
    componentResourceManager.ApplyResources((object) this.textObjName, "textObjName");
    this.textObjName.Name = "textObjName";
    this.textObjName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textObjName.Properties.ReadOnly = true;
    this.textObjName.ButtonClick += new ButtonPressedEventHandler(this.textObjName_ButtonClick);
    componentResourceManager.ApplyResources((object) this.textAttName, "textAttName");
    this.textAttName.Name = "textAttName";
    this.textAttName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textAttName.Properties.ReadOnly = true;
    this.textAttName.ButtonClick += new ButtonPressedEventHandler(this.textAttName_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.AttrTypeLbl.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.AttrTypeLbl, "AttrTypeLbl");
    this.AttrTypeLbl.Name = "AttrTypeLbl";
    componentResourceManager.ApplyResources((object) this.AttrNameLbl, "AttrNameLbl");
    this.AttrNameLbl.BorderStyle = BorderStyle.Fixed3D;
    this.AttrNameLbl.Name = "AttrNameLbl";
    this.panelBottom.Controls.Add((Control) this.groupBox1);
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    this.panelBottom.Controls.Add((Control) this.buttonAccept);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.buttonAccept, "buttonAccept");
    this.buttonAccept.DialogResult = DialogResult.OK;
    this.buttonAccept.Name = "buttonAccept";
    this.buttonAccept.Click += new EventHandler(this.buttonAccept_Click);
    this.checkObjType.Checked = true;
    this.checkObjType.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this.checkObjType, "checkObjType");
    this.checkObjType.Name = "checkObjType";
    this.checkObjType.CheckedChanged += new EventHandler(this.checkObjType_Click);
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.AttrTypeLbl);
    this.Controls.Add((Control) this.AttrNameLbl);
    this.Controls.Add((Control) this.btnClearAttr);
    this.Controls.Add((Control) this.textObjName);
    this.Controls.Add((Control) this.textAttName);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.checkObjType);
    this.Name = nameof (SelObjAttrControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.textObjName.Properties.EndInit();
    this.textAttName.Properties.EndInit();
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Initialize session data (call this once)</summary>
  public void LoadSessionData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.attrData = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("");
      this.objTypeData = sessionKeeper.Session.GetObjectTypeCollection(-2).Select("");
    }
  }

  private void LoadBitmaps(ImageList imageList)
  {
    string name = "Intermech.Client.Core.Navigator.Selections.SelObjAttrControl.bmp";
    Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream(name);
    if (manifestResourceStream == null)
      return;
    try
    {
      using (Bitmap images = new Bitmap(manifestResourceStream))
      {
        images.MakeTransparent();
        NamedImageList namedImageList1 = new NamedImageList(imageList);
        namedImageList1.AddStrip((Image) images, new string[2]
        {
          "imgSelObjType",
          "imgDelAttr"
        });
        INamedImageList namedImageList2 = (INamedImageList) namedImageList1;
        this.selAttrIL.Images.Add(namedImageList2.ImageList.Images[0]);
        this.selAttrIL.Images.Add(namedImageList2.ImageList.Images[1]);
        this.btnClearAttr.ImageIndex = namedImageList2.ImageIndex("imgDelAttr");
      }
    }
    finally
    {
      manifestResourceStream.Close();
    }
  }

  private void CheckViews()
  {
    if (this.attrData != null && this.objTypeData != null)
      return;
    this.LoadSessionData();
  }

  private static string GetShortFTDescr(FieldTypes ft)
  {
    switch (ft)
    {
      case FieldTypes.ftUnknown:
        return LocalizationHolder.rm.GetString("Client.Core_425");
      case FieldTypes.ftString:
        return LocalizationHolder.rm.GetString("Client.Core_426");
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return LocalizationHolder.rm.GetString("Client.Core_427");
      case FieldTypes.ftDouble:
      case FieldTypes.ftMeasured:
        return LocalizationHolder.rm.GetString("Client.Core_428");
      case FieldTypes.ftDateTime:
        return LocalizationHolder.rm.GetString("Client.Core_429");
      case FieldTypes.ftShortBlob:
        return LocalizationHolder.rm.GetString("Client.Core_430");
      case FieldTypes.ftFile:
        return LocalizationHolder.rm.GetString("Client.Core_431");
      case FieldTypes.ftExternalLink:
        return LocalizationHolder.rm.GetString("Client.Core_432");
      case FieldTypes.ftObjectLink:
        return LocalizationHolder.rm.GetString("Client.Core_433");
      case FieldTypes.ftPassword:
        return LocalizationHolder.rm.GetString("Client.Core_434");
      case FieldTypes.ftMemo:
        return LocalizationHolder.rm.GetString("Client.Core_435");
      case FieldTypes.ftBlob:
        return "BLOB";
      case FieldTypes.ftBoolean:
        return LocalizationHolder.rm.GetString("Client.Core_436");
      case FieldTypes.ftSystem:
        return LocalizationHolder.rm.GetString("Client.Core_437");
      case FieldTypes.ftObjectLinkByID:
        return LocalizationHolder.rm.GetString("Client.Core_433id");
      default:
        return "";
    }
  }

  private void ReflectAttr()
  {
    if (this.selAttr == null)
    {
      this.textAttName.Text = "";
      this.AttrTypeLbl.Text = "";
      this.AttrNameLbl.Text = "";
    }
    else
    {
      if (this.selAttr.shortName != "" && !this._FullNames)
        this.textAttName.Text = this.selAttr.shortName;
      else
        this.textAttName.Text = this.selAttr.longName;
      if (this.selObjType == null)
      {
        this.checkObjType.Checked = false;
        this.textObjName.Text = "";
      }
      IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.selAttr.ID);
      this.attrType = attributeType.AttributeType;
      string str = SelObjAttrControl.GetShortFTDescr(this.attrType);
      this.multi = attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
      if (this.multi)
        str = $"{{{str}}}";
      this.AttrTypeLbl.Text = str;
      this.AttrNameLbl.Text = attributeType.Name;
    }
  }

  private void ReflectObjType()
  {
    if (!this.checkObjType.Checked || this.selObjType == null)
      this.textObjName.Text = "";
    else if (this.selObjType.shortName != "" && !this._FullNames)
      this.textObjName.Text = this.selObjType.shortName;
    else
      this.textObjName.Text = this.selObjType.longName;
  }

  internal bool PerformObjType(int objType)
  {
    if (this.selObjType != null && this.selObjType.ID == objType || objType == -1)
      return false;
    if (this.selObjType == null)
      this.selObjType = new SelFormResult();
    this.selObjType.ID = objType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.selObjType.ID);
      if (objectType == null)
        return false;
      this.selObjType.GUID = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
      this.selObjType.longName = objectType.PropertiesStructure.ObjectTypeName;
      this.selObjType.shortName = objectType.PropertiesStructure.ObjectTypeShortName;
    }
    if (!this.checkObjType.Checked)
      this.checkObjType.Checked = true;
    this.attrChanged = true;
    this.ReflectObjType();
    return true;
  }

  private void textObjName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int selectID = 0;
    if (this.selObjType != null)
      selectID = this.selObjType.ID;
    AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableType, AttributableElements.Object, -1, selectID);
    if (advSelectorForm.ShowDialog() != DialogResult.OK || !this.PerformObjType(advSelectorForm.ObjectType))
      return;
    this.OnChanged((EventArgs) null);
  }

  private void btnObjTree_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_88"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int int32 = Convert.ToInt32(selectorForm.IDList[0]);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
      if (objectType == null)
        return;
      ObjectTypeProperties propertiesStructure = objectType.PropertiesStructure;
      if (this.selObjType == null)
        this.selObjType = new SelFormResult();
      this.selObjType.ID = int32;
      this.selObjType.GUID = propertiesStructure.ObjectTypeGuid.ToString();
      this.selObjType.longName = propertiesStructure.ObjectInstanceName;
      if (this.selObjType.longName == "")
        this.selObjType.longName = propertiesStructure.ObjectTypeName;
      this.selObjType.shortName = propertiesStructure.ObjectTypeShortName;
      this.attrChanged = true;
      this.ReflectObjType();
      this.OnChanged((EventArgs) null);
    }
  }

  private void btnClearAttr_Click(object sender, EventArgs e)
  {
    this.selAttr = (SelFormResult) null;
    this.selObjType = (SelFormResult) null;
    this.textObjName.Text = "";
    this.textAttName.Text = "";
    this.AttrTypeLbl.Text = "";
    this.AttrNameLbl.Text = "";
    this.attrChanged = true;
    this.attrType = FieldTypes.ftUnknown;
    this.OnChanged((EventArgs) null);
  }

  internal bool PerformAttrType(int attrType)
  {
    if (this.selAttr != null && this.selAttr.ID == attrType)
      return false;
    if (this.selAttr == null)
      this.selAttr = new SelFormResult();
    this.selAttr.ID = attrType;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.selAttr.ID);
    if (attributeType == null)
      return false;
    this.selAttr.GUID = attributeType.PropertiesStructure.AttributeGuid.ToString();
    this.selAttr.longName = attributeType.Name;
    this.selAttr.shortName = attributeType.ShortName;
    this.attrChanged = true;
    this.ReflectAttr();
    return true;
  }

  private void textAttName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int num = -1;
    int selectID = -1;
    if (this.selAttr != null)
      num = this.selAttr.ID;
    if (this.checkObjType.Checked && this.selObjType != null)
      selectID = this.selObjType.ID;
    bool flag1 = false;
    AdvSelectorForm advSelectorForm;
    if (selectID == -1)
    {
      if (num == -1)
        advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object);
      else
        advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, -1, new int[1]
        {
          num
        });
    }
    else
    {
      if (num == -1)
        advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object, -1, selectID);
      else
        advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, selectID, new int[1]
        {
          num
        });
      flag1 = true;
    }
    if (advSelectorForm.ShowDialog() != DialogResult.OK)
      return;
    bool flag2 = false;
    bool flag3;
    if (flag1)
    {
      flag3 = flag2 || this.PerformObjType(advSelectorForm.ObjectType);
    }
    else
    {
      flag3 = advSelectorForm.ObjectType != -1;
      if (flag3)
        this.PerformObjType(advSelectorForm.ObjectType);
    }
    if (this.selAttr == null)
      this.selAttr = new SelFormResult();
    if (!(this.PerformAttrType(advSelectorForm.AttributeTypes[0]) | flag3))
      return;
    this.OnChanged((EventArgs) null);
  }

  private void checkObjType_Click(object sender, EventArgs e)
  {
    if (this.lockCheck)
      return;
    this.textObjName.Enabled = this.checkObjType.Checked;
    this.ReflectObjType();
    this.OnChanged((EventArgs) null);
  }

  /// <summary>Set current attribute and object type for the control</summary>
  /// <param name="attrGUID">Attribute GUID</param>
  /// <param name="objTypeGUID">Object Type GUID</param>
  /// <returns>true if successful</returns>
  public bool SetAttrAndObjType(string attrGUID, string objTypeGUID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType dbObjectType = (IDBObjectType) null;
      if (objTypeGUID != "")
        dbObjectType = sessionKeeper.Session.GetObjectType(new Guid(objTypeGUID), false);
      if (dbObjectType != null)
      {
        ObjectTypeProperties propertiesStructure = dbObjectType.PropertiesStructure;
        if (this.selObjType == null)
          this.selObjType = new SelFormResult();
        this.selObjType.ID = dbObjectType.ObjectType;
        this.selObjType.GUID = propertiesStructure.ObjectTypeGuid.ToString();
        this.selObjType.longName = propertiesStructure.ObjectInstanceName;
        if (this.selObjType.longName == "")
          this.selObjType.longName = propertiesStructure.ObjectTypeName;
        this.selObjType.shortName = propertiesStructure.ObjectTypeShortName;
      }
      else
        this.selObjType = (SelFormResult) null;
      IDBAttributeType dbAttributeType = (IDBAttributeType) null;
      if (attrGUID != "")
        dbAttributeType = sessionKeeper.Session.GetAttributeType(new Guid(attrGUID), false);
      if (dbAttributeType != null)
      {
        this.attrType = dbAttributeType.AttributeType;
        string str = SelObjAttrControl.GetShortFTDescr(this.attrType);
        this.multi = dbAttributeType.MultipleValued == MultiValueModes.MultiValues || dbAttributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
        if (this.multi)
          str = $"{{{str}}}";
        this.AttrTypeLbl.Text = str;
        this.AttrNameLbl.Text = dbAttributeType.Name;
        if (this.selAttr == null)
          this.selAttr = new SelFormResult();
        this.selAttr.GUID = attrGUID;
        this.selAttr.ID = dbAttributeType.AttributeID;
        if (this.attrData == null)
          this.attrData = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("");
        DataRow[] dataRowArray = this.attrData.Select("F_ATTRIBUTE_ID = " + Convert.ToString(this.selAttr.ID));
        if (dataRowArray.Length != 0)
        {
          this.selAttr.shortName = Convert.ToString(dataRowArray[0]["F_SHORT_NAME"]);
          this.selAttr.longName = Convert.ToString(dataRowArray[0]["F_NAME"]);
        }
      }
      else
        this.selAttr = (SelFormResult) null;
      if (this.selAttr == null)
      {
        this.AttrTypeLbl.Text = "";
        this.AttrNameLbl.Text = "";
      }
    }
    return true;
  }

  /// <summary>Is objType checkbox checked?</summary>
  public bool NoObjType => !this.checkObjType.Checked;

  /// <summary>Text of current Object Type</summary>
  public string objTypeText
  {
    get => this.textObjName.Text.Trim();
    set
    {
      this.lockCheck = true;
      try
      {
        this.textObjName.Text = value;
        this.checkObjType.Checked = value != "";
        this.textObjName.Enabled = this.checkObjType.Checked;
      }
      finally
      {
        this.lockCheck = false;
      }
    }
  }

  /// <summary>Text of current Attribute</summary>
  public string attrText
  {
    get => this.textAttName.Text.Trim();
    set => this.textAttName.Text = value;
  }

  internal static void InitDialog(
    out Form formLocale,
    out SelObjAttrControl SOAControl,
    ref object aSender)
  {
    formLocale = new Form();
    formLocale.MaximizeBox = false;
    formLocale.MaximizeBox = false;
    SOAControl = new SelObjAttrControl();
    formLocale.CancelButton = (IButtonControl) SOAControl.CancelButton;
    formLocale.AcceptButton = (IButtonControl) SOAControl.AcceptButton;
    formLocale.Width = 400;
    formLocale.Height = 150;
    formLocale.StartPosition = FormStartPosition.CenterScreen;
    if (aSender != null)
    {
      string objTypeGUID = ((InputObjectAttribute) aSender).ObjectGUID.Equals(Guid.Empty) ? "" : Convert.ToString((object) ((InputObjectAttribute) aSender).ObjectGUID);
      string attrGUID = ((InputObjectAttribute) aSender).AttributeGUID.Equals(Guid.Empty) ? "" : Convert.ToString((object) ((InputObjectAttribute) aSender).AttributeGUID);
      SOAControl.SetAttrAndObjType(attrGUID, objTypeGUID);
      SOAControl.ReflectObjType();
      SOAControl.ReflectAttr();
    }
    SOAControl.ShowButtons = true;
    SOAControl.Parent = (Control) formLocale;
    SOAControl.Dock = DockStyle.Fill;
    SOAControl.BringToFront();
    SOAControl.sortByShort = false;
    SOAControl.checkMultiValAttr = true;
    SOAControl.Show();
  }

  public static bool ShowDialog(
    ref object aSender,
    string Caption,
    bool FullNames,
    bool allowEmptyAttr,
    bool allowEmptyObj)
  {
    Form formLocale = (Form) null;
    SelObjAttrControl SOAControl = (SelObjAttrControl) null;
    SelObjAttrControl.InitDialog(out formLocale, out SOAControl, ref aSender);
    formLocale.Text = Caption;
    SOAControl._FullNames = FullNames;
    SOAControl.allowEmptyAttr = allowEmptyAttr;
    SOAControl.allowEmptyObj = allowEmptyObj;
    if (!allowEmptyObj)
    {
      SOAControl.checkObjType.Checked = true;
      SOAControl.checkObjType.Enabled = false;
    }
    formLocale.MinimizeBox = false;
    formLocale.MaximizeBox = false;
    formLocale.MinimumSize = new Size(250, 150);
    formLocale.MaximumSize = new Size(600, 250);
    int num = (int) formLocale.ShowDialog();
    aSender = (object) new InputObjectAttribute();
    if (formLocale.DialogResult != DialogResult.OK)
      return false;
    InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) aSender;
    if (SOAControl.selObjType != null && SOAControl.selObjType.GUID != "")
      inputObjectAttribute.ObjectGUID = new Guid(SOAControl.selObjType.GUID);
    if (SOAControl.selAttr != null && SOAControl.selAttr.GUID != "")
      inputObjectAttribute.AttributeGUID = new Guid(SOAControl.selAttr.GUID);
    return true;
  }

  public static bool ShowDialog(ref object aSender)
  {
    Form formLocale = (Form) null;
    SelObjAttrControl SOAControl = (SelObjAttrControl) null;
    SelObjAttrControl.InitDialog(out formLocale, out SOAControl, ref aSender);
    int num = (int) formLocale.ShowDialog();
    aSender = (object) new InputObjectAttribute();
    if (formLocale.DialogResult != DialogResult.OK)
      return false;
    InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) aSender;
    if (SOAControl.selObjType != null && SOAControl.selObjType.GUID != "")
      inputObjectAttribute.ObjectGUID = new Guid(SOAControl.selObjType.GUID);
    if (SOAControl.selAttr != null && SOAControl.selAttr.GUID != "")
      inputObjectAttribute.AttributeGUID = new Guid(SOAControl.selAttr.GUID);
    return true;
  }

  private void buttonAccept_Click(object sender, EventArgs e)
  {
    if (!this.allowEmptyAttr && (this.selAttr == null || this.selAttr.ID == 0))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_438"), LocalizationHolder.rm.GetString("Client.Core_82"));
      this.ParentForm.DialogResult = DialogResult.None;
    }
    if (this.allowEmptyObj || this.selObjType != null && this.selObjType.ID != 0)
      return;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_705"), LocalizationHolder.rm.GetString("Client.Core_82"));
    this.ParentForm.DialogResult = DialogResult.None;
  }
}
