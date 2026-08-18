
// Type: Intermech.Client.Core.FormDesigner.Controls.AnyLinkRichTextBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// AnyLinkRichTextBox - RichTextBox, который парсит любые url, а не только стандартные http/https/mailto/file
/// 
/// https://www.codeproject.com/Articles/826821/AnyLinkRichTextBox - документация здесь
/// https://github.com/israelss/AnyLinkRichTextBox - исходник здесь, перенесен сюда и доработан
/// 
/// https://xeon1.imdomain:8443/svn/IPS_SVN/Private/Alex/AnyLinkRichTextBox_Src - пересохраненные к себе исходник с документацией
/// </summary>
public class AnyLinkRichTextBox : RichTextBox
{
  private AnyLinkRichTextBox.CHARFORMAT2_STRUCT cf;
  private const uint CFE_LINK = 32 /*0x20*/;
  private const uint CFM_LINK = 32 /*0x20*/;
  private const int WM_USER = 1024 /*0x0400*/;
  private const int EM_SETCHARFORMAT = 1092;
  private const int SCF_SELECTION = 1;
  private const int WM_SETREDRAW = 11;
  private const int FALSE = 0;
  private const int TRUE = 1;
  /// <summary>
  /// This Regex is used for parse the text and search for any normal link,
  /// by normal, I mean any link which starts with any protocol (http://|https://|etc...)
  /// or without protocol, but starting with 'www.' (www.example.com)
  /// </summary>
  private static Regex NormalLinks = new Regex("(?<Protocol>\\w+):\\/\\/(?<Domain1>[\\w@][\\w.:@]+)\\/?[\\w\\.?=%&=\\-@#!();:+/$,]*|(?<Domain2>w{3}\\.[\\w@][\\w.:@]+)\\/?[\\w\\.?=%&=\\-@#!();:+/$,]*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
  /// <summary>
  /// This Regex is used for parse the text and search for any IP like link,
  /// for example '255.255.255.255'
  /// </summary>
  private static Regex IPLinks = new Regex("(?<First>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.(?<Second>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.(?<Third>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.(?<Fourth>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
  /// <summary>
  /// This Regex is used for parse the text and search for any mail like link,
  /// for example 'user@company.com'.
  /// The mail links which starts with the protocol 'mailto:' are identified
  /// with the Regex normalLinks
  /// </summary>
  private static Regex MailLinks = new Regex("(mailto:)?([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
  private Dictionary<KeyValuePair<int, int>, string> hyperlinks = new Dictionary<KeyValuePair<int, int>, string>();
  private Point pt;
  private bool lockTextChanged;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

  /// <summary>
  /// That method is used to pause the drawing of the rich text box, so the user doesn't see
  /// any flickering nor the process of parsing the text looking for links
  /// </summary>
  private void SuspendDrawing() => AnyLinkRichTextBox.SendMessage(this.Handle, 11, 0, 0);

  /// <summary>
  /// That method is used to resume the drawing, so the user can see any modification
  /// on the text, as a result of the process of parsing the text looking for links
  /// </summary>
  private void ResumeDrawing()
  {
    AnyLinkRichTextBox.SendMessage(this.Handle, 11, 1, 0);
    this.Invalidate(true);
  }

  public AnyLinkRichTextBox()
  {
    base.DetectUrls = false;
    this.DetectUrls = false;
  }

  /// <summary>That property overrides base.DetectUrls</summary>
  [Browsable(true)]
  [DefaultValue(false)]
  public new bool DetectUrls { get; set; }

  /// <summary>Обработать текст на наличие ссылок</summary>
  private void ProcessURLs()
  {
    if (this.lockTextChanged)
      return;
    this.lockTextChanged = true;
    try
    {
      if (!this.DetectUrls)
        return;
      int selectionStart = this.SelectionStart;
      int indexFromPosition = this.GetCharIndexFromPosition(new Point(1, 1));
      this.SuspendDrawing();
      try
      {
        this.RemoveLinks();
        this.CheckNormalLinks();
        this.CheckMailLinks();
      }
      finally
      {
        this.SelectionStart = indexFromPosition;
        this.ScrollToCaret();
        this.ResumeDrawing();
      }
      if (selectionStart > 0)
        this.Select(selectionStart, 0);
      else
        this.Select(0, 0);
      this.Invalidate(true);
    }
    finally
    {
      this.lockTextChanged = false;
    }
  }

  /// <summary>
  /// This event occurs whenever the mouse move in the control area,
  /// so it is used to know where the user clicked and later calculate
  /// the index of caret at that position
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    this.pt = e.Location;
    base.OnMouseMove(e);
  }

  /// <summary>
  /// This event occurs whenever the rich text box text is changed,
  /// so it is used to start the parsing of text, to search for any changes
  /// in links, adding or removing
  /// </summary>
  /// <param name="e"></param>
  protected override void OnTextChanged(EventArgs e)
  {
    if (this.lockTextChanged)
      return;
    this.ProcessURLs();
    base.OnTextChanged(e);
  }

  /// <summary>
  /// This event occurs whenever the user clicks a link,
  /// so it is used to process the link according to the type
  /// (normal, IP, mail or custom link)
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLinkClicked(LinkClickedEventArgs e)
  {
    if (this.DetectUrls)
    {
      if (AnyLinkRichTextBox.NormalLinks.IsMatch(e.LinkText))
        Process.Start(e.LinkText);
      else if (AnyLinkRichTextBox.MailLinks.IsMatch(e.LinkText))
      {
        if (e.LinkText.StartsWith("mailto:"))
          Process.Start(e.LinkText);
        else
          Process.Start("mailto:" + e.LinkText);
      }
      else if (AnyLinkRichTextBox.IPLinks.IsMatch(e.LinkText))
      {
        Process.Start("http://" + e.LinkText);
      }
      else
      {
        int mouseClick = this.GetCharIndexFromPosition(this.pt);
        try
        {
          Process.Start(this.hyperlinks.Where<KeyValuePair<KeyValuePair<int, int>, string>>((Func<KeyValuePair<KeyValuePair<int, int>, string>, bool>) (k =>
          {
            int mouseClick1 = mouseClick;
            KeyValuePair<int, int> key1 = k.Key;
            int key2 = key1.Key;
            key1 = k.Key;
            int length = key1.Value;
            return this.IsInRange(mouseClick1, key2, length);
          })).Select<KeyValuePair<KeyValuePair<int, int>, string>, string>((Func<KeyValuePair<KeyValuePair<int, int>, string>, string>) (k => k.Value)).ToList<string>().First<string>());
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Некорректная ссылка");
        }
      }
    }
    base.OnLinkClicked(e);
  }

  private void RemoveLinks()
  {
    this.SelectAll();
    this.SetSelectionStyle(32U /*0x20*/, 0U);
  }

  private void CheckNormalLinks() => this.MarkMatches(AnyLinkRichTextBox.NormalLinks);

  private void CheckMailLinks() => this.MarkMatches(AnyLinkRichTextBox.MailLinks);

  private void CheckIPLinks() => this.MarkMatches(AnyLinkRichTextBox.IPLinks);

  private void MarkMatches(Regex regex)
  {
    IEnumerable<Match> source = regex.Matches(this.Text).Cast<Match>();
    if (!source.Any<Match>())
      return;
    foreach (Match match in source)
    {
      this.Select(match.Index, match.Length);
      this.SetSelectionStyle(32U /*0x20*/, 32U /*0x20*/);
    }
  }

  /// <summary>
  /// This method is used to know which link the user has clicked
  /// </summary>
  /// <param name="mouseClick">The caret index at the position where the user has clicked</param>
  /// <param name="start">The start index of link</param>
  /// <param name="length">The length of link</param>
  /// <returns>Returns true if the user has clicked that link, false otherwise</returns>
  private bool IsInRange(int mouseClick, int start, int length)
  {
    int num = start + length;
    return mouseClick >= start && mouseClick <= num;
  }

  /// <summary>Set the current selection's link style</summary>
  private void SetSelectionStyle(uint mask, uint effect)
  {
    this.cf.cbSize = (uint) Marshal.SizeOf<AnyLinkRichTextBox.CHARFORMAT2_STRUCT>(this.cf);
    this.cf.dwMask = mask;
    this.cf.dwEffects = effect;
    IntPtr wParam = new IntPtr(1);
    IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOf<AnyLinkRichTextBox.CHARFORMAT2_STRUCT>(this.cf));
    Marshal.StructureToPtr<AnyLinkRichTextBox.CHARFORMAT2_STRUCT>(this.cf, num, false);
    AnyLinkRichTextBox.SendMessage(this.Handle, 1092, wParam, num);
    Marshal.FreeCoTaskMem(num);
  }

  private struct CHARFORMAT2_STRUCT
  {
    public uint cbSize;
    public uint dwMask;
    public uint dwEffects;
    public int yHeight;
    public int yOffset;
    public int crTextColor;
    public byte bCharSet;
    public byte bPitchAndFamily;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 /*0x20*/)]
    public char[] szFaceName;
    public ushort wWeight;
    public ushort sSpacing;
    public int crBackColor;
    public int lcid;
    public int dwReserved;
    public short sStyle;
    public short wKerning;
    public byte bUnderlineType;
    public byte bAnimation;
    public byte bRevAuthor;
    public byte bReserved1;
  }
}
