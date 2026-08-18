// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GlobalVariablesList
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Workflow
{
    /// <summary>
    /// Вспомогательный класс для хранения глобальных переменных шаблонов/процессов
    /// </summary>
    public class GlobalVariablesList : VarList
    {
      private List<int> _variablesIDList = new List<int>();

      public GlobalVariablesList(IUserSession session, bool isServer, bool isPumper)
        : base(session, isServer, isPumper)
      {
      }

      public GlobalVariablesList(IDBObject schemeObject, bool isServer, bool isPumper)
        : base(schemeObject?.Session, isServer, isPumper)
      {
        if (!(schemeObject is IScheme scheme))
          return;
        this.Load(scheme);
      }

      public void Load(IScheme scheme)
      {
        List<int> attributesInGroup = MetaDataHelper.GetAttributesInGroup(wfConsts.GlobalVariablesGroupID);
        if (attributesInGroup.Count <= 0 || scheme == null)
          return;
        IEnumerable<int> second = scheme.Attributes.ToList().Select<IDBAttribute, int>((Func<IDBAttribute, int>) (x => x.AttributeID));
        this._variablesIDList = attributesInGroup.Intersect<int>(second).ToList<int>();
        foreach (int variablesId in this._variablesIDList)
        {
          IDBAttribute attributeById = scheme.GetAttributeByID(variablesId);
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(variablesId);
          Variable variable = new Variable((VarList) this)
          {
            AttrTypeID = variablesId,
            VariableType = MiscFunx.DetermineVarType(attributeType),
            Kind = VarKind.Global,
            Name = attributeType.Name
          };
          if (variable.VariableType != VarType.DateTime)
            variable.TypedValue = attributeById.Value;
          else if (attributeById.Value != DBNull.Value)
            variable.TypedValue = attributeById.Value;
          this.Add(variable);
        }
      }
    }
}
