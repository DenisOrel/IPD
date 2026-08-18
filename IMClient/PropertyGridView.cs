
// Type: IMClient.PropertyGridView




using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient
{
    public class PropertyGridView : DockControl, IPropertyGridView
    {
      private ComboBox _comboBox;
      private PropertyGrid _propertyGrid;
      private bool _updating;
      private ComboBox.ObjectCollection _objects;
      private System.ComponentModel.Container components;

      public event PropertyValueChangedEventHandler PropertyValueChanged;

      public event EventHandler SelectedObjectChanged;

      public PropertyGridView()
      {
        this.InitializeComponent();
        INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service != null)
          this.TabImageIndex = service.ImageIndex("imgProp");
        this._objects = this._comboBox.Items;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertyGridView));
        this._comboBox = new ComboBox();
        this._propertyGrid = new PropertyGrid();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._comboBox, "_comboBox");
        this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        this._comboBox.Name = "_comboBox";
        this._comboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
        componentResourceManager.ApplyResources((object) this._propertyGrid, "_propertyGrid");
        this._propertyGrid.LineColor = SystemColors.ScrollBar;
        this._propertyGrid.Name = "_propertyGrid";
        this._propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.OnPropertyValueChanged);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.Controls.Add((Control) this._propertyGrid);
        this.Controls.Add((Control) this._comboBox);
        this.Guid = ViewGuids.PropertyGridView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (PropertyGridView);
        this.ResumeLayout(false);
      }

      private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
      {
        if (this._updating)
          return;
        int selectedIndex = this._comboBox.SelectedIndex;
        this._propertyGrid.SelectedObject = selectedIndex == -1 ? (object) null : this._comboBox.Items[selectedIndex];
        this.OnSelectedObjectChanged();
      }

      private void OnSelectedObjectChanged()
      {
        if (this.SelectedObjectChanged == null)
          return;
        this.SelectedObjectChanged((object) this, new EventArgs());
      }

      private void OnPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
      {
        if (this.PropertyValueChanged == null)
          return;
        this.PropertyValueChanged(s, e);
      }

      public PropertyGrid PropertyGrid => this._propertyGrid;

      public object DesignableObject
      {
        get => this._objects.Count > 0 ? this._objects[0] : (object) null;
        set
        {
          this._objects.Clear();
          if (value != null)
            this._objects.Add(value);
          this.UpdateView();
        }
      }

      private void UpdateView()
      {
      }

      public object[] DesignableObjects
      {
        get
        {
          object[] destination = new object[this._objects.Count];
          this._objects.CopyTo(destination, 0);
          return destination;
        }
        set
        {
          this._objects.Clear();
          if (value != null && value.Length != 0)
            this._objects.AddRange(value);
          this.UpdateView();
        }
      }

      public void SetDesignableObjects(params object[] objects) => this.DesignableObjects = objects;

      public object SelectedObject
      {
        get => this._propertyGrid.SelectedObject;
        set
        {
          this._propertyGrid.SelectedObject = value;
          this.SetSelected(value);
        }
      }

      private void SetSelected(object value)
      {
        this._updating = true;
        try
        {
          int num = this._objects.IndexOf(value);
          if (num == -1)
            return;
          this._comboBox.SelectedIndex = num;
        }
        finally
        {
          this._updating = false;
        }
      }

      public object[] SelectedObjects
      {
        get => this._propertyGrid.SelectedObjects;
        set
        {
          this._propertyGrid.SelectedObjects = value;
          if (value == null || value.Length == 0)
            return;
          this.SetSelected(value[0]);
        }
      }
    }
}
