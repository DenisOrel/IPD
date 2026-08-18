
// Type: Intermech.Runtime.ComInterop.ComTypes.STATFLAG
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.ComTypes
{
    internal enum STATFLAG : uint
    {
      /// <summary>
      /// Requests that the statistics include the pwcsName member of the STATSTG structure.
      /// </summary>
      STATFLAG_DEFAULT,
      /// <summary>
      /// Requests that the statistics not include the pwcsName member of the STATSTG structure.
      /// If the name is omitted, there is no need for the Stat methods to allocate and free
      /// memory for the string value of the name, therefore the method reduces time and
      /// resources used in an allocation and free operation.
      /// </summary>
      STATFLAG_NONAME,
      /// <summary>Not implemented.</summary>
      STATFLAG_NOOPEN,
    }
}
