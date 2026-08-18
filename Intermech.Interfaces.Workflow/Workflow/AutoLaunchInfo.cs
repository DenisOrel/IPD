// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.AutoLaunchInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System;
using System.Linq;

#nullable disable
namespace Intermech.Workflow;

public class AutoLaunchInfo
{
  private int _typeID;
  private long _schemeID;
  private ProcessPriority _processPriority;
  public string TypeName = "";
  public string SchemeName = "";

  public int TypeID => this._typeID;

  public long SchemeID => this._schemeID;

  public ProcessPriority ProcessPriority
  {
    get => this._processPriority;
    set => this._processPriority = value;
  }

  public string ProcessPriorityName
  {
    get
    {
      Type type = this.ProcessPriority.GetType();
      CustomDescription customDescription = type.GetField(Enum.GetName(type, (object) this.ProcessPriority)).GetCustomAttributes(false).OfType<CustomDescription>().SingleOrDefault<CustomDescription>();
      return customDescription != null ? customDescription.Description : string.Empty;
    }
  }

  public AutoLaunchInfo(int typeID, long schemeID)
  {
    this._typeID = typeID;
    this._schemeID = schemeID;
  }

  public override int GetHashCode()
  {
    return this.TypeID.GetHashCode() * 17 + this.SchemeID.GetHashCode() + this.ProcessPriority.GetHashCode();
  }

  public override bool Equals(object obj)
  {
    return obj is AutoLaunchInfo ? this.GetHashCode() == ((AutoLaunchInfo) obj).GetHashCode() : base.Equals(obj);
  }
}
