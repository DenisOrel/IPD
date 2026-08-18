
// Type: Intermech.Interfaces.Dictionary.DictWord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Класс для хранения настроек на слово</summary>
    [Serializable]
    public class DictWord : ISerializable
    {
      private string _word = string.Empty;
      private List<DictEnding> _endings = new List<DictEnding>();

      /// <summary>Конструктор</summary>
      public DictWord()
      {
      }

      /// <summary>Слово (форма без окончаний)</summary>
      public string Word
      {
        get => this._word;
        set => this._word = value;
      }

      /// <summary>Список возможных окончаний</summary>
      public List<DictEnding> Endings => this._endings;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this._word;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected DictWord(SerializationInfo info, StreamingContext context)
      {
        int int32 = info.GetInt32("Version");
        if (int32.Equals(100))
          this._word = info.GetString(nameof (Word));
        if (int32 <= 100)
          return;
        this._word = info.GetString(nameof (Word));
        this._endings = info.GetValue(nameof (Endings), typeof (List<DictEnding>)) as List<DictEnding>;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Version", 101);
        info.AddValue("Word", (object) this._word);
        info.AddValue("Endings", (object) this._endings);
      }
    }
}
