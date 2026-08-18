
// Type: Intermech.PropertyEditors.PossibleValuesClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PossibleValuesClass.</summary>
public class PossibleValuesClass
{
  private object lValue;
  private FieldTypes fieldType;
  private object inListId = (object) DBNull.Value;
  private object oId = (object) DBNull.Value;
  private string descr = string.Empty;
  private Hashtable captions;

  public object Value
  {
    get => this.lValue;
    set => this.lValue = value;
  }

  public object InListId
  {
    get => this.inListId;
    set => this.inListId = value;
  }

  /// <summary>Старое значение InListId</summary>
  public object OId => this.oId;

  public string Description
  {
    get => this.descr;
    set => this.descr = value;
  }

  public FieldTypes FieldType => this.fieldType;

  public PossibleValuesClass(
    object aValue,
    FieldTypes ft,
    object aInListId,
    object aOId,
    string description,
    Hashtable aObjCaptions)
  {
    this.fieldType = ft;
    this.lValue = aValue;
    this.inListId = aInListId;
    this.oId = aOId;
    this.descr = description;
    this.captions = aObjCaptions;
  }

  public PossibleValuesClass(
    object aValue,
    FieldTypes ft,
    object aInListId,
    object aOId,
    string description)
    : this(aValue, ft, aInListId, aOId, description, (Hashtable) null)
  {
  }

  [DebuggerStepThrough]
  public override string ToString()
  {
    if (this.lValue == null)
      return string.Empty;
    string str = this.fieldType != FieldTypes.ftDateTime ? this.lValue.ToString() : ((DateTime) this.lValue).ToString("dd.MM.yyyy");
    if (this.fieldType == FieldTypes.ftObjectLink || this.fieldType == FieldTypes.ftObjectLinkByID)
    {
      if (this.captions == null)
        this.captions = new Hashtable();
      if (this.captions.ContainsKey(this.lValue))
      {
        str = this.captions[this.lValue].ToString();
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = (IDBObject) null;
          try
          {
            if (this.fieldType == FieldTypes.ftObjectLink)
              dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(this.lValue));
            if (this.fieldType == FieldTypes.ftObjectLinkByID)
              dbObject = sessionKeeper.Session.GetObjectBaseVersionByID(Convert.ToInt64(this.lValue), true);
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
          if (dbObject != null)
          {
            str = dbObject.Caption;
            this.captions.Add(this.lValue, (object) str);
          }
        }
      }
    }
    return str;
  }
}
