// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ObjsFromRowDescr
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Views;

internal class ObjsFromRowDescr : ICustomTypeDescriptor
{
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  private bool _empty = true;

  internal ObjsFromRowDescr(IUserSession session, long linkID, long recID)
  {
    if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
      return;
    string filter = $"[-2]={recID}";
    DataTable recordsTable = (DataTable) null;
    AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
    customService.LoadRecords(session.SessionGUID, linkID, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out columnsAttributes, out keyInfo);
    if (columnsAttributes.Length == 0)
      return;
    IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
    string empty = string.Empty;
    string category = LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_Caption");
    if (recordsTable.Rows.Count == 0)
    {
      if (service == null)
        return;
      string text = string.Format(LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_RowDeleted"), (object) linkID, (object) recID);
      service.ClearText(category);
      service.WriteString(category, text);
      service.Activate(category);
    }
    else
    {
      foreach (AttributeTypeProperties attributeTypeProperties in columnsAttributes)
      {
        object obj = recordsTable.Rows[0][attributeTypeProperties.AttributeID.ToString()];
        Type type = Helper.ConvertType(attributeTypeProperties.FieldType);
        if (!(type == (Type) null))
          this._pdc.Add((PropertyDescriptor) new PropDescriptor(-1, (object) this, attributeTypeProperties.Name, obj, type, (TypeConverter) null, (object) null, string.Empty, string.Empty, true, true, false));
      }
      service?.ClearText(category);
      if (this._pdc.Count == 0)
        return;
      this._empty = false;
    }
  }

  internal bool IsEmpty => this._empty;

  public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes((object) this, true);

  public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this, true);

  public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents((object) this, true);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();

  public PropertyDescriptorCollection GetProperties()
  {
    return this._pdc != null ? this._pdc : new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;
}
