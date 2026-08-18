
// Type: Intermech.ReadOnlyAttributeException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение генерится ядром при попытке записи в системный атрибут, предназначенный только для чтения
    /// </summary>
    [Serializable]
    public class ReadOnlyAttributeException : KernelException
    {
      private string _AttrName;
      private int _AttrID;

      public ReadOnlyAttributeException(string attrName, int attrID)
      {
        this._AttrName = attrName;
        this._AttrID = attrID;
      }

      protected ReadOnlyAttributeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._AttrName = info.GetString(nameof (_AttrName));
        this._AttrID = info.GetInt32(nameof (_AttrID));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_AttrName", (object) this._AttrName);
        info.AddValue("_AttrID", this._AttrID);
      }

      public override string Message
      {
        get => $"В системный атрибут '{this._AttrName}' ({this._AttrID}) нельзя записывать данные.";
      }
    }
}
