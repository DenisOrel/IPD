
// Type: Intermech.UI.Winforms.SystemIcon
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    public static class SystemIcon
    {
      private static Size? _largeSize;
      private static Size? _smallSize;
      [NotNull]
      private static readonly ConcurrentDictionary<CachedIconID, Icon> _cachedIcons = new ConcurrentDictionary<CachedIconID, Icon>();
      private const Shell32.SHSTOCKICONID EmptyIcon = (Shell32.SHSTOCKICONID) -2147483648 /*0x80000000*/;

      public static Size LargeSize
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return SystemIcon._largeSize ?? (SystemIcon._largeSize = new Size?(new Size(User32.GetSystemMetrics(User32.SystemMetric.CXICON), User32.GetSystemMetrics(User32.SystemMetric.CYICON)))).Value;
        }
      }

      public static Size SmallSize
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return SystemIcon._smallSize ?? (SystemIcon._smallSize = new Size?(new Size(User32.GetSystemMetrics(User32.SystemMetric.CXSMICON), User32.GetSystemMetrics(User32.SystemMetric.CYSMICON)))).Value;
        }
      }

      [NotNull]
      public static Icon Get(
        Shell32.SHSTOCKICONID iconToGet,
        IconSize iconSize = IconSize.Large,
        bool withOverlay = false,
        bool selected = false)
      {
            CachedIconID key = new CachedIconID(iconToGet, iconSize, withOverlay, selected);
        return Intermech.Diagnostics.Check.Result.NotNull(SystemIcon._cachedIcons.GetOrAdd(key, new Func<CachedIconID, Icon>(SystemIcon.GetIcon)));
      }

      [NotNull]
      private static Icon GetIcon(CachedIconID cachedIconID)
      {
        Icon icon;
        if (cachedIconID.Icon == (Shell32.SHSTOCKICONID) -2147483648 /*0x80000000*/)
        {
          Size size;
          switch (cachedIconID.IconSize)
          {
            case IconSize.Large:
              ref Size local1 = ref size;
              Size largeSize = SystemIcon.LargeSize;
              int width1 = largeSize.Width;
              largeSize = SystemIcon.LargeSize;
              int height1 = largeSize.Height;
              local1 = new Size(width1, height1);
              break;
            case IconSize.Small:
              ref Size local2 = ref size;
              Size smallSize = SystemIcon.SmallSize;
              int width2 = smallSize.Width;
              smallSize = SystemIcon.SmallSize;
              int height2 = smallSize.Height;
              local2 = new Size(width2, height2);
              break;
            default:
              throw new ArgumentOutOfRangeException("IconSize", (object) cachedIconID.IconSize, (string) null);
          }
          Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
          bitmap.MakeTransparent();
          icon = Icon.FromHandle(bitmap.GetHicon());
        }
        else
        {
          Shell32.SHSTOCKICONINFO psii = new Shell32.SHSTOCKICONINFO();
          Shell32.SHGSI uFlags = (Shell32.SHGSI) ((IconSize) 256 /*0x0100*/ | cachedIconID.IconSize);
          if (cachedIconID.WithOverlay)
            uFlags |= Shell32.SHGSI.LINKOVERLAY;
          if (cachedIconID.Selected)
            uFlags |= Shell32.SHGSI.SELECTED;
          Marshal.ThrowExceptionForHR(Shell32.SHGetStockIconInfo(cachedIconID.Icon, uFlags, psii));
          icon = (Icon) Icon.FromHandle(psii.hIcon).Clone();
          Shell32.DestroyIcon(psii.hIcon);
        }
        return icon;
      }

      [NotNull]
      public static Icon Get(
        MessageBoxIcon messageBoxIcon,
        IconSize iconSize = IconSize.Large,
        bool withOverlay = false,
        bool selected = false)
      {
        Shell32.SHSTOCKICONID icon;
        switch (messageBoxIcon)
        {
          case MessageBoxIcon.None:
            icon = (Shell32.SHSTOCKICONID) -2147483648 /*0x80000000*/;
            break;
          case MessageBoxIcon.Hand:
            icon = Shell32.SHSTOCKICONID.ERROR;
            break;
          case MessageBoxIcon.Question:
            icon = Shell32.SHSTOCKICONID.HELP;
            break;
          case MessageBoxIcon.Exclamation:
            icon = Shell32.SHSTOCKICONID.WARNING;
            break;
          case MessageBoxIcon.Asterisk:
            icon = Shell32.SHSTOCKICONID.INFO;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (messageBoxIcon), (object) messageBoxIcon, (string) null);
        }
            CachedIconID key = new CachedIconID(icon, iconSize, withOverlay, selected);
        return Intermech.Diagnostics.Check.Result.NotNull(SystemIcon._cachedIcons.GetOrAdd(key, new Func<CachedIconID, Icon>(SystemIcon.GetIcon)));
      }

      private readonly struct CachedIconID : IEquatable<CachedIconID>
      {
        public readonly Shell32.SHSTOCKICONID Icon;
        public readonly IconSize IconSize;
        public readonly bool WithOverlay;
        public readonly bool Selected;
        private readonly int _hashCode;

        public CachedIconID(
          Shell32.SHSTOCKICONID icon,
          IconSize iconSize,
          bool withOverlay,
          bool selected)
        {
          this.Icon = icon;
          this.IconSize = iconSize;
          this.WithOverlay = withOverlay;
          this.Selected = selected;
          this._hashCode = (int) this.Icon;
          this._hashCode = (int) ((IconSize) (this._hashCode * 397) ^ this.IconSize);
          this._hashCode = this._hashCode * 397 ^ (this.WithOverlay ? 1 : 0);
          this._hashCode = this._hashCode * 397 ^ (this.Selected ? 1 : 0);
        }

        public override bool Equals(object obj)
        {
          return obj is CachedIconID cachedIconId && this.Icon == cachedIconId.Icon && this.IconSize == cachedIconId.IconSize && this.WithOverlay == cachedIconId.WithOverlay && this.Selected == cachedIconId.Selected;
        }

        public override int GetHashCode() => this._hashCode;

        public bool Equals(CachedIconID other)
        {
          return this.Icon == other.Icon && this.IconSize == other.IconSize && this.WithOverlay == other.WithOverlay && this.Selected == other.Selected;
        }

        public static bool operator ==(CachedIconID left, CachedIconID right)
        {
          return left.Equals(right);
        }

        public static bool operator !=(CachedIconID left, CachedIconID right)
        {
          return !left.Equals(right);
        }
      }
    }
}
