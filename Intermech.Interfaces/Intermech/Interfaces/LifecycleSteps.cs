
// Type: Intermech.Interfaces.LifecycleSteps
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces
{
    public class LifecycleSteps
    {
      public ArrayList _LCStepList;

      public LifecycleSteps() => this._LCStepList = new ArrayList();

      public void Add(LifecycleStep step)
      {
        int index = this.Search(step.Step);
        if (index >= 0)
        {
          if (step.Attr == -1)
            return;
          ((LifecycleStep) this._LCStepList[index]).Attr += step.Attr;
        }
        else
          this._LCStepList.Add((object) step);
      }

      private int Search(int needle)
      {
        int num = -1;
        for (int index = 0; index < this.Count(); ++index)
        {
          if (((LifecycleStep) this._LCStepList[index]).Step == needle)
          {
            num = index;
            break;
          }
        }
        return num;
      }

      public int Count() => this._LCStepList.Count;

      public int GoodCount(int countNeed)
      {
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < this.Count(); ++index)
        {
          if (((LifecycleStep) this._LCStepList[index]).Attr == -1)
            ++num1;
          else if (((LifecycleStep) this._LCStepList[index]).Attr == countNeed)
          {
            ++num1;
            ++num2;
          }
        }
        return num2 <= 0 ? num2 : num1;
      }
    }
}
