
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingForm : Form
{
  private const string GroupAttributesChangingControlStateKey = "GroupAttributesChangingControlState";
  private long[] _objectVersionIds;
  private BindingList<ObjectBlank> _objects = new BindingList<ObjectBlank>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _acceptButton;
  private Button _closeButton;
  private GroupAttributesChangingControl _groupAttributesChangingControl;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;

  public GroupAttributesChangingForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool TrySetCommonEditableAttributesAsDefault
  {
    get => this._groupAttributesChangingControl.TrySetCommonEditableAttributesAsDefault;
    set => this._groupAttributesChangingControl.TrySetCommonEditableAttributesAsDefault = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long[] ObjectVersionIds
  {
    get => this._objectVersionIds;
    set
    {
      if (value == null || value.Length == 0 || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) value))
        throw new ArgumentException();
      if (this._objectVersionIds == value)
        return;
      this._objects.ListChanged -= new ListChangedEventHandler(this.Objects_ListChanged);
      this._objectVersionIds = ((IEnumerable<long>) value).Distinct<long>().ToArray<long>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IGroupAttributesChangingServerService customService = (IGroupAttributesChangingServerService) sessionKeeper.Session.GetCustomService(typeof (IGroupAttributesChangingServerService));
        this._objects = new BindingList<ObjectBlank>();
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        long[] objectVersionIds = this._objectVersionIds;
        foreach (ObjectBlank objectBlank in customService.FindObjects(sessionGuid, objectVersionIds))
          this._objects.Add(objectBlank);
      }
      this._objects.ListChanged += new ListChangedEventHandler(this.Objects_ListChanged);
      this._groupAttributesChangingControl.Objects = this._objects;
      this.UpdateControls();
    }
  }

  private void Objects_ListChanged(object sender, ListChangedEventArgs e) => this.UpdateControls();

  private void GroupAttributesChangingForm_Load(object sender, EventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    FormStorage.LoadLayout((Control) this, (IDictionary) hashtable);
    if (!hashtable.ContainsKey((object) "GroupAttributesChangingControlState"))
      return;
    string stateAsString = hashtable[(object) "GroupAttributesChangingControlState"] as string;
    if (string.IsNullOrEmpty(stateAsString))
      return;
    try
    {
      object memento = this.DeserializeGroupAttributesChangingControlState(stateAsString);
      if (memento == null)
        return;
      this._groupAttributesChangingControl.SetMemento(memento);
    }
    catch
    {
    }
  }

  private void GroupAttributesChangingForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Hashtable()
    {
      {
        (object) "GroupAttributesChangingControlState",
        (object) this.SerializeGroupAttributesChangingControlState(this._groupAttributesChangingControl.CreateMemento())
      }
    });
  }

  private void AcceptButton_Click(object sender, EventArgs e)
  {
    bool listChangedEvents = this._objects.RaiseListChangedEvents;
    this._objects.RaiseListChangedEvents = false;
    try
    {
      ObjectBlank[] array = this._objects.Where<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsChanged)).ToArray<ObjectBlank>();
      foreach (ObjectBlank objectBlank in array)
        this._objects.Remove(objectBlank);
      Dictionary<long, AttributeValues[]> dictionary1 = new Dictionary<long, AttributeValues[]>();
      foreach (ObjectBlank objectBlank in array)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectBlank.ObjectVersionID, false);
          dictionary1.Add(objectBlank.ObjectVersionID, dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeCaption));
        }
      }
      ObjectBlank[] objectBlankArray;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (NotificationContext.Create(sessionKeeper.Session, (object) this))
        {
          objectBlankArray = ((IGroupAttributesChangingServerService) sessionKeeper.Session.GetCustomService(typeof (IGroupAttributesChangingServerService))).SaveObjects(sessionKeeper.Session.SessionGUID, array);
          foreach (ObjectBlank objectBlank in objectBlankArray)
            this._objects.Add(objectBlank);
        }
      }
      Dictionary<long, AttributeValues[]> dictionary2 = new Dictionary<long, AttributeValues[]>();
      foreach (ObjectBlank objectBlank in objectBlankArray)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectBlank.ObjectVersionID, false);
          dictionary2.Add(objectBlank.ObjectVersionID, dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeCaption));
        }
      }
      INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      foreach (ObjectBlank objectBlank in array)
      {
        AttributeValues[] attributeValuesArray = (AttributeValues[]) null;
        dictionary1.TryGetValue(objectBlank.ObjectVersionID, out attributeValuesArray);
        AttributeValues[] source = (AttributeValues[]) null;
        dictionary2.TryGetValue(objectBlank.ObjectVersionID, out source);
        if (attributeValuesArray != null && source != null)
        {
          List<AttributeValues> attributeValuesList1 = new List<AttributeValues>();
          List<AttributeValues> attributeValuesList2 = new List<AttributeValues>();
          foreach (AttributeValues attributeValues1 in attributeValuesArray)
          {
            AttributeValues dirtyObjectAttributeValues = attributeValues1;
            AttributeValues attributeValues2 = ((IEnumerable<AttributeValues>) source).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (o => o.AttributeID == dirtyObjectAttributeValues.AttributeID));
            if (dictionary2 != null && dirtyObjectAttributeValues.Value != attributeValues2.Value)
            {
              attributeValuesList1.Add(dirtyObjectAttributeValues);
              attributeValuesList2.Add(attributeValues2);
            }
          }
          DBObjectsExtendedEventArgs e1 = new DBObjectsExtendedEventArgs("ObjectsChanged", objectBlank.ObjectVersionID, objectBlank.ObjectTypeID, attributeValuesList1.ToArray(), attributeValuesList2.ToArray());
          service.FireEvent((object) "ObjectsChanged", (NotificationEventArgs) e1);
        }
      }
    }
    finally
    {
      this._objects.RaiseListChangedEvents = listChangedEvents;
      this._objects.ResetBindings();
    }
    this.UpdateControls();
  }

  private void CloseButton_Click(object sender, EventArgs e) => this.Close();

  private void Objects_ObjectChanged(object sender, EventArgs e) => this.UpdateControls();

  private void UpdateControls()
  {
    this._acceptButton.Enabled = this._objects.Any<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsChanged));
  }

  private string SerializeGroupAttributesChangingControlState(object state)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, state);
      return Convert.ToBase64String(serializationStream.GetBuffer());
    }
  }

  private object DeserializeGroupAttributesChangingControlState(string stateAsString)
  {
    using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(stateAsString)))
      return new BinaryFormatter().Deserialize((Stream) serializationStream);
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
    this._acceptButton = new Button();
    this._closeButton = new Button();
    this._groupAttributesChangingControl = new GroupAttributesChangingControl();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    ((ISupportInitialize) this._groupAttributesChangingControl).BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this._acceptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._acceptButton.AutoSize = true;
    this._acceptButton.Location = new Point(841, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 1;
    this._acceptButton.Text = "Применить";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    this._closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._closeButton.AutoSize = true;
    this._closeButton.Location = new Point(922, 3);
    this._closeButton.Name = "_closeButton";
    this._closeButton.Size = new Size(75, 23);
    this._closeButton.TabIndex = 1;
    this._closeButton.Text = "Закрыть";
    this._closeButton.UseVisualStyleBackColor = true;
    this._closeButton.Click += new EventHandler(this.CloseButton_Click);
    this._groupAttributesChangingControl.Dock = DockStyle.Fill;
    this._groupAttributesChangingControl.Location = new Point(3, 3);
    this._groupAttributesChangingControl.Name = "_groupAttributesChangingControl";
    this._groupAttributesChangingControl.Size = new Size(1000, 536);
    this._groupAttributesChangingControl.TabIndex = 2;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._groupAttributesChangingControl, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
    this.tableLayoutPanel1.Size = new Size(1006, 582);
    this.tableLayoutPanel1.TabIndex = 3;
    this.flowLayoutPanel1.Controls.Add((Control) this._closeButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 545);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(1000, 34);
    this.flowLayoutPanel1.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(1006, 582);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (GroupAttributesChangingForm);
    this.ShowIcon = false;
    this.Text = "Изменение атрибутов";
    this.FormClosing += new FormClosingEventHandler(this.GroupAttributesChangingForm_FormClosing);
    this.Load += new EventHandler(this.GroupAttributesChangingForm_Load);
    ((ISupportInitialize) this._groupAttributesChangingControl).EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
