
// Type: Intermech.Runtime.ComInterop.ComTypes.STGM
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.ComTypes
{
    /// <summary>Storage medium constants and flags</summary>
    internal static class STGM
    {
      public const int STGM_READ = 0;
      public const int STGM_WRITE = 1;
      public const int STGM_READWRITE = 2;
      public const int STGM_SHARE_DENY_NONE = 64 /*0x40*/;
      public const int STGM_SHARE_DENY_READ = 48 /*0x30*/;
      public const int STGM_SHARE_DENY_WRITE = 32 /*0x20*/;
      public const int STGM_SHARE_EXCLUSIVE = 16 /*0x10*/;
      public const int STGM_PRIORITY = 262144 /*0x040000*/;
      public const int STGM_CREATE = 4096 /*0x1000*/;
      public const int STGM_CONVERT = 131072 /*0x020000*/;
      public const int STGM_FAILIFTHERE = 0;
      public const int STGM_DIRECT = 0;
      public const int STGM_TRANSACTED = 65536 /*0x010000*/;
      public const int STGM_NOSCRATCH = 1048576 /*0x100000*/;
      public const int STGM_NOSNAPSHOT = 2097152 /*0x200000*/;
      public const int STGM_SIMPLE = 134217728 /*0x08000000*/;
      public const int STGM_DIRECT_SWMR = 4194304 /*0x400000*/;
      public const int STGM_DELETEONRELEASE = 67108864 /*0x04000000*/;
    }
}
