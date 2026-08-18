
// Type: Intermech.Runtime.ComInterop.ComTypes.STGTY
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.ComTypes
{
    /// <summary>
    /// The STGTY enumeration values are used in the type member of the STATSTG structure to indicate the type of the storage element. A storage element is a storage object, a stream object, or a byte-array object (LOCKBYTES).
    /// </summary>
    internal enum STGTY
    {
      /// <summary>
      /// STGTY_STORAGE Indicates that the storage element is a storage object.
      /// </summary>
      STGTY_STORAGE = 1,
      /// <summary>
      /// STGTY_STREAM Indicates that the storage element is a stream object.
      /// </summary>
      STGTY_STREAM = 2,
      /// <summary>
      /// STGTY_LOCKBYTES Indicates that the storage element is a byte-array object.
      /// </summary>
      STGTY_LOCKBYTES = 3,
      /// <summary>
      /// STGTY_PROPERTYIndicates that the storage element is a property storage object.
      /// </summary>
      STGTY_PROPERTY = 4,
    }
}
