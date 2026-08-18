
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.HtmlWriter




using Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools
{
    internal class HtmlWriter
    {
      private StringWriter writer = new StringWriter();
      private Stack<string> tagsStack = new Stack<string>();

      public HtmlWriter()
      {
        this.writer.Flush();
        this.tagsStack.Clear();
      }

      public void AddBeginTag(HtmlTags tag, params HtmlProperty[] attributes)
      {
        string tag1 = this.NormalizeTag(tag);
        IEnumerable<HtmlProperty> source = attributes.OfType<HtmlProperty>();
        this.WriteTag(tag1, source.ToArray<HtmlProperty>());
        this.tagsStack.Push(tag1);
      }

      public void AddBeginTag(string tag, params HtmlProperty[] attributes)
      {
        string tag1 = this.NormalizeTag(tag);
        this.WriteTag(tag1, attributes);
        this.tagsStack.Push(tag1);
      }

      public void AddEndTag()
      {
        string tag = this.tagsStack.Pop();
        this.writer.WriteLine(this.GenerateTabs() + this.GenerateEndTag(tag));
      }

      public void AddText(string text) => this.writer.Write(text);

      public void AddTextWithinTag(HtmlTags tag, string text, params HtmlProperty[] attributes)
      {
        string tag1 = this.NormalizeTag(tag);
        this.writer.WriteLine(this.GenerateTabs() + this.GenerateTag(tag1, attributes) + text + this.GenerateEndTag(tag1));
      }

      public void AddUnpairedTag(HtmlTags tag, params HtmlProperty[] attributes)
      {
        this.WriteTag(this.NormalizeTag(tag), attributes);
      }

      public void AddUnpairedTag(string tag, params HtmlProperty[] attributes)
      {
        this.WriteTag(this.NormalizeTag(tag), attributes);
      }

      public void AddClassCssStyle(string className, params CssProperty[] attributes)
      {
        this.AddTagCssStyle("." + className, attributes);
      }

      public void AddTagCssStyle(HtmlTags tag, params CssProperty[] attributes)
      {
        this.AddTagCssStyle(this.NormalizeTag(tag), attributes);
      }

      public void AddTagCssStyle(string tag, params CssProperty[] attributes)
      {
        this.NormalizeTag(tag);
        this.writer.WriteLine($"{this.GenerateTabs()}{tag} {{");
        foreach (CssProperty attribute in attributes)
          this.writer.WriteLine($"{this.GenerateTabs()}\t{attribute.Name}: {attribute.Value};");
        this.writer.WriteLine(this.GenerateTabs() + "}");
        this.writer.WriteLine();
      }

      public void Close() => this.writer.Close();

      public override string ToString() => this.writer.ToString();

      private string GenerateTabs()
      {
        string tabs = "";
        for (int index = 0; index < this.tagsStack.Count; ++index)
          tabs += "\t";
        return tabs;
      }

      private string GenerateEndTag(string tag) => $"</{tag}>";

      private string GenerateTag(string tag, params HtmlProperty[] attributes)
      {
        string str = "<" + tag;
        foreach (HtmlProperty attribute in attributes)
          str = $"{str} {attribute.Name}=\"{attribute.Value}\"";
        return str + ">";
      }

      private string NormalizeTag(HtmlTags tag) => this.NormalizeTag(tag.ToString());

      private string NormalizeTag(string tag) => tag.ToLower();

      private void WriteTag(string tag, params HtmlProperty[] attributes)
      {
        this.writer.WriteLine(this.GenerateTabs() + this.GenerateTag(tag, attributes));
      }
    }
}
