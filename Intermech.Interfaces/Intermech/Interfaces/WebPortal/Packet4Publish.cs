
// Type: Intermech.Interfaces.WebPortal.Packet4Publish
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Пакет</summary>
    [Serializable]
    public class Packet4Publish
    {
      /// <summary>Обозначение</summary>
      public string Designation { get; set; }

      /// <summary>Наименование</summary>
      public string Name { get; set; }

      /// <summary>Коментарии</summary>
      public string Note { get; set; }

      /// <summary>Глобальный идентификатор пакета</summary>
      public Guid GUID { get; set; }

      public Packet4Publish(string designation, string name, string note)
        : this(designation, name, note, Guid.NewGuid())
      {
      }

      public Packet4Publish(string designation, string name, string note, Guid guid)
      {
        this.GUID = guid;
        this.Designation = designation;
        this.Name = name;
        this.Note = note;
      }

      public static string Caption(string designation, string name, Guid guid)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (string.IsNullOrEmpty(designation) && string.IsNullOrEmpty(name))
          stringBuilder.Append(guid.ToString());
        else if (!string.IsNullOrEmpty(designation))
        {
          stringBuilder.Append(designation);
          if (!string.IsNullOrEmpty(name))
            stringBuilder.AppendFormat("({0})", (object) name);
        }
        else
          stringBuilder.Append(name);
        return stringBuilder.ToString();
      }
    }
}
