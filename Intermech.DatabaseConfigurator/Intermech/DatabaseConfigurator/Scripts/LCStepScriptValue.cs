// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepScriptValue
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Services;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

internal class LCStepScriptValue
{
  private InheritModes _overriden = InheritModes.Public;
  private long scriptId = -1;
  private long? newScriptId;
  private string scriptName;
  private string newscriptName;
  private Guid _id = Guid.Empty;
  private int _objectType = -1;
  private bool readOnly;

  public LCStepScriptValue(
    Guid id,
    int objectType,
    long scriptId,
    bool readOnly,
    long? newscriptId = null)
  {
    this.Id = id;
    this.ObjectType = objectType;
    this.ReadOnly = readOnly;
    this.scriptId = scriptId;
    this.newScriptId = newscriptId;
    this.Update();
  }

  internal LCStepScriptValue Clone()
  {
    return new LCStepScriptValue(this.Id, this.ObjectType, this.ScriptId, this.ReadOnly, this.NewScriptId);
  }

  private void Update()
  {
    this._overriden = this.ReadOnly ? InheritModes.Inherited : InheritModes.Public;
    if (this.scriptId == -1L)
    {
      this.scriptName = "";
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.scriptName = sessionKeeper.Session.GetObjectInfo(this.scriptId).Caption;
    }
    if (!this.newScriptId.HasValue)
    {
      this.newscriptName = "";
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.newscriptName = sessionKeeper.Session.GetObjectInfo(this.newScriptId.Value).Caption;
    }
  }

  public bool SaveStep(Guid stepID)
  {
    this._id = stepID;
    if (stepID != Guid.Empty && !this._overriden.Equals((object) InheritModes.Inherited) && this.newScriptId.HasValue && this.newScriptId.Value != this.scriptId)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.ScriptId != -1L)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ScriptId, false);
          if (dbObject != null)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(LCStepScriptValue.Attr_LCScriptObject);
            if (attributeById != null)
            {
              string str = attributeById.AsString.Replace(stepID.ToString() + ";", "").Replace(stepID.ToString(), "");
              attributeById.AsString = str;
            }
          }
        }
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.newScriptId.Value, false);
        if (dbObject1 != null)
        {
          IDBAttribute dbAttribute = dbObject1.GetAttributeByID(LCStepScriptValue.Attr_LCScriptObject) ?? dbObject1.Attributes.AddAttribute(LCStepScriptValue.Attr_LCScriptObject, false);
          if (dbAttribute != null)
          {
            string str = $"{dbAttribute.AsString.Replace(stepID.ToString() + ";", "").Replace(stepID.ToString(), "")}{stepID.ToString()};";
            dbAttribute.AsString = str;
          }
        }
        this.ScriptId = this.NewScriptId.Value;
        LCStepScriptManager.UpdateDict();
        if (sessionKeeper.Session.GetCustomService(typeof (ILCScriptService)) is ILCScriptService customService)
          customService.UpdateCache();
      }
    }
    return true;
  }

  public long ScriptId
  {
    get => this.scriptId;
    set
    {
      this.scriptId = value;
      this.Update();
    }
  }

  public long? NewScriptId
  {
    get => this.newScriptId;
    set
    {
      this.newScriptId = value;
      this.Update();
    }
  }

  public string ScriptName
  {
    get => this.scriptName;
    set => this.scriptName = value;
  }

  public string NewScriptName
  {
    get => this.newscriptName;
    set => this.newscriptName = value;
  }

  public Guid Id
  {
    get => this._id;
    set => this._id = value;
  }

  public int ObjectType
  {
    get => this._objectType;
    set => this._objectType = value;
  }

  public bool ReadOnly
  {
    get => this.readOnly;
    set => this.readOnly = value;
  }

  internal static int LCScriptTypeId
  {
    get => MetaDataHelper.GetObjectTypeID(new Guid("cadd94ff-306c-11d8-b4e9-00304f19f545"));
  }

  internal static int Attr_LCScriptObject
  {
    get => MetaDataHelper.GetAttributeTypeID(new Guid("cadd9500-306c-11d8-b4e9-00304f19f545"));
  }
}
