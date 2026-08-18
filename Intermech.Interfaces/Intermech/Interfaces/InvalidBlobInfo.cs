
// Type: Intermech.Interfaces.InvalidBlobInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Информация о нечитаемом блобе</summary>
    [Serializable]
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectTypeID"></param>
    /// <param name="objectCaption"></param>
    /// <param name="objectID"></param>
    /// <param name="attrID"></param>
    /// <param name="attrIndex"></param>
    /// <param name="blobID"></param>
    /// <param name="fileName"></param>
    public struct InvalidBlobInfo(
      int objectTypeID,
      long objectID,
      string objectCaption,
      int attrID,
      int attrIndex,
      long blobID,
      string fileName) : IComparable
    {
      /// <summary>тип объекта</summary>
      public int objectTypeID = objectTypeID;
      /// <summary>версия объекта</summary>
      public long objectID = objectID;
      /// <summary>наименование объекта</summary>
      public string objectCaption = objectCaption;
      /// <summary>список атрибута, блобы которых не читаются</summary>
      public int attrID = attrID;
      /// <summary>id файла/блоба</summary>
      public long blobID = blobID;
      /// <summary>имя файла</summary>
      public string fileName = fileName;
      /// <summary>Индекс  в спиcке значений</summary>
      public int attrIndex = attrIndex;

      public int CompareTo(object obj)
      {
        InvalidBlobInfo invalidBlobInfo = (InvalidBlobInfo) obj;
        int num1 = this.objectCaption.CompareTo(invalidBlobInfo.objectCaption);
        if (num1 != 0)
          return num1;
        int num2 = this.objectID.CompareTo(invalidBlobInfo.objectID);
        if (num2 != 0)
          return num2;
        int num3 = this.attrID.CompareTo(invalidBlobInfo.attrID);
        if (num3 == 0)
          num3 = this.blobID.CompareTo(invalidBlobInfo.blobID);
        return num3;
      }
    }
}
