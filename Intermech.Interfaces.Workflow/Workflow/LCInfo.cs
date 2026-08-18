// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.LCInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Briefcase;
using System;
using System.Xml;

#nullable disable
namespace Intermech.Workflow;

public class LCInfo : IValidatedItem
{
  public LCKind Kind = LCKind.Level;
  private int _objectType;
  private Guid _objectTypeGuid;
  private int _stepID;
  private Guid _stepGuid;
  public LCExec ExecTime;
  private string _typeName = "";
  private string _stepName = "";
  private int _levelID;
  private bool _invalid;
  private bool _inited;

  public int ObjectType
  {
    get
    {
      this.Init();
      return this._objectType;
    }
    set
    {
      if (this._objectType == value)
        return;
      this._objectType = value;
      this._objectTypeGuid = Guid.Empty;
      this._inited = false;
    }
  }

  public Guid ObjectTypeGuid
  {
    get
    {
      this.Init();
      return this._objectTypeGuid;
    }
    set => this._objectTypeGuid = value;
  }

  public int StepID
  {
    get => this._stepID;
    set
    {
      if (this._stepID == value)
        return;
      this._stepID = value;
      this._stepGuid = Guid.Empty;
      this._inited = false;
    }
  }

  public Guid StepGuid
  {
    get
    {
      this.Init();
      return this._stepGuid;
    }
    set => this._stepGuid = value;
  }

  public string TypeName
  {
    get
    {
      this.Init();
      return this._typeName;
    }
  }

  public string StepName
  {
    get
    {
      this.Init();
      return this._stepName;
    }
  }

  public int LevelID
  {
    get => this.Kind == LCKind.Step ? this._levelID : this.StepID;
    set => this._levelID = value;
  }

  public void Assign(LCInfo src)
  {
    this.Kind = src.Kind;
    this.ObjectType = src.ObjectType;
    this.StepID = src.StepID;
    this.LevelID = src.LevelID;
    this.ExecTime = src.ExecTime;
    this._typeName = src.TypeName;
    this._stepName = src.StepName;
  }

  public override bool Equals(object obj)
  {
    return obj is LCInfo lcInfo && lcInfo.Kind == this.Kind && lcInfo.ObjectType == this.ObjectType && lcInfo.StepID == this.StepID && lcInfo.ExecTime == this.ExecTime;
  }

  public override int GetHashCode() => base.GetHashCode();

  public bool Invalid
  {
    get
    {
      this.Init();
      return this._invalid;
    }
  }

  public void Init()
  {
    if (this._inited)
      return;
    this._inited = true;
    this._invalid = false;
    SimpleBriefcase globalBriefcase = BriefcaseAccessor.GlobalBriefcase;
    if (this._objectType == -1)
    {
      this._typeName = wfConsts.AllObjectsCaption;
      this._objectTypeGuid = Guid.Empty;
    }
    else
    {
      IMSObjectType imsObjectType = !(this._objectTypeGuid != Guid.Empty) ? MetaDataHelper.GetObjectType(this._objectType) : MetaDataHelper.GetObjectType(this._objectTypeGuid);
      if (imsObjectType != null)
      {
        this._objectType = imsObjectType.ObjectTypeID;
        this._objectTypeGuid = imsObjectType.Guid;
        this._typeName = imsObjectType.ObjectTypeName;
      }
      else
      {
        if (globalBriefcase != null)
          this._typeName = globalBriefcase.GetCaption(Domain.ObjectTypes, (long) this._objectType);
        this._invalid = true;
      }
    }
    if (this._typeName == "")
      this._typeName = wfConsts.UnknownStr;
    if (this.Kind == LCKind.Level)
    {
      IMSLifeCycleLevel imsLifeCycleLevel = !(this._stepGuid != Guid.Empty) ? MetaDataHelper.GetLCLevel(this._stepID) : MetaDataHelper.GetLCLevel(this._stepGuid);
      if (imsLifeCycleLevel != null)
      {
        this._stepID = imsLifeCycleLevel.LevelID;
        this._stepGuid = imsLifeCycleLevel.Guid;
        this._stepName = imsLifeCycleLevel.Name;
      }
      else
      {
        if (globalBriefcase != null)
          this._stepName = globalBriefcase.GetCaption(Domain.Levels, (long) this._stepID);
        this._invalid = true;
      }
    }
    else
    {
      IMSLifeCycleStep imsLifeCycleStep = !(this._stepGuid != Guid.Empty) ? MetaDataHelper.GetLCStep(this._stepID) : MetaDataHelper.GetLCStep(this._stepGuid);
      if (imsLifeCycleStep != null)
      {
        this._stepID = imsLifeCycleStep.LCStepID;
        this._stepGuid = imsLifeCycleStep.Guid;
        this._stepName = imsLifeCycleStep.Name;
      }
      else
      {
        if (globalBriefcase != null)
          this._stepName = globalBriefcase.GetCaption(Domain.Steps, (long) this._stepID);
        this._invalid = true;
      }
    }
    if (!(this._stepName == ""))
      return;
    this._stepName = wfConsts.UnknownStr;
  }

  internal void Load(XmlTextReader reader)
  {
    if (reader.IsStartElement("TypeGuid"))
    {
      reader.ReadStartElement("TypeGuid");
      this._objectTypeGuid = new Guid(reader.ReadString());
      reader.ReadEndElement();
    }
    reader.ReadStartElement("Type");
    this._objectType = Convert.ToInt32(reader.ReadString());
    reader.ReadEndElement();
    reader.ReadStartElement("Kind");
    this.Kind = (LCKind) Convert.ToInt32(reader.ReadString());
    reader.ReadEndElement();
    if (reader.IsStartElement("StepGuid"))
    {
      reader.ReadStartElement("StepGuid");
      this._stepGuid = new Guid(reader.ReadString());
      reader.ReadEndElement();
    }
    reader.ReadStartElement("StepID");
    this._stepID = Convert.ToInt32(reader.ReadString());
    reader.ReadEndElement();
    try
    {
      if (reader.IsStartElement("LevelID"))
      {
        reader.ReadStartElement("LevelID");
        this._levelID = Convert.ToInt32(reader.ReadString());
        reader.ReadEndElement();
      }
    }
    catch
    {
    }
    try
    {
      if (!reader.IsStartElement("ExecTime"))
        return;
      reader.ReadStartElement("ExecTime");
      this.ExecTime = (LCExec) Convert.ToInt32(reader.ReadString());
      reader.ReadEndElement();
    }
    catch
    {
    }
  }

  internal void Save(XmlTextWriter writer, bool WriteGuids)
  {
    if (WriteGuids && this.ObjectType != -1)
    {
      writer.WriteStartElement("TypeGuid");
      writer.WriteValue(this.ObjectTypeGuid.ToString());
      writer.WriteEndElement();
    }
    writer.WriteStartElement("Type");
    writer.WriteValue(this.ObjectType);
    writer.WriteEndElement();
    writer.WriteStartElement("Kind");
    writer.WriteValue((int) this.Kind);
    writer.WriteEndElement();
    if (WriteGuids)
    {
      writer.WriteStartElement("StepGuid");
      writer.WriteString(this.StepGuid.ToString());
      writer.WriteEndElement();
    }
    writer.WriteStartElement("StepID");
    writer.WriteString(this.StepID.ToString());
    writer.WriteEndElement();
    writer.WriteStartElement("LevelID");
    writer.WriteString(this.LevelID.ToString());
    writer.WriteEndElement();
    writer.WriteStartElement("ExecTime");
    writer.WriteValue((int) this.ExecTime);
    writer.WriteEndElement();
  }
}
