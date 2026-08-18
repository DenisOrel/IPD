
// Type: Intermech.Client.Core.SelectAttributesFromObjects
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Выбор атрибута(ов) из атрибутов объекта(ов).</summary>
public class SelectAttributesFromObjects : Form
{
  private List<long> _objsIDs;
  private bool _isEdit = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;
  private ListView _lvAttrs;
  private ColumnHeader _colName;

  /// <summary>Список идентификаторов выделлых атрибутов.</summary>
  public Dictionary<int, List<long>> SelectedAttrsIDs
  {
    get
    {
      if (this._lvAttrs.SelectedItems.Count == 0)
        return (Dictionary<int, List<long>>) null;
      Dictionary<int, List<long>> selectedAttrsIds = new Dictionary<int, List<long>>(this._lvAttrs.SelectedItems.Count);
      foreach (ListViewItem selectedItem in this._lvAttrs.SelectedItems)
        selectedAttrsIds.Add(Convert.ToInt32(selectedItem.Name), selectedItem.Tag as List<long>);
      return selectedAttrsIds;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objsIDs">Список идентификаторов объектов</param>
  /// <param name="isEdit">Для чего вызывается диалог (для добавления или для редактирования атрибутов)</param>
  public SelectAttributesFromObjects(List<long> objsIDs, bool isEdit)
  {
    this.InitializeComponent();
    this._objsIDs = objsIDs;
    this._isEdit = isEdit;
    this._lvAttrs.Columns[0].Width = -2;
    if (Statics.IconSrv == null)
      return;
    this._lvAttrs.SmallImageList = Statics.IconSrv.ImageList;
  }

  /// <summary>Изменение выделенного элемента.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnOK.Enabled = this._lvAttrs.SelectedItems.Count > 0;
  }

  /// <summary>Изменение размеров ListView.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvAttrs_SizeChanged(object sender, EventArgs e)
  {
    if (!(sender is ListView listView) || listView.Columns.Count == 0 || listView.Columns[0] == null)
      return;
    listView.Columns[0].Width = -2;
  }

  /// <summary>Загрузка формы.</summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this._objsIDs == null || this._objsIDs.Count == 0)
      return;
    Dictionary<int, SelectAttributesFromObjects.InfoClass> dictionary = new Dictionary<int, SelectAttributesFromObjects.InfoClass>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int objsId in this._objsIDs)
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy((long) objsId, false);
        if (objectActualCopy != null)
        {
          if (this._isEdit)
          {
            GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption;
            foreach (AttributeValues attributesValue in objectActualCopy.GetAttributesValues(modes))
            {
              if (!attributesValue.ReadOnly)
              {
                if (dictionary.ContainsKey(attributesValue.AttributeID))
                {
                  dictionary[attributesValue.AttributeID].AddObjID(objsId);
                }
                else
                {
                  int imgIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributesValue.AttributeType);
                  dictionary.Add(attributesValue.AttributeID, new SelectAttributesFromObjects.InfoClass(attributesValue.AttributeName, imgIndex, objsId));
                }
              }
            }
          }
          else
          {
            DataTable dataTable = sessionKeeper.Session.GetObjectType(objectActualCopy.ObjectType).VisibleAttributes.Select(string.Empty);
            if (dataTable != null)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
                DataRow attribute = DataHolders.AttributesHolder.GetAttribute(int32);
                if (attribute != null)
                {
                  if ((Convert.ToInt32(attribute["F_OPTIONS"]) & 128 /*0x80*/) == 0)
                  {
                    if (dictionary.ContainsKey(int32))
                    {
                      dictionary[int32].AddObjID(objsId);
                    }
                    else
                    {
                      int imgIndex = Statics.IconSrv.IndexOf(3, -1, (object) (FieldTypes) Convert.ToInt32(attribute["F_ATTRIBUTE_TYPE"]));
                      dictionary.Add(int32, new SelectAttributesFromObjects.InfoClass(attribute["F_NAME"].ToString(), imgIndex, objsId));
                    }
                  }
                }
                else
                {
                  IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(int32, false);
                  if (attributeType != null && (attributeType.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.None)
                  {
                    if (dictionary.ContainsKey(int32))
                    {
                      dictionary[int32].AddObjID(objsId);
                    }
                    else
                    {
                      int imgIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeID);
                      dictionary.Add(int32, new SelectAttributesFromObjects.InfoClass(attributeType.Name, imgIndex, objsId));
                    }
                  }
                }
              }
            }
          }
        }
      }
      foreach (KeyValuePair<int, SelectAttributesFromObjects.InfoClass> keyValuePair in dictionary)
        this._lvAttrs.Items.Add(new ListViewItem(keyValuePair.Value.Name, keyValuePair.Value.ImgIndex)
        {
          Name = keyValuePair.Key.ToString(),
          Tag = (object) keyValuePair.Value.ObjsIDs
        });
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectAttributesFromObjects));
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._lvAttrs = new ListView();
    this._colName = new ColumnHeader();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._lvAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this._colName
    });
    componentResourceManager.ApplyResources((object) this._lvAttrs, "_lvAttrs");
    this._lvAttrs.FullRowSelect = true;
    this._lvAttrs.HideSelection = false;
    this._lvAttrs.Name = "_lvAttrs";
    this._lvAttrs.Sorting = SortOrder.Ascending;
    this._lvAttrs.UseCompatibleStateImageBehavior = false;
    this._lvAttrs.View = View.Details;
    this._lvAttrs.SelectedIndexChanged += new EventHandler(this.On_lvAttrs_SelectedIndexChanged);
    this._lvAttrs.SizeChanged += new EventHandler(this.On_lvAttrs_SizeChanged);
    componentResourceManager.ApplyResources((object) this._colName, "_colName");
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.CancelButton = (IButtonControl) this._btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._lvAttrs);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (SelectAttributesFromObjects);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Класс для хранения некоторой информации об атрибуте.</summary>
  private class InfoClass
  {
    private int _imgIndex = -1;
    private string _name = string.Empty;
    private List<long> _objsIDs = new List<long>();

    /// <summary>Индекс изображения.</summary>
    public int ImgIndex => this._imgIndex;

    /// <summary>Наименование атрибута.</summary>
    public string Name => this._name;

    /// <summary>Идентификаторы объектов.</summary>
    public List<long> ObjsIDs => this._objsIDs;

    /// <summary>Конструктор.</summary>
    /// <param name="name">Наименование атрибута</param>
    /// <param name="imgIndex">Индекс изображения</param>
    /// <param name="objID">Идентификатор объекта</param>
    internal InfoClass(string name, int imgIndex, int objID)
    {
      this._name = name;
      this._imgIndex = imgIndex;
      this._objsIDs.Add((long) objID);
    }

    /// <summary>
    /// Добавление идентификатора типа объекта в список идентификаторов.
    /// </summary>
    /// <param name="objID">Идентификатор типа объекта</param>
    internal void AddObjID(int objID)
    {
      if (this._objsIDs.Contains((long) objID))
        return;
      this._objsIDs.Add((long) objID);
    }
  }
}
