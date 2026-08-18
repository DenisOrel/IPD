
// Type: Intermech.Interfaces.ShortObjectDecription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Краткий дескриптор объекта</summary>
    public class ShortObjectDecription
    {
      /// <summary>Идентификатор объекта</summary>
      private long _objID = -1;
      /// <summary>Заголовок объекта</summary>
      private string _objCaption = string.Empty;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objID"></param>
      /// <param name="objCaption"></param>
      public ShortObjectDecription(long objID, string objCaption)
      {
        this._objID = objID;
        this._objCaption = objCaption;
      }

      /// <summary>Идентификатор объекта</summary>
      public long ObjID => this._objID;

      /// <summary>Заголовок объекта</summary>
      public string ObjCaption => this._objCaption;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this.ObjCaption;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        return obj is ShortObjectDecription objectDecription && this.ObjID.Equals(objectDecription.ObjID);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => this.ObjID.GetHashCode();
    }
}
