
// Type: Intermech.Interfaces.ImportingObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Структура, хранящая импортируемый объект</summary>
    [Serializable]
    public class ImportingObject : ImportingAttributable
    {
      /// <summary>Объект</summary>
      public ObjectRecord Object;
      /// <summary>Шаги ЖЦ на которых он находился(тся) и время</summary>
      public List<LCStepRecord> LCSteps;
      /// <summary>Замечания</summary>
      public List<RemarkRecord> Remarks;
      [NonSerialized]
      public object Tag;

      public ImportingObject(ObjectRecord obj)
      {
        this.Object = obj;
        this.LCSteps = new List<LCStepRecord>();
        this.Remarks = new List<RemarkRecord>();
      }

      /// <summary>Добавить запись по шагу ЖЦ</summary>
      /// <param name="step"></param>
      public void AddLCStep(LCStepRecord step) => this.LCSteps.Add(step);

      /// <summary>Добавить атрибут</summary>
      /// <param name="attribute"></param>
      public void AddRemark(RemarkRecord attribute) => this.Remarks.Add(attribute);
    }
}
