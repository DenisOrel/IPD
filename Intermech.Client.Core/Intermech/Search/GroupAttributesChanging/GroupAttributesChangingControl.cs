
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingControl : UserControl, ISupportInitialize
{
  private BindingList<ObjectBlank> _objects = new BindingList<ObjectBlank>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel2;
  private ContextMenuStrip _objectBlankListControlContextMenuStrip;
  private ToolStripMenuItem _checkOutToolStripMenuItem;
  private ToolStripMenuItem _cancelChangesToolStripMenuItem;
  private ToolStripMenuItem _changeColumnsToolStripMenuItem;
  private GroupAttributesChangingSettingsControl _groupAttributesChangingSettingsControl;
  private ObjectBlankListControl _objectBlankListControl;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _replaceAllButton;
  private Button _replaceButton;
  private Button _findNextButton;
  private SplitContainer _splitContainer;
  private ToolStripMenuItem _cancelAttributeChangesToolStripMenuItem;

  public GroupAttributesChangingControl()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool TrySetCommonEditableAttributesAsDefault
  {
    get => this._objectBlankListControl.TrySetCommonEditableAttributesAsDefault;
    set => this._objectBlankListControl.TrySetCommonEditableAttributesAsDefault = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BindingList<ObjectBlank> Objects
  {
    get => this._objects;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._objects == value)
        return;
      this._objects.ListChanged -= new ListChangedEventHandler(this.Objects_ListChanged);
      this._objects = value;
      this._objects.ListChanged += new ListChangedEventHandler(this.Objects_ListChanged);
      this._objectBlankListControl.Objects = this._objects;
      this.UpdateControls();
    }
  }

  public object CreateMemento()
  {
    return (object) new GroupAttributesChangingControl.GroupAttributesChangingControlMemento()
    {
      SplitterDistance = ((double) this._splitContainer.SplitterDistance / (double) this._splitContainer.Width),
      ObjectBlankListControlState = this._objectBlankListControl.CreateMemento()
    };
  }

  public void SetMemento(object memento)
  {
    GroupAttributesChangingControl.GroupAttributesChangingControlMemento changingControlMemento = memento is GroupAttributesChangingControl.GroupAttributesChangingControlMemento ? (GroupAttributesChangingControl.GroupAttributesChangingControlMemento) memento : throw new ArgumentException();
    this._splitContainer.SplitterDistance = (int) ((double) this._splitContainer.Width * changingControlMemento.SplitterDistance);
    this._objectBlankListControl.SetMemento(changingControlMemento.ObjectBlankListControlState);
  }

  void ISupportInitialize.BeginInit()
  {
  }

  void ISupportInitialize.EndInit()
  {
  }

  private void Objects_ListChanged(object sender, ListChangedEventArgs e) => this.UpdateControls();

  private void GroupAttributesChangingSettingsControl_SettingsChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void FindNextButton_Click(object sender, EventArgs e)
  {
    foreach (ObjectBlank objectBlank in this._objectBlankListControl.GetNextAfterLastSelectedObjectThenLastSelectObject())
    {
      Regex findWhat = this._groupAttributesChangingSettingsControl.FindWhat;
      if (!(objectBlank.GetAttributeValue(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID) is string input))
        input = string.Empty;
      if (findWhat.IsMatch(input))
      {
        this._objectBlankListControl.SelectedObject = objectBlank;
        return;
      }
    }
    int num = (int) MessageBox.Show("Поиск завершен. Ничего не найдено.", "Результаты поиска", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void ReplaceButton_Click(object sender, EventArgs e)
  {
    this.Replace((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects);
  }

  private void ReplaceAllButton_Click(object sender, EventArgs e)
  {
    this.Replace(this._objectBlankListControl.Objects.Where<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsNotNullNotReadOnlyAttribute(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID))));
  }

  private void ObjectBlankListControl_ColumnsChanged(object sender, EventArgs e)
  {
    int[] array = this._objectBlankListControl.Columns.Where<NodeColumn>((Func<NodeColumn, bool>) (o => o.Attribute != null && GroupAttributesChangingHelper.IsEditableAttribute(o.Attribute.AttributeID))).Select<NodeColumn, int>((Func<NodeColumn, int>) (o => o.Attribute.AttributeID)).Distinct<int>().ToArray<int>();
    this._groupAttributesChangingSettingsControl.AttributeTypeIds = array;
    this._groupAttributesChangingSettingsControl.ReplacementAttributeTypeIds = array;
  }

  private void ObjectBlankListControl_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void ObjectBlankListControl_CurrentColumnChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void CheckOutToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects).Any<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsChanged)))
    {
      if (MessageBox.Show("Среди выделенных объектов есть объекты содержащие правки, выполнение команды приведет к потере этих правок. Продолжить?", "Измение атрибутов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.CheckOutSelectedObjects();
    }
    else
      this.CheckOutSelectedObjects();
  }

  private void CancelChangesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    foreach (ObjectBlank selectedObject in this._objectBlankListControl.SelectedObjects)
      selectedObject.RejectChanges();
  }

  private void CancelAttributeChangesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    foreach (ObjectBlank selectedObject in this._objectBlankListControl.SelectedObjects)
    {
      if (selectedObject.IsAttributeChanged(this._objectBlankListControl.CurrentColumn.Attribute.AttributeID))
        selectedObject.Attributes[this._objectBlankListControl.CurrentColumn.Attribute.AttributeID].RejectChanges();
    }
  }

  private void ChangeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this._objectBlankListControl.ChangeColumns();
  }

  private void NotificationService_ObjectsChekedOut(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsCheckOutEventArgs))
      return;
    DBObjectsCheckOutEventArgs checkOutEventArgs = (DBObjectsCheckOutEventArgs) e;
    if (checkOutEventArgs.NewObjectIDs.Count <= 0)
      return;
    bool listChangedEvents = this._objects.RaiseListChangedEvents;
    this._objects.RaiseListChangedEvents = false;
    try
    {
      List<long> oldObjectIds = new List<long>();
      foreach (ObjectBlank objectBlank in this._objects.ToArray<ObjectBlank>())
      {
        if (checkOutEventArgs.ObjectIDs.Contains(objectBlank.ObjectVersionID))
        {
          this._objects.Remove(objectBlank);
          oldObjectIds.Add(objectBlank.ObjectVersionID);
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (ObjectBlank objectBlank in ((IGroupAttributesChangingServerService) sessionKeeper.Session.GetCustomService(typeof (IGroupAttributesChangingServerService))).FindObjects(sessionKeeper.Session.SessionGUID, checkOutEventArgs.NewObjectIDs.Where<long>((Func<long, bool>) (o => oldObjectIds.Contains(-o))).ToArray<long>()))
          this._objects.Add(objectBlank);
      }
    }
    finally
    {
      this._objects.RaiseListChangedEvents = listChangedEvents;
      this._objects.ResetBindings();
    }
  }

  private void UpdateControls()
  {
    this._findNextButton.Enabled = this.CanFindNext();
    this._replaceButton.Enabled = this.CanReplace();
    this._replaceAllButton.Enabled = this.CanReplaceAll();
    this._checkOutToolStripMenuItem.Enabled = this.CanCheckOut();
    this._cancelChangesToolStripMenuItem.Enabled = this.CanCancelChanges();
    this._cancelAttributeChangesToolStripMenuItem.Enabled = this.CanCancelAttributeChanges();
  }

  private bool CanFindNext()
  {
    return this._groupAttributesChangingSettingsControl.FindWhat != null && !AttributeTypeHelper.IsUnknownAttributeTypeID(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID);
  }

  private bool CanReplace()
  {
    return this.CanReplace((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects);
  }

  private bool CanReplaceAll() => this.CanReplace((IEnumerable<ObjectBlank>) this._objects);

  private bool CanReplace(IEnumerable<ObjectBlank> objects)
  {
    return this._groupAttributesChangingSettingsControl.FindWhat != null && !AttributeTypeHelper.IsUnknownAttributeTypeID(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID) && (!this._groupAttributesChangingSettingsControl.UseReplacementAttribute || this._groupAttributesChangingSettingsControl.UseReplacementAttribute && !AttributeTypeHelper.IsUnknownAttributeTypeID(this._groupAttributesChangingSettingsControl.SelectedReplacementAttributeTypeID)) && objects.Any<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsNotNullNotReadOnlyAttribute(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID)));
  }

  private bool CanCheckOut()
  {
    return this._objectBlankListControl.SelectedObjects.Length != 0 && ((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects).All<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.CanCheckOut));
  }

  private bool CanCancelChanges()
  {
    return this._objectBlankListControl.SelectedObjects.Length != 0 && ((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects).All<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsChanged));
  }

  private bool CanCancelAttributeChanges()
  {
    return this._objectBlankListControl.CurrentColumn != null && this._objectBlankListControl.CurrentColumn.Attribute != null && ((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects).All<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.IsAttributeChanged(this._objectBlankListControl.CurrentColumn.Attribute.AttributeID)));
  }

  private void Replace(IEnumerable<ObjectBlank> objects)
  {
    ReplaceWithBuilder replaceWithBuilder = new ReplaceWithBuilder();
    replaceWithBuilder.CharacterCaseTransformation = this._groupAttributesChangingSettingsControl.ReplacementCharacterCaseTransformation;
    replaceWithBuilder.Counters = new Dictionary<int, Counter>();
    foreach (ObjectBlank objectBlank in objects)
    {
      string attributeValue = objectBlank.GetAttributeValue(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID) as string;
      if (this._groupAttributesChangingSettingsControl.FindWhat.IsMatch(attributeValue))
      {
        replaceWithBuilder.CurrentAttributeValue = attributeValue;
        if (this._groupAttributesChangingSettingsControl.UseReplacementText)
          replaceWithBuilder.ReplaceWithTemplate = this._groupAttributesChangingSettingsControl.Replacement;
        else if (!AttributeTypeHelper.IsUnknownAttributeTypeID(this._groupAttributesChangingSettingsControl.SelectedReplacementAttributeTypeID))
          replaceWithBuilder.ReplaceWithAttributeValue = objectBlank.GetAttributeValue(this._groupAttributesChangingSettingsControl.SelectedReplacementAttributeTypeID) as string;
        objectBlank.SetAttributeValue(this._groupAttributesChangingSettingsControl.SelectedAttributeTypeID, (object) this._groupAttributesChangingSettingsControl.FindWhat.Replace(attributeValue, replaceWithBuilder.GetResult()));
      }
    }
  }

  private void CheckOutSelectedObjects()
  {
    INotificationService notificationService = ServiceLocator.Get<INotificationService>();
    notificationService.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotificationService_ObjectsChekedOut));
    try
    {
      ObjectCommands.CheckoutCommand(SelectedItemsHelper.CreateSelectedItemsForObjects(((IEnumerable<ObjectBlank>) this._objectBlankListControl.SelectedObjects).Select<ObjectBlank, long>((Func<ObjectBlank, long>) (o => o.ObjectVersionID)).Distinct<long>().ToArray<long>()), (System.IServiceProvider) ServicesManager.ServiceContainer, (object) null);
    }
    finally
    {
      notificationService.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotificationService_ObjectsChekedOut));
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
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._replaceAllButton = new Button();
    this._replaceButton = new Button();
    this._findNextButton = new Button();
    this._groupAttributesChangingSettingsControl = new GroupAttributesChangingSettingsControl();
    this._objectBlankListControlContextMenuStrip = new ContextMenuStrip(this.components);
    this._checkOutToolStripMenuItem = new ToolStripMenuItem();
    this._cancelChangesToolStripMenuItem = new ToolStripMenuItem();
    this._cancelAttributeChangesToolStripMenuItem = new ToolStripMenuItem();
    this._changeColumnsToolStripMenuItem = new ToolStripMenuItem();
    this._splitContainer = new SplitContainer();
    this._objectBlankListControl = new ObjectBlankListControl();
    this.tableLayoutPanel2.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this._objectBlankListControlContextMenuStrip.SuspendLayout();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    ((ISupportInitialize) this._objectBlankListControl).BeginInit();
    this.SuspendLayout();
    this.tableLayoutPanel2.AutoSize = true;
    this.tableLayoutPanel2.ColumnCount = 1;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this._groupAttributesChangingSettingsControl, 0, 0);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(0, 0);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 2;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel2.Size = new Size(333, 600);
    this.tableLayoutPanel2.TabIndex = 0;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.Controls.Add((Control) this._replaceAllButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._replaceButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._findNextButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 568);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(327, 29);
    this.flowLayoutPanel1.TabIndex = 3;
    this._replaceAllButton.AutoSize = true;
    this._replaceAllButton.Location = new Point(236, 3);
    this._replaceAllButton.Name = "_replaceAllButton";
    this._replaceAllButton.Size = new Size(88, 23);
    this._replaceAllButton.TabIndex = 0;
    this._replaceAllButton.Text = "Заменить все";
    this._replaceAllButton.UseVisualStyleBackColor = true;
    this._replaceAllButton.Click += new EventHandler(this.ReplaceAllButton_Click);
    this._replaceButton.AutoSize = true;
    this._replaceButton.Location = new Point(163, 3);
    this._replaceButton.Name = "_replaceButton";
    this._replaceButton.Size = new Size(67, 23);
    this._replaceButton.TabIndex = 0;
    this._replaceButton.Text = "Заменить";
    this._replaceButton.UseVisualStyleBackColor = true;
    this._replaceButton.Click += new EventHandler(this.ReplaceButton_Click);
    this._findNextButton.AutoSize = true;
    this._findNextButton.Location = new Point(76, 3);
    this._findNextButton.Name = "_findNextButton";
    this._findNextButton.Size = new Size(81, 23);
    this._findNextButton.TabIndex = 0;
    this._findNextButton.Text = "Найти далее";
    this._findNextButton.UseVisualStyleBackColor = true;
    this._findNextButton.Click += new EventHandler(this.FindNextButton_Click);
    this._groupAttributesChangingSettingsControl.AutoSize = true;
    this._groupAttributesChangingSettingsControl.Dock = DockStyle.Fill;
    this._groupAttributesChangingSettingsControl.Location = new Point(3, 3);
    this._groupAttributesChangingSettingsControl.Name = "_groupAttributesChangingSettingsControl";
    this._groupAttributesChangingSettingsControl.Size = new Size(327, 559);
    this._groupAttributesChangingSettingsControl.TabIndex = 2;
    this._groupAttributesChangingSettingsControl.SettingsChanged += new EventHandler(this.GroupAttributesChangingSettingsControl_SettingsChanged);
    this._objectBlankListControlContextMenuStrip.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._checkOutToolStripMenuItem,
      (ToolStripItem) this._cancelChangesToolStripMenuItem,
      (ToolStripItem) this._cancelAttributeChangesToolStripMenuItem,
      (ToolStripItem) this._changeColumnsToolStripMenuItem
    });
    this._objectBlankListControlContextMenuStrip.Name = "_newAttributesObjectsListContextMenuStrip";
    this._objectBlankListControlContextMenuStrip.Size = new Size(223, 92);
    this._checkOutToolStripMenuItem.Name = "_checkOutToolStripMenuItem";
    this._checkOutToolStripMenuItem.Size = new Size(222, 22);
    this._checkOutToolStripMenuItem.Text = "Взять на изменение";
    this._checkOutToolStripMenuItem.Click += new EventHandler(this.CheckOutToolStripMenuItem_Click);
    this._cancelChangesToolStripMenuItem.Name = "_cancelChangesToolStripMenuItem";
    this._cancelChangesToolStripMenuItem.Size = new Size(222, 22);
    this._cancelChangesToolStripMenuItem.Text = "Отменить правки объекта";
    this._cancelChangesToolStripMenuItem.Click += new EventHandler(this.CancelChangesToolStripMenuItem_Click);
    this._cancelAttributeChangesToolStripMenuItem.Name = "_cancelAttributeChangesToolStripMenuItem";
    this._cancelAttributeChangesToolStripMenuItem.Size = new Size(222, 22);
    this._cancelAttributeChangesToolStripMenuItem.Text = "Отменить правки атрибута";
    this._cancelAttributeChangesToolStripMenuItem.Click += new EventHandler(this.CancelAttributeChangesToolStripMenuItem_Click);
    this._changeColumnsToolStripMenuItem.Name = "_changeColumnsToolStripMenuItem";
    this._changeColumnsToolStripMenuItem.Size = new Size(222, 22);
    this._changeColumnsToolStripMenuItem.Text = "Настройка отображения";
    this._changeColumnsToolStripMenuItem.Click += new EventHandler(this.ChangeColumnsToolStripMenuItem_Click);
    this._splitContainer.Dock = DockStyle.Fill;
    this._splitContainer.Location = new Point(0, 0);
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this.tableLayoutPanel2);
    this._splitContainer.Panel2.Controls.Add((Control) this._objectBlankListControl);
    this._splitContainer.Size = new Size(1000, 600);
    this._splitContainer.SplitterDistance = 333;
    this._splitContainer.TabIndex = 3;
    this._objectBlankListControl.AutoSize = true;
    this._objectBlankListControl.ContextMenuStrip = this._objectBlankListControlContextMenuStrip;
    this._objectBlankListControl.Dock = DockStyle.Fill;
    this._objectBlankListControl.Location = new Point(0, 0);
    this._objectBlankListControl.Name = "_objectBlankListControl";
    this._objectBlankListControl.Size = new Size(663, 600);
    this._objectBlankListControl.TabIndex = 1;
    this._objectBlankListControl.SelectionChanged += new EventHandler(this.ObjectBlankListControl_SelectionChanged);
    this._objectBlankListControl.ColumnsChanged += new EventHandler(this.ObjectBlankListControl_ColumnsChanged);
    this._objectBlankListControl.CurrentColumnChanged += new EventHandler(this.ObjectBlankListControl_CurrentColumnChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._splitContainer);
    this.Name = nameof (GroupAttributesChangingControl);
    this.Size = new Size(1000, 600);
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this._objectBlankListControlContextMenuStrip.ResumeLayout(false);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel1.PerformLayout();
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.Panel2.PerformLayout();
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    ((ISupportInitialize) this._objectBlankListControl).EndInit();
    this.ResumeLayout(false);
  }

  [Serializable]
  private sealed class GroupAttributesChangingControlMemento
  {
    public double SplitterDistance { get; set; }

    public object ObjectBlankListControlState { get; internal set; }
  }
}
