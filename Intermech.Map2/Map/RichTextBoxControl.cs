// Decompiled with JetBrains decompiler
// Type: Intermech.Map.RichTextBoxControl
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Map
{
    public sealed class RichTextBoxControl : RichTextBox, IMapControlObject
    {
      /// <summary>тип класса CharacterMap.CharacterMapForm</summary>
      private static readonly Lazy<System.Type> TypeCharacterMapForm = new Lazy<System.Type>((Func<System.Type>) (() => System.Type.GetType("CharacterMap.CharacterMapForm,Intermech.Controls", false)));
      private MapControl myMapControl;

      /// <summary>Clean up any resources being used.</summary>
      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this.TextChanged -= new EventHandler(this.Control_TextChanged);
          this.myMapControl = (MapControl) null;
          this.MapView = (MapView) null;
        }
        base.Dispose(disposing);
      }

      public RichTextBoxControl()
      {
        this.AcceptsReturn = true;
        this.myMapControl = (MapControl) null;
        this.MapView = (MapView) null;
        this.HideSelection = false;
        this.ReadOnly = false;
        this.Capture = false;
        this.Text = "";
        this.CreateContextMenuStrip();
        this.TextChanged += new EventHandler(this.Control_TextChanged);
      }

      public bool AcceptsReturn { get; set; }

      public Size GetMeasureText(string value, Font font)
      {
        TextRenderer.MeasureText(value, this.Font);
        return Size.Empty;
      }

      public string GetText()
      {
        int selectionStart = this.SelectionStart;
        int selectionLength = this.SelectionLength;
        List<MapRedNoteText.Fragment> values = new List<MapRedNoteText.Fragment>();
        for (int start = 0; start < this.TextLength; ++start)
        {
          this.Select(start, 1);
          string name = this.SelectionFont.Name != this.Font.Name ? this.SelectionFont.Name : "";
          values.Add(new MapRedNoteText.Fragment()
          {
            FontName = name,
            Text = this.SelectedText
          });
        }
        this.Select(selectionStart, selectionLength);
        return string.Join<MapRedNoteText.Fragment>("", (IEnumerable<MapRedNoteText.Fragment>) values).Replace("\r\n", "\n").Replace("\n", "\r\n");
      }

      public void SetText(string value)
      {
        this.TextChanged -= new EventHandler(this.Control_TextChanged);
        try
        {
          this.Text = "";
          foreach (MapRedNoteText.Fragment fragment in MapRedNoteText.Fragment.SplitText(value.Replace("\r\n", "\n")).ToArray())
          {
            this.SelectionProtected = false;
            this.SelectionFont = fragment.FontName == "" ? this.Font : new Font(fragment.FontName, this.Font.Size, this.Font.Style, GraphicsUnit.Point);
            this.SelectedText = fragment.Text;
          }
        }
        finally
        {
          this.TextChanged += new EventHandler(this.Control_TextChanged);
        }
      }

      /// <summary>конец ввода две пустые строки</summary>
      /// <param name="str">проверяемая строка</param>
      /// <returns>положение конца строки, иначе -1</returns>
      private int FindEndIndexChars(string str)
      {
        string[] array = ((IEnumerable<string>) new string[2]
        {
          "\r\n\r\n",
          "\n\n"
        }).Where<string>((Func<string, bool>) (s =>
        {
          int num = str.LastIndexOf(s, StringComparison.Ordinal);
          return num != -1 && num == str.Length - s.Length;
        })).ToArray<string>();
        return array.Length == 0 ? -1 : str.Length - array[0].Length;
      }

      internal void Control_TextChanged(object sender, EventArgs e)
      {
        if (this.MapControl == null || this.MapView == null)
          return;
        if (this.MapControl.EditedObject is MapText && this.AcceptsReturn)
        {
          string text = this.GetText();
          int endIndexChars = this.FindEndIndexChars(text);
          if (endIndexChars != -1)
          {
            this.SetText(text.Substring(0, endIndexChars));
            this.AcceptText();
            this.MapView.InitFocus();
            return;
          }
        }
        Size measureText = this.GetMeasureText(this.GetText() + ".", this.Font);
        Size view = this.MapView.ConvertDocToView(this.MapControl.Size);
        this.MapControl.Size = this.MapView.ConvertViewToDoc(new Size(Math.Max(measureText.Width, view.Width), Math.Max(measureText.Height, view.Height)));
        this.Update();
        this.Focus();
      }

      private void AcceptText()
      {
        MapControl mapControl = this.MapControl;
        if (mapControl == null)
          return;
        if (mapControl.EditedObject is MapText editedObject)
          editedObject.DoEdit(this.MapView, editedObject.Text, this.GetText());
        mapControl.DoEndEdit(this.MapView);
      }

      private bool HandleKey(Keys key)
      {
        switch (key)
        {
          case Keys.Tab:
          case Keys.Return:
            if (key == Keys.Return && this.AcceptsReturn)
            {
              string text = this.GetText();
              int endIndexChars = this.FindEndIndexChars(text);
              if (endIndexChars == -1)
                return false;
              this.SetText(text.Substring(0, endIndexChars));
            }
            this.AcceptText();
            this.MapView.InitFocus();
            return true;
          case Keys.Escape:
            MapControl mapControl = this.MapControl;
            MapView mapView = this.MapView;
            mapControl?.DoEndEdit(this.MapView);
            mapView.InitFocus();
            return true;
          default:
            return false;
        }
      }

      protected override void OnLeave(EventArgs evt)
      {
        this.AcceptText();
        base.OnLeave(evt);
      }

      protected override bool ProcessDialogKey(Keys key)
      {
        return this.HandleKey(key) || base.ProcessDialogKey(key);
      }

      public MapControl MapControl
      {
        get => this.myMapControl;
        set
        {
          if (this.myMapControl == value)
            return;
          this.myMapControl = value;
          if (value == null || !(value.EditedObject is MapText editedObject))
            return;
          this.Initialize(editedObject);
          if (editedObject.Multiline)
          {
            this.SetText(editedObject.Text);
          }
          else
          {
            int firstLineBreak = editedObject.FindFirstLineBreak(editedObject.Text, 0);
            this.SetText(firstLineBreak >= 0 ? editedObject.Text.Substring(0, firstLineBreak) : editedObject.Text);
          }
        }
      }

      private void CreateContextMenuStrip()
      {
        ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
        contextMenuStrip1.ShowImageMargin = false;
        ContextMenuStrip contextMenuStrip2 = contextMenuStrip1;
        if (Environment.OSVersion.Version.Major < 6)
          contextMenuStrip2.RenderMode = ToolStripRenderMode.System;
        contextMenuStrip2.Items.Add((ToolStripItem) new ToolStripSeparator());
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Вырезать");
        toolStripMenuItem1.Click += (EventHandler) ((sender, e) => this.Cut());
        contextMenuStrip2.Items.Add((ToolStripItem) toolStripMenuItem1);
        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Копировать");
        toolStripMenuItem2.Click += (EventHandler) ((sender, e) => this.Copy());
        contextMenuStrip2.Items.Add((ToolStripItem) toolStripMenuItem2);
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Вставить");
        toolStripMenuItem3.Click += (EventHandler) ((sender, e) => this.Paste());
        contextMenuStrip2.Items.Add((ToolStripItem) toolStripMenuItem3);
        ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("Удалить");
        toolStripMenuItem4.Click += (EventHandler) ((sender, e) => this.SelectedText = string.Empty);
        contextMenuStrip2.Items.Add((ToolStripItem) toolStripMenuItem4);
        contextMenuStrip2.Items.Add((ToolStripItem) new ToolStripSeparator());
        ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem("Вставка спецсимвола");
        toolStripMenuItem5.Click += (EventHandler) ((sender, e) => this.SpecSimbol());
        contextMenuStrip2.Items.Add((ToolStripItem) toolStripMenuItem5);
        this.ContextMenuStrip = contextMenuStrip2;
      }

      private void SpecSimbol()
      {
        int selectionStart = this.SelectionStart;
        this.Select(this.SelectionStart, 0);
        string itemFontName = "";
        string itemText = "";
        using (Form form = (Form) Activator.CreateInstance(RichTextBoxControl.TypeCharacterMapForm.Value))
        {
          object obj1 = Convert.ChangeType((object) form, RichTextBoxControl.TypeCharacterMapForm.Value);
          FieldInfo field = obj1.GetType().GetField("characterMap", BindingFlags.Instance | BindingFlags.NonPublic);
          if (field != (FieldInfo) null)
          {
            object obj2 = field.GetValue(obj1);
            RichTextBoxControl.AddEventHandler(obj2.GetType().GetEvent("OnCharSelected"), obj2, (Action<object, EventArgs>) ((o, e) =>
            {
              string str = (string) e.GetType().GetProperty("SelectedChar").GetValue((object) e, (object[]) null);
              Font font = (Font) e.GetType().GetProperty("SelectedFont").GetValue((object) e, (object[]) null);
              itemText = str;
              itemFontName = font.Name;
              form.DialogResult = DialogResult.OK;
            }));
          }
          if (form.ShowDialog() != DialogResult.OK)
            itemText = "";
        }
        if (itemText != "")
        {
          this.Select(selectionStart, 0);
          this.SelectionProtected = false;
          this.SelectionFont = itemFontName == this.Font.Name ? this.Font : new Font(itemFontName, this.Font.Size, this.Font.Style, GraphicsUnit.Point);
          this.SelectedText = itemText;
          this.DeselectAll();
          this.Select(selectionStart + 1, 0);
        }
        Size measureText = this.GetMeasureText(this.GetText() + ".", this.Font);
        Size view = this.MapView.ConvertDocToView(this.MapControl.Size);
        this.MapControl.Size = this.MapView.ConvertViewToDoc(new Size(Math.Max(measureText.Width, view.Width), Math.Max(measureText.Height, view.Height)));
        this.Update();
        this.Focus();
      }

      private static void AddEventHandler(
        EventInfo eventInfo,
        object item,
        Action<object, EventArgs> action)
      {
        ParameterExpression[] array = ((IEnumerable<ParameterInfo>) eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters()).Select<ParameterInfo, ParameterExpression>((Func<ParameterInfo, ParameterExpression>) (parameter => Expression.Parameter(parameter.ParameterType))).ToArray<ParameterExpression>();
        MethodInfo method = action.GetType().GetMethod("Invoke");
        if (!(method != (MethodInfo) null))
          return;
        Delegate handler = Expression.Lambda(eventInfo.EventHandlerType, (Expression) Expression.Call((Expression) Expression.Constant((object) action), method, (Expression) array[0], (Expression) array[1]), array).Compile();
        eventInfo.AddEventHandler(item, handler);
      }

      private void Initialize(MapText text1)
      {
        if (text1 == null)
          return;
        try
        {
          switch (text1.Alignment)
          {
            case 1:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            case 2:
            case 3:
            case 16 /*0x10*/:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            case 4:
            case 8:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            case 32 /*0x20*/:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            case 64 /*0x40*/:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            case 128 /*0x80*/:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
            default:
              this.Multiline = text1.Multiline || text1.Wrapping;
              this.AcceptsReturn = text1.Multiline;
              this.WordWrap = text1.Wrapping;
              this.RightToLeft = text1.isRightToLeft(this.MapView) ? RightToLeft.Yes : RightToLeft.No;
              break;
          }
        }
        finally
        {
          Font font = text1.Font;
          float size = font.Size * (this.MapView != null ? this.MapView.DocScale : 1f);
          this.Font = text1.makeFont(font.Name, size, font.Style);
        }
      }

      public MapView MapView { get; set; }
    }
}
