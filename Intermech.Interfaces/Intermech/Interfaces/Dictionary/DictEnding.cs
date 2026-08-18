
// Type: Intermech.Interfaces.Dictionary.DictEnding
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Класс для хранения окончания и правил на него</summary>
    [Serializable]
    public class DictEnding : ISerializable, IComparable
    {
      private string _ending = string.Empty;
      private List<DictRule> _rules = new List<DictRule>();

      /// <summary>Конструктор</summary>
      public DictEnding()
      {
      }

      /// <summary>Окончание</summary>
      public string Ending
      {
        get => this._ending;
        set => this._ending = value;
      }

      /// <summary>Список правил для окончания</summary>
      public List<DictRule> Rules => this._rules;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this._ending;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected DictEnding(SerializationInfo info, StreamingContext context)
      {
        int int32 = info.GetInt32("Version");
        if (int32.Equals(100))
        {
          this._ending = info.GetString("Ext");
          if (info.GetValue(nameof (Rules), typeof (ArrayList)) is ArrayList arrayList)
          {
            foreach (DictRule dictRule in arrayList)
              this._rules.Add(dictRule);
          }
        }
        if (int32 <= 100)
          return;
        this._ending = info.GetString(nameof (Ending));
        this._rules = info.GetValue(nameof (Rules), typeof (List<DictRule>)) as List<DictRule>;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Version", 101);
        info.AddValue("Ending", (object) this._ending);
        info.AddValue("Rules", (object) this._rules);
      }

      public int CompareTo(object obj)
      {
        return obj == null ? 1 : this._ending.CompareTo((obj as DictEnding)._ending);
      }
    }
}
