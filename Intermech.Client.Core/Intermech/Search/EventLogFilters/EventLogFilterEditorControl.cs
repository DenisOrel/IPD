
// Type: Intermech.Search.EventLogFilters.EventLogFilterEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.EventLogFilters;

public sealed class EventLogFilterEditorControl : UserControl, ISupportInitialize
{
  private static readonly RelationalOperators[] SingleValueFromListRelops = new RelationalOperators[2]
  {
    RelationalOperators.Equal,
    RelationalOperators.NotEqual
  };
  private const RelationalOperators SingleValueFromListDefaultSelectedRelop = RelationalOperators.Equal;
  private static readonly RelationalOperators[] DateTimeAndIntRelops = new RelationalOperators[6]
  {
    RelationalOperators.Equal,
    RelationalOperators.Greater,
    RelationalOperators.GreaterOrEqual,
    RelationalOperators.Less,
    RelationalOperators.LessOrEqual,
    RelationalOperators.NotEqual
  };
  private const RelationalOperators DateTimeDefaultSelectedRelop = RelationalOperators.Greater;
  private const RelationalOperators IntDefaultSelectedRelop = RelationalOperators.Equal;
  private static readonly RelationalOperators[] MultiValueFromListRelops = new RelationalOperators[2]
  {
    RelationalOperators.In,
    RelationalOperators.NotIn
  };
  private const RelationalOperators MultiValueFromListDefaultSelectedRelop = RelationalOperators.In;
  private static readonly RelationalOperators[] StringRelops = new RelationalOperators[8]
  {
    RelationalOperators.EndString,
    RelationalOperators.Equal,
    RelationalOperators.NotEndString,
    RelationalOperators.NotEqual,
    RelationalOperators.NotStartString,
    RelationalOperators.NotSubstring,
    RelationalOperators.StartString,
    RelationalOperators.Substring
  };
  private const RelationalOperators StringDefaultSelectedRelop = RelationalOperators.Substring;
  private EventLogFilter _filter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private TableLayoutPanel tableLayoutPanel4;
  private CheckBox _typeСheckBox;
  private ComboBox _typeRelopComboBox;
  private ComboBox _typeComboBox;
  private TableLayoutPanel tableLayoutPanel5;
  private CheckBox _eventStartCheckBox;
  private ComboBox _eventStartRelopComboBox;
  private DateTimePicker _eventStartDateTimePicker;
  private TableLayoutPanel tableLayoutPanel6;
  private CheckBox _eventEndCheckBox;
  private ComboBox _eventEndRelopComboBox;
  private DateTimePicker _eventEndDateTimePicker;
  private TableLayoutPanel tableLayoutPanel7;
  private CheckBox _eventIDCheckBox;
  private ComboBox _eventIDRelopComboBox;
  private TableLayoutPanel tableLayoutPanel8;
  private CheckBox _actionCheckBox;
  private ComboBox _actionRelopComboBox;
  private CheckedListBox _actionCheckedListBox;
  private TableLayoutPanel tableLayoutPanel9;
  private CheckBox _objectNameCheckBox;
  private ComboBox _objectNameRelopComboBox;
  private TextBox _objectNameTextBox;
  private TableLayoutPanel tableLayoutPanel10;
  private CheckBox _userCheckBox;
  private ComboBox _userRelopComboBox;
  private TableLayoutPanel tableLayoutPanel11;
  private CheckBox _objectVersionIDCheckBox;
  private ComboBox _objectVersionIDRelopComboBox;
  private TableLayoutPanel tableLayoutPanel12;
  private CheckBox _relationIDCheckBox;
  private ComboBox _relationIDRelopComboBox;
  private TableLayoutPanel tableLayoutPanel13;
  private CheckBox _commentCheckBox;
  private ComboBox _commentRelopComboBox;
  private TextBox _commentTextBox;
  private TableLayoutPanel tableLayoutPanel14;
  private CheckBox _categoryCheckBox;
  private ComboBox _categoryRelopComboBox;
  private ComboBox _categoryComboBox;
  private TableLayoutPanel tableLayoutPanel15;
  private CheckBox _categoryIDCheckBox;
  private ComboBox _categoryIDRelopComboBox;
  private TableLayoutPanel tableLayoutPanel16;
  private CheckBox _machineNameCheckBox;
  private ComboBox _machineNameRelopComboBox;
  private TextBox _machineNameTextBox;
  private TableLayoutPanel tableLayoutPanel2;
  private Label label1;
  private TextBox _nameTextBox;
  private ObjectLinkBox _userLinkBox;
  private Int64Box _eventIDInt64Box;
  private Int64Box _objectVersionIDInt64Box;
  private Int64Box _relationIDInt64Box;
  private Int64Box _categoryIDInt64Box;

  public EventLogFilterEditorControl()
  {
    this.InitializeComponent();
    this.InitializeTypeRelopComboBox();
    this.InitializeTypeComboBox();
    this.InitializeEventStartRelopComboBox();
    this.InitializeEventEndRelopComboBox();
    this.InitializeEventIDRelopComboBox();
    this.InitializeActionRelopComboBox();
    this.InitializeActionCheckedListBox();
    this.InitializeObjectNameRelopComboBox();
    this.InitializeUserRelopComboBox();
    this._userLinkBox.AllowEmpty = false;
    this.InitializeObjectVersionIDRelopComboBox();
    this.InitializeRelaionIDRelopComboBox();
    this.InitializeCommentRelopComboBox();
    this.InitializeCategoryRelopComboBox();
    this.InitializeCategoryComboBox();
    this.InitilaizeCategoryIDRelopComboBox();
    this.InitializeMachineNameRelopComboBox();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public EventLogFilter Filter
  {
    get => this._filter;
    set
    {
      if (this._filter == value)
        return;
      if (this._filter != null)
        this._filter.PropertyChanged -= new PropertyChangedEventHandler(this.Filter_PropertyChanged);
      this._filter = value;
      this._nameTextBox.Text = this._filter.Name;
      this._typeСheckBox.CheckedChanged -= new EventHandler(this.TypeСheckBox_CheckedChanged);
      try
      {
        this._typeСheckBox.Checked = this._filter.HasTypeCondition;
      }
      finally
      {
        this._typeСheckBox.CheckedChanged += new EventHandler(this.TypeСheckBox_CheckedChanged);
      }
      if (this._filter.TypeRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._typeRelopComboBox, (object) this._filter.TypeRelop);
      this.SetComboBoxSelectedValue(this._typeComboBox, (object) this._filter.Type);
      this._eventStartCheckBox.CheckedChanged -= new EventHandler(this.EventStartCheckBox_CheckedChanged);
      try
      {
        this._eventStartCheckBox.Checked = this._filter.HasEventStartCondition;
      }
      finally
      {
        this._eventStartCheckBox.CheckedChanged += new EventHandler(this.EventStartCheckBox_CheckedChanged);
      }
      if (this._filter.EventStartRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._eventStartRelopComboBox, (object) this._filter.EventStartRelop);
      if (this._filter.EventStart > DateTime.MinValue)
        this._eventStartDateTimePicker.Value = this._filter.EventStart;
      this._eventEndCheckBox.CheckedChanged -= new EventHandler(this.EventEndCheckBox_CheckedChanged);
      try
      {
        this._eventEndCheckBox.Checked = this._filter.HasEventEndCondition;
      }
      finally
      {
        this._eventEndCheckBox.CheckedChanged += new EventHandler(this.EventEndCheckBox_CheckedChanged);
      }
      if (this._filter.EventEndRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._eventEndRelopComboBox, (object) this._filter.EventEndRelop);
      if (this._filter.EventEnd > DateTime.MinValue)
        this._eventEndDateTimePicker.Value = this._filter.EventEnd;
      this._eventIDCheckBox.CheckedChanged -= new EventHandler(this.EventIDCheckBox_CheckedChanged);
      try
      {
        this._eventIDCheckBox.Checked = this._filter.HasEventIDCondition;
      }
      finally
      {
        this._eventIDCheckBox.CheckedChanged += new EventHandler(this.EventIDCheckBox_CheckedChanged);
      }
      if (this._filter.EventIDRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._eventIDRelopComboBox, (object) this._filter.EventIDRelop);
      this._eventIDInt64Box.Value = (object) this._filter.EventID;
      this._actionCheckBox.CheckedChanged -= new EventHandler(this.ActionCheckBox_CheckedChanged);
      try
      {
        this._actionCheckBox.Checked = this._filter.HasActionCondition;
      }
      finally
      {
        this._actionCheckBox.CheckedChanged += new EventHandler(this.ActionCheckBox_CheckedChanged);
      }
      if (this._filter.ActionRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._actionRelopComboBox, (object) this._filter.ActionRelop);
      this._actionCheckedListBox.SelectedIndexChanged -= new EventHandler(this.ActionCheckedListBox_SelectedIndexChanged);
      try
      {
        for (int index = 0; index < this._actionCheckedListBox.Items.Count; ++index)
          this._actionCheckedListBox.SetItemChecked(index, ((IEnumerable<string>) this._filter.Action).Contains<string>((string) ((Tuple<object, string>) this._actionCheckedListBox.Items[index]).Item1));
      }
      finally
      {
        this._actionCheckedListBox.SelectedIndexChanged += new EventHandler(this.ActionCheckedListBox_SelectedIndexChanged);
      }
      this._objectNameCheckBox.CheckedChanged -= new EventHandler(this.ObjectNameCheckBox_CheckedChanged);
      try
      {
        this._objectNameCheckBox.Checked = this._filter.HasObjectNameCondition;
      }
      finally
      {
        this._objectNameCheckBox.CheckedChanged += new EventHandler(this.ObjectNameCheckBox_CheckedChanged);
      }
      if (this._filter.ObjectNameRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._objectNameRelopComboBox, (object) this._filter.ObjectNameRelop);
      this._objectNameTextBox.Text = this._filter.ObjectName;
      this._userCheckBox.CheckedChanged -= new EventHandler(this.UserCheckBox_CheckedChanged);
      try
      {
        this._userCheckBox.Checked = this._filter.HasUserCondition;
      }
      finally
      {
        this._userCheckBox.CheckedChanged += new EventHandler(this.UserCheckBox_CheckedChanged);
      }
      if (this._filter.UserRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._userRelopComboBox, (object) this._filter.UserRelop);
      this._userLinkBox.Value = (object) this._filter.UserVersionID;
      this._objectVersionIDCheckBox.CheckedChanged -= new EventHandler(this.ObjectVersionIDCheckBox_CheckedChanged);
      try
      {
        this._objectVersionIDCheckBox.Checked = this._filter.HasObjectVersionIDCondition;
      }
      finally
      {
        this._objectVersionIDCheckBox.CheckedChanged += new EventHandler(this.ObjectVersionIDCheckBox_CheckedChanged);
      }
      if (this._filter.ObjectVersionIDRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._objectVersionIDRelopComboBox, (object) this._filter.ObjectVersionIDRelop);
      if (!ObjectHelper.IsUnknownObjectVersionID(this._filter.ObjectVersionID))
        this._objectVersionIDInt64Box.Value = (object) this._filter.ObjectVersionID;
      this._relationIDCheckBox.CheckedChanged -= new EventHandler(this.RelationIDCheckBox_CheckedChanged);
      try
      {
        this._relationIDCheckBox.Checked = this._filter.HasRelationIDCondition;
      }
      finally
      {
        this._relationIDCheckBox.CheckedChanged += new EventHandler(this.RelationIDCheckBox_CheckedChanged);
      }
      if (this._filter.RelationIDRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._relationIDRelopComboBox, (object) this._filter.RelationIDRelop);
      if (!RelationHelper.IsUnknownRelationID(this._filter.RelationID))
        this._relationIDInt64Box.Value = (object) this._filter.RelationID;
      this._commentCheckBox.CheckedChanged -= new EventHandler(this.CommentCheckBox_CheckedChanged);
      try
      {
        this._commentCheckBox.Checked = this._filter.HasCommentCondition;
      }
      finally
      {
        this._commentCheckBox.CheckedChanged += new EventHandler(this.CommentCheckBox_CheckedChanged);
      }
      if (this._filter.CommentRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._commentRelopComboBox, (object) this._filter.CommentRelop);
      this._commentTextBox.Text = this._filter.Comment;
      this._categoryCheckBox.CheckedChanged -= new EventHandler(this.CategoryCheckBox_CheckedChanged);
      try
      {
        this._categoryCheckBox.Checked = this._filter.HasCategoryCondition;
      }
      finally
      {
        this._categoryCheckBox.CheckedChanged += new EventHandler(this.CategoryCheckBox_CheckedChanged);
      }
      if (this._filter.CategoryRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._categoryRelopComboBox, (object) this._filter.CategoryRelop);
      this.SetComboBoxSelectedValue(this._categoryComboBox, (object) this._filter.Category);
      this._categoryIDCheckBox.CheckedChanged -= new EventHandler(this.CategoryIDCheckBox_CheckedChanged);
      try
      {
        this._categoryIDCheckBox.Checked = this._filter.HasCategoryIDCondition;
      }
      finally
      {
        this._categoryIDCheckBox.CheckedChanged += new EventHandler(this.CategoryIDCheckBox_CheckedChanged);
      }
      if (this.Filter.CategoryIDRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._categoryIDRelopComboBox, (object) this._filter.CategoryIDRelop);
      this._categoryIDInt64Box.Value = (object) this._filter.CategoryID;
      this._machineNameCheckBox.CheckedChanged -= new EventHandler(this.MachineNameCheckBox_CheckedChanged);
      try
      {
        this._machineNameCheckBox.Checked = this._filter.HasMachineNameCondition;
      }
      finally
      {
        this._machineNameCheckBox.CheckedChanged += new EventHandler(this.MachineNameCheckBox_CheckedChanged);
      }
      if (this._filter.MachineNameRelop != RelationalOperators.Empty)
        this.SetComboBoxSelectedValue(this._machineNameRelopComboBox, (object) this._filter.MachineNameRelop);
      this._machineNameTextBox.Text = this._filter.MachineName;
      this.UpdateControls();
      if (this._filter == null)
        return;
      this._filter.PropertyChanged += new PropertyChangedEventHandler(this.Filter_PropertyChanged);
    }
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    this._userLinkBox.ObjectTypeID = Intermech.Search.Constants.UserObjectTypeID;
  }

  private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this.UpdateControls();
  }

  private void NameTextBox_TextChanged(object sender, EventArgs e)
  {
    this._filter.Name = this._nameTextBox.Text;
  }

  private void TypeСheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasTypeCondition = this._typeСheckBox.Checked;
    this._filter.TypeRelop = this.GetSelectedRelop(this._typeRelopComboBox);
    this._filter.Type = this.GetComboBoxSelectedValue<EventlogRecordType>(this._typeComboBox);
  }

  private void TypeRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.TypeRelop = this.GetSelectedRelop(this._typeRelopComboBox);
  }

  private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.Type = this.GetComboBoxSelectedValue<EventlogRecordType>(this._typeComboBox);
  }

  private void EventStartCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasEventStartCondition = this._eventStartCheckBox.Checked;
    this._filter.EventStartRelop = this.GetSelectedRelop(this._eventStartRelopComboBox);
    this._filter.EventStart = this._eventStartDateTimePicker.Value;
  }

  private void EventStartRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.EventStartRelop = this.GetSelectedRelop(this._eventStartRelopComboBox);
  }

  private void EventStartDateTimePicker_ValueChanged(object sender, EventArgs e)
  {
    this._filter.EventStart = this._eventStartDateTimePicker.Value;
  }

  private void EventEndCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasEventEndCondition = this._eventEndCheckBox.Checked;
    this._filter.EventEndRelop = this.GetSelectedRelop(this._eventEndRelopComboBox);
    this._filter.EventEnd = this._eventEndDateTimePicker.Value;
  }

  private void EventEndRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.EventEndRelop = this.GetSelectedRelop(this._eventEndRelopComboBox);
  }

  private void EventEndDateTimePicker_ValueChanged(object sender, EventArgs e)
  {
    this._filter.EventEnd = this._eventEndDateTimePicker.Value;
  }

  private void EventIDCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasEventIDCondition = this._eventIDCheckBox.Checked;
    this._filter.EventIDRelop = this.GetSelectedRelop(this._eventIDRelopComboBox);
    this._filter.EventID = this._eventIDInt64Box.TypedValue;
  }

  private void EventIDRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.EventIDRelop = this.GetSelectedRelop(this._eventIDRelopComboBox);
  }

  private void EventIDInt64Box_ValueChanged(object sender, EventArgs e)
  {
    this._filter.EventID = this._eventIDInt64Box.TypedValue;
  }

  private void ActionCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasActionCondition = this._actionCheckBox.Checked;
    this._filter.ActionRelop = this.GetSelectedRelop(this._actionRelopComboBox);
    this._filter.Action = this._actionCheckedListBox.CheckedItems.Cast<Tuple<object, string>>().Select<Tuple<object, string>, object>((Func<Tuple<object, string>, object>) (o => o.Item1)).Cast<string>().ToArray<string>();
  }

  private void ActionRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.ActionRelop = this.GetSelectedRelop(this._actionRelopComboBox);
  }

  private void ActionCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.Action = this._actionCheckedListBox.CheckedItems.Cast<Tuple<object, string>>().Select<Tuple<object, string>, object>((Func<Tuple<object, string>, object>) (o => o.Item1)).Cast<string>().ToArray<string>();
  }

  private void ObjectNameCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasObjectNameCondition = this._objectNameCheckBox.Checked;
    this._filter.ObjectNameRelop = this.GetSelectedRelop(this._objectNameRelopComboBox);
    this._filter.ObjectName = this._objectNameTextBox.Text;
  }

  private void ObjectNameRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.ObjectNameRelop = this.GetSelectedRelop(this._objectNameRelopComboBox);
  }

  private void ObjectNameTextBox_TextChanged(object sender, EventArgs e)
  {
    this._filter.ObjectName = this._objectNameTextBox.Text;
  }

  private void UserCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasUserCondition = this._userCheckBox.Checked;
    this._filter.UserRelop = this.GetSelectedRelop(this._userRelopComboBox);
    this._filter.UserVersionID = this._userLinkBox.TypedValue;
  }

  private void UserRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.UserRelop = this.GetSelectedRelop(this._userRelopComboBox);
  }

  private void UserLinkBox_ValueChanged(object sender, EventArgs e)
  {
    this._filter.UserVersionID = this._userLinkBox.TypedValue;
  }

  private void ObjectVersionIDCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasObjectVersionIDCondition = this._objectVersionIDCheckBox.Checked;
    this._filter.ObjectVersionIDRelop = this.GetSelectedRelop(this._objectVersionIDRelopComboBox);
    this._filter.ObjectVersionID = this._objectVersionIDInt64Box.TypedValue;
  }

  private void ObjectVersionIDRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.ObjectVersionIDRelop = this.GetSelectedRelop(this._objectVersionIDRelopComboBox);
  }

  private void ObjectVersionIDInt64Box_ValueChanged(object sender, EventArgs e)
  {
    this._filter.ObjectVersionID = this._objectVersionIDInt64Box.TypedValue;
  }

  private void RelationIDCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasRelationIDCondition = this._relationIDCheckBox.Checked;
    this._filter.RelationIDRelop = this.GetSelectedRelop(this._relationIDRelopComboBox);
    this._filter.RelationID = this._relationIDInt64Box.TypedValue;
  }

  private void RelationIDRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.RelationIDRelop = this.GetSelectedRelop(this._relationIDRelopComboBox);
  }

  private void RelationIDInt64Box_ValueChanged(object sender, EventArgs e)
  {
    this._filter.RelationID = this._relationIDInt64Box.TypedValue;
  }

  private void CommentCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasCommentCondition = this._commentCheckBox.Checked;
    this._filter.CommentRelop = this.GetSelectedRelop(this._commentRelopComboBox);
    this._filter.Comment = this._commentTextBox.Text;
  }

  private void CommentRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.CommentRelop = this.GetSelectedRelop(this._commentRelopComboBox);
  }

  private void CommentTextBox_TextChanged(object sender, EventArgs e)
  {
    this._filter.Comment = this._commentTextBox.Text;
  }

  private void CategoryCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasCategoryCondition = this._categoryCheckBox.Checked;
    this._filter.CategoryRelop = this.GetSelectedRelop(this._categoryRelopComboBox);
    this._filter.Category = this.GetComboBoxSelectedValue<int>(this._categoryComboBox);
  }

  private void CategoryRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.CategoryRelop = this.GetSelectedRelop(this._categoryRelopComboBox);
  }

  private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.Category = this.GetComboBoxSelectedValue<int>(this._categoryComboBox);
  }

  private void CategoryIDCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasCategoryIDCondition = this._categoryIDCheckBox.Checked;
    this._filter.CategoryIDRelop = this.GetSelectedRelop(this._categoryIDRelopComboBox);
    this._filter.CategoryID = this._categoryIDInt64Box.TypedValue;
  }

  private void CategoryIDRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.CategoryIDRelop = this.GetSelectedRelop(this._categoryIDRelopComboBox);
  }

  private void CategoryIDInt64Box_ValueChanged(object sender, EventArgs e)
  {
    this._filter.CategoryID = this._categoryIDInt64Box.TypedValue;
  }

  private void MachineNameCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._filter.HasMachineNameCondition = this._machineNameCheckBox.Checked;
    this._filter.MachineNameRelop = this.GetSelectedRelop(this._machineNameRelopComboBox);
    this._filter.MachineName = this._machineNameTextBox.Text;
  }

  private void MachineNameRelopComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._filter.MachineNameRelop = this.GetSelectedRelop(this._machineNameRelopComboBox);
  }

  private void MachineNameTextBox_TextChanged(object sender, EventArgs e)
  {
    this._filter.MachineName = this._machineNameTextBox.Text;
  }

  private void InitializeTypeRelopComboBox()
  {
    this._typeRelopComboBox.SelectedIndexChanged -= new EventHandler(this.TypeRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._typeRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.SingleValueFromListRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._typeRelopComboBox.SelectedIndexChanged += new EventHandler(this.TypeRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeTypeComboBox()
  {
    this._typeComboBox.SelectedIndexChanged -= new EventHandler(this.TypeComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._typeComboBox, this.CreateComboBoxItemsForEventLogRecordTypes(), (object) EventlogRecordType.AccessDenied);
    }
    finally
    {
      this._typeComboBox.SelectedIndexChanged += new EventHandler(this.TypeComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeEventStartRelopComboBox()
  {
    this._eventStartRelopComboBox.SelectedIndexChanged -= new EventHandler(this.EventStartRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._eventStartRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Greater);
    }
    finally
    {
      this._eventStartRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventStartRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeEventEndRelopComboBox()
  {
    this._eventEndRelopComboBox.SelectedIndexChanged -= new EventHandler(this.EventEndRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._eventEndRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Greater);
    }
    finally
    {
      this._eventEndRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventEndRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeEventIDRelopComboBox()
  {
    this._eventIDRelopComboBox.SelectedIndexChanged -= new EventHandler(this.EventIDRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._eventIDRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._eventIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventIDRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeActionRelopComboBox()
  {
    this._actionRelopComboBox.SelectedIndexChanged -= new EventHandler(this.ActionRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._actionRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.MultiValueFromListRelops, RelationalOperators.In);
    }
    finally
    {
      this._actionRelopComboBox.SelectedIndexChanged += new EventHandler(this.ActionRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeActionCheckedListBox()
  {
    this._actionCheckedListBox.SelectedIndexChanged -= new EventHandler(this.ActionCheckedListBox_SelectedIndexChanged);
    try
    {
      this.InitializeCheckedListBox(this._actionCheckedListBox, (IEnumerable<Tuple<object, string>>) this.CreateComboBoxItemsForEventLogActions());
    }
    finally
    {
      this._actionCheckedListBox.SelectedIndexChanged += new EventHandler(this.ActionCheckedListBox_SelectedIndexChanged);
    }
  }

  private Tuple<object, string>[] CreateComboBoxItemsForEventLogActions()
  {
    return ((IEnumerable<Tuple<ActionType, string>>) EventLogFiltersHelper.GetAllEventLogActions()).Select<Tuple<ActionType, string>, string>((Func<Tuple<ActionType, string>, string>) (o => o.Item2)).Where<string>((Func<string, bool>) (o => !string.IsNullOrEmpty(o))).Distinct<string>().Select<string, Tuple<object, string>>((Func<string, Tuple<object, string>>) (o => new Tuple<object, string>((object) o, o))).ToArray<Tuple<object, string>>();
  }

  private void InitializeObjectNameRelopComboBox()
  {
    this._objectNameRelopComboBox.SelectedIndexChanged -= new EventHandler(this.ObjectNameRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._objectNameRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.StringRelops, RelationalOperators.Substring);
    }
    finally
    {
      this._objectNameRelopComboBox.SelectedIndexChanged += new EventHandler(this.ObjectNameRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeUserRelopComboBox()
  {
    this._userRelopComboBox.SelectedIndexChanged -= new EventHandler(this.UserRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._userRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.SingleValueFromListRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._userRelopComboBox.SelectedIndexChanged += new EventHandler(this.UserRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeObjectVersionIDRelopComboBox()
  {
    this._objectVersionIDRelopComboBox.SelectedIndexChanged -= new EventHandler(this.ObjectVersionIDRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._objectVersionIDRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._objectVersionIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.ObjectVersionIDRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeRelaionIDRelopComboBox()
  {
    this._relationIDRelopComboBox.SelectedIndexChanged -= new EventHandler(this.RelationIDRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._relationIDRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._relationIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.RelationIDRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeCommentRelopComboBox()
  {
    this._commentRelopComboBox.SelectedIndexChanged -= new EventHandler(this.CommentRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._commentRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.StringRelops, RelationalOperators.Substring);
    }
    finally
    {
      this._commentRelopComboBox.SelectedIndexChanged += new EventHandler(this.CommentRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeCategoryRelopComboBox()
  {
    this._categoryRelopComboBox.SelectedIndexChanged -= new EventHandler(this.CategoryRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._categoryRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.SingleValueFromListRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._categoryRelopComboBox.SelectedIndexChanged += new EventHandler(this.CategoryRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeCategoryComboBox()
  {
    this._categoryComboBox.SelectedIndexChanged -= new EventHandler(this.CategoryComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._categoryComboBox, ((IEnumerable<int>) Consts.GetCategoryTypeIds()).Select<int, Tuple<object, string>>((Func<int, Tuple<object, string>>) (o => new Tuple<object, string>((object) o, Consts.GetCategoryName(o)))), (object) 0);
    }
    finally
    {
      this._categoryComboBox.SelectedIndexChanged += new EventHandler(this.CategoryComboBox_SelectedIndexChanged);
    }
  }

  private void InitilaizeCategoryIDRelopComboBox()
  {
    this._categoryIDRelopComboBox.SelectedIndexChanged -= new EventHandler(this.CategoryIDRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._categoryIDRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.DateTimeAndIntRelops, RelationalOperators.Equal);
    }
    finally
    {
      this._categoryIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.CategoryIDRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeMachineNameRelopComboBox()
  {
    this._machineNameRelopComboBox.SelectedIndexChanged -= new EventHandler(this.MachineNameRelopComboBox_SelectedIndexChanged);
    try
    {
      this.InitializeComboBox(this._machineNameRelopComboBox, (IEnumerable<RelationalOperators>) EventLogFilterEditorControl.StringRelops, RelationalOperators.Substring);
    }
    finally
    {
      this._machineNameRelopComboBox.SelectedIndexChanged += new EventHandler(this.MachineNameRelopComboBox_SelectedIndexChanged);
    }
  }

  private void InitializeComboBox(
    ComboBox comboBox,
    IEnumerable<RelationalOperators> values,
    RelationalOperators selectedValue)
  {
    this.InitializeComboBox(comboBox, values.Cast<Enum>(), (Enum) selectedValue);
  }

  private void InitializeComboBox(ComboBox comboBox, IEnumerable<Enum> values, Enum selectedValue)
  {
    this.InitializeComboBox(comboBox, this.CreateComboBoxItemsFromEnum(values), (object) selectedValue);
  }

  private IEnumerable<Tuple<object, string>> CreateComboBoxItemsFromEnum(IEnumerable<Enum> values)
  {
    return values.Where<Enum>((Func<Enum, bool>) (o => !string.IsNullOrEmpty(o.GetDescription()))).Select<Enum, Tuple<object, string>>((Func<Enum, Tuple<object, string>>) (o => new Tuple<object, string>((object) o, o.GetDescription())));
  }

  private void InitializeComboBox(
    ComboBox comboBox,
    IEnumerable<Tuple<object, string>> items,
    object selectedValue)
  {
    comboBox.DisplayMember = "Item2";
    comboBox.ValueMember = "Item1";
    comboBox.BeginUpdate();
    try
    {
      comboBox.Items.Clear();
      foreach (Tuple<object, string> tuple in (IEnumerable<Tuple<object, string>>) items.OrderBy<Tuple<object, string>, string>((Func<Tuple<object, string>, string>) (o => o.Item2)))
        comboBox.Items.Add((object) tuple);
      if (comboBox.Items.Count <= 0)
        return;
      this.SetComboBoxSelectedValue(comboBox, selectedValue);
    }
    finally
    {
      comboBox.EndUpdate();
    }
  }

  private IEnumerable<Tuple<object, string>> CreateComboBoxItemsForEventLogRecordTypes()
  {
    return ((IEnumerable<Enum>) EventLogFiltersHelper.GetAllEnumValues(typeof (EventlogRecordType))).Select<Enum, Tuple<object, string>>((Func<Enum, Tuple<object, string>>) (o => new Tuple<object, string>((object) o, EventlogRecordTypeHelper.GetCaption((EventlogRecordType) o))));
  }

  private void InitializeCheckedListBox(
    CheckedListBox checkedListBox,
    IEnumerable<Tuple<object, string>> values)
  {
    checkedListBox.DisplayMember = "Item2";
    checkedListBox.ValueMember = "Item1";
    checkedListBox.BeginUpdate();
    try
    {
      checkedListBox.Items.Clear();
      foreach (Tuple<object, string> tuple in (IEnumerable<Tuple<object, string>>) values.OrderBy<Tuple<object, string>, string>((Func<Tuple<object, string>, string>) (o => o.Item2)))
        checkedListBox.Items.Add((object) tuple);
    }
    finally
    {
      checkedListBox.EndUpdate();
    }
  }

  private void SetComboBoxSelectedValue(ComboBox comboBox, object value)
  {
    comboBox.SelectedItem = (object) comboBox.Items.Cast<Tuple<object, string>>().FirstOrDefault<Tuple<object, string>>((Func<Tuple<object, string>, bool>) (o => object.Equals(o.Item1, value)));
  }

  private void UpdateControls()
  {
    this._typeСheckBox.Checked = this._typeRelopComboBox.Enabled = this._typeComboBox.Enabled = this._filter.HasTypeCondition;
    this._eventStartCheckBox.Checked = this._eventStartRelopComboBox.Enabled = this._eventStartDateTimePicker.Enabled = this._filter.HasEventStartCondition;
    this._eventEndCheckBox.Checked = this._eventEndRelopComboBox.Enabled = this._eventEndDateTimePicker.Enabled = this._filter.HasEventEndCondition;
    this._eventIDCheckBox.Checked = this._eventIDRelopComboBox.Enabled = this._eventIDInt64Box.Enabled = this._filter.HasEventIDCondition;
    this._actionCheckBox.Checked = this._actionRelopComboBox.Enabled = this._actionCheckedListBox.Enabled = this._filter.HasActionCondition;
    this._objectNameCheckBox.Checked = this._objectNameRelopComboBox.Enabled = this._objectNameTextBox.Enabled = this._filter.HasObjectNameCondition;
    this._userCheckBox.Checked = this._userRelopComboBox.Enabled = this._userLinkBox.Enabled = this._filter.HasUserCondition;
    this._objectVersionIDCheckBox.Checked = this._objectVersionIDRelopComboBox.Enabled = this._objectVersionIDInt64Box.Enabled = this._filter.HasObjectVersionIDCondition;
    this._relationIDCheckBox.Checked = this._relationIDRelopComboBox.Enabled = this._relationIDInt64Box.Enabled = this._filter.HasRelationIDCondition;
    this._commentCheckBox.Checked = this._commentRelopComboBox.Enabled = this._commentTextBox.Enabled = this._filter.HasCommentCondition;
    this._categoryCheckBox.Checked = this._categoryRelopComboBox.Enabled = this._categoryComboBox.Enabled = this._filter.HasCategoryCondition;
    this._categoryIDCheckBox.Checked = this._categoryIDRelopComboBox.Enabled = this._categoryIDInt64Box.Enabled = this._filter.HasCategoryIDCondition;
    this._machineNameCheckBox.Checked = this._machineNameRelopComboBox.Enabled = this._machineNameTextBox.Enabled = this._filter.HasMachineNameCondition;
  }

  private RelationalOperators GetSelectedRelop(ComboBox comboBox)
  {
    return this.GetComboBoxSelectedValue<RelationalOperators>(comboBox);
  }

  private T GetComboBoxSelectedValue<T>(ComboBox comboBox)
  {
    return (T) ((Tuple<object, string>) comboBox.SelectedItem).Item1;
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.tableLayoutPanel4 = new TableLayoutPanel();
    this._typeСheckBox = new CheckBox();
    this._typeRelopComboBox = new ComboBox();
    this._typeComboBox = new ComboBox();
    this.tableLayoutPanel5 = new TableLayoutPanel();
    this._eventStartCheckBox = new CheckBox();
    this._eventStartRelopComboBox = new ComboBox();
    this._eventStartDateTimePicker = new DateTimePicker();
    this.tableLayoutPanel6 = new TableLayoutPanel();
    this._eventEndCheckBox = new CheckBox();
    this._eventEndRelopComboBox = new ComboBox();
    this._eventEndDateTimePicker = new DateTimePicker();
    this.tableLayoutPanel7 = new TableLayoutPanel();
    this._eventIDCheckBox = new CheckBox();
    this._eventIDRelopComboBox = new ComboBox();
    this._eventIDInt64Box = new Int64Box();
    this.tableLayoutPanel8 = new TableLayoutPanel();
    this._actionCheckBox = new CheckBox();
    this._actionRelopComboBox = new ComboBox();
    this._actionCheckedListBox = new CheckedListBox();
    this.tableLayoutPanel9 = new TableLayoutPanel();
    this._objectNameCheckBox = new CheckBox();
    this._objectNameRelopComboBox = new ComboBox();
    this._objectNameTextBox = new TextBox();
    this.tableLayoutPanel10 = new TableLayoutPanel();
    this._userCheckBox = new CheckBox();
    this._userRelopComboBox = new ComboBox();
    this._userLinkBox = new ObjectLinkBox();
    this.tableLayoutPanel11 = new TableLayoutPanel();
    this._objectVersionIDCheckBox = new CheckBox();
    this._objectVersionIDRelopComboBox = new ComboBox();
    this._objectVersionIDInt64Box = new Int64Box();
    this.tableLayoutPanel12 = new TableLayoutPanel();
    this._relationIDCheckBox = new CheckBox();
    this._relationIDRelopComboBox = new ComboBox();
    this._relationIDInt64Box = new Int64Box();
    this.tableLayoutPanel13 = new TableLayoutPanel();
    this._commentCheckBox = new CheckBox();
    this._commentRelopComboBox = new ComboBox();
    this._commentTextBox = new TextBox();
    this.tableLayoutPanel14 = new TableLayoutPanel();
    this._categoryCheckBox = new CheckBox();
    this._categoryRelopComboBox = new ComboBox();
    this._categoryComboBox = new ComboBox();
    this.tableLayoutPanel15 = new TableLayoutPanel();
    this._categoryIDCheckBox = new CheckBox();
    this._categoryIDRelopComboBox = new ComboBox();
    this._categoryIDInt64Box = new Int64Box();
    this.tableLayoutPanel16 = new TableLayoutPanel();
    this._machineNameCheckBox = new CheckBox();
    this._machineNameRelopComboBox = new ComboBox();
    this._machineNameTextBox = new TextBox();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.label1 = new Label();
    this._nameTextBox = new TextBox();
    this.tableLayoutPanel1.SuspendLayout();
    this.tableLayoutPanel4.SuspendLayout();
    this.tableLayoutPanel5.SuspendLayout();
    this.tableLayoutPanel6.SuspendLayout();
    this.tableLayoutPanel7.SuspendLayout();
    this.tableLayoutPanel8.SuspendLayout();
    this.tableLayoutPanel9.SuspendLayout();
    this.tableLayoutPanel10.SuspendLayout();
    this.tableLayoutPanel11.SuspendLayout();
    this.tableLayoutPanel12.SuspendLayout();
    this.tableLayoutPanel13.SuspendLayout();
    this.tableLayoutPanel14.SuspendLayout();
    this.tableLayoutPanel15.SuspendLayout();
    this.tableLayoutPanel16.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel4, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel5, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel6, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel7, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel8, 0, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel9, 0, 6);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel10, 0, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel11, 0, 8);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel12, 0, 9);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel13, 0, 10);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel14, 0, 11);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel15, 0, 12);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel16, 0, 13);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 15;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(800, 603);
    this.tableLayoutPanel1.TabIndex = 0;
    this.tableLayoutPanel4.AutoSize = true;
    this.tableLayoutPanel4.ColumnCount = 3;
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel4.Controls.Add((Control) this._typeСheckBox, 0, 0);
    this.tableLayoutPanel4.Controls.Add((Control) this._typeRelopComboBox, 1, 0);
    this.tableLayoutPanel4.Controls.Add((Control) this._typeComboBox, 2, 0);
    this.tableLayoutPanel4.Dock = DockStyle.Fill;
    this.tableLayoutPanel4.Location = new Point(3, 35);
    this.tableLayoutPanel4.Name = "tableLayoutPanel4";
    this.tableLayoutPanel4.RowCount = 1;
    this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel4.Size = new Size(794, 27);
    this.tableLayoutPanel4.TabIndex = 0;
    this._typeСheckBox.AutoSize = true;
    this._typeСheckBox.Location = new Point(3, 3);
    this._typeСheckBox.Name = "_typeСheckBox";
    this._typeСheckBox.Size = new Size(91, 17);
    this._typeСheckBox.TabIndex = 0;
    this._typeСheckBox.Text = "Тип события";
    this._typeСheckBox.UseVisualStyleBackColor = true;
    this._typeСheckBox.CheckedChanged += new EventHandler(this.TypeСheckBox_CheckedChanged);
    this._typeRelopComboBox.Dock = DockStyle.Fill;
    this._typeRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._typeRelopComboBox.FormattingEnabled = true;
    this._typeRelopComboBox.Location = new Point(267, 3);
    this._typeRelopComboBox.Name = "_typeRelopComboBox";
    this._typeRelopComboBox.Size = new Size(258, 21);
    this._typeRelopComboBox.TabIndex = 1;
    this._typeRelopComboBox.SelectedIndexChanged += new EventHandler(this.TypeRelopComboBox_SelectedIndexChanged);
    this._typeComboBox.Dock = DockStyle.Fill;
    this._typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._typeComboBox.FormattingEnabled = true;
    this._typeComboBox.Location = new Point(531, 3);
    this._typeComboBox.Name = "_typeComboBox";
    this._typeComboBox.Size = new Size(260, 21);
    this._typeComboBox.TabIndex = 2;
    this._typeComboBox.SelectedIndexChanged += new EventHandler(this.TypeComboBox_SelectedIndexChanged);
    this.tableLayoutPanel5.AutoSize = true;
    this.tableLayoutPanel5.ColumnCount = 3;
    this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel5.Controls.Add((Control) this._eventStartCheckBox, 0, 0);
    this.tableLayoutPanel5.Controls.Add((Control) this._eventStartRelopComboBox, 1, 0);
    this.tableLayoutPanel5.Controls.Add((Control) this._eventStartDateTimePicker, 2, 0);
    this.tableLayoutPanel5.Dock = DockStyle.Fill;
    this.tableLayoutPanel5.Location = new Point(3, 68);
    this.tableLayoutPanel5.Name = "tableLayoutPanel5";
    this.tableLayoutPanel5.RowCount = 1;
    this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel5.Size = new Size(794, 27);
    this.tableLayoutPanel5.TabIndex = 0;
    this._eventStartCheckBox.AutoSize = true;
    this._eventStartCheckBox.Location = new Point(3, 3);
    this._eventStartCheckBox.Name = "_eventStartCheckBox";
    this._eventStartCheckBox.Size = new Size(109, 17);
    this._eventStartCheckBox.TabIndex = 0;
    this._eventStartCheckBox.Text = "Начало события";
    this._eventStartCheckBox.UseVisualStyleBackColor = true;
    this._eventStartCheckBox.CheckedChanged += new EventHandler(this.EventStartCheckBox_CheckedChanged);
    this._eventStartRelopComboBox.Dock = DockStyle.Fill;
    this._eventStartRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._eventStartRelopComboBox.FormattingEnabled = true;
    this._eventStartRelopComboBox.Location = new Point(267, 3);
    this._eventStartRelopComboBox.Name = "_eventStartRelopComboBox";
    this._eventStartRelopComboBox.Size = new Size(258, 21);
    this._eventStartRelopComboBox.TabIndex = 1;
    this._eventStartRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventStartRelopComboBox_SelectedIndexChanged);
    this._eventStartDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
    this._eventStartDateTimePicker.Dock = DockStyle.Fill;
    this._eventStartDateTimePicker.Format = DateTimePickerFormat.Custom;
    this._eventStartDateTimePicker.Location = new Point(531, 3);
    this._eventStartDateTimePicker.Name = "_eventStartDateTimePicker";
    this._eventStartDateTimePicker.Size = new Size(260, 20);
    this._eventStartDateTimePicker.TabIndex = 2;
    this._eventStartDateTimePicker.ValueChanged += new EventHandler(this.EventStartDateTimePicker_ValueChanged);
    this.tableLayoutPanel6.AutoSize = true;
    this.tableLayoutPanel6.ColumnCount = 3;
    this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel6.Controls.Add((Control) this._eventEndCheckBox, 0, 0);
    this.tableLayoutPanel6.Controls.Add((Control) this._eventEndRelopComboBox, 1, 0);
    this.tableLayoutPanel6.Controls.Add((Control) this._eventEndDateTimePicker, 2, 0);
    this.tableLayoutPanel6.Dock = DockStyle.Fill;
    this.tableLayoutPanel6.Location = new Point(3, 101);
    this.tableLayoutPanel6.Name = "tableLayoutPanel6";
    this.tableLayoutPanel6.RowCount = 1;
    this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel6.Size = new Size(794, 27);
    this.tableLayoutPanel6.TabIndex = 0;
    this._eventEndCheckBox.AutoSize = true;
    this._eventEndCheckBox.Location = new Point(3, 3);
    this._eventEndCheckBox.Name = "_eventEndCheckBox";
    this._eventEndCheckBox.Size = new Size(135, 17);
    this._eventEndCheckBox.TabIndex = 0;
    this._eventEndCheckBox.Text = "Завершение события";
    this._eventEndCheckBox.UseVisualStyleBackColor = true;
    this._eventEndCheckBox.CheckedChanged += new EventHandler(this.EventEndCheckBox_CheckedChanged);
    this._eventEndRelopComboBox.Dock = DockStyle.Fill;
    this._eventEndRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._eventEndRelopComboBox.FormattingEnabled = true;
    this._eventEndRelopComboBox.Location = new Point(267, 3);
    this._eventEndRelopComboBox.Name = "_eventEndRelopComboBox";
    this._eventEndRelopComboBox.Size = new Size(258, 21);
    this._eventEndRelopComboBox.TabIndex = 1;
    this._eventEndRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventEndRelopComboBox_SelectedIndexChanged);
    this._eventEndDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
    this._eventEndDateTimePicker.Dock = DockStyle.Fill;
    this._eventEndDateTimePicker.Format = DateTimePickerFormat.Custom;
    this._eventEndDateTimePicker.Location = new Point(531, 3);
    this._eventEndDateTimePicker.Name = "_eventEndDateTimePicker";
    this._eventEndDateTimePicker.Size = new Size(260, 20);
    this._eventEndDateTimePicker.TabIndex = 2;
    this._eventEndDateTimePicker.ValueChanged += new EventHandler(this.EventEndDateTimePicker_ValueChanged);
    this.tableLayoutPanel7.AutoSize = true;
    this.tableLayoutPanel7.ColumnCount = 3;
    this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel7.Controls.Add((Control) this._eventIDCheckBox, 0, 0);
    this.tableLayoutPanel7.Controls.Add((Control) this._eventIDRelopComboBox, 1, 0);
    this.tableLayoutPanel7.Controls.Add((Control) this._eventIDInt64Box, 2, 0);
    this.tableLayoutPanel7.Dock = DockStyle.Fill;
    this.tableLayoutPanel7.Location = new Point(3, 134);
    this.tableLayoutPanel7.Name = "tableLayoutPanel7";
    this.tableLayoutPanel7.RowCount = 1;
    this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel7.Size = new Size(794, 27);
    this.tableLayoutPanel7.TabIndex = 0;
    this._eventIDCheckBox.AutoSize = true;
    this._eventIDCheckBox.Location = new Point(3, 3);
    this._eventIDCheckBox.Name = "_eventIDCheckBox";
    this._eventIDCheckBox.Size = new Size(83, 17);
    this._eventIDCheckBox.TabIndex = 0;
    this._eventIDCheckBox.Text = "ID события";
    this._eventIDCheckBox.UseVisualStyleBackColor = true;
    this._eventIDCheckBox.CheckedChanged += new EventHandler(this.EventIDCheckBox_CheckedChanged);
    this._eventIDRelopComboBox.Dock = DockStyle.Fill;
    this._eventIDRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._eventIDRelopComboBox.FormattingEnabled = true;
    this._eventIDRelopComboBox.Location = new Point(267, 3);
    this._eventIDRelopComboBox.Name = "_eventIDRelopComboBox";
    this._eventIDRelopComboBox.Size = new Size(258, 21);
    this._eventIDRelopComboBox.TabIndex = 1;
    this._eventIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.EventIDRelopComboBox_SelectedIndexChanged);
    this._eventIDInt64Box.AutoSize = true;
    this._eventIDInt64Box.Dock = DockStyle.Fill;
    this._eventIDInt64Box.Location = new Point(531, 3);
    this._eventIDInt64Box.Name = "_eventIDInt64Box";
    this._eventIDInt64Box.Size = new Size(260, 21);
    this._eventIDInt64Box.TabIndex = 2;
    this._eventIDInt64Box.ValueChanged += new EventHandler(this.EventIDInt64Box_ValueChanged);
    this.tableLayoutPanel8.AutoSize = true;
    this.tableLayoutPanel8.ColumnCount = 3;
    this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel8.Controls.Add((Control) this._actionCheckBox, 0, 0);
    this.tableLayoutPanel8.Controls.Add((Control) this._actionRelopComboBox, 1, 0);
    this.tableLayoutPanel8.Controls.Add((Control) this._actionCheckedListBox, 2, 0);
    this.tableLayoutPanel8.Dock = DockStyle.Fill;
    this.tableLayoutPanel8.Location = new Point(3, 167);
    this.tableLayoutPanel8.Name = "tableLayoutPanel8";
    this.tableLayoutPanel8.RowCount = 1;
    this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel8.Size = new Size(794, 126);
    this.tableLayoutPanel8.TabIndex = 0;
    this._actionCheckBox.AutoSize = true;
    this._actionCheckBox.Location = new Point(3, 3);
    this._actionCheckBox.Name = "_actionCheckBox";
    this._actionCheckBox.Size = new Size(95, 17);
    this._actionCheckBox.TabIndex = 0;
    this._actionCheckBox.Text = "Вид действия";
    this._actionCheckBox.UseVisualStyleBackColor = true;
    this._actionCheckBox.CheckedChanged += new EventHandler(this.ActionCheckBox_CheckedChanged);
    this._actionRelopComboBox.Dock = DockStyle.Fill;
    this._actionRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._actionRelopComboBox.FormattingEnabled = true;
    this._actionRelopComboBox.Location = new Point(267, 3);
    this._actionRelopComboBox.Name = "_actionRelopComboBox";
    this._actionRelopComboBox.Size = new Size(258, 21);
    this._actionRelopComboBox.TabIndex = 1;
    this._actionRelopComboBox.SelectedIndexChanged += new EventHandler(this.ActionRelopComboBox_SelectedIndexChanged);
    this._actionCheckedListBox.CheckOnClick = true;
    this._actionCheckedListBox.Dock = DockStyle.Fill;
    this._actionCheckedListBox.FormattingEnabled = true;
    this._actionCheckedListBox.Location = new Point(531, 3);
    this._actionCheckedListBox.Name = "_actionCheckedListBox";
    this._actionCheckedListBox.Size = new Size(260, 120);
    this._actionCheckedListBox.TabIndex = 2;
    this._actionCheckedListBox.SelectedIndexChanged += new EventHandler(this.ActionCheckedListBox_SelectedIndexChanged);
    this.tableLayoutPanel9.AutoSize = true;
    this.tableLayoutPanel9.ColumnCount = 3;
    this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel9.Controls.Add((Control) this._objectNameCheckBox, 0, 0);
    this.tableLayoutPanel9.Controls.Add((Control) this._objectNameRelopComboBox, 1, 0);
    this.tableLayoutPanel9.Controls.Add((Control) this._objectNameTextBox, 2, 0);
    this.tableLayoutPanel9.Dock = DockStyle.Fill;
    this.tableLayoutPanel9.Location = new Point(3, 299);
    this.tableLayoutPanel9.Name = "tableLayoutPanel9";
    this.tableLayoutPanel9.RowCount = 1;
    this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel9.Size = new Size(794, 27);
    this.tableLayoutPanel9.TabIndex = 0;
    this._objectNameCheckBox.AutoSize = true;
    this._objectNameCheckBox.Location = new Point(3, 3);
    this._objectNameCheckBox.Name = "_objectNameCheckBox";
    this._objectNameCheckBox.Size = new Size(93, 17);
    this._objectNameCheckBox.TabIndex = 0;
    this._objectNameCheckBox.Text = "Имя объекта";
    this._objectNameCheckBox.UseVisualStyleBackColor = true;
    this._objectNameCheckBox.CheckedChanged += new EventHandler(this.ObjectNameCheckBox_CheckedChanged);
    this._objectNameRelopComboBox.Dock = DockStyle.Fill;
    this._objectNameRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._objectNameRelopComboBox.FormattingEnabled = true;
    this._objectNameRelopComboBox.Location = new Point(267, 3);
    this._objectNameRelopComboBox.Name = "_objectNameRelopComboBox";
    this._objectNameRelopComboBox.Size = new Size(258, 21);
    this._objectNameRelopComboBox.TabIndex = 1;
    this._objectNameRelopComboBox.SelectedIndexChanged += new EventHandler(this.ObjectNameRelopComboBox_SelectedIndexChanged);
    this._objectNameTextBox.Dock = DockStyle.Fill;
    this._objectNameTextBox.Location = new Point(531, 3);
    this._objectNameTextBox.Name = "_objectNameTextBox";
    this._objectNameTextBox.Size = new Size(260, 20);
    this._objectNameTextBox.TabIndex = 2;
    this._objectNameTextBox.TextChanged += new EventHandler(this.ObjectNameTextBox_TextChanged);
    this.tableLayoutPanel10.AutoSize = true;
    this.tableLayoutPanel10.ColumnCount = 3;
    this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel10.Controls.Add((Control) this._userCheckBox, 0, 0);
    this.tableLayoutPanel10.Controls.Add((Control) this._userRelopComboBox, 1, 0);
    this.tableLayoutPanel10.Controls.Add((Control) this._userLinkBox, 2, 0);
    this.tableLayoutPanel10.Dock = DockStyle.Fill;
    this.tableLayoutPanel10.Location = new Point(3, 332);
    this.tableLayoutPanel10.Name = "tableLayoutPanel10";
    this.tableLayoutPanel10.RowCount = 1;
    this.tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel10.Size = new Size(794, 27);
    this.tableLayoutPanel10.TabIndex = 0;
    this._userCheckBox.AutoSize = true;
    this._userCheckBox.Location = new Point(3, 3);
    this._userCheckBox.Name = "_userCheckBox";
    this._userCheckBox.Size = new Size(99, 17);
    this._userCheckBox.TabIndex = 0;
    this._userCheckBox.Text = "Пользователь";
    this._userCheckBox.UseVisualStyleBackColor = true;
    this._userCheckBox.CheckedChanged += new EventHandler(this.UserCheckBox_CheckedChanged);
    this._userRelopComboBox.Dock = DockStyle.Fill;
    this._userRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._userRelopComboBox.FormattingEnabled = true;
    this._userRelopComboBox.Location = new Point(267, 3);
    this._userRelopComboBox.Name = "_userRelopComboBox";
    this._userRelopComboBox.Size = new Size(258, 21);
    this._userRelopComboBox.TabIndex = 1;
    this._userRelopComboBox.SelectedIndexChanged += new EventHandler(this.UserRelopComboBox_SelectedIndexChanged);
    this._userLinkBox.AutoSize = true;
    this._userLinkBox.BackColor = SystemColors.ControlLightLight;
    this._userLinkBox.BorderStyle = BorderStyle.FixedSingle;
    this._userLinkBox.Dock = DockStyle.Fill;
    this._userLinkBox.Location = new Point(531, 3);
    this._userLinkBox.Name = "_userLinkBox";
    this._userLinkBox.Size = new Size(260, 21);
    this._userLinkBox.TabIndex = 2;
    this._userLinkBox.ValueChanged += new EventHandler(this.UserLinkBox_ValueChanged);
    this.tableLayoutPanel11.AutoSize = true;
    this.tableLayoutPanel11.ColumnCount = 3;
    this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel11.Controls.Add((Control) this._objectVersionIDCheckBox, 0, 0);
    this.tableLayoutPanel11.Controls.Add((Control) this._objectVersionIDRelopComboBox, 1, 0);
    this.tableLayoutPanel11.Controls.Add((Control) this._objectVersionIDInt64Box, 2, 0);
    this.tableLayoutPanel11.Dock = DockStyle.Fill;
    this.tableLayoutPanel11.Location = new Point(3, 365);
    this.tableLayoutPanel11.Name = "tableLayoutPanel11";
    this.tableLayoutPanel11.RowCount = 1;
    this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel11.Size = new Size(794, 27);
    this.tableLayoutPanel11.TabIndex = 0;
    this._objectVersionIDCheckBox.AutoSize = true;
    this._objectVersionIDCheckBox.Location = new Point(3, 3);
    this._objectVersionIDCheckBox.Name = "_objectVersionIDCheckBox";
    this._objectVersionIDCheckBox.Size = new Size(121, 17);
    this._objectVersionIDCheckBox.TabIndex = 0;
    this._objectVersionIDCheckBox.Text = "ID версии объекта";
    this._objectVersionIDCheckBox.UseVisualStyleBackColor = true;
    this._objectVersionIDCheckBox.CheckedChanged += new EventHandler(this.ObjectVersionIDCheckBox_CheckedChanged);
    this._objectVersionIDRelopComboBox.Dock = DockStyle.Fill;
    this._objectVersionIDRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._objectVersionIDRelopComboBox.FormattingEnabled = true;
    this._objectVersionIDRelopComboBox.Location = new Point(267, 3);
    this._objectVersionIDRelopComboBox.Name = "_objectVersionIDRelopComboBox";
    this._objectVersionIDRelopComboBox.Size = new Size(258, 21);
    this._objectVersionIDRelopComboBox.TabIndex = 1;
    this._objectVersionIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.ObjectVersionIDRelopComboBox_SelectedIndexChanged);
    this._objectVersionIDInt64Box.AutoSize = true;
    this._objectVersionIDInt64Box.Dock = DockStyle.Fill;
    this._objectVersionIDInt64Box.Location = new Point(531, 3);
    this._objectVersionIDInt64Box.Name = "_objectVersionIDInt64Box";
    this._objectVersionIDInt64Box.Size = new Size(260, 21);
    this._objectVersionIDInt64Box.TabIndex = 2;
    this._objectVersionIDInt64Box.ValueChanged += new EventHandler(this.ObjectVersionIDInt64Box_ValueChanged);
    this.tableLayoutPanel12.AutoSize = true;
    this.tableLayoutPanel12.ColumnCount = 3;
    this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel12.Controls.Add((Control) this._relationIDCheckBox, 0, 0);
    this.tableLayoutPanel12.Controls.Add((Control) this._relationIDRelopComboBox, 1, 0);
    this.tableLayoutPanel12.Controls.Add((Control) this._relationIDInt64Box, 2, 0);
    this.tableLayoutPanel12.Dock = DockStyle.Fill;
    this.tableLayoutPanel12.Location = new Point(3, 398);
    this.tableLayoutPanel12.Name = "tableLayoutPanel12";
    this.tableLayoutPanel12.RowCount = 1;
    this.tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel12.Size = new Size(794, 27);
    this.tableLayoutPanel12.TabIndex = 0;
    this._relationIDCheckBox.AutoSize = true;
    this._relationIDCheckBox.Location = new Point(3, 3);
    this._relationIDCheckBox.Name = "_relationIDCheckBox";
    this._relationIDCheckBox.Size = new Size(70, 17);
    this._relationIDCheckBox.TabIndex = 0;
    this._relationIDCheckBox.Text = "ID связи";
    this._relationIDCheckBox.UseVisualStyleBackColor = true;
    this._relationIDCheckBox.CheckedChanged += new EventHandler(this.RelationIDCheckBox_CheckedChanged);
    this._relationIDRelopComboBox.Dock = DockStyle.Fill;
    this._relationIDRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._relationIDRelopComboBox.FormattingEnabled = true;
    this._relationIDRelopComboBox.Location = new Point(267, 3);
    this._relationIDRelopComboBox.Name = "_relationIDRelopComboBox";
    this._relationIDRelopComboBox.Size = new Size(258, 21);
    this._relationIDRelopComboBox.TabIndex = 1;
    this._relationIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.RelationIDRelopComboBox_SelectedIndexChanged);
    this._relationIDInt64Box.AutoSize = true;
    this._relationIDInt64Box.Dock = DockStyle.Fill;
    this._relationIDInt64Box.Location = new Point(531, 3);
    this._relationIDInt64Box.Name = "_relationIDInt64Box";
    this._relationIDInt64Box.Size = new Size(260, 21);
    this._relationIDInt64Box.TabIndex = 2;
    this._relationIDInt64Box.ValueChanged += new EventHandler(this.RelationIDInt64Box_ValueChanged);
    this.tableLayoutPanel13.AutoSize = true;
    this.tableLayoutPanel13.ColumnCount = 3;
    this.tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel13.Controls.Add((Control) this._commentCheckBox, 0, 0);
    this.tableLayoutPanel13.Controls.Add((Control) this._commentRelopComboBox, 1, 0);
    this.tableLayoutPanel13.Controls.Add((Control) this._commentTextBox, 2, 0);
    this.tableLayoutPanel13.Dock = DockStyle.Fill;
    this.tableLayoutPanel13.Location = new Point(3, 431);
    this.tableLayoutPanel13.Name = "tableLayoutPanel13";
    this.tableLayoutPanel13.RowCount = 1;
    this.tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel13.Size = new Size(794, 27);
    this.tableLayoutPanel13.TabIndex = 0;
    this._commentCheckBox.AutoSize = true;
    this._commentCheckBox.Location = new Point(3, 3);
    this._commentCheckBox.Name = "_commentCheckBox";
    this._commentCheckBox.Size = new Size(96 /*0x60*/, 17);
    this._commentCheckBox.TabIndex = 0;
    this._commentCheckBox.Text = "Комментарии";
    this._commentCheckBox.UseVisualStyleBackColor = true;
    this._commentCheckBox.CheckedChanged += new EventHandler(this.CommentCheckBox_CheckedChanged);
    this._commentRelopComboBox.Dock = DockStyle.Fill;
    this._commentRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._commentRelopComboBox.FormattingEnabled = true;
    this._commentRelopComboBox.Location = new Point(267, 3);
    this._commentRelopComboBox.Name = "_commentRelopComboBox";
    this._commentRelopComboBox.Size = new Size(258, 21);
    this._commentRelopComboBox.TabIndex = 1;
    this._commentRelopComboBox.SelectedIndexChanged += new EventHandler(this.CommentRelopComboBox_SelectedIndexChanged);
    this._commentTextBox.Dock = DockStyle.Fill;
    this._commentTextBox.Location = new Point(531, 3);
    this._commentTextBox.Name = "_commentTextBox";
    this._commentTextBox.Size = new Size(260, 20);
    this._commentTextBox.TabIndex = 2;
    this._commentTextBox.TextChanged += new EventHandler(this.CommentTextBox_TextChanged);
    this.tableLayoutPanel14.AutoSize = true;
    this.tableLayoutPanel14.ColumnCount = 3;
    this.tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel14.Controls.Add((Control) this._categoryCheckBox, 0, 0);
    this.tableLayoutPanel14.Controls.Add((Control) this._categoryRelopComboBox, 1, 0);
    this.tableLayoutPanel14.Controls.Add((Control) this._categoryComboBox, 2, 0);
    this.tableLayoutPanel14.Dock = DockStyle.Fill;
    this.tableLayoutPanel14.Location = new Point(3, 464);
    this.tableLayoutPanel14.Name = "tableLayoutPanel14";
    this.tableLayoutPanel14.RowCount = 1;
    this.tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel14.Size = new Size(794, 27);
    this.tableLayoutPanel14.TabIndex = 0;
    this._categoryCheckBox.AutoSize = true;
    this._categoryCheckBox.Location = new Point(3, 3);
    this._categoryCheckBox.Name = "_categoryCheckBox";
    this._categoryCheckBox.Size = new Size(79, 17);
    this._categoryCheckBox.TabIndex = 0;
    this._categoryCheckBox.Text = "Категория";
    this._categoryCheckBox.UseVisualStyleBackColor = true;
    this._categoryCheckBox.CheckedChanged += new EventHandler(this.CategoryCheckBox_CheckedChanged);
    this._categoryRelopComboBox.Dock = DockStyle.Fill;
    this._categoryRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._categoryRelopComboBox.FormattingEnabled = true;
    this._categoryRelopComboBox.Location = new Point(267, 3);
    this._categoryRelopComboBox.Name = "_categoryRelopComboBox";
    this._categoryRelopComboBox.Size = new Size(258, 21);
    this._categoryRelopComboBox.TabIndex = 1;
    this._categoryRelopComboBox.SelectedIndexChanged += new EventHandler(this.CategoryRelopComboBox_SelectedIndexChanged);
    this._categoryComboBox.Dock = DockStyle.Fill;
    this._categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._categoryComboBox.FormattingEnabled = true;
    this._categoryComboBox.Location = new Point(531, 3);
    this._categoryComboBox.Name = "_categoryComboBox";
    this._categoryComboBox.Size = new Size(260, 21);
    this._categoryComboBox.TabIndex = 2;
    this._categoryComboBox.SelectedIndexChanged += new EventHandler(this.CategoryComboBox_SelectedIndexChanged);
    this.tableLayoutPanel15.AutoSize = true;
    this.tableLayoutPanel15.ColumnCount = 3;
    this.tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel15.Controls.Add((Control) this._categoryIDCheckBox, 0, 0);
    this.tableLayoutPanel15.Controls.Add((Control) this._categoryIDRelopComboBox, 1, 0);
    this.tableLayoutPanel15.Controls.Add((Control) this._categoryIDInt64Box, 2, 0);
    this.tableLayoutPanel15.Dock = DockStyle.Fill;
    this.tableLayoutPanel15.Location = new Point(3, 497);
    this.tableLayoutPanel15.Name = "tableLayoutPanel15";
    this.tableLayoutPanel15.RowCount = 1;
    this.tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel15.Size = new Size(794, 27);
    this.tableLayoutPanel15.TabIndex = 0;
    this._categoryIDCheckBox.AutoSize = true;
    this._categoryIDCheckBox.Location = new Point(3, 3);
    this._categoryIDCheckBox.Name = "_categoryIDCheckBox";
    this._categoryIDCheckBox.Size = new Size(92, 17);
    this._categoryIDCheckBox.TabIndex = 0;
    this._categoryIDCheckBox.Text = "ID категории";
    this._categoryIDCheckBox.UseVisualStyleBackColor = true;
    this._categoryIDCheckBox.CheckedChanged += new EventHandler(this.CategoryIDCheckBox_CheckedChanged);
    this._categoryIDRelopComboBox.Dock = DockStyle.Fill;
    this._categoryIDRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._categoryIDRelopComboBox.FormattingEnabled = true;
    this._categoryIDRelopComboBox.Location = new Point(267, 3);
    this._categoryIDRelopComboBox.Name = "_categoryIDRelopComboBox";
    this._categoryIDRelopComboBox.Size = new Size(258, 21);
    this._categoryIDRelopComboBox.TabIndex = 1;
    this._categoryIDRelopComboBox.SelectedIndexChanged += new EventHandler(this.CategoryIDRelopComboBox_SelectedIndexChanged);
    this._categoryIDInt64Box.AutoSize = true;
    this._categoryIDInt64Box.Dock = DockStyle.Fill;
    this._categoryIDInt64Box.Location = new Point(531, 3);
    this._categoryIDInt64Box.Name = "_categoryIDInt64Box";
    this._categoryIDInt64Box.Size = new Size(260, 21);
    this._categoryIDInt64Box.TabIndex = 2;
    this._categoryIDInt64Box.ValueChanged += new EventHandler(this.CategoryIDInt64Box_ValueChanged);
    this.tableLayoutPanel16.AutoSize = true;
    this.tableLayoutPanel16.ColumnCount = 3;
    this.tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel16.Controls.Add((Control) this._machineNameCheckBox, 0, 0);
    this.tableLayoutPanel16.Controls.Add((Control) this._machineNameRelopComboBox, 1, 0);
    this.tableLayoutPanel16.Controls.Add((Control) this._machineNameTextBox, 2, 0);
    this.tableLayoutPanel16.Dock = DockStyle.Fill;
    this.tableLayoutPanel16.Location = new Point(3, 530);
    this.tableLayoutPanel16.Name = "tableLayoutPanel16";
    this.tableLayoutPanel16.RowCount = 1;
    this.tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel16.Size = new Size(794, 27);
    this.tableLayoutPanel16.TabIndex = 0;
    this._machineNameCheckBox.AutoSize = true;
    this._machineNameCheckBox.Location = new Point(3, 3);
    this._machineNameCheckBox.Name = "_machineNameCheckBox";
    this._machineNameCheckBox.Size = new Size(114, 17);
    this._machineNameCheckBox.TabIndex = 0;
    this._machineNameCheckBox.Text = "Имя компьютера";
    this._machineNameCheckBox.UseVisualStyleBackColor = true;
    this._machineNameCheckBox.CheckedChanged += new EventHandler(this.MachineNameCheckBox_CheckedChanged);
    this._machineNameRelopComboBox.Dock = DockStyle.Fill;
    this._machineNameRelopComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._machineNameRelopComboBox.FormattingEnabled = true;
    this._machineNameRelopComboBox.Location = new Point(267, 3);
    this._machineNameRelopComboBox.Name = "_machineNameRelopComboBox";
    this._machineNameRelopComboBox.Size = new Size(258, 21);
    this._machineNameRelopComboBox.TabIndex = 1;
    this._machineNameRelopComboBox.SelectedIndexChanged += new EventHandler(this.MachineNameRelopComboBox_SelectedIndexChanged);
    this._machineNameTextBox.Dock = DockStyle.Fill;
    this._machineNameTextBox.Location = new Point(531, 3);
    this._machineNameTextBox.Name = "_machineNameTextBox";
    this._machineNameTextBox.Size = new Size(260, 20);
    this._machineNameTextBox.TabIndex = 2;
    this._machineNameTextBox.TextChanged += new EventHandler(this.MachineNameTextBox_TextChanged);
    this.tableLayoutPanel2.AutoSize = true;
    this.tableLayoutPanel2.ColumnCount = 2;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66666f));
    this.tableLayoutPanel2.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this._nameTextBox, 1, 0);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(3, 3);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 1;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Size = new Size(794, 26);
    this.tableLayoutPanel2.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(3, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(103, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Название фильтра";
    this._nameTextBox.Dock = DockStyle.Fill;
    this._nameTextBox.Location = new Point(267, 3);
    this._nameTextBox.Name = "_nameTextBox";
    this._nameTextBox.Size = new Size(524, 20);
    this._nameTextBox.TabIndex = 1;
    this._nameTextBox.TextChanged += new EventHandler(this.NameTextBox_TextChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (EventLogFilterEditorControl);
    this.Size = new Size(800, 603);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.tableLayoutPanel4.ResumeLayout(false);
    this.tableLayoutPanel4.PerformLayout();
    this.tableLayoutPanel5.ResumeLayout(false);
    this.tableLayoutPanel5.PerformLayout();
    this.tableLayoutPanel6.ResumeLayout(false);
    this.tableLayoutPanel6.PerformLayout();
    this.tableLayoutPanel7.ResumeLayout(false);
    this.tableLayoutPanel7.PerformLayout();
    this.tableLayoutPanel8.ResumeLayout(false);
    this.tableLayoutPanel8.PerformLayout();
    this.tableLayoutPanel9.ResumeLayout(false);
    this.tableLayoutPanel9.PerformLayout();
    this.tableLayoutPanel10.ResumeLayout(false);
    this.tableLayoutPanel10.PerformLayout();
    this.tableLayoutPanel11.ResumeLayout(false);
    this.tableLayoutPanel11.PerformLayout();
    this.tableLayoutPanel12.ResumeLayout(false);
    this.tableLayoutPanel12.PerformLayout();
    this.tableLayoutPanel13.ResumeLayout(false);
    this.tableLayoutPanel13.PerformLayout();
    this.tableLayoutPanel14.ResumeLayout(false);
    this.tableLayoutPanel14.PerformLayout();
    this.tableLayoutPanel15.ResumeLayout(false);
    this.tableLayoutPanel15.PerformLayout();
    this.tableLayoutPanel16.ResumeLayout(false);
    this.tableLayoutPanel16.PerformLayout();
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
