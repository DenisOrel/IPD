// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.SchemeLCStepConverter
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow;

public class SchemeLCStepConverter : DictionaryConverter<int, string>
{
  private Dictionary<int, string> _lcSteps;

  public SchemeLCStepConverter()
    : base((Dictionary<int, string>) null)
  {
  }

  private Dictionary<int, string> LCSteps
  {
    get
    {
      if (this._lcSteps == null)
      {
        this._lcSteps = new Dictionary<int, string>()
        {
          {
            0,
            "(Не выбран)"
          }
        };
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (DataRow dataRow in sessionKeeper.Session.GetLifecycleStepCollection(wfConsts.SchemesTypeID).GetSchema().Tables["IMS_LC_STEPS"].Select())
            this._lcSteps.Add(Convert.ToInt32(dataRow["F_LC_STEP"]), dataRow["F_LC_NAME"].ToString());
        }
      }
      return this._lcSteps;
    }
  }

  protected override Dictionary<int, string> Dict => this.LCSteps;
}
