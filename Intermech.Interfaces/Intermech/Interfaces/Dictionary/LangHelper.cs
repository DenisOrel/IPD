
// Type: Intermech.Interfaces.Dictionary.LangHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Language helper structure</summary>
    [Serializable]
    public class LangHelper : ISerializable
    {
      private string _id = string.Empty;
      private string _name = string.Empty;
      private int _def;
      private List<DictWord> _words = new List<DictWord>();

      /// <summary>Constructor</summary>
      /// <param name="id"></param>
      /// <param name="name"></param>
      /// <param name="def"></param>
      public LangHelper(string id, string name, int def)
      {
        this._id = id;
        this._name = name;
        this._def = def;
      }

      /// <summary>Language's ID</summary>
      public string ID => this._id;

      /// <summary>Language's name</summary>
      public string Name => this._name;

      /// <summary>Default flag</summary>
      public int Default => this._def;

      /// <summary>Words list</summary>
      public List<DictWord> Words => this._words;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this._name;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected LangHelper(SerializationInfo info, StreamingContext context)
      {
        int int32 = info.GetInt32("Version");
        if (int32 >= 100)
        {
          this._id = info.GetString(nameof (ID));
          this._name = info.GetString(nameof (Name));
          this._def = info.GetInt32("Def");
        }
        if (int32.Equals(100) && info.GetValue(nameof (Words), typeof (ArrayList)) is ArrayList arrayList)
        {
          foreach (DictWord dictWord in arrayList)
            this._words.Add(dictWord);
        }
        if (int32 <= 100)
          return;
        this._words = info.GetValue(nameof (Words), typeof (List<DictWord>)) as List<DictWord>;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Version", 101);
        info.AddValue("ID", (object) this._id);
        info.AddValue("Name", (object) this._name);
        info.AddValue("Def", this._def);
        info.AddValue("Words", (object) this._words);
      }

      /// <summary>Const's class</summary>
      public static class Consts
      {
        /// <summary>Internal structure version</summary>
        public const int Version = 101;
      }
    }
}
