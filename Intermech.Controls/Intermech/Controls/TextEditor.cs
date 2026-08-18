
// Type: Intermech.Controls.TextEditor
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Controls.Properties;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;


namespace Intermech.Controls;

public class TextEditor : UserControl, ISearchableBrowser
{
  private mshtml.IHTMLDocument2 doc;
  private bool updatingFontName;
  private bool updatingFontSize;
  private bool setup;
  private bool init_timer;
  private DateTime lastSplash = DateTime.Now;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip toolStrip1;
  private WebBrowser webBrowser1;
  private ToolStripButton boldButton;
  private ToolStripButton italicButton;
  private ToolStripComboBox fontComboBox;
  private ToolStripButton toolStripButton1;
  private ToolStripButton toolStripButton2;
  private ToolStripComboBox fontSizeComboBox;
  private ToolStripButton underlineButton;
  private ToolStripButton colorButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem copyToolStripMenuItem;
  private ToolStripMenuItem pasteToolStripMenuItem;
  private ToolStripMenuItem pasteToolStripMenuItem1;
  private ToolStripMenuItem cutToolStripMenuItem;
  private ToolStripMenuItem copyToolStripMenuItem1;
  private ToolStripMenuItem pasteToolStripMenuItem2;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem cutToolStripMenuItem1;
  private ToolStripMenuItem copyToolStripMenuItem2;
  private ToolStripMenuItem pasteToolStripMenuItem3;
  private ToolStripMenuItem deleteToolStripMenuItem;
  private Timer timer;
  private ToolStripButton outdentButton;
  private ToolStripButton indentButton;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripButton backColorButton;
  private ToolStripButton orderedListButton;
  private ToolStripButton unorderedListButton;
  private ToolStripSeparator toolStripSeparator5;
  private ToolStripButton justifyLeftButton;
  private ToolStripButton justifyCenterButton;
  private ToolStripButton justifyRightButton;
  private ToolStripButton justifyFullButton;
  private ToolStripMenuItem backgroundColorToolStripMenuItem;
  private ToolStripMenuItem cSSToolStripMenuItem;

  public event TextEditor.TickDelegate Tick;

  public event WebBrowserNavigatedEventHandler Navigated;

  public event EventHandler<TextEditor.EnterKeyEventArgs> EnterKeyEvent;

  public TextEditor()
  {
    this.Load += new EventHandler(this.Editor_Load);
    this.InitializeComponent();
    this.SetupEvents();
    this.SetupTimer();
    this.SetupBrowser();
    this.SetupFontComboBox();
    this.SetupFontSizeComboBox();
    this.boldButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.BoldChanged == null)
        return;
      this.BoldChanged();
    });
    this.italicButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.ItalicChanged == null)
        return;
      this.ItalicChanged();
    });
    this.underlineButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.UnderlineChanged == null)
        return;
      this.UnderlineChanged();
    });
    this.orderedListButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.OrderedListChanged == null)
        return;
      this.OrderedListChanged();
    });
    this.unorderedListButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.UnorderedListChanged == null)
        return;
      this.UnorderedListChanged();
    });
    this.justifyLeftButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.JustifyLeftChanged == null)
        return;
      this.JustifyLeftChanged();
    });
    this.justifyCenterButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.JustifyCenterChanged == null)
        return;
      this.JustifyCenterChanged();
    });
    this.justifyRightButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.JustifyRightChanged == null)
        return;
      this.JustifyRightChanged();
    });
    this.justifyFullButton.CheckedChanged += (EventHandler) ((_param1, _param2) =>
    {
      if (this.JustifyFullChanged == null)
        return;
      this.JustifyFullChanged();
    });
  }

  private void Editor_Load(object sender, EventArgs e) => this.timer.Start();

  private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.timer.Stop();
    this.ParentForm.FormClosed -= new FormClosedEventHandler(this.ParentForm_FormClosed);
  }

  /// <summary>Setup navigation and focus event handlers.</summary>
  private void SetupEvents()
  {
    this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
    this.webBrowser1.GotFocus += new EventHandler(this.webBrowser1_GotFocus);
    if (this.webBrowser1.Version.Major < 9)
      return;
    this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
  }

  private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
  {
    this.webBrowser1.Document.Write(this.webBrowser1.DocumentText);
    this.doc.designMode = "On";
    if (!(this.webBrowser1.Document.Body != (HtmlElement) null))
      return;
    this.webBrowser1.Document.Body.SetAttribute("contentEditable", "true");
  }

  /// <summary>
  /// When this control receives focus, it transfers focus to the
  /// document body.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void webBrowser1_GotFocus(object sender, EventArgs e) => this.SuperFocus();

  /// <summary>
  /// This is called when the initial html/body framework is set up,
  /// or when document.DocumentText is set.  At this point, the
  /// document is editable.
  /// </summary>
  /// <param name="sender">sender</param>
  /// <param name="e">navigation args</param>
  private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
  {
    this.SetBackgroundColor(this.BackColor);
    if (this.Navigated == null)
      return;
    this.Navigated((object) this, e);
  }

  /// <summary>Setup timer with 200ms interval</summary>
  private void SetupTimer()
  {
    this.timer.Interval = 200;
    this.timer.Tick += new EventHandler(this.timer_Tick);
  }

  /// <summary>
  /// Add document body, turn on design mode on the whole document,
  /// and overred the context menu
  /// </summary>
  private void SetupBrowser()
  {
    this.webBrowser1.DocumentText = "<html><body></body></html>";
    this.doc = this.webBrowser1.Document.DomDocument as mshtml.IHTMLDocument2;
    this.doc.designMode = "On";
    this.webBrowser1.Document.ContextMenuShowing += new HtmlElementEventHandler(this.Document_ContextMenuShowing);
  }

  /// <summary>Set the focus on the document body.</summary>
  private void SuperFocus()
  {
    if (!(this.webBrowser1.Document != (HtmlDocument) null) || !(this.webBrowser1.Document.Body != (HtmlElement) null))
      return;
    this.webBrowser1.Document.Body.Focus();
  }

  /// <summary>
  /// Get/Set the background color of the editor.
  /// Note that if this is called before the document is rendered and
  /// complete, the navigated event handler will set the body's
  /// background color based on the state of BackColor.
  /// </summary>
  [Browsable(true)]
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      base.BackColor = value;
      if (this.ReadyState != ReadyStates.Complete)
        return;
      this.SetBackgroundColor(value);
    }
  }

  /// <summary>
  /// Set the background color of the body by setting it's CSS style
  /// </summary>
  /// <param name="value">the color to use for the background</param>
  private void SetBackgroundColor(Color value)
  {
    if (!(this.webBrowser1.Document != (HtmlDocument) null) || !(this.webBrowser1.Document.Body != (HtmlElement) null))
      return;
    this.webBrowser1.Document.Body.Style = $"background-color: {value.Name}";
  }

  /// <summary>
  /// Clear the contents of the document, leaving the body intact.
  /// </summary>
  public void Clear()
  {
    if (!(this.webBrowser1.Document.Body != (HtmlElement) null))
      return;
    this.webBrowser1.Document.Body.InnerHtml = "";
  }

  /// <summary>Get the web browser component's document</summary>
  public HtmlDocument Document => this.webBrowser1.Document;

  /// <summary>
  /// Document text should be used to load/save the entire document,
  /// including html and body start/end tags.
  /// </summary>
  [Browsable(false)]
  public string DocumentText
  {
    get
    {
      string html = this.webBrowser1.DocumentText;
      if (html != null)
        html = this.ReplaceFileSystemImages(html);
      return html;
    }
    set => this.webBrowser1.DocumentText = value;
  }

  /// <summary>Get the html document title from document.</summary>
  [Browsable(false)]
  public string DocumentTitle => this.webBrowser1.DocumentTitle;

  /// <summary>Get/Set the contents of the document Body, in html.</summary>
  [Browsable(false)]
  public string BodyHtml
  {
    get
    {
      if (!(this.webBrowser1.Document != (HtmlDocument) null) || !(this.webBrowser1.Document.Body != (HtmlElement) null))
        return string.Empty;
      string html = this.webBrowser1.Document.Body.InnerHtml;
      if (html != null)
        html = this.ReplaceFileSystemImages(html);
      return html;
    }
    set
    {
      if (!(this.webBrowser1.Document.Body != (HtmlElement) null))
        return;
      this.webBrowser1.Document.Body.InnerHtml = value;
    }
  }

  public MailMessage ToMailMessage()
  {
    if (this.webBrowser1.Document != (HtmlDocument) null && this.webBrowser1.Document.Body != (HtmlElement) null)
    {
      string innerHtml = this.webBrowser1.Document.Body.InnerHtml;
      if (innerHtml != null)
        return this.LinkImages(innerHtml);
      return new MailMessage() { IsBodyHtml = true };
    }
    return new MailMessage()
    {
      IsBodyHtml = true,
      Body = string.Empty
    };
  }

  private MailMessage LinkImages(string html)
  {
    MailMessage mailMessage = new MailMessage();
    mailMessage.IsBodyHtml = true;
    MatchCollection matchCollection = Regex.Matches(html, "<img[^>]*?src\\s*=\\s*([\"']?[^'\">]+?['\"])[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
    List<LinkedResource> linkedResourceList = new List<LinkedResource>();
    int num = 1;
    foreach (Match match in matchCollection)
    {
      string str1 = match.Groups[1].Value.Trim('"');
      if (File.Exists(str1))
      {
        string str2 = Path.GetExtension(str1);
        if (str2.Length > 0)
        {
          string str3 = str2.Substring(1);
          LinkedResource linkedResource = new LinkedResource(str1);
          linkedResource.ContentId = $"img{num++}.{str3}";
          linkedResource.TransferEncoding = TransferEncoding.Base64;
          linkedResource.ContentType.MediaType = $"image/{str3}";
          linkedResource.ContentType.Name = linkedResource.ContentId;
          linkedResourceList.Add(linkedResource);
          string newValue = $"'cid:{linkedResource.ContentId}'";
          html = html.Replace(match.Groups[1].Value, newValue);
        }
      }
    }
    AlternateView alternateViewFromString = AlternateView.CreateAlternateViewFromString(html, (Encoding) null, "text/html");
    foreach (LinkedResource linkedResource in linkedResourceList)
      alternateViewFromString.LinkedResources.Add(linkedResource);
    mailMessage.AlternateViews.Add(alternateViewFromString);
    return mailMessage;
  }

  private string ReplaceFileSystemImages(string html)
  {
    foreach (Match match in Regex.Matches(html, "<img[^>]*?src\\s*=\\s*([\"']?[^'\">]+?['\"])[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace))
    {
      string path = match.Groups[1].Value.Trim('"');
      if (File.Exists(path))
      {
        string str = Path.GetExtension(path);
        if (str.Length > 0)
        {
          string newValue = $"'data:image/{str.Substring(1)};base64,{Convert.ToBase64String(File.ReadAllBytes(path))}'";
          html = html.Replace(match.Groups[1].Value, newValue);
        }
      }
    }
    return html;
  }

  /// <summary>Get/Set the documents body as text.</summary>
  [Browsable(false)]
  public string BodyText
  {
    get
    {
      return this.webBrowser1.Document != (HtmlDocument) null && this.webBrowser1.Document.Body != (HtmlElement) null ? this.webBrowser1.Document.Body.InnerText : string.Empty;
    }
    set
    {
      this.Document.OpenNew(false);
      if (!(this.webBrowser1.Document.Body != (HtmlElement) null))
        return;
      this.webBrowser1.Document.Body.InnerText = HttpUtility.HtmlEncode(value);
    }
  }

  [Browsable(false)]
  public string Html
  {
    get
    {
      return this.webBrowser1.Document != (HtmlDocument) null && this.webBrowser1.Document.Body != (HtmlElement) null ? this.webBrowser1.Document.Body.InnerHtml : string.Empty;
    }
    set
    {
      this.Document.OpenNew(true);
      mshtml.IHTMLDocument2 domDocument = this.Document.DomDocument as mshtml.IHTMLDocument2;
      try
      {
        if (value == null)
          domDocument.clear();
        else
          domDocument.write((object) value);
      }
      finally
      {
        domDocument.close();
      }
    }
  }

  /// <summary>
  /// Determine the status of the Undo command in the document editor.
  /// </summary>
  /// <returns>whether or not an undo operation is currently valid</returns>
  public bool CanUndo() => this.doc.queryCommandEnabled("Undo");

  /// <summary>
  /// Determine the status of the Redo command in the document editor.
  /// </summary>
  /// <returns>whether or not a redo operation is currently valid</returns>
  public bool CanRedo() => this.doc.queryCommandEnabled("Redo");

  /// <summary>
  /// Determine the status of the Cut command in the document editor.
  /// </summary>
  /// <returns>whether or not a cut operation is currently valid</returns>
  public bool CanCut() => this.doc.queryCommandEnabled("Cut");

  /// <summary>
  /// Determine the status of the Copy command in the document editor.
  /// </summary>
  /// <returns>whether or not a copy operation is currently valid</returns>
  public bool CanCopy() => this.doc.queryCommandEnabled("Copy");

  /// <summary>
  /// Determine the status of the Paste command in the document editor.
  /// </summary>
  /// <returns>whether or not a copy operation is currently valid</returns>
  public bool CanPaste() => this.doc.queryCommandEnabled("Paste");

  /// <summary>
  /// Determine the status of the Delete command in the document editor.
  /// </summary>
  /// <returns>whether or not a copy operation is currently valid</returns>
  public bool CanDelete() => this.doc.queryCommandEnabled("Delete");

  /// <summary>
  /// Determine whether the current block is left justified.
  /// </summary>
  /// <returns>true if left justified, otherwise false</returns>
  public bool IsJustifyLeft() => this.doc.queryCommandState("JustifyLeft");

  /// <summary>
  /// Determine whether the current block is right justified.
  /// </summary>
  /// <returns>true if right justified, otherwise false</returns>
  public bool IsJustifyRight() => this.doc.queryCommandState("JustifyRight");

  /// <summary>
  /// Determine whether the current block is center justified.
  /// </summary>
  /// <returns>true if center justified, false otherwise</returns>
  public bool IsJustifyCenter() => this.doc.queryCommandState("JustifyCenter");

  /// <summary>
  /// Determine whether the current block is full justified.
  /// </summary>
  /// <returns>true if full justified, false otherwise</returns>
  public bool IsJustifyFull() => this.doc.queryCommandState("JustifyFull");

  /// <summary>
  /// Determine whether the current selection is in Bold mode.
  /// </summary>
  /// <returns>whether or not the current selection is Bold</returns>
  public bool IsBold() => this.doc.queryCommandState("Bold");

  /// <summary>
  /// Determine whether the current selection is in Italic mode.
  /// </summary>
  /// <returns>whether or not the current selection is Italicized</returns>
  public bool IsItalic() => this.doc.queryCommandState("Italic");

  /// <summary>
  /// Determine whether the current selection is in Underline mode.
  /// </summary>
  /// <returns>whether or not the current selection is Underlined</returns>
  public bool IsUnderline() => this.doc.queryCommandState("Underline");

  /// <summary>
  /// Determine whether the current paragraph is an ordered list.
  /// </summary>
  /// <returns>true if current paragraph is ordered, false otherwise</returns>
  public bool IsOrderedList() => this.doc.queryCommandState("InsertOrderedList");

  /// <summary>
  /// Determine whether the current paragraph is an unordered list.
  /// </summary>
  /// <returns>true if current paragraph is ordered, false otherwise</returns>
  public bool IsUnorderedList() => this.doc.queryCommandState("InsertUnorderedList");

  /// <summary>
  /// Called when the editor context menu should be displayed.
  /// The return value of the event is set to false to disable the
  /// default context menu.  A custom context menu (contextMenuStrip1) is
  /// shown instead.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">HtmlElementEventArgs</param>
  private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
  {
    e.ReturnValue = false;
    this.cutToolStripMenuItem1.Enabled = this.CanCut();
    this.copyToolStripMenuItem2.Enabled = this.CanCopy();
    this.pasteToolStripMenuItem3.Enabled = this.CanPaste();
    this.deleteToolStripMenuItem.Enabled = this.CanDelete();
    this.cSSToolStripMenuItem.Enabled = this.SelectionType != SelectionTypes.None;
    this.contextMenuStrip1.Show((Control) this, e.ClientMousePosition);
  }

  /// <summary>
  /// Populate the font size combobox.
  /// Add text changed and key press handlers to handle input and update
  /// the editor selection font size.
  /// </summary>
  private void SetupFontSizeComboBox()
  {
    for (int index = 1; index <= 7; ++index)
      this.fontSizeComboBox.Items.Add((object) index.ToString());
    this.fontSizeComboBox.TextChanged += new EventHandler(this.fontSizeComboBox_TextChanged);
    this.fontSizeComboBox.KeyPress += new KeyPressEventHandler(this.fontSizeComboBox_KeyPress);
  }

  /// <summary>
  /// Called when a key is pressed on the font size combo box.
  /// The font size in the boxy box is set to the key press value.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">KeyPressEventArgs</param>
  private void fontSizeComboBox_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (char.IsNumber(e.KeyChar))
    {
      e.Handled = true;
      if (e.KeyChar > '7' || e.KeyChar <= '0')
        return;
      this.fontSizeComboBox.Text = e.KeyChar.ToString();
    }
    else
    {
      if (char.IsControl(e.KeyChar))
        return;
      e.Handled = true;
    }
  }

  /// <summary>
  /// Set editor's current selection to the value of the font size combo box.
  /// Ignore if the timer is currently updating the font size to synchronize
  /// the font size combo box with the editor's current selection.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void fontSizeComboBox_TextChanged(object sender, EventArgs e)
  {
    if (this.updatingFontSize)
      return;
    switch (this.fontSizeComboBox.Text.Trim())
    {
      case "1":
        this.FontSize = FontSizes.One;
        break;
      case "2":
        this.FontSize = FontSizes.Two;
        break;
      case "3":
        this.FontSize = FontSizes.Three;
        break;
      case "4":
        this.FontSize = FontSizes.Four;
        break;
      case "5":
        this.FontSize = FontSizes.Five;
        break;
      case "6":
        this.FontSize = FontSizes.Six;
        break;
      case "7":
        this.FontSize = FontSizes.Seven;
        break;
      default:
        this.FontSize = FontSizes.Seven;
        break;
    }
  }

  /// <summary>
  /// Populate the font combo box and autocomplete handlers.
  /// Add a text changed handler to the font combo box to handle new font selections.
  /// </summary>
  private void SetupFontComboBox()
  {
    AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
    foreach (FontFamily family in FontFamily.Families)
    {
      this.fontComboBox.Items.Add((object) family.Name);
      stringCollection.Add(family.Name);
    }
    this.fontComboBox.Leave += new EventHandler(this.fontComboBox_TextChanged);
    this.fontComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
    this.fontComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this.fontComboBox.AutoCompleteCustomSource = stringCollection;
  }

  /// <summary>
  /// Called when the font combo box has changed.
  /// Ignores the event when the timer is updating the font combo Box
  /// to synchronize the editor selection with the font combo box.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void fontComboBox_TextChanged(object sender, EventArgs e)
  {
    if (this.updatingFontName)
      return;
    FontFamily fontFamily;
    try
    {
      fontFamily = new FontFamily(this.fontComboBox.Text);
    }
    catch (Exception ex)
    {
      this.updatingFontName = true;
      this.fontComboBox.Text = this.FontName.GetName(0);
      this.updatingFontName = false;
      return;
    }
    this.FontName = fontFamily;
  }

  private void UpdateImageSizes()
  {
    foreach (HTMLImg image in this.doc.images)
    {
      if (image != null)
      {
        if (image.height != image.style.pixelHeight && image.style.pixelHeight != 0)
          image.height = image.style.pixelHeight;
        if (image.width != image.style.pixelWidth && image.style.pixelWidth != 0)
          image.width = image.style.pixelWidth;
      }
    }
  }

  public event MethodInvoker BoldChanged;

  public event MethodInvoker ItalicChanged;

  public event MethodInvoker UnderlineChanged;

  public event MethodInvoker OrderedListChanged;

  public event MethodInvoker UnorderedListChanged;

  public event MethodInvoker JustifyLeftChanged;

  public event MethodInvoker JustifyCenterChanged;

  public event MethodInvoker JustifyRightChanged;

  public event MethodInvoker JustifyFullChanged;

  public event MethodInvoker IsLinkChanged;

  public event MethodInvoker HtmlFontChanged;

  public event MethodInvoker HtmlFontSizeChanged;

  /// <summary>
  /// Called when the timer fires to synchronize the format buttons
  /// with the text editor current selection.
  /// SetupKeyListener if necessary.
  /// Set bold, italic, underline and link buttons as based on editor state.
  /// Synchronize the font combo box and the font size combo box.
  /// Finally, fire the Tick event to allow external components to synchronize
  /// their state with the editor.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void timer_Tick(object sender, EventArgs e)
  {
    if (!this.init_timer)
    {
      this.ParentForm.FormClosed += new FormClosedEventHandler(this.ParentForm_FormClosed);
      this.init_timer = true;
      this.lastSplash = DateTime.Now;
    }
    if (this.ReadyState != ReadyStates.Complete)
      return;
    this.SetupKeyListener();
    this.boldButton.Checked = this.IsBold();
    this.italicButton.Checked = this.IsItalic();
    this.underlineButton.Checked = this.IsUnderline();
    this.orderedListButton.Checked = this.IsOrderedList();
    this.unorderedListButton.Checked = this.IsUnorderedList();
    this.justifyLeftButton.Checked = this.IsJustifyLeft();
    this.justifyCenterButton.Checked = this.IsJustifyCenter();
    this.justifyRightButton.Checked = this.IsJustifyRight();
    this.justifyFullButton.Checked = this.IsJustifyFull();
    this.UpdateFontComboBox();
    this.UpdateFontSizeComboBox();
    this.UpdateImageSizes();
    if (this.Tick == null)
      return;
    this.Tick();
  }

  /// <summary>
  /// Update the font size combo box.
  /// Sets a flag to indicate that the combo box is updating, and should
  /// not update the editor's selection.
  /// </summary>
  private void UpdateFontSizeComboBox()
  {
    if (this.fontSizeComboBox.Focused)
      return;
    int num;
    switch (this.FontSize)
    {
      case FontSizes.One:
        num = 1;
        break;
      case FontSizes.Two:
        num = 2;
        break;
      case FontSizes.Three:
        num = 3;
        break;
      case FontSizes.Four:
        num = 4;
        break;
      case FontSizes.Five:
        num = 5;
        break;
      case FontSizes.Six:
        num = 6;
        break;
      case FontSizes.Seven:
        num = 7;
        break;
      case FontSizes.NA:
        num = 0;
        break;
      default:
        num = 7;
        break;
    }
    string str = Convert.ToString(num);
    if (!(str != this.fontSizeComboBox.Text))
      return;
    this.updatingFontSize = true;
    this.fontSizeComboBox.Text = str;
    if (this.HtmlFontSizeChanged != null)
      this.HtmlFontSizeChanged();
    this.updatingFontSize = false;
  }

  /// <summary>
  /// Update the font combo box.
  /// Sets a flag to indicate that the combo box is updating, and should
  /// not update the editor's selection.
  /// </summary>
  private void UpdateFontComboBox()
  {
    if (this.fontComboBox.Focused)
      return;
    FontFamily fontName = this.FontName;
    if (fontName == null)
      return;
    string name = fontName.Name;
    if (!(name != this.fontComboBox.Text))
      return;
    this.updatingFontName = true;
    this.fontComboBox.Text = name;
    if (this.HtmlFontChanged != null)
      this.HtmlFontChanged();
    this.updatingFontName = false;
  }

  public Color BodyBackgroundColor
  {
    get
    {
      return this.doc.body != null && this.doc.body.style != null && this.doc.body.style.backgroundColor != null ? TextEditor.ConvertToColor(Convert.ToString(this.doc.body.style.backgroundColor)) : Color.White;
    }
    set
    {
      if (this.ReadyState != ReadyStates.Complete || this.doc.body == null || this.doc.body.style == null)
        return;
      this.doc.body.style.backgroundColor = (object) $"#{value.R:X2}{value.G:X2}{value.B:X2}";
    }
  }

  /// <summary>
  /// Set up a key listener on the body once.
  /// The key listener checks for specific key strokes and takes
  /// special action in certain cases.
  /// </summary>
  private void SetupKeyListener()
  {
    if (this.setup)
      return;
    this.webBrowser1.Document.Body.KeyDown += new HtmlElementEventHandler(this.Body_KeyDown);
    this.setup = true;
  }

  /// <summary>
  /// If the user hits the enter key, and event will fire (EnterKeyEvent),
  /// and the consumers of this event can cancel the projecessing of the
  /// enter key by cancelling the event.
  /// This is useful if your application would like to take some action
  /// when the enter key is pressed, such as a submission to a web service.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">HtmlElementEventArgs</param>
  private void Body_KeyDown(object sender, HtmlElementEventArgs e)
  {
    if (e.KeyPressedCode != 13 || e.ShiftKeyPressed)
      return;
    bool flag = false;
    if (this.EnterKeyEvent != null)
    {
      TextEditor.EnterKeyEventArgs e1 = new TextEditor.EnterKeyEventArgs();
      this.EnterKeyEvent((object) this, e1);
      flag = e1.Cancel;
    }
    e.ReturnValue = !flag;
  }

  /// <summary>
  /// Embed a break at the current selection.
  /// This is a placeholder for future functionality.
  /// </summary>
  public void EmbedBr()
  {
    IHTMLTxtRange range = this.doc.selection.createRange() as IHTMLTxtRange;
    range.pasteHTML("<br/>");
    range.collapse(false);
    range.select();
  }

  /// <summary>
  /// Paste the clipboard text into the current selection.
  /// This is a placeholder for future functionality.
  /// </summary>
  private void SuperPaste()
  {
    if (!Clipboard.ContainsText())
      return;
    IHTMLTxtRange range = this.doc.selection.createRange() as IHTMLTxtRange;
    range.pasteHTML(Clipboard.GetText(TextDataFormat.Text));
    range.collapse(false);
    range.select();
  }

  /// <summary>Print the current document</summary>
  public void Print() => this.webBrowser1.Document.ExecCommand(nameof (Print), true, (object) null);

  /// <summary>Insert a paragraph break</summary>
  public void InsertParagraph()
  {
    this.webBrowser1.Document.ExecCommand(nameof (InsertParagraph), false, (object) null);
  }

  /// <summary>Insert a horizontal rule</summary>
  public void InsertBreak()
  {
    this.webBrowser1.Document.ExecCommand("InsertHorizontalRule", false, (object) null);
  }

  /// <summary>Select all text in the document.</summary>
  public void SelectAll()
  {
    this.webBrowser1.Document.ExecCommand(nameof (SelectAll), false, (object) null);
  }

  /// <summary>Undo the last operation</summary>
  public void Undo() => this.webBrowser1.Document.ExecCommand(nameof (Undo), false, (object) null);

  /// <summary>Redo based on the last Undo</summary>
  public void Redo() => this.webBrowser1.Document.ExecCommand(nameof (Redo), false, (object) null);

  /// <summary>
  /// Cut the current selection and place it in the clipboard.
  /// </summary>
  public void Cut() => this.webBrowser1.Document.ExecCommand(nameof (Cut), false, (object) null);

  /// <summary>
  /// Paste the contents of the clipboard into the current selection.
  /// </summary>
  public void Paste()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Paste), false, (object) null);
  }

  /// <summary>Copy the current selection into the clipboard.</summary>
  public void Copy() => this.webBrowser1.Document.ExecCommand(nameof (Copy), false, (object) null);

  /// <summary>
  /// Toggle the ordered list property for the current paragraph.
  /// </summary>
  public void OrderedList()
  {
    this.webBrowser1.Document.ExecCommand("InsertOrderedList", false, (object) null);
  }

  /// <summary>
  /// Toggle the unordered list property for the current paragraph.
  /// </summary>
  public void UnorderedList()
  {
    this.webBrowser1.Document.ExecCommand("InsertUnorderedList", false, (object) null);
  }

  /// <summary>
  /// Toggle the left justify property for the currnet block.
  /// </summary>
  public void JustifyLeft()
  {
    this.webBrowser1.Document.ExecCommand(nameof (JustifyLeft), false, (object) null);
  }

  /// <summary>
  /// Toggle the right justify property for the current block.
  /// </summary>
  public void JustifyRight()
  {
    this.webBrowser1.Document.ExecCommand(nameof (JustifyRight), false, (object) null);
  }

  /// <summary>
  /// Toggle the center justify property for the current block.
  /// </summary>
  public void JustifyCenter()
  {
    this.webBrowser1.Document.ExecCommand(nameof (JustifyCenter), false, (object) null);
  }

  /// <summary>
  /// Toggle the full justify property for the current block.
  /// </summary>
  public void JustifyFull()
  {
    this.webBrowser1.Document.ExecCommand(nameof (JustifyFull), false, (object) null);
  }

  /// <summary>Toggle bold formatting on the current selection.</summary>
  public void Bold() => this.webBrowser1.Document.ExecCommand(nameof (Bold), false, (object) null);

  /// <summary>Toggle italic formatting on the current selection.</summary>
  public void Italic()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Italic), false, (object) null);
  }

  /// <summary>Toggle underline formatting on the current selection.</summary>
  public void Underline()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Underline), false, (object) null);
  }

  /// <summary>Delete the current selection.</summary>
  public void Delete()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Delete), false, (object) null);
  }

  /// <summary>Indent the current paragraph.</summary>
  public void Indent()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Indent), false, (object) null);
  }

  /// <summary>Outdent the current paragraph.</summary>
  public void Outdent()
  {
    this.webBrowser1.Document.ExecCommand(nameof (Outdent), false, (object) null);
  }

  /// <summary>Insert a link at the current selection.</summary>
  /// <param name="url">The link url</param>
  public void InsertLink(string url)
  {
    this.webBrowser1.Document.ExecCommand("CreateLink", false, (object) url);
  }

  /// <summary>
  /// Get the ready state of the internal browser component.
  /// </summary>
  public ReadyStates ReadyState
  {
    get
    {
      switch (this.doc.readyState.ToLower())
      {
        case "uninitialized":
          return ReadyStates.Uninitialized;
        case "loading":
          return ReadyStates.Loading;
        case "loaded":
          return ReadyStates.Loaded;
        case "interactive":
          return ReadyStates.Interactive;
        case "complete":
          return ReadyStates.Complete;
        default:
          return ReadyStates.Uninitialized;
      }
    }
  }

  /// <summary>Get the current selection type.</summary>
  public SelectionTypes SelectionType
  {
    get
    {
      switch (this.doc.selection.type.ToLower())
      {
        case "text":
          return SelectionTypes.Text;
        case "control":
          return SelectionTypes.Control;
        case "none":
          return SelectionTypes.None;
        default:
          return SelectionTypes.None;
      }
    }
  }

  /// <summary>Get/Set the current font size.</summary>
  [Browsable(false)]
  public FontSizes FontSize
  {
    get
    {
      if (this.ReadyState != ReadyStates.Complete)
        return FontSizes.NA;
      switch (this.doc.queryCommandValue(nameof (FontSize)).ToString())
      {
        case "1":
          return FontSizes.One;
        case "2":
          return FontSizes.Two;
        case "3":
          return FontSizes.Three;
        case "4":
          return FontSizes.Four;
        case "5":
          return FontSizes.Five;
        case "6":
          return FontSizes.Six;
        case "7":
          return FontSizes.Seven;
        default:
          return FontSizes.NA;
      }
    }
    set
    {
      int num;
      switch (value)
      {
        case FontSizes.One:
          num = 1;
          break;
        case FontSizes.Two:
          num = 2;
          break;
        case FontSizes.Three:
          num = 3;
          break;
        case FontSizes.Four:
          num = 4;
          break;
        case FontSizes.Five:
          num = 5;
          break;
        case FontSizes.Six:
          num = 6;
          break;
        case FontSizes.Seven:
          num = 7;
          break;
        default:
          num = 7;
          break;
      }
      this.webBrowser1.Document.ExecCommand(nameof (FontSize), false, (object) num.ToString());
    }
  }

  /// <summary>Get/Set the current font name.</summary>
  [Browsable(false)]
  public FontFamily FontName
  {
    get
    {
      if (this.ReadyState != ReadyStates.Complete)
        return (FontFamily) null;
      return !(this.doc.queryCommandValue(nameof (FontName)) is string name) ? (FontFamily) null : new FontFamily(name);
    }
    set
    {
      if (value == null)
        return;
      this.webBrowser1.Document.ExecCommand(nameof (FontName), false, (object) value.Name);
    }
  }

  /// <summary>
  /// Get/Set the editor's foreground (text) color for the current selection.
  /// </summary>
  [Browsable(false)]
  public Color EditorForeColor
  {
    get
    {
      return this.ReadyState != ReadyStates.Complete ? Color.Black : TextEditor.ConvertToColor(this.doc.queryCommandValue("ForeColor").ToString());
    }
    set
    {
      this.webBrowser1.Document.ExecCommand("ForeColor", false, (object) $"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }
  }

  /// <summary>
  /// Get/Set the editor's background color for the current selection.
  /// </summary>
  [Browsable(false)]
  public Color EditorBackColor
  {
    get
    {
      return this.ReadyState != ReadyStates.Complete ? Color.White : TextEditor.ConvertToColor(this.doc.queryCommandValue("BackColor").ToString());
    }
    set
    {
      this.webBrowser1.Document.ExecCommand("BackColor", false, (object) $"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }
  }

  public void SelectBodyColor()
  {
    Color bodyBackgroundColor = this.BodyBackgroundColor;
    if (!this.ShowColorDialog(ref bodyBackgroundColor))
      return;
    this.BodyBackgroundColor = bodyBackgroundColor;
  }

  /// <summary>
  /// Initiate the foreground (text) color dialog for the current selection.
  /// </summary>
  public void SelectForeColor()
  {
    Color editorForeColor = this.EditorForeColor;
    if (!this.ShowColorDialog(ref editorForeColor))
      return;
    this.EditorForeColor = editorForeColor;
  }

  /// <summary>
  /// Initiate the background color dialog for the current selection.
  /// </summary>
  public void SelectBackColor()
  {
    Color editorBackColor = this.EditorBackColor;
    if (!this.ShowColorDialog(ref editorBackColor))
      return;
    this.EditorBackColor = editorBackColor;
  }

  /// <summary>
  /// Convert the custom integer (B G R) format to a color object.
  /// </summary>
  /// <param name="clrs">the custorm color as a string</param>
  /// <returns>the color</returns>
  private static Color ConvertToColor(string clrs)
  {
    int red;
    int green;
    int blue;
    if (clrs.StartsWith("#"))
    {
      int int32 = Convert.ToInt32(clrs.Substring(1), 16 /*0x10*/);
      red = int32 >> 16 /*0x10*/ & (int) byte.MaxValue;
      green = int32 >> 8 & (int) byte.MaxValue;
      blue = int32 & (int) byte.MaxValue;
    }
    else
    {
      int int32 = Convert.ToInt32(clrs);
      red = int32 & (int) byte.MaxValue;
      green = int32 >> 8 & (int) byte.MaxValue;
      blue = int32 >> 16 /*0x10*/ & (int) byte.MaxValue;
    }
    return Color.FromArgb(red, green, blue);
  }

  /// <summary>
  /// Called when the cut tool strip button on the editor context menu
  /// is clicked.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void cutToolStripButton_Click(object sender, EventArgs e) => this.Cut();

  /// <summary>
  /// Called when the paste tool strip button on the editor context menu
  /// is clicked.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void pasteToolStripButton_Click(object sender, EventArgs e) => this.Paste();

  /// <summary>
  /// Called when the copy tool strip button on the editor context menu
  /// is clicked.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void copyToolStripButton_Click(object sender, EventArgs e) => this.Copy();

  /// <summary>
  /// Called when the bold button on the tool strip is pressed.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void boldButton_Click(object sender, EventArgs e) => this.Bold();

  /// <summary>
  /// Called when the italic button on the tool strip is pressed.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void italicButton_Click(object sender, EventArgs e) => this.Italic();

  /// <summary>
  /// Called when the underline button on the tool strip is pressed.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void underlineButton_Click(object sender, EventArgs e) => this.Underline();

  /// <summary>
  /// Called when the foreground color button on the tool strip is pressed.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void colorButton_Click(object sender, EventArgs e) => this.SelectForeColor();

  /// <summary>
  /// Called when the background color button on the tool strip is pressed.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void backColorButton_Click(object sender, EventArgs e) => this.SelectBackColor();

  /// <summary>Show the interactive Color dialog.</summary>
  /// <param name="color">the input and output color</param>
  /// <returns>true if dialog accepted, false if dialog cancelled</returns>
  private bool ShowColorDialog(ref Color color)
  {
    bool flag;
    using (ColorDialog colorDialog = new ColorDialog())
    {
      colorDialog.SolidColorOnly = true;
      colorDialog.AllowFullOpen = false;
      colorDialog.AnyColor = false;
      colorDialog.FullOpen = false;
      colorDialog.CustomColors = (int[]) null;
      colorDialog.Color = color;
      if (colorDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
      {
        flag = true;
        color = colorDialog.Color;
      }
      else
        flag = false;
    }
    return flag;
  }

  /// <summary>
  /// Called when the outdent button on the toolstrip is clicked.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void outdentButton_Click(object sender, EventArgs e) => this.Outdent();

  /// <summary>
  /// Called when the indent button on the toolstrip is clicked.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void indentButton_Click(object sender, EventArgs e) => this.Indent();

  /// <summary>
  /// Called when the cut button is clicked on the editor context menu.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void cutToolStripMenuItem1_Click(object sender, EventArgs e) => this.Cut();

  /// <summary>
  /// Called when the copy button is clicked on the editor context menu.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void copyToolStripMenuItem2_Click(object sender, EventArgs e) => this.Copy();

  /// <summary>
  /// Called when the paste button is clicked on the editor context menu.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void pasteToolStripMenuItem3_Click(object sender, EventArgs e) => this.Paste();

  /// <summary>
  /// Called when the delete button is clicked on the editor context menu.
  /// </summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => this.Delete();

  /// <summary>
  /// Search the document from the current selection, and reset the
  /// the selection to the text found, if successful.
  /// </summary>
  /// <param name="text">the text for which to search</param>
  /// <param name="forward">true for forward search, false for backward</param>
  /// <param name="matchWholeWord">true to match whole word, false otherwise</param>
  /// <param name="matchCase">true to match case, false otherwise</param>
  /// <returns></returns>
  public bool Search(string text, bool forward, bool matchWholeWord, bool matchCase)
  {
    bool flag = false;
    if (this.webBrowser1.Document != (HtmlDocument) null)
    {
      mshtml.IHTMLDocument2 domDocument = this.webBrowser1.Document.DomDocument as mshtml.IHTMLDocument2;
      if (domDocument.body is IHTMLBodyElement body)
      {
        IHTMLTxtRange htmlTxtRange;
        if (domDocument.selection != null)
        {
          htmlTxtRange = domDocument.selection.createRange() as IHTMLTxtRange;
          IHTMLTxtRange range = htmlTxtRange.duplicate();
          range.collapse();
          if (htmlTxtRange.isEqual(range))
            htmlTxtRange = body.createTextRange();
          else if (forward)
            htmlTxtRange.moveStart("character");
          else
            htmlTxtRange.moveEnd("character", -1);
        }
        else
          htmlTxtRange = body.createTextRange();
        int Flags = 0;
        if (matchWholeWord)
          Flags += 2;
        if (matchCase)
          Flags += 4;
        flag = htmlTxtRange.findText(text, forward ? 999999 : -999999, Flags);
        if (flag)
        {
          htmlTxtRange.select();
          htmlTxtRange.scrollIntoView(!forward);
        }
      }
    }
    return flag;
  }

  /// <summary>Event handler for the ordered list toolbar button</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void orderedListButton_Click(object sender, EventArgs e) => this.OrderedList();

  /// <summary>Event handler for the unordered list toolbar button</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void unorderedListButton_Click(object sender, EventArgs e) => this.UnorderedList();

  /// <summary>Event handler for the left justify toolbar button.</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void justifyLeftButton_Click(object sender, EventArgs e) => this.JustifyLeft();

  /// <summary>Event handler for the center justify toolbar button.</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void justifyCenterButton_Click(object sender, EventArgs e) => this.JustifyCenter();

  /// <summary>Event handler for the right justify toolbar button.</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void justifyRightButton_Click(object sender, EventArgs e) => this.JustifyRight();

  /// <summary>Event handler for the full justify toolbar button.</summary>
  /// <param name="sender">the sender</param>
  /// <param name="e">EventArgs</param>
  private void justifyFullButton_Click(object sender, EventArgs e) => this.JustifyFull();

  private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.SelectBodyColor();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.toolStrip1 = new ToolStrip();
    this.fontComboBox = new ToolStripComboBox();
    this.fontSizeComboBox = new ToolStripComboBox();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.boldButton = new ToolStripButton();
    this.italicButton = new ToolStripButton();
    this.underlineButton = new ToolStripButton();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this.colorButton = new ToolStripButton();
    this.backColorButton = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.justifyLeftButton = new ToolStripButton();
    this.justifyCenterButton = new ToolStripButton();
    this.justifyRightButton = new ToolStripButton();
    this.justifyFullButton = new ToolStripButton();
    this.toolStripSeparator5 = new ToolStripSeparator();
    this.orderedListButton = new ToolStripButton();
    this.unorderedListButton = new ToolStripButton();
    this.outdentButton = new ToolStripButton();
    this.indentButton = new ToolStripButton();
    this.toolStripButton2 = new ToolStripButton();
    this.toolStripButton1 = new ToolStripButton();
    this.webBrowser1 = new WebBrowser();
    this.cutToolStripMenuItem = new ToolStripMenuItem();
    this.copyToolStripMenuItem1 = new ToolStripMenuItem();
    this.pasteToolStripMenuItem2 = new ToolStripMenuItem();
    this.copyToolStripMenuItem = new ToolStripMenuItem();
    this.pasteToolStripMenuItem = new ToolStripMenuItem();
    this.pasteToolStripMenuItem1 = new ToolStripMenuItem();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.cutToolStripMenuItem1 = new ToolStripMenuItem();
    this.copyToolStripMenuItem2 = new ToolStripMenuItem();
    this.pasteToolStripMenuItem3 = new ToolStripMenuItem();
    this.deleteToolStripMenuItem = new ToolStripMenuItem();
    this.backgroundColorToolStripMenuItem = new ToolStripMenuItem();
    this.cSSToolStripMenuItem = new ToolStripMenuItem();
    this.timer = new Timer(this.components);
    this.toolStrip1.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip1.Items.AddRange(new ToolStripItem[19]
    {
      (ToolStripItem) this.fontComboBox,
      (ToolStripItem) this.fontSizeComboBox,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.boldButton,
      (ToolStripItem) this.italicButton,
      (ToolStripItem) this.underlineButton,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this.colorButton,
      (ToolStripItem) this.backColorButton,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.justifyLeftButton,
      (ToolStripItem) this.justifyCenterButton,
      (ToolStripItem) this.justifyRightButton,
      (ToolStripItem) this.justifyFullButton,
      (ToolStripItem) this.toolStripSeparator5,
      (ToolStripItem) this.orderedListButton,
      (ToolStripItem) this.unorderedListButton,
      (ToolStripItem) this.outdentButton,
      (ToolStripItem) this.indentButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(627, 25);
    this.toolStrip1.TabIndex = 1;
    this.toolStrip1.Text = "toolStrip1";
    this.fontComboBox.Name = "fontComboBox";
    this.fontComboBox.Size = new Size(140, 25);
    this.fontComboBox.ToolTipText = "Font";
    this.fontComboBox.TextChanged += new EventHandler(this.fontComboBox_TextChanged);
    this.fontSizeComboBox.Name = "fontSizeComboBox";
    this.fontSizeComboBox.Size = new Size(75, 25);
    this.fontSizeComboBox.ToolTipText = "Font Size";
    this.fontSizeComboBox.TextChanged += new EventHandler(this.fontSizeComboBox_TextChanged);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this.boldButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.boldButton.Image = (System.Drawing.Image) Resources.bold;
    this.boldButton.ImageTransparentColor = Color.Magenta;
    this.boldButton.Name = "boldButton";
    this.boldButton.Size = new Size(23, 22);
    this.boldButton.Text = "toolStripButton1";
    this.boldButton.ToolTipText = "Bold";
    this.boldButton.Click += new EventHandler(this.boldButton_Click);
    this.italicButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.italicButton.Image = (System.Drawing.Image) Resources.italic;
    this.italicButton.ImageTransparentColor = Color.Magenta;
    this.italicButton.Name = "italicButton";
    this.italicButton.Size = new Size(23, 22);
    this.italicButton.Text = "toolStripButton2";
    this.italicButton.ToolTipText = "Italic";
    this.italicButton.Click += new EventHandler(this.italicButton_Click);
    this.underlineButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.underlineButton.Image = (System.Drawing.Image) Resources.underscore;
    this.underlineButton.ImageTransparentColor = Color.Magenta;
    this.underlineButton.Name = "underlineButton";
    this.underlineButton.Size = new Size(23, 22);
    this.underlineButton.Text = "toolStripButton3";
    this.underlineButton.ToolTipText = "Underline";
    this.underlineButton.Click += new EventHandler(this.underlineButton_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    this.toolStripSeparator4.Size = new Size(6, 25);
    this.colorButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.colorButton.Image = (System.Drawing.Image) Resources.color;
    this.colorButton.ImageTransparentColor = Color.Magenta;
    this.colorButton.Name = "colorButton";
    this.colorButton.Size = new Size(23, 22);
    this.colorButton.Text = "toolStripButton3";
    this.colorButton.ToolTipText = "Font Color";
    this.colorButton.Click += new EventHandler(this.colorButton_Click);
    this.backColorButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.backColorButton.Image = (System.Drawing.Image) Resources.backcolor;
    this.backColorButton.ImageTransparentColor = Color.Magenta;
    this.backColorButton.Name = "backColorButton";
    this.backColorButton.Size = new Size(23, 22);
    this.backColorButton.Text = "toolStripButton3";
    this.backColorButton.ToolTipText = "Back Color";
    this.backColorButton.Click += new EventHandler(this.backColorButton_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 25);
    this.justifyLeftButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.justifyLeftButton.Image = (System.Drawing.Image) Resources.lj;
    this.justifyLeftButton.ImageTransparentColor = Color.Magenta;
    this.justifyLeftButton.Name = "justifyLeftButton";
    this.justifyLeftButton.Size = new Size(23, 22);
    this.justifyLeftButton.Text = "toolStripButton3";
    this.justifyLeftButton.ToolTipText = "Justify Left";
    this.justifyLeftButton.Click += new EventHandler(this.justifyLeftButton_Click);
    this.justifyCenterButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.justifyCenterButton.Image = (System.Drawing.Image) Resources.cj;
    this.justifyCenterButton.ImageTransparentColor = Color.Magenta;
    this.justifyCenterButton.Name = "justifyCenterButton";
    this.justifyCenterButton.Size = new Size(23, 22);
    this.justifyCenterButton.Text = "toolStripButton4";
    this.justifyCenterButton.ToolTipText = "Justify Center";
    this.justifyCenterButton.Click += new EventHandler(this.justifyCenterButton_Click);
    this.justifyRightButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.justifyRightButton.Image = (System.Drawing.Image) Resources.rj;
    this.justifyRightButton.ImageTransparentColor = Color.Magenta;
    this.justifyRightButton.Name = "justifyRightButton";
    this.justifyRightButton.Size = new Size(23, 22);
    this.justifyRightButton.Text = "toolStripButton5";
    this.justifyRightButton.ToolTipText = "Justify Right";
    this.justifyRightButton.Click += new EventHandler(this.justifyRightButton_Click);
    this.justifyFullButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.justifyFullButton.Image = (System.Drawing.Image) Resources.fj;
    this.justifyFullButton.ImageTransparentColor = Color.Magenta;
    this.justifyFullButton.Name = "justifyFullButton";
    this.justifyFullButton.Size = new Size(23, 22);
    this.justifyFullButton.Text = "toolStripButton6";
    this.justifyFullButton.ToolTipText = "Justify Full";
    this.justifyFullButton.Click += new EventHandler(this.justifyFullButton_Click);
    this.toolStripSeparator5.Name = "toolStripSeparator5";
    this.toolStripSeparator5.Size = new Size(6, 25);
    this.orderedListButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.orderedListButton.Image = (System.Drawing.Image) Resources.ol;
    this.orderedListButton.ImageTransparentColor = Color.Magenta;
    this.orderedListButton.Name = "orderedListButton";
    this.orderedListButton.Size = new Size(23, 22);
    this.orderedListButton.Text = "toolStripButton3";
    this.orderedListButton.ToolTipText = "Ordered List";
    this.orderedListButton.Click += new EventHandler(this.orderedListButton_Click);
    this.unorderedListButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.unorderedListButton.Image = (System.Drawing.Image) Resources.uol;
    this.unorderedListButton.ImageTransparentColor = Color.Magenta;
    this.unorderedListButton.Name = "unorderedListButton";
    this.unorderedListButton.Size = new Size(23, 22);
    this.unorderedListButton.Text = "toolStripButton4";
    this.unorderedListButton.ToolTipText = "Unordered List";
    this.unorderedListButton.Click += new EventHandler(this.unorderedListButton_Click);
    this.outdentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.outdentButton.Image = (System.Drawing.Image) Resources.outdent;
    this.outdentButton.ImageTransparentColor = Color.Magenta;
    this.outdentButton.Name = "outdentButton";
    this.outdentButton.Size = new Size(23, 22);
    this.outdentButton.Text = "toolStripButton3";
    this.outdentButton.ToolTipText = "Outdent";
    this.outdentButton.Click += new EventHandler(this.outdentButton_Click);
    this.indentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.indentButton.Image = (System.Drawing.Image) Resources.indent;
    this.indentButton.ImageTransparentColor = Color.Magenta;
    this.indentButton.Name = "indentButton";
    this.indentButton.Size = new Size(23, 22);
    this.indentButton.Text = "toolStripButton4";
    this.indentButton.ToolTipText = "Indent";
    this.indentButton.Click += new EventHandler(this.indentButton_Click);
    this.toolStripButton2.Name = "toolStripButton2";
    this.toolStripButton2.Size = new Size(23, 23);
    this.toolStripButton1.Name = "toolStripButton1";
    this.toolStripButton1.Size = new Size(23, 23);
    this.webBrowser1.Dock = DockStyle.Fill;
    this.webBrowser1.Location = new Point(0, 25);
    this.webBrowser1.MinimumSize = new Size(20, 20);
    this.webBrowser1.Name = "webBrowser1";
    this.webBrowser1.Size = new Size(627, 125);
    this.webBrowser1.TabIndex = 2;
    this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
    this.cutToolStripMenuItem.Size = new Size(32 /*0x20*/, 19);
    this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
    this.copyToolStripMenuItem1.Size = new Size(32 /*0x20*/, 19);
    this.pasteToolStripMenuItem2.Name = "pasteToolStripMenuItem2";
    this.pasteToolStripMenuItem2.Size = new Size(32 /*0x20*/, 19);
    this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
    this.copyToolStripMenuItem.Size = new Size(32 /*0x20*/, 19);
    this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
    this.pasteToolStripMenuItem.Size = new Size(32 /*0x20*/, 19);
    this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
    this.pasteToolStripMenuItem1.Size = new Size(32 /*0x20*/, 19);
    this.pasteToolStripMenuItem1.Text = "Paste";
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.cutToolStripMenuItem1,
      (ToolStripItem) this.copyToolStripMenuItem2,
      (ToolStripItem) this.pasteToolStripMenuItem3,
      (ToolStripItem) this.deleteToolStripMenuItem,
      (ToolStripItem) this.backgroundColorToolStripMenuItem,
      (ToolStripItem) this.cSSToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(171, 136);
    this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
    this.cutToolStripMenuItem1.Size = new Size(170, 22);
    this.cutToolStripMenuItem1.Text = "Cut";
    this.cutToolStripMenuItem1.Click += new EventHandler(this.cutToolStripMenuItem1_Click);
    this.copyToolStripMenuItem2.Name = "copyToolStripMenuItem2";
    this.copyToolStripMenuItem2.Size = new Size(170, 22);
    this.copyToolStripMenuItem2.Text = "Copy";
    this.copyToolStripMenuItem2.Click += new EventHandler(this.copyToolStripMenuItem2_Click);
    this.pasteToolStripMenuItem3.Name = "pasteToolStripMenuItem3";
    this.pasteToolStripMenuItem3.Size = new Size(170, 22);
    this.pasteToolStripMenuItem3.Text = "Paste";
    this.pasteToolStripMenuItem3.Click += new EventHandler(this.pasteToolStripMenuItem3_Click);
    this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
    this.deleteToolStripMenuItem.Size = new Size(170, 22);
    this.deleteToolStripMenuItem.Text = "Delete";
    this.deleteToolStripMenuItem.Click += new EventHandler(this.deleteToolStripMenuItem_Click);
    this.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
    this.backgroundColorToolStripMenuItem.Size = new Size(170, 22);
    this.backgroundColorToolStripMenuItem.Text = "Background Color";
    this.backgroundColorToolStripMenuItem.Click += new EventHandler(this.backgroundColorToolStripMenuItem_Click);
    this.cSSToolStripMenuItem.Name = "cSSToolStripMenuItem";
    this.cSSToolStripMenuItem.Size = new Size(170, 22);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.webBrowser1);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (TextEditor);
    this.Size = new Size(627, 150);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public delegate void TickDelegate();

  public class EnterKeyEventArgs : EventArgs
  {
    private bool _cancel;

    public bool Cancel
    {
      get => this._cancel;
      set => this._cancel = value;
    }
  }
}
