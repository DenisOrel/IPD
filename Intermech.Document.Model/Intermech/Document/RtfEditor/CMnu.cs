// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CMnu
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CMnu : COp
{
  internal CMnu(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal void AddMenuItem(
    ContextMenu parent,
    string text,
    int CmdId,
    bool ShowShortcut,
    Shortcut shortcut)
  {
    OurMenuItem ourMenuItem = new OurMenuItem();
    ourMenuItem.Text = text;
    ourMenuItem.Click += new EventHandler(this.MenuClick);
    ourMenuItem.CmdId = CmdId;
    ourMenuItem.ShowShortcut = ShowShortcut;
    if (shortcut != Shortcut.None)
      ourMenuItem.Shortcut = shortcut;
    parent?.MenuItems.Add((MenuItem) ourMenuItem);
  }

  internal void AddMenuItem(
    MenuItem parent,
    string text,
    int CmdId,
    bool ShowShortcut,
    Shortcut shortcut)
  {
    OurMenuItem ourMenuItem = new OurMenuItem();
    ourMenuItem.Text = text;
    ourMenuItem.Click += new EventHandler(this.MenuClick);
    ourMenuItem.CmdId = CmdId;
    ourMenuItem.ShowShortcut = ShowShortcut;
    if (shortcut != Shortcut.None)
      ourMenuItem.Shortcut = shortcut;
    parent?.MenuItems.Add((MenuItem) ourMenuItem);
  }

  internal void AddSeparator(ContextMenu parent)
  {
    MenuItem menuItem = new MenuItem("-");
    parent?.MenuItems.Add(menuItem);
  }

  internal void AddSeparator(MenuItem parent)
  {
    MenuItem menuItem = new MenuItem("-");
    parent?.MenuItems.Add(menuItem);
  }

  internal OurMenuItem AddSubMenu(MenuItem parent, string text)
  {
    OurMenuItem ourMenuItem = new OurMenuItem();
    ourMenuItem.Text = text;
    ourMenuItem.CmdId = 0;
    parent?.MenuItems.Add((MenuItem) ourMenuItem);
    return ourMenuItem;
  }

  internal OurMenuItem AddTopMenu(MainMenu parent, string text)
  {
    OurMenuItem ourMenuItem = new OurMenuItem();
    ourMenuItem.Popup += new EventHandler(this.MenuPopup);
    ourMenuItem.Text = text;
    parent.MenuItems.Add((MenuItem) ourMenuItem);
    return ourMenuItem;
  }

  internal OurMenuItem AddTopMenuItem(MainMenu parent, string text, int CmdId)
  {
    OurMenuItem ourMenuItem = new OurMenuItem();
    ourMenuItem.Click += new EventHandler(this.MenuClick);
    ourMenuItem.Text = text;
    ourMenuItem.CmdId = CmdId;
    parent.MenuItems.Add((MenuItem) ourMenuItem);
    return ourMenuItem;
  }

  internal MainMenu BuildMenu()
  {
    MainMenu parent1 = new MainMenu();
    OurMenuItem parent2 = this.AddTopMenu(parent1, "&File");
    this.AddMenuItem((MenuItem) parent2, "&New", 626, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "&Open...", 627, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "&Save...\tF3", 640, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "Save&As...\tShift+F3", 641, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent2);
    this.AddMenuItem((MenuItem) parent2, "Page &Layout...", 644, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "Printer &Setup...\tShift+F4", 645, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "Print...\tF4", 643, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent2, "Print Preview", 717, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent2);
    this.AddMenuItem((MenuItem) parent2, "E&xit\tCtrl+F3", 642, false, Shortcut.None);
    OurMenuItem parent3 = this.AddTopMenu(parent1, "&Edit");
    this.AddMenuItem((MenuItem) parent3, "Cut\tCtrl+X", 628, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "Copy\tCtrl+C", 629, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "Paste\tCtrl+V", 630, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "Paste Special...", 631, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent3);
    this.AddMenuItem((MenuItem) parent3, "Edit Picture...", 685, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "&Undo\tCtrl+Z", 638, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "&Redo\tCtrl+Y", 747, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent3);
    this.AddMenuItem((MenuItem) parent3, "Select All\tCtrl+A", 625, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent3);
    this.AddMenuItem((MenuItem) parent3, "&Repaginate", 671, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "&Edit Section...", 673, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "Edit Style...", 730, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent3, "Edit Input Field...", 769, false, Shortcut.None);
    OurMenuItem parent4 = this.AddSubMenu((MenuItem) parent3, "&Edit Frame/Drawing Object");
    this.AddMenuItem((MenuItem) parent4, "Edit Drawing Object...", 736, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent4, "Vertical Base Position...", 735, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent4, "Rotate Text...", 789, false, Shortcut.None);
    OurMenuItem parent5 = this.AddSubMenu((MenuItem) parent3, "&List and Overrides");
    this.AddMenuItem((MenuItem) parent5, "Create List Item...", 779, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent5, "Edit List Item...", 780, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent5);
    this.AddMenuItem((MenuItem) parent5, "Create List Override...", 781, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent5, "Edit List Override...", 782, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent5);
    this.AddMenuItem((MenuItem) parent5, "Edit List Level...", 783, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent3);
    this.AddMenuItem((MenuItem) parent3, "Edit Page Header/Footer", 677, false, Shortcut.None);
    OurMenuItem parent6 = this.AddSubMenu((MenuItem) parent3, "First Page Header/Footer");
    this.AddMenuItem((MenuItem) parent6, "Create First Page Header", 754, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent6, "Create First Page Footer", 755, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent6);
    this.AddMenuItem((MenuItem) parent6, "Delete First Page Header", 756, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent6, "Delete First Page Footer", 757, false, Shortcut.None);
    OurMenuItem parent7 = this.AddSubMenu((MenuItem) parent3, "Edit Footnote/Endnote");
    this.AddMenuItem((MenuItem) parent7, "Edit Footnote Text", 722, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent7, "Edit Endnote Text", 778, false, Shortcut.None);
    OurMenuItem parent8 = this.AddSubMenu((MenuItem) parent3, "Track Changes");
    this.AddMenuItem((MenuItem) parent8, "Enable Tracking", 795, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent8, "Find Next Change\tCtrl+N", 804, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent8, "Find Prev Change\tCtrl+P", 805, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent8);
    this.AddMenuItem((MenuItem) parent8, "Accept Change", 806, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent8, "Accept All Changes...", 807, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent3);
    this.AddMenuItem((MenuItem) parent3, "Document Text Flow...", 793, false, Shortcut.None);
    OurMenuItem parent9 = this.AddTopMenu(parent1, "&View");
    this.AddMenuItem((MenuItem) parent9, "&Ruler", 680, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "&Tool Bar", 681, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "&Status Ribbon", 682, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "&Paragraph &Marker", 692, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent9);
    this.AddMenuItem((MenuItem) parent9, "&Hidden Text", 686, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "&Field Names", 758, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "Hyper&link Cursor", 710, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent9);
    this.AddMenuItem((MenuItem) parent9, "Page Header/&Footer", 676, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent9, "Page Border", 744, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent9);
    this.AddMenuItem((MenuItem) parent9, "Zoom...", 733, false, Shortcut.None);
    OurMenuItem parent10 = this.AddTopMenu(parent1, "&Insert");
    OurMenuItem parent11 = this.AddSubMenu((MenuItem) parent10, "&Insert Break");
    this.AddMenuItem((MenuItem) parent11, "&Page Break\tCtrl+Enter", 670, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent11, "&Section Break", 672, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent11, "&Column Break", 675, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent10);
    this.AddMenuItem((MenuItem) parent10, "Embedd Picture...", 632, true, Shortcut.AltF8);
    this.AddMenuItem((MenuItem) parent10, "Link Picture...", 738, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent10);
    this.AddMenuItem((MenuItem) parent10, "Frame", 718, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Drawing Objects...", 726, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent10);
    this.AddMenuItem((MenuItem) parent10, "Page Number", 719, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Page Count", 752, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Date and Time...", 770, false, Shortcut.None);
    OurMenuItem parent12 = this.AddSubMenu((MenuItem) parent10, "Footnote/Endnote");
    this.AddMenuItem((MenuItem) parent12, "Footnote...", 721, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent12, "Endnote...", 777, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Bookmark...", 753, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Table of Contents", 771, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent10);
    this.AddMenuItem((MenuItem) parent10, "Data Field...", 766, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Text Input Field...", 767 /*0x02FF*/, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Checkbox Field...", 768 /*0x0300*/, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Hyperlink...", 794, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent10);
    this.AddMenuItem((MenuItem) parent10, "Non-breaking Space", 740, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Non-breaking Dash", 746, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent10, "Optional Hyphen", 759, false, Shortcut.None);
    OurMenuItem parent13 = this.AddTopMenu(parent1, "&Font");
    this.AddMenuItem((MenuItem) parent13, "Normal", 647, false, Shortcut.Alt0);
    this.AddSeparator((MenuItem) parent13);
    this.AddMenuItem((MenuItem) parent13, "Bold", 648, true, Shortcut.CtrlB);
    this.AddMenuItem((MenuItem) parent13, "Underline", 649, true, Shortcut.CtrlU);
    this.AddMenuItem((MenuItem) parent13, "Double Underline", 688, true, Shortcut.CtrlD);
    this.AddMenuItem((MenuItem) parent13, "Italic", 650, true, Shortcut.CtrlI);
    this.AddMenuItem((MenuItem) parent13, "Superscript", 652, true, Shortcut.Alt4);
    this.AddMenuItem((MenuItem) parent13, "Subscript", 653, true, Shortcut.Alt5);
    this.AddMenuItem((MenuItem) parent13, "Strike", 651, true, Shortcut.Alt6);
    this.AddMenuItem((MenuItem) parent13, "Double Strike", 832, true, Shortcut.Alt7);
    this.AddMenuItem((MenuItem) parent13, "All Caps", 774, true, Shortcut.None);
    this.AddMenuItem((MenuItem) parent13, "Small Caps", 775, true, Shortcut.None);
    this.AddSeparator((MenuItem) parent13);
    this.AddMenuItem((MenuItem) parent13, "Fonts...", 655, true, Shortcut.AltF10);
    this.AddMenuItem((MenuItem) parent13, "Style...", 731, true, Shortcut.None);
    this.AddSeparator((MenuItem) parent13);
    this.AddMenuItem((MenuItem) parent13, "Text Color...", 654, true, Shortcut.None);
    this.AddMenuItem((MenuItem) parent13, "Background Color...", 711, true, Shortcut.None);
    this.AddMenuItem((MenuItem) parent13, "Underline Color...", 796, true, Shortcut.None);
    this.AddSeparator((MenuItem) parent13);
    this.AddMenuItem((MenuItem) parent13, "Spacing...", 760, true, Shortcut.None);
    this.AddMenuItem((MenuItem) parent13, "Hidden", 687, true, Shortcut.CtrlH);
    this.AddMenuItem((MenuItem) parent13, "Boxed", 725, true, Shortcut.None);
    this.AddMenuItem((MenuItem) parent13, "Protect", 689, true, Shortcut.None);
    OurMenuItem parent14 = this.AddTopMenu(parent1, "&Paragraph");
    this.AddMenuItem((MenuItem) parent14, "Normal", 656, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Center", 657, true, Shortcut.Alt8);
    this.AddMenuItem((MenuItem) parent14, "Right Justify", 658, true, Shortcut.Alt9);
    this.AddMenuItem((MenuItem) parent14, "Justify Both", 663, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Double Space", 661, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Indent Left", 659, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Indent Right", 660, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Handing Indent", 662, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Keep Together", 723, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Keep with Next", 724, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Widow/Orphan Control", 743, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Page Break Before", 773, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Border and Shading...", 691, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Paragraph Spacing...", 720, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Background Color...", 749, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Bullet", 729, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Numbering", 748, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Increase Level", 797, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Decrease Level", 798, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "List Numbering...", 784, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Set Tab...", 734, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Clear Tab...", 666, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Clear All Tabs", 667, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent14);
    this.AddMenuItem((MenuItem) parent14, "Style...", 732, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent14, "Text Flow...", 790, false, Shortcut.None);
    OurMenuItem parent15 = this.AddTopMenu(parent1, "&Table");
    this.AddMenuItem((MenuItem) parent15, "Insert &Table...", 694, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent15);
    this.AddMenuItem((MenuItem) parent15, "&Insert Row", 695, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Insert Column", 704, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "&Merge Cells", 697, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "&Split Cells", 696, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "&Delete Cells...", 698, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent15);
    this.AddMenuItem((MenuItem) parent15, "&Row Position...", 700, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Row &Height...", 703, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "&Header Row", 762, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "&Keep Row Together", 764, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Row Text &Flow...", 791, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent15);
    this.AddMenuItem((MenuItem) parent15, "Cell &Width...", 788, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell &Border Width...", 701, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell &Border Color...", 786, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell S&hading...", 702, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell &Color...", 765, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell Vertical Align...", 750, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Cell Rotate Text...", 803, false, Shortcut.None);
    this.AddSeparator((MenuItem) parent15);
    this.AddMenuItem((MenuItem) parent15, "Select Current Column", 751, false, Shortcut.None);
    this.AddMenuItem((MenuItem) parent15, "Show &Gridlines", 699, false, Shortcut.None);
    OurMenuItem parent16 = this.AddTopMenu(parent1, "&Other");
    this.AddMenuItem((MenuItem) parent16, "&Search...\tF5", 633, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Search &Forward\tCtrl+F", 634, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Search &Backward\tCtrl+Shift+F", 635, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "&Replace...\tF6", 636, false, Shortcut.Alt0);
    this.AddSeparator((MenuItem) parent16);
    this.AddMenuItem((MenuItem) parent16, "&Jump...\tF10", 646, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "&Protection Lock", 690, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Protec Form", 801, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Snap to &Grid", 728, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Background Picture...", 737, false, Shortcut.Alt0);
    this.AddMenuItem((MenuItem) parent16, "Watermark Picture...", 802, false, Shortcut.Alt0);
    if (!tc.StSearched)
      this.SearchSpellTime();
    if (tc.hSpell != (Assembly) null)
    {
      this.AddSeparator((MenuItem) parent16);
      this.AddMenuItem((MenuItem) parent16, "Spell Check", 741, false, Shortcut.Alt0);
      this.AddMenuItem((MenuItem) parent16, "Auto Spell", 776, false, Shortcut.Alt0);
    }
    this.AddTopMenuItem(parent1, "&Help", 637);
    return parent1;
  }

  internal void MenuClick(object sender, EventArgs ev)
  {
    int cmdId = ((OurMenuItem) sender).CmdId;
    if (cmdId == 0)
      return;
    this.e.TerCommand(cmdId);
  }

  internal void MenuPopup(object sender, EventArgs ev)
  {
    foreach (MenuItem menuItem in ((Menu) sender).MenuItems)
    {
      if (!(menuItem.Text == "-"))
      {
        OurMenuItem sender1 = (OurMenuItem) menuItem;
        if (sender1.MenuItems.Count > 0)
        {
          this.MenuPopup((object) sender1, ev);
        }
        else
        {
          int cmdId = sender1.CmdId;
          if (cmdId < 1000)
          {
            sender1.Enabled = this.TerMenuEnable2(cmdId);
            sender1.Checked = this.TerMenuSelect2(cmdId);
          }
        }
      }
    }
  }

  internal new bool ProcessCommand(int CmdId)
  {
    if (this.e.WindowBeingCreated || this.e.HoldMessages || this.e.WaitForOle || !this.e.TerArg.open)
      return true;
    this.e.MessageId = 273;
    this.e.CommandId = CmdId;
    if (this.e.InPrintPreview && CmdId != 600 && CmdId != 601 && CmdId != 717)
      return true;
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    if (this.e.CurLine >= 0 && this.e.CurLine < this.e.TotalLines && (this.LineTextAngle(this.e.CurLine) != 0 || (this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0))
      CmdId = this.e.CommandId = this.XlateCommandId(CmdId);
    if (this.SendPreprocessMessage(273, CmdId, 0))
    {
      if (curLine != this.e.CurLine || curCol != this.e.CurCol)
        this.e.OnCursorPosChanged();
      return true;
    }
    bool flag = !this.e.TerArg.ReadOnly;
    tc.arg_list terArg = this.e.TerArg;
    this.e.CaretPositioned = false;
    this.e.InAutoComp = false;
    if ((this.e.TerFlags4 & 1024 /*0x0400*/) != 0)
      this.e.DocHeight = this.GetDocHeight();
    this.e.SendActionMsg = true;
    if (CmdId >= 2500 && CmdId <= 2511)
      this.DoPopupSelection(CmdId);
    else if (CmdId < 1000 || CmdId > 1100)
    {
      if (this.e.TerHelpWanted)
      {
        Help.ShowHelp((Control) this.e, this.e.TerHelpFile);
        this.e.TerHelpWanted = false;
      }
      else if (this.e.HilightType != 0 && (this.e.StretchHilight || this.e.TblSelCursShowing) && !this.e.IgnoreMouseMove && CmdId != 606)
      {
        this.MessageBeep(0);
      }
      else
      {
        if (!this.e.CaretEnabled && this.UseCaret())
          this.InitCaret();
        if (!this.e.CaretEngaged && !this.e.InPrintPreview)
          this.EngageCaret(CmdId);
        if (this.e.WheelShowing)
          this.ResetWheel();
        switch (CmdId)
        {
          case 600:
            this.TerPageUp(true);
            break;
          case 601:
            this.TerPageDn(true);
            break;
          case 602:
            this.TerUp();
            break;
          case 603:
            this.TerDown();
            break;
          case 604:
            this.TerLeft();
            break;
          case 605:
            this.TerRight();
            break;
          case 606:
            int textTag = this.e.GetTextTag(this.e.CurLine, this.e.CurCol, (IList<int>) tc.ReplacedCharTags);
            if (textTag != -1 | flag || this.EditingInputField(true, false))
            {
              this.edit.TerDel(textTag != -1 && (this.e.CharTag[textTag].AuxText == null || this.e.CharTag[textTag].AuxText == ""));
              break;
            }
            break;
          case 607:
            if (flag || this.EditingInputField(false, true))
            {
              this.TerBackSpace();
              break;
            }
            break;
          case 608:
          case 614:
            this.TerInsertTab();
            break;
          case 609:
            this.TerBackTab();
            break;
          case 610:
            this.TerBeginLine();
            break;
          case 611:
            this.TerEndLine();
            break;
          case 612:
            this.TerBeginFile();
            break;
          case 613:
            this.TerEndFile();
            break;
          case 620:
            this.TerNextWord();
            break;
          case 621:
            this.TerPrevWord(true);
            break;
          case 625:
            this.e.HilightType = 2;
            this.e.HilightBegRow = this.e.HilightBegCol = 0;
            this.e.HilightEndRow = this.e.TotalLines - 1;
            this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
            this.e.StretchHilight = false;
            this.PaintTer();
            break;
          case 626:
            if (flag)
            {
              this.TerNew("");
              break;
            }
            break;
          case 627:
            this.TerOpen();
            break;
          case 628:
            if (flag || this.EditingInputField(true, false))
            {
              this.CopyToClipboard(CmdId, true);
              break;
            }
            break;
          case 629:
            this.CopyToClipboard(CmdId, true);
            break;
          case 630:
            if (flag || this.EditingInputField(true, false))
            {
              this.CopyFromClipboard("", (DataObject) null);
              break;
            }
            break;
          case 631:
            if (flag)
            {
              this.TerPasteSpecial();
              break;
            }
            break;
          case 632:
            if (flag)
            {
              this.e.TerInsertPictureFile((string) null, true, 0, true);
              break;
            }
            break;
          case 633:
            this.TerSearchString();
            break;
          case 634:
            this.TerSearchForward();
            break;
          case 635:
            this.TerSearchBackward();
            break;
          case 636:
            if (flag)
            {
              this.TerReplaceString();
              break;
            }
            break;
          case 637:
            try
            {
              Help.ShowHelp((Control) this.e, "ter_hlp.hlp");
              break;
            }
            catch (Exception ex)
            {
              this.PrintError(220, "Error");
              break;
            }
          case 638:
            if (flag)
            {
              this.TerUndo(true);
              break;
            }
            break;
          case 639:
            if (flag)
            {
              this.TerInsert();
              break;
            }
            break;
          case 640:
            if (flag || this.e.ProtectForm)
            {
              if (this.e.DocName.Length > 0)
              {
                this.TerSave(this.e.DocName, true);
                break;
              }
              this.TerSaveAs(this.e.DocName);
              break;
            }
            break;
          case 641:
            this.TerSaveAs(this.e.DocName);
            break;
          case 642:
            if (this.e.TerQueryExit())
              this.e.Parent.Dispose();
            return true;
          case 643:
            this.e.TerPrint(true);
            break;
          case 644:
            this.TerPageOptions();
            break;
          case 645:
            this.TerPrintOptions();
            break;
          case 646:
            this.TerJump();
            break;
          case 647:
            if (flag)
            {
              int FmtType = 319;
              if (this.e.ShowHiddenText)
                FmtType |= 64 /*0x40*/;
              if (!this.e.ProtectionLock)
                FmtType |= 512 /*0x0200*/;
              this.e.SetTerCharStyle(FmtType, false, true);
              break;
            }
            break;
          case 651:
            if (flag)
            {
              this.e.SetTerCharStyle(8, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 652:
            if (flag)
            {
              this.e.SetTerCharStyle(16 /*0x10*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 653:
            if (flag)
            {
              this.e.SetTerCharStyle(32 /*0x20*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 654:
            if (flag)
            {
              this.TerColors(true);
              break;
            }
            break;
          case 655:
            if (flag)
            {
              this.TerFonts();
              break;
            }
            break;
          case 656:
            if (flag)
            {
              this.e.ParaNormal(true);
              break;
            }
            break;
          case 657:
            if (flag)
            {
              this.e.SetTerParaFmt(1, this.TerMenuSelect(657) == 0, true);
              break;
            }
            break;
          case 658:
            if (flag)
            {
              this.e.SetTerParaFmt(2, this.TerMenuSelect(658) == 0, true);
              break;
            }
            break;
          case 659:
            if (flag)
            {
              this.e.ParaLeftIndent(true, true);
              break;
            }
            break;
          case 660:
            if (flag)
            {
              this.e.ParaRightIndent(true, true);
              break;
            }
            break;
          case 661:
            if (flag)
            {
              this.e.SetTerParaFmt(4, this.TerMenuSelect(661) == 0, true);
              break;
            }
            break;
          case 662:
            if (flag)
            {
              this.e.ParaHangingIndent(true, true);
              break;
            }
            break;
          case 663:
            if (flag)
            {
              this.e.SetTerParaFmt(2048 /*0x0800*/, this.TerMenuSelect(663) == 0, true);
              break;
            }
            break;
          case 666:
            if (flag)
            {
              this.ClearTabDlg();
              break;
            }
            break;
          case 667:
            if (flag)
            {
              this.e.ClearAllTabs(true);
              break;
            }
            break;
          case 670:
            if (flag)
            {
              this.e.TerPageBreak(true);
              break;
            }
            break;
          case 671:
            this.Repaginate(false, false, 0, true);
            this.PaintTer();
            break;
          case 672:
            if (flag)
            {
              this.e.TerSectBreak(true);
              break;
            }
            break;
          case 673:
            this.e.TerSetSect(0, 0, true);
            break;
          case 674:
            this.TogglePageMode();
            break;
          case 675:
            if (flag)
            {
              this.e.TerColBreak(true);
              break;
            }
            break;
          case 676:
            this.ToggleViewHdrFtr();
            break;
          case 677:
            this.ToggleEditHdrFtr();
            break;
          case 680:
            this.ToggleRuler();
            break;
          case 681:
            this.ToggleToolBar();
            break;
          case 682:
            this.ToggleStatusRibbon();
            break;
          case 685:
            if (flag)
            {
              this.EditPicture();
              break;
            }
            break;
          case 686:
            this.ToggleHiddenText();
            break;
          case 687:
            if (flag)
            {
              this.e.SetTerCharStyle(64 /*0x40*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 688:
            if (flag)
            {
              this.e.SetTerCharStyle(256 /*0x0100*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 689:
            if (flag)
            {
              this.e.SetTerCharStyle(512 /*0x0200*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 690:
            if (flag)
            {
              this.e.ProtectionLock = !this.e.ProtectionLock;
              break;
            }
            break;
          case 691:
            if (flag)
            {
              this.TerParaBorder();
              break;
            }
            break;
          case 692:
            this.ToggleParaMark();
            break;
          case 694:
            if (flag)
            {
              this.e.TerCreateTable(-1, -1, true);
              break;
            }
            break;
          case 695:
            if (flag)
            {
              this.e.TerInsertTableRow(true, true);
              break;
            }
            break;
          case 696:
            if (flag)
            {
              this.TerSplitCell();
              break;
            }
            break;
          case 697:
            if (flag)
            {
              this.TerMergeCells();
              break;
            }
            break;
          case 698:
            if (flag)
            {
              this.e.TerDeleteCells(0, true);
              break;
            }
            break;
          case 699:
            this.TerToggleTableGrid();
            break;
          case 700:
            if (flag)
            {
              this.e.TerRowPosition(0, true, true);
              break;
            }
            break;
          case 701:
            if (flag)
            {
              this.e.TerCellBorder(0, 0, 0, 0, 0, true);
              break;
            }
            break;
          case 702:
            if (flag)
            {
              this.e.TerCellShading(0, 0, true);
              break;
            }
            break;
          case 703:
            if (flag)
            {
              this.e.TerRowHeight(-1, true, true);
              break;
            }
            break;
          case 704:
            if (flag)
            {
              this.e.TerInsertTableCol(true, true, true);
              break;
            }
            break;
          case 710:
            this.e.ShowHyperlinkCursor = !this.e.ShowHyperlinkCursor;
            break;
          case 711:
            if (flag)
            {
              this.TerColors(false);
              break;
            }
            break;
          case 715:
            this.TerCtrlUp();
            break;
          case 716:
            this.TerCtrlDown();
            break;
          case 717:
            this.TerPrintPreviewMode(this.e.ShowPvToolbar);
            this.SendActionMessage(273, CmdId, 0);
            return true;
          case 718:
            if (flag)
            {
              this.e.TerInsertParaFrame(-1, -1, -1, -1, true);
              break;
            }
            break;
          case 719:
            if (flag)
            {
              this.InsertDynField(1, (string) null);
              break;
            }
            break;
          case 720:
            if (flag)
            {
              this.TerParaSpacing();
              break;
            }
            break;
          case 721:
            if (flag)
            {
              this.e.TerInsertFootnote((string) null, (string) null, 0, true);
              break;
            }
            break;
          case 722:
            this.ToggleFootnoteEdit(true);
            break;
          case 723:
            if (flag)
            {
              this.e.SetTerParaFmt(16384 /*0x4000*/, this.TerMenuSelect(723) == 0, true);
              break;
            }
            break;
          case 724:
            if (flag)
            {
              this.e.SetTerParaFmt(32768 /*0x8000*/, this.TerMenuSelect(724) == 0, true);
              break;
            }
            break;
          case 725:
            if (flag)
            {
              this.e.SetTerCharStyle(8192 /*0x2000*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 726:
            if (flag)
            {
              this.e.TerInsertDrawObject(-1, -1, -1, -1, -1);
              break;
            }
            break;
          case 728:
            if (flag)
            {
              this.e.SnapToGrid = !this.e.SnapToGrid;
              break;
            }
            break;
          case 729:
            if (flag)
            {
              if (this.TerMenuSelect2(748))
                this.e.TerSetListBullet(false, 0, -1, 1, "", ".", false);
              this.e.TerSetListBullet(this.TerMenuSelect(729) == 0, 23, -1, 1, "", "", true);
              break;
            }
            break;
          case 730:
            if (flag)
            {
              this.e.EditStyle(true, (string) null, true, 0, true);
              break;
            }
            break;
          case 731:
            if (flag)
            {
              this.e.TerSelectCharStyle(-1, true);
              break;
            }
            break;
          case 732:
            if (flag)
            {
              this.e.TerSelectParaStyle(-1, true);
              break;
            }
            break;
          case 733:
            this.e.TerSetZoom(-1);
            break;
          case 734:
            if (flag)
            {
              this.e.SetTab(0, -1, true);
              break;
            }
            break;
          case 735:
            if (flag)
            {
              this.e.TerSetFrameYBase(-1, 0);
              break;
            }
            break;
          case 736:
            if (flag)
            {
              this.e.TerSetObjectAttrib(-1, 0, 0, Color.Black, true, Color.Black);
              break;
            }
            break;
          case 737:
            if (this.e.BkPictId != 0)
            {
              this.e.TerSetBkPictId(0, 0, true);
              break;
            }
            this.e.TerSetBkPictId(-1, 1, true);
            break;
          case 738:
            if (flag)
            {
              this.e.TerInsertPictureFile((string) null, false, 0, true);
              break;
            }
            break;
          case 739:
            this.ToggleFittedView();
            break;
          case 740:
            if (flag)
            {
              this.TerAscii('\u000E');
              break;
            }
            break;
          case 741:
            this.e.TerSpellCheck(false, true);
            break;
          case 743:
            if (flag)
            {
              this.e.TerSetPflags(32 /*0x20*/, this.TerMenuSelect(743) == 0, true);
              break;
            }
            break;
          case 744:
            this.TogglePageBorder();
            break;
          case 745:
            if (flag)
            {
              this.TerDelPrevWord();
              break;
            }
            break;
          case 746:
            if (flag)
            {
              this.TerAscii('\u0017');
              break;
            }
            break;
          case 747:
            if (flag)
            {
              this.TerUndo(false);
              break;
            }
            break;
          case 748:
            if (flag)
            {
              if (this.TerMenuSelect2(729))
                this.e.TerSetListBullet(false, 23, -1, 1, "", "", false);
              this.e.TerSetListBullet(this.TerMenuSelect(748) == 0, 0, -1, 1, "", ".", true);
              break;
            }
            break;
          case 749:
            if (flag)
            {
              this.e.TerSetParaBkColor(true, Color.White, true);
              break;
            }
            break;
          case 750:
            if (flag)
            {
              this.e.TerCellVertAlign(0, 0, true);
              break;
            }
            break;
          case 751:
            if (flag)
            {
              this.e.TerSelectCol(true);
              break;
            }
            break;
          case 752:
            if (flag)
            {
              this.InsertDynField(5, (string) null);
              break;
            }
            break;
          case 753:
            if (flag)
            {
              this.InsertBookmark();
              break;
            }
            break;
          case 754:
            this.e.TerCreateFirstHdrFtr(true);
            break;
          case 755:
            this.e.TerCreateFirstHdrFtr(false);
            break;
          case 756:
            this.e.TerDeleteFirstHdrFtr(true, true);
            break;
          case 757:
            this.e.TerDeleteFirstHdrFtr(false, true);
            break;
          case 758:
            this.ToggleFieldNames();
            break;
          case 759:
            if (flag)
            {
              this.TerAscii('\u0006');
              break;
            }
            break;
          case 760:
            if (flag)
            {
              this.e.TerSetCharSpace(true, 0, true);
              break;
            }
            break;
          case 761:
            if (flag)
            {
              if (this.e.ImeEnabled)
                this.DisableIme(true);
              this.e.InlineIme = !this.e.InlineIme;
              break;
            }
            break;
          case 762:
            if (flag)
            {
              this.e.TerSetHdrRow(0, this.TerMenuSelect(762) == 0, true);
              break;
            }
            break;
          case 763:
            if (flag)
            {
              this.TerReturn();
              break;
            }
            break;
          case 764:
            if (flag)
            {
              this.e.TerSetRowKeep(0, this.TerMenuSelect(764) == 0, true);
              break;
            }
            break;
          case 765:
            if (flag)
            {
              this.e.TerCellColor(0, Color.Black, true);
              break;
            }
            break;
          case 766:
            if (flag)
            {
              this.e.TerInsertField((string) null, (string) null, true);
              break;
            }
            break;
          case 767 /*0x02FF*/:
            if (flag)
            {
              this.e.TerInsertTextInputField((string) null, (string) null, 0, true, (string) null, 0, 0, Color.Black, true, true);
              break;
            }
            break;
          case 768 /*0x0300*/:
            if (flag)
            {
              this.e.TerInsertCheckBoxField((string) null, 0, false, true, true);
              break;
            }
            break;
          case 769:
            this.EditInputField();
            break;
          case 770:
            if (flag)
            {
              this.e.TerInsertDateTime((string) null, true);
              break;
            }
            break;
          case 771:
            if (flag)
            {
              this.e.TerInsertToc(true);
              break;
            }
            break;
          case 772:
            if (flag)
            {
              this.e.SetTerParaFmt(1024 /*0x0400*/, this.TerMenuSelect(772) == 0, true);
              break;
            }
            break;
          case 773:
            if (flag)
            {
              this.e.TerSetPflags(64 /*0x40*/, this.TerMenuSelect(773) == 0, true);
              break;
            }
            break;
          case 774:
            if (flag)
            {
              this.e.SetTerCharStyle(65536 /*0x010000*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 775:
            if (flag)
            {
              this.e.SetTerCharStyle(131072 /*0x020000*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
          case 776:
            if (this.e.st != null)
            {
              if ((this.e.TerFlags4 & 256 /*0x0100*/) == 0)
                this.e.TerFlags4 |= 256 /*0x0100*/;
              else
                this.e.TerFlags4 = tc.ResetFlag(this.e.TerFlags4, 256 /*0x0100*/);
              this.PaintTer();
              break;
            }
            break;
          case 777:
            if (flag)
            {
              this.e.TerInsertFootnote2((string) null, (string) null, 0, false, true);
              break;
            }
            break;
          case 778:
            this.ToggleFootnoteEdit(false);
            break;
          case 779:
            if (flag)
            {
              this.e.TerEditList(true, -1, true, (string) null, true, 0);
              break;
            }
            break;
          case 780:
            if (flag)
            {
              this.e.TerEditList(false, -1, true, (string) null, false, 0);
              break;
            }
            break;
          case 781:
            if (flag)
            {
              this.e.TerEditListOr(true, -1, true, 1, true, 0);
              break;
            }
            break;
          case 782:
            if (flag)
            {
              this.e.TerEditListOr(false, -1, true, 1, true, 0);
              break;
            }
            break;
          case 783:
            if (flag)
            {
              this.e.TerEditListLevel(true, -1, 0, 1, 0, 0, "", 0, 0);
              break;
            }
            break;
          case 784:
            if (flag)
            {
              this.e.TerSetParaList(true, -1, 0, 0, true);
              break;
            }
            break;
          case 785:
            if (flag)
            {
              this.e.ParaLeftIndent(false, true);
              break;
            }
            break;
          case 786:
            if (flag)
            {
              this.e.TerCellBorderColor(0, Color.Black, Color.Black, Color.Black, Color.Black, true);
              break;
            }
            break;
          case 787:
            if (flag)
            {
              this.CopyFromClipboard(DataFormats.UnicodeText, (DataObject) null);
              break;
            }
            break;
          case 788:
            if (flag)
            {
              this.e.TerCellWidth(0, 0, 0, true);
              break;
            }
            break;
          case 789:
            if (flag)
            {
              this.e.TerRotateFrameText(true, this.e.CurLine, 0, true);
              break;
            }
            break;
          case 790:
            if (flag)
            {
              this.e.TerSetParaTextFlow(true, 0, true);
              break;
            }
            break;
          case 791:
            if (flag)
            {
              this.e.TerSetRowTextFlow(true, true, 0, true);
              break;
            }
            break;
          case 793:
            if (flag)
            {
              this.e.TerSetDocTextFlow(true, 0, true);
              break;
            }
            break;
          case 794:
            if (flag)
            {
              this.InsertHyperlink();
              break;
            }
            break;
          case 795:
            if (flag)
            {
              this.e.TerEnableTracking(!this.e.TrackChanges, (string) null, true, 0, tc.CLR_AUTO, 0, tc.CLR_AUTO);
              break;
            }
            break;
          case 796:
            if (flag)
            {
              this.e.TerSetUlineColor(true, tc.CLR_AUTO, true);
              break;
            }
            break;
          case 797:
            if (flag)
            {
              this.e.TerSetListLevel(-1, 1, true);
              break;
            }
            break;
          case 798:
            if (flag)
            {
              this.e.TerSetListLevel(-1, -1, true);
              break;
            }
            break;
          case 800:
            this.e.TerArg.BorderMargin = !this.e.TerArg.BorderMargin;
            this.e.TerRepaint(true);
            break;
          case 801:
            this.e.ProtectForm = !this.e.ProtectForm;
            this.e.TerSetReadOnly(this.e.ProtectForm);
            break;
          case 802:
            if (this.e.WmParaFID > 0)
            {
              this.e.TerSetWatermarkPict(0, true, true);
              break;
            }
            this.e.TerSetWatermarkPict(-1, true, true);
            break;
          case 803:
            if (flag)
            {
              this.e.TerCellRotateText(0, 0, true);
              break;
            }
            break;
          case 804:
            this.e.TerFindNextChange(true, true);
            break;
          case 805:
            this.e.TerFindNextChange(false, true);
            break;
          case 806:
            if (flag)
            {
              this.e.TerAcceptChanges(false, false, true);
              break;
            }
            break;
          case 807:
            if (flag)
            {
              this.e.TerAcceptChanges(true, true, true);
              break;
            }
            break;
          case 832:
            if (flag)
            {
              this.e.SetTerCharStyle(524288 /*0x080000*/, this.TerMenuSelect(CmdId) == 0, true);
              break;
            }
            break;
        }
      }
    }
    this.TerPostProcessing(273, CmdId, 0);
    this.SendActionMessage(273, CmdId, 0);
    if (curLine != this.e.CurLine || curCol != this.e.CurCol)
      this.e.OnCursorPosChanged();
    return true;
  }

  internal void TerAddMenuItem(MenuItem parent, string text, int CmdId)
  {
    if (text == "-")
      this.AddSeparator(parent);
    else
      this.AddMenuItem(parent, text, CmdId, false, Shortcut.None);
  }

  internal bool TerCommand(int CmdId) => this.TerCommand2(CmdId, true);

  internal bool TerCommand2(int CmdId, bool send)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.TerMenuEnable(CmdId) == 0)
    {
      if (this.e.UseWin)
      {
        if (send)
          this.SendMessage(this.e.hTerWnd, 2737, CmdId, 0);
        else
          this.PostMessage(this.e.hTerWnd, 2737, CmdId, 0);
      }
      else
        this.ProcessCommand(CmdId);
    }
    return true;
  }

  internal bool TerIgnoreCommand()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.SkipCommand = true;
    return true;
  }

  internal int TerMenuEnable(int MenuId)
  {
    bool flag1 = true;
    int num1 = 0;
    bool flag2 = false;
    int num2 = 0;
    if (!this.e.TerArg.open)
      return 1;
    if (this.e.InPrintPreview)
      return MenuId != 717 && MenuId != 600 && MenuId != 601 ? 1 : 0;
    bool flag3 = this.e.TerArg.ReadOnly;
    bool flag4 = !flag3;
    if (!flag3 && (this.e.CurSID < 0 || this.e.StyleId[this.e.CurSID].type == 2) && this.e.TerArg.WordWrap)
      flag2 = true;
    if (flag3 || this.e.CurSID >= 0)
      flag1 = false;
    bool wordWrap = this.e.TerArg.WordWrap;
    int section = this.GetSection(this.e.CurLine);
    int index1 = this.e.CurLine;
    if (this.e.HilightType != 0)
      index1 = this.e.HilightEndRow >= this.e.HilightBegRow ? this.e.HilightEndRow : this.e.HilightBegRow;
    int pfmt = this.e.text[index1].pfmt;
    int index2 = this.e.text[this.e.CurLine].fid;
    int curCfmt1 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if ((this.e.TerFont[curCfmt1].style & 128 /*0x80*/) != 0 && this.e.TerFont[curCfmt1].FrameType != 0)
      num1 = this.e.TerFont[curCfmt1].ParaFID;
    if (index2 == 0)
      index2 = num1;
    bool flag5 = !this.CanInsert(this.e.CurLine, this.e.CurCol);
    bool flag6 = flag5 && (this.e.TerFlags2 & 16384 /*0x4000*/) != 0;
    if (flag6)
      flag4 = false;
    bool flag7 = this.CanInsertBreakChar(this.e.CurLine, this.e.CurCol);
    this.CanInsertObject(this.e.CurLine, this.e.CurCol);
    bool flag8 = this.CanInsertTextObject(this.e.CurLine, this.e.CurCol);
    int index3 = 1;
    while (index3 < this.e.TotalLists && !this.e.list[index3].InUse)
      ++index3;
    int index4 = 1;
    while (index4 < this.e.TotalListOr && !this.e.ListOr[index4].InUse)
      ++index4;
    switch (MenuId)
    {
      case 615:
        return !flag1 || wordWrap || this.e.InFootnote ? 1 : 0;
      case 616:
        return !flag1 || wordWrap || this.e.InFootnote ? 1 : 0;
      case 617:
        return !flag1 || this.e.InFootnote ? 1 : 0;
      case 618:
        return !flag1 || wordWrap || this.e.InFootnote ? 1 : 0;
      case 619:
        return !flag1 || wordWrap || this.e.InFootnote ? 1 : 0;
      case 623:
        return !flag1 || this.e.HilightType != 1 ? 1 : 0;
      case 624:
        return !flag1 || this.e.HilightType != 1 ? 1 : 0;
      case 625:
        return 0;
      case 626:
        return !flag1 ? 1 : 0;
      case 627:
label_486:
        return 0;
      case 628:
        return !flag1 || this.e.HilightType != 2 && this.e.HilightType != 1 ? 1 : 0;
      case 629:
        return this.e.HilightType != 2 && this.e.HilightType != 1 ? 1 : 0;
      case 630:
        return !flag1 || this.e.InFootnote || flag5 || !this.IsClipboardFormatAvailable(1) && (!this.e.TerArg.WordWrap || !this.IsClipboardFormatAvailable(this.e.RtfClipFormat) && !this.IsClipboardFormatAvailable(2) && !this.IsClipboardFormatAvailable(8) && !this.IsClipboardFormatAvailable(3)) ? 1 : 0;
      case 631:
        return (this.e.TerFlags & 268435456 /*0x10000000*/) == 0 && flag1 && this.e.TerArg.WordWrap && !this.e.WaitForOle && !flag5 && !this.e.InFootnote && (this.IsClipboardFormatAvailable(1) || this.IsClipboardFormatAvailable(2) || this.IsClipboardFormatAvailable(8) || this.IsClipboardFormatAvailable(3) || this.IsClipboardFormatAvailable(this.e.RtfClipFormat) || this.IsClipboardFormatAvailable(this.e.OwnerLinkClipFormat) || this.IsClipboardFormatAvailable(this.e.ObjectLinkClipFormat)) ? 0 : 1;
      case 632:
        return !flag1 || this.e.InFootnote ? 1 : 0;
      case 638:
        return !flag1 || this.e.UndoCount <= 0 ? 1 : 0;
      case 640:
        return !flag1 && !this.e.ProtectForm ? 1 : 0;
      case 641:
        return 0;
      case 642:
        return 0;
      case 643:
        return !this.e.PrinterAvailable ? 1 : 0;
      case 644:
        return !flag1 ? 1 : 0;
      case 645:
        return !this.e.PrinterAvailable ? 1 : 0;
      case 647:
        return !flag4 ? 1 : 0;
      case 648:
        return !flag4 ? 1 : 0;
      case 649:
        return !flag4 ? 1 : 0;
      case 650:
        return !flag4 ? 1 : 0;
      case 651:
      case 832:
        return !flag4 ? 1 : 0;
      case 652:
        return !flag4 ? 1 : 0;
      case 653:
        return !flag4 ? 1 : 0;
      case 654:
        return !flag4 ? 1 : 0;
      case 655:
        return !flag4 ? 1 : 0;
      case 673:
        return !flag1 ? 1 : 0;
      case 685:
      case 727:
        int curCfmt2 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        return MenuId == 685 ? (!flag1 || this.e.InFootnote || (this.e.TerFont[curCfmt2].style & 128 /*0x80*/) == 0 ? 1 : 0) : (!flag1 || this.e.InFootnote || (this.e.TerFont[curCfmt2].style & 128 /*0x80*/) == 0 || this.e.TerFont[curCfmt2].ObjectType == 0 || (this.e.TerFlags & 4194304 /*0x400000*/) != 0 ? 1 : 0);
      case 687:
        return !flag4 ? 1 : 0;
      case 688:
        return !flag4 ? 1 : 0;
      case 689:
        return flag3 || this.e.ProtectionLock ? 1 : 0;
      case 711:
        return !flag4 ? 1 : 0;
      case 717:
        return 0;
      case 725:
        return !flag4 || this.e.CurSID >= 0 ? 1 : 0;
      case 730:
        return flag3 ? 1 : 0;
      case 731:
        return flag3 || flag6 || this.e.CurSID >= 0 ? 1 : 0;
      case 738:
        return !flag1 || this.e.InFootnote ? 1 : 0;
      case 742:
        return !flag4 ? 1 : 0;
      case 747:
        return !flag1 || this.e.UndoTblSize <= this.e.UndoCount ? 1 : 0;
      case 760:
        return !flag4 ? 1 : 0;
      case 761:
        return !flag1 ? 1 : 0;
      case 769:
        return !flag1 || !this.IsFormField(this.e.CurInputField, 0) && this.e.TerFont[curCfmt1].FieldId != 2 ? 1 : 0;
      case 774:
        return !flag4 ? 1 : 0;
      case 775:
        return !flag4 ? 1 : 0;
      case 779:
        return flag3 ? 1 : 0;
      case 780:
        return flag3 || index3 >= this.e.TotalLists ? 1 : 0;
      case 781:
        return flag3 || index3 >= this.e.TotalLists ? 1 : 0;
      case 782:
        return flag3 || index4 >= this.e.TotalListOr ? 1 : 0;
      case 783:
        return flag3 || index3 >= this.e.TotalLists ? 1 : 0;
      case 787:
        return !flag1 || this.e.InFootnote || flag5 || !this.IsClipboardFormatAvailable(1) ? 1 : 0;
      case 793:
        return !flag1 ? 1 : 0;
      case 796:
        return !flag4 ? 1 : 0;
      default:
        int num3;
        int bltId;
        int leftIndentTwips;
        if (this.e.EditingParaStyle)
        {
          num3 = this.e.StyleId[this.e.CurSID].ParaFlags;
          bltId = this.e.StyleId[this.e.CurSID].BltId;
          leftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          if (this.e.StyleId[this.e.CurSID].FirstIndentTwips < 0)
            leftIndentTwips += this.e.StyleId[this.e.CurSID].FirstIndentTwips;
        }
        else
        {
          num3 = this.e.PfmtId[pfmt].flags;
          bltId = this.e.PfmtId[pfmt].BltId;
          leftIndentTwips = this.e.PfmtId[pfmt].LeftIndentTwips;
          if (this.e.PfmtId[pfmt].FirstIndentTwips < 0)
            leftIndentTwips += this.e.PfmtId[pfmt].FirstIndentTwips;
        }
        if ((num3 & 8) != 0 && this.e.TerBlt[bltId].ls > 0)
          num2 = this.e.TerBlt[bltId].ls;
        switch (MenuId)
        {
          case 656:
            return !flag2 ? 1 : 0;
          case 657:
            return !flag2 ? 1 : 0;
          case 658:
            return !flag2 ? 1 : 0;
          case 659:
            return !flag2 ? 1 : 0;
          case 660:
            return !flag2 ? 1 : 0;
          case 661:
            return !flag2 ? 1 : 0;
          case 662:
            return !flag2 ? 1 : 0;
          case 663:
            return !flag2 ? 1 : 0;
          case 666:
            int index5 = 0;
            if (this.e.TerArg.WordWrap)
              index5 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId;
            return !flag2 || this.e.TerTab[index5].count <= 0 ? 1 : 0;
          case 667:
            int index6 = 0;
            if (this.e.TerArg.WordWrap)
              index6 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId;
            return !flag2 || this.e.TerTab[index6].count <= 0 ? 1 : 0;
          case 691:
            return !flag2 ? 1 : 0;
          case 720:
            return !flag2 ? 1 : 0;
          case 723:
            return !flag2 ? 1 : 0;
          case 724:
            return !flag2 ? 1 : 0;
          case 729:
            return !flag2 ? 1 : 0;
          case 732:
            return flag3 || this.e.CurSID >= 0 ? 1 : 0;
          case 734:
            return !flag2 ? 1 : 0;
          case 743:
            return !flag2 ? 1 : 0;
          case 748:
            return !flag2 ? 1 : 0;
          case 749:
            return !flag2 ? 1 : 0;
          case 772:
            return !flag2 ? 1 : 0;
          case 773:
            return !flag2 ? 1 : 0;
          case 784:
            return !flag2 || index4 >= this.e.TotalListOr ? 1 : 0;
          case 785:
            return !flag2 || leftIndentTwips <= 0 ? 1 : 0;
          case 790:
            return !flag2 ? 1 : 0;
          case 797:
            return !flag2 || num2 <= 0 ? 1 : 0;
          case 798:
            return !flag2 || num2 <= 0 ? 1 : 0;
          default:
            bool flag9 = this.e.HilightType != 2 || this.e.text[this.e.HilightBegRow].cid <= 0 && this.e.text[this.e.HilightEndRow].cid <= 0 ? this.True(this.e.text[this.e.CurLine].cid) : this.InSameTable(this.e.text[this.e.HilightBegRow].cid, this.e.text[this.e.HilightEndRow].cid);
            if (!flag1 || !this.e.TerArg.PrintView)
              flag9 = false;
            switch (MenuId)
            {
              case 636:
                return !flag1 ? 1 : 0;
              case 670:
                return !(flag1 & flag7 & wordWrap) ? 1 : 0;
              case 671:
                return !(flag1 & wordWrap) || !this.e.TerArg.PrintView ? 1 : 0;
              case 672:
                return !(flag1 & flag7) || !this.e.TerArg.PrintView || this.e.EditPageHdrFtr ? 1 : 0;
              case 674:
                return !this.e.TerArg.PrintView ? 1 : 0;
              case 675:
                return !(flag1 & flag7) || !this.e.TerArg.PrintView || this.e.TerSect[section].columns <= 1 ? 1 : 0;
              case 676:
                return !this.e.TerArg.PrintView ? 1 : 0;
              case 677:
                return !flag1 || !this.e.TerArg.PageMode ? 1 : 0;
              case 686:
                return !flag1 ? 1 : 0;
              case 690:
                return !flag1 ? 1 : 0;
              case 692:
                return !this.e.TerArg.WordWrap ? 1 : 0;
              case 693:
                return !flag1 || this.e.InFootnote || (this.e.TerFlags & 268435456 /*0x10000000*/) != 0 ? 1 : 0;
              case 694:
                return !flag1 || !this.CanInsertTable(this.e.CurLine, this.e.CurCol) || !this.e.TerArg.PrintView ? 1 : 0;
              case 695:
                return !flag9 ? 1 : 0;
              case 696:
                return !flag9 || this.e.HilightType != 0 ? 1 : 0;
              case 697:
                return !flag9 ? 1 : 0;
              case 698:
                return !flag9 ? 1 : 0;
              case 699:
                return !this.e.TerArg.PageMode ? 1 : 0;
              case 700:
                return !flag9 ? 1 : 0;
              case 701:
                return !flag9 ? 1 : 0;
              case 702:
                return !flag9 ? 1 : 0;
              case 703:
                return !flag9 ? 1 : 0;
              case 704:
                return !flag9 ? 1 : 0;
              case 718:
                return !flag1 || this.e.InFootnote || !this.e.TerArg.PrintView ? 1 : 0;
              case 719:
                return !flag1 || this.e.InFootnote ? 1 : 0;
              case 721:
                return !flag1 || this.e.InFootnote || (this.e.PfmtId[pfmt].flags & 12288 /*0x3000*/) != 0 ? 1 : 0;
              case 722:
                return !flag1 || this.e.TerArg.FittedView ? 1 : 0;
              case 726:
                return !flag1 || this.e.InFootnote || !this.e.TerArg.PrintView ? 1 : 0;
              case 728:
                return !flag1 ? 1 : 0;
              case 735:
                return !(flag1 & wordWrap) || !this.e.TerArg.PageMode || index2 <= 0 ? 1 : 0;
              case 736:
                return !(flag1 & wordWrap) || !this.e.TerArg.PageMode || index2 <= 0 || (this.e.ParaFrame[index2].flags & 896) == 0 ? 1 : 0;
              case 739:
                return !this.e.TerArg.PrintView ? 1 : 0;
              case 740:
                return !flag1 || flag5 ? 1 : 0;
              case 741:
                return !flag1 || this.e.st == null ? 1 : 0;
              case 744:
                return !this.e.TerArg.WordWrap || !this.e.TerArg.PageMode || this.e.TerArg.FittedView ? 1 : 0;
              case 745:
                return !flag1 ? 1 : 0;
              case 746:
                return !flag1 || flag5 ? 1 : 0;
              case 750:
                return !flag9 ? 1 : 0;
              case 751:
                return !flag1 || !this.e.TerArg.PrintView || this.e.HilightType != 0 || this.e.text[this.e.CurLine].cid <= 0 ? 1 : 0;
              case 752:
                return !flag1 || this.e.InFootnote ? 1 : 0;
              case 753:
                return !flag1 ? 1 : 0;
              case 754:
                return !flag1 || !this.e.EditPageHdrFtr || this.e.TerSect1[section].fhdr.FirstLine != -1 ? 1 : 0;
              case 755:
                return !flag1 || !this.e.EditPageHdrFtr || this.e.TerSect1[section].fftr.FirstLine != -1 ? 1 : 0;
              case 756:
                return !flag1 || !this.e.EditPageHdrFtr || this.e.TerSect1[section].fhdr.FirstLine < 0 ? 1 : 0;
              case 757:
                return !flag1 || !this.e.EditPageHdrFtr || this.e.TerSect1[section].fftr.FirstLine < 0 ? 1 : 0;
              case 758:
                return !flag1 ? 1 : 0;
              case 759:
                return !flag1 || flag5 ? 1 : 0;
              case 762:
                return !flag9 ? 1 : 0;
              case 764:
                return !flag9 ? 1 : 0;
              case 765:
                return !flag9 ? 1 : 0;
              case 767 /*0x02FF*/:
                return !(flag1 & flag8) ? 1 : 0;
              case 768 /*0x0300*/:
                return !(flag1 & flag8) ? 1 : 0;
              case 770:
                return !flag1 || this.e.InFootnote ? 1 : 0;
              case 771:
                return !flag1 || this.e.InFootnote || !this.e.DocHasHeadings ? 1 : 0;
              case 776:
                return !flag1 || this.e.st == null ? 1 : 0;
              case 777:
                return !flag1 || this.e.InFootnote || (this.e.PfmtId[pfmt].flags & 12288 /*0x3000*/) != 0 ? 1 : 0;
              case 778:
                return !flag1 || this.e.TerArg.FittedView ? 1 : 0;
              case 786:
                return !flag9 ? 1 : 0;
              case 788:
                return !flag9 ? 1 : 0;
              case 789:
                return !(flag1 & wordWrap) || !this.e.TerArg.PageMode || index2 <= 0 || (this.e.ParaFrame[index2].flags & 768 /*0x0300*/) != 0 || this.e.ParaFrame[index2].pict != 0 ? 1 : 0;
              case 791:
                return !flag9 ? 1 : 0;
              case 794:
                return !(flag1 & flag8) ? 1 : 0;
              case 795:
                return !flag1 ? 1 : 0;
              case 802:
                return !flag1 ? 1 : 0;
              case 803:
                return !flag9 ? 1 : 0;
              case 806:
                return !flag1 || this.e.TrackChanges || !this.IsTrackChangeFont(curCfmt1) ? 1 : 0;
              case 807:
                return !flag1 || this.e.TrackChanges ? 1 : 0;
              default:
                goto label_486;
            }
        }
    }
  }

  internal bool TerMenuEnable2(int MenuId) => this.TerMenuEnable(MenuId) == 0;

  internal int TerMenuSelect(int MenuId)
  {
    int index = 0;
    int num1 = 0;
    int num2 = 0;
    tc.StrListLevel pLevel = new tc.StrListLevel();
    if (this.e.TerArg.open)
    {
      switch (MenuId)
      {
        case 648:
        case 649:
        case 650:
        case 651:
        case 652:
        case 653:
        case 687:
        case 688:
        case 689:
        case 719:
        case 725:
        case 731:
        case 742:
        case 752:
        case 760:
        case 770:
        case 771:
        case 774:
        case 775:
        case 832:
          index = this.GetEffectiveCfmt();
          break;
        case 674:
          return !this.e.TerArg.PageMode ? 0 : 8;
        case 676:
          return !this.e.ViewPageHdrFtr ? 0 : 8;
        case 680:
          return !this.e.TerArg.ruler ? 0 : 8;
        case 681:
          return !this.e.TerArg.ToolBar ? 0 : 8;
        case 682:
          return !this.e.TerArg.ShowStatus ? 0 : 8;
        case 686:
          return !this.e.ShowHiddenText ? 0 : 8;
        case 690:
          return !this.e.ProtectionLock ? 0 : 8;
        case 692:
          return !this.e.ShowParaMark ? 0 : 8;
        case 717:
          return !this.e.InPrintPreview && this.e.PvDlg == null ? 0 : 8;
        case 722:
          return !this.e.EditFootnoteText ? 0 : 8;
        case 728:
          return !this.e.SnapToGrid ? 0 : 8;
        case 739:
          return !this.e.TerArg.PageMode || !this.e.TerArg.FittedView ? 0 : 8;
        case 744:
          return !this.e.ShowPageBorder ? 0 : 8;
        case 758:
          return !this.e.ShowFieldNames ? 0 : 8;
        case 778:
          return !this.e.EditEndnoteText ? 0 : 8;
        case 795:
          return !this.e.TrackChanges ? 0 : 8;
        case 800:
          return !this.e.TerArg.BorderMargin ? 0 : 8;
        case 801:
          return !this.e.ProtectForm ? 0 : 8;
      }
      int num3 = this.e.CurSID < 0 ? this.e.TerFont[index].style : this.e.StyleId[this.e.CurSID].style;
      int x = this.e.CurSID < 0 ? this.e.TerFont[index].expand : this.e.StyleId[this.e.CurSID].expand;
      switch (MenuId)
      {
        case 648:
          return (num3 & 2) == 0 ? 0 : 8;
        case 649:
          return (num3 & 1) == 0 ? 0 : 8;
        case 650:
          return (num3 & 4) == 0 ? 0 : 8;
        case 651:
          return (num3 & 8) == 0 ? 0 : 8;
        case 652:
          return (num3 & 16 /*0x10*/) == 0 ? 0 : 8;
        case 653:
          return (num3 & 32 /*0x20*/) == 0 ? 0 : 8;
        case 687:
          return (num3 & 64 /*0x40*/) == 0 ? 0 : 8;
        case 688:
          return (num3 & 256 /*0x0100*/) == 0 ? 0 : 8;
        case 689:
          return (num3 & 512 /*0x0200*/) == 0 ? 0 : 8;
        case 719:
          return this.e.TerFont[index].FieldId != 1 ? 0 : 8;
        case 725:
          return (num3 & 8192 /*0x2000*/) == 0 ? 0 : 8;
        case 731:
          return this.e.TerFont[index].CharStyId <= 1 ? 0 : 8;
        case 740:
          char[] txt1 = this.e.text[this.e.CurLine].txt;
          return this.e.CurCol >= this.e.text[this.e.CurLine].len || txt1[this.e.CurCol] != '\u000E' ? 0 : 8;
        case 742:
          return (num3 & 16384 /*0x4000*/) == 0 ? 0 : 8;
        case 746:
          char[] txt2 = this.e.text[this.e.CurLine].txt;
          return this.e.CurCol >= this.e.text[this.e.CurLine].len || txt2[this.e.CurCol] != '\u0017' ? 0 : 8;
        case 752:
          return this.e.TerFont[index].FieldId != 5 ? 0 : 8;
        case 759:
          char[] txt3 = this.e.text[this.e.CurLine].txt;
          return this.e.CurCol >= this.e.text[this.e.CurLine].len || txt3[this.e.CurCol] != '\u0006' ? 0 : 8;
        case 760:
          return !this.True(x) ? 0 : 8;
        case 770:
          return this.e.TerFont[index].FieldId != 8 && this.e.TerFont[index].FieldId != 10 ? 0 : 8;
        case 771:
          return this.e.TerFont[index].FieldId != 9 ? 0 : 8;
        case 774:
          return (num3 & 65536 /*0x010000*/) == 0 ? 0 : 8;
        case 775:
          return (num3 & 131072 /*0x020000*/) == 0 ? 0 : 8;
        case 832:
          return (num3 & 524288 /*0x080000*/) == 0 ? 0 : 8;
        default:
          int pfmt = this.e.text[this.e.CurLine].pfmt;
          if (this.e.CurLine > 0 && this.e.CurCol == 0 && this.e.HilightType != 0)
          {
            if (this.e.HilightEndRow == this.e.CurLine - 1 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len)
              pfmt = this.e.text[this.e.CurLine - 1].pfmt;
            if (this.e.HilightEndRow == this.e.CurLine && this.e.HilightEndCol == 0 && this.e.HilightBegRow < this.e.CurLine)
              pfmt = this.e.text[this.e.CurLine - 1].pfmt;
          }
          int num4;
          int leftIndentTwips;
          int rightIndentTwips;
          int firstIndentTwips;
          int spaceBefore;
          int spaceAfter;
          int spaceBetween;
          int lineSpacing;
          int bltId;
          int pflags;
          Color color;
          if (this.e.EditingParaStyle)
          {
            num4 = this.e.StyleId[this.e.CurSID].ParaFlags;
            leftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
            rightIndentTwips = this.e.StyleId[this.e.CurSID].RightIndentTwips;
            firstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
            spaceBefore = this.e.StyleId[this.e.CurSID].SpaceBefore;
            spaceAfter = this.e.StyleId[this.e.CurSID].SpaceAfter;
            spaceBetween = this.e.StyleId[this.e.CurSID].SpaceBetween;
            lineSpacing = this.e.StyleId[this.e.CurSID].LineSpacing;
            bltId = this.e.StyleId[this.e.CurSID].BltId;
            pflags = this.e.StyleId[this.e.CurSID].pflags;
            color = this.e.StyleId[this.e.CurSID].ParaBkColor;
          }
          else
          {
            num4 = this.e.PfmtId[pfmt].flags;
            leftIndentTwips = this.e.PfmtId[pfmt].LeftIndentTwips;
            rightIndentTwips = this.e.PfmtId[pfmt].RightIndentTwips;
            if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
              this.SwapInts(ref leftIndentTwips, ref rightIndentTwips);
            firstIndentTwips = this.e.PfmtId[pfmt].FirstIndentTwips;
            spaceBefore = this.e.PfmtId[pfmt].SpaceBefore;
            spaceAfter = this.e.PfmtId[pfmt].SpaceAfter;
            spaceBetween = this.e.PfmtId[pfmt].SpaceBetween;
            lineSpacing = this.e.PfmtId[pfmt].LineSpacing;
            bltId = this.e.PfmtId[pfmt].BltId;
            pflags = this.e.PfmtId[pfmt].pflags;
            color = this.e.PfmtId[pfmt].BkColor;
            num1 = this.e.PfmtId[pfmt].flow;
          }
          if ((num4 & 8) != 0 && this.e.TerBlt[bltId].ls > 0)
          {
            this.GetListLevelPtr(this.e.TerBlt[bltId].ls, this.e.TerBlt[bltId].lvl, out pLevel);
            num2 = this.e.TerBlt[bltId].ls;
          }
          switch (MenuId)
          {
            case 657:
              return (num4 & 1) == 0 ? 0 : 8;
            case 658:
              return (num4 & 2) == 0 ? 0 : 8;
            case 659:
              return leftIndentTwips == 0 ? 0 : 8;
            case 660:
              return rightIndentTwips == 0 ? 0 : 8;
            case 661:
              return (num4 & 4) == 0 ? 0 : 8;
            case 662:
              return firstIndentTwips == 0 ? 0 : 8;
            case 663:
              return (num4 & 2048 /*0x0800*/) == 0 ? 0 : 8;
            case 691:
              return (num4 & 65776 /*0x0100F0*/) == 0 ? 0 : 8;
            case 720:
              return !this.True(spaceBefore) && !this.True(spaceAfter) && !this.True(spaceBetween) && !this.True(lineSpacing) ? 0 : 8;
            case 723:
              return (num4 & 16384 /*0x4000*/) == 0 ? 0 : 8;
            case 724:
              return (num4 & 32768 /*0x8000*/) == 0 ? 0 : 8;
            case 729:
              return (num4 & 8) == 0 || (num2 != 0 || !this.e.TerBlt[bltId].IsBullet) && (num2 == 0 || pLevel.NumType != 23) ? 0 : 8;
            case 732:
              return this.e.PfmtId[pfmt].StyId <= 0 ? 0 : 8;
            case 743:
              return (pflags & 32 /*0x20*/) == 0 ? 0 : 8;
            case 748:
              return (num4 & 8) == 0 || (num2 != 0 || this.e.TerBlt[bltId].IsBullet) && (num2 == 0 || pLevel.NumType == 23) ? 0 : 8;
            case 749:
              return !(color != tc.CLR_WHITE) ? 0 : 8;
            case 772:
              return (num4 & 2051) != 0 ? 0 : 8;
            case 773:
              return (pflags & 64 /*0x40*/) == 0 ? 0 : 8;
            case 784:
              return (num4 & 8) == 0 || this.e.TerBlt[bltId].ls == 0 ? 0 : 8;
            case 785:
              return 0;
            case 790:
              return num1 != 2 ? 0 : 8;
            default:
              int cid = this.e.text[this.e.CurLine].cid;
              int row = cid <= 0 || cid > this.e.TotalCells ? 0 : this.e.cell[cid].row;
              switch (MenuId)
              {
                case 639:
                  return !this.e.InsertMode ? 0 : 8;
                case 677:
                  return !this.e.EditPageHdrFtr ? 0 : 8;
                case 699:
                  return !this.e.ShowTableGridLines || !this.e.TerArg.PageMode ? 0 : 8;
                case 710:
                  return !this.e.ShowHyperlinkCursor ? 0 : 8;
                case 730:
                  return this.e.CurSID < 0 ? 0 : 8;
                case 733:
                  return this.e.ZoomPercent == 100 ? 0 : 8;
                case 737:
                  return this.e.BkPictId <= 0 ? 0 : 8;
                case 761:
                  return !this.e.InlineIme ? 0 : 8;
                case 762:
                  return row <= 0 || (this.e.TableRow[row].flags & 4) == 0 ? 0 : 8;
                case 764:
                  return row <= 0 || (this.e.TableRow[row].flags & 8192 /*0x2000*/) == 0 ? 0 : 8;
                case 776:
                  return !(tc.hSpell != (Assembly) null) || (this.e.TerFlags4 & 256 /*0x0100*/) == 0 ? 0 : 8;
                case 791:
                  return row <= 0 || this.e.TableRow[row].flow != 2 ? 0 : 8;
                case 793:
                  return this.e.DocTextFlow != 2 ? 0 : 8;
                case 802:
                  return this.e.WmParaFID <= 0 ? 0 : 8;
              }
              break;
          }
      }
    }
    return 0;
  }

  internal bool TerMenuSelect2(int MenuId) => this.TerMenuSelect(MenuId) == 8;
}
