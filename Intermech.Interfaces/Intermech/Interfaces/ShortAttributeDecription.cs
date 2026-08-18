
// Type: Intermech.Interfaces.ShortAttributeDecription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary> Краткий дескриптор атрибута </summary>
    public class ShortAttributeDecription
    {
      private int _attributeID = -1;
      private string _attributeCaption = string.Empty;

      public ShortAttributeDecription(int attributeID, string attributeCaption)
      {
        this._attributeID = attributeID;
        this._attributeCaption = attributeCaption;
      }

      /// <summary> Идентификатор атрибута </summary>
      public int AttributeID => this._attributeID;

      /// <summary> имя атрибута </summary>
      public string AttributeCaption
      {
        get => this._attributeCaption;
        set => this._attributeCaption = value;
      }
    }
}
