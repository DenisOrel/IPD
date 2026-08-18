using Intermech.Interfaces;
using System;


namespace Intermech.Workflow
{
    [Serializable]
    public class CalculatedSystemVariable(VarList owner, IDBObject obj, int typeID) : SystemVariable(owner, obj, typeID)
    {
      public override bool Calculated => true;

      protected override string GetValue()
      {
        if (!this._loaded)
        {
          this._value = this.CalcValue();
          this._loaded = true;
        }
        return this._value;
      }

      protected virtual string CalcValue() => "";

      public void ClearCache()
      {
        this._value = "";
        this._loaded = false;
      }

      protected override void AfterSetValue()
      {
        base.AfterSetValue();
        this._loaded = true;
      }
    }
}
