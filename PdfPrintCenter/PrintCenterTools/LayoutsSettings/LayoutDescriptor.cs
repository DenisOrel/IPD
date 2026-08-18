using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class LayoutDescriptor : IPdfPageProducer
    {
        public static readonly string DefaultMainFormatName = "A4";
        public static readonly string DefaultCaption = "Новый макет";
        private static readonly Dictionary<LayoutDescriptor.XmlAttributeNames, string> xmlAttributeNames = new Dictionary<LayoutDescriptor.XmlAttributeNames, string>()
      {
        {
          LayoutDescriptor.XmlAttributeNames.LayoutName,
          "name"
        },
        {
          LayoutDescriptor.XmlAttributeNames.FormatName,
          "format_name"
        },
        {
          LayoutDescriptor.XmlAttributeNames.IsPortrait,
          "is_portrait"
        },
        {
          LayoutDescriptor.XmlAttributeNames.Left,
          "left"
        },
        {
          LayoutDescriptor.XmlAttributeNames.Top,
          "top"
        }
      };
        private static readonly Dictionary<LayoutDescriptor.XmlElementNames, string> xmlElementNames = new Dictionary<LayoutDescriptor.XmlElementNames, string>()
      {
        {
          LayoutDescriptor.XmlElementNames.Layout,
          "layout"
        },
        {
          LayoutDescriptor.XmlElementNames.Input,
          "input"
        }
      };

        public LayoutDescriptor()
        {
            this.Caption = LayoutDescriptor.DefaultCaption;
            this.MainFormat = KnownPaperFormats.GetFormat(LayoutDescriptor.DefaultMainFormatName);
            this.InternalFormats = new List<FormatLocation>();
            this.IsLoaded = true;
        }

        public LayoutDescriptor(string caption, string xmlContent)
        {
            this.Caption = caption;
            this.LoadFromXml(xmlContent);
        }

        public string Caption { get; set; }

        public List<FormatLocation> InternalFormats { get; set; }

        public bool IsLoaded { get; private set; }

        public KnownPaperFormat MainFormat { get; set; }

        public int Width => this.MainFormat.Width;

        public int Height => this.MainFormat.Height;

        public float WidthF => this.MainFormat.WidthF;

        public float HeightF => this.MainFormat.HeightF;

        public bool CanDistributePage(SizeF pageSize)
        {
            KnownPaperFormat aptFormat = LayoutsUtils.FindAptPageFormat(pageSize);
            if (aptFormat == null)
                return false;
            return this.InternalFormats.FirstOrDefault<FormatLocation>((Func<FormatLocation, bool>)(format => format.Format.BaseName == aptFormat.BaseName)) != null || this.CanDistributePage(new SizeF((float)(aptFormat.Width + 1), (float)(aptFormat.Height + 1)));
        }

        public string CreateXml()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", (string)null);
            XmlElement documentElement = xmlDocument.DocumentElement;
            xmlDocument.InsertBefore((XmlNode)xmlDeclaration, (XmlNode)documentElement);
            XmlElement element1 = xmlDocument.CreateElement(string.Empty, LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Layout], string.Empty);
            element1.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName], this.MainFormat.FullName);
            element1.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.IsPortrait], this.MainFormat.IsPortait.ToString());
            xmlDocument.AppendChild((XmlNode)element1);
            foreach (FormatLocation internalFormat in this.InternalFormats)
            {
                XmlElement element2 = xmlDocument.CreateElement(string.Empty, LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Input], string.Empty);
                element2.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName], internalFormat.Format.BaseName);
                element2.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.IsPortrait], internalFormat.Format.IsPortait.ToString());
                element2.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Left], internalFormat.Left.ToString());
                element2.SetAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Top], internalFormat.Top.ToString());
                element1.AppendChild((XmlNode)element2);
            }
            return xmlDocument.InnerXml;
        }

        public void SetMainFormat(string mainFormatName, bool isPortrait = true)
        {
            this.MainFormat = KnownPaperFormats.GetFormat(mainFormatName);
        }

        public void LoadFromXml(string xmlContent)
        {
            this.IsLoaded = false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.InnerXml = xmlContent;
            if (!this.IsXmlCorrect(xmlDocument))
                return;
            this.InternalFormats = new List<FormatLocation>();
            string str1 = xmlDocument.DocumentElement.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName]].Value;
            if (!KnownPaperFormats.IsFormatName(str1))
                return;
            bool boolean1 = Convert.ToBoolean(xmlDocument.DocumentElement.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.IsPortrait]].Value);
            this.SetMainFormat(str1, boolean1);
            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.SelectNodes($"/{LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Layout]}/{LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Input]}").OfType<XmlNode>())
            {
                string str2 = xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName]].Value;
                if (!KnownPaperFormats.IsFormatName(str2))
                    return;
                bool boolean2 = Convert.ToBoolean(xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.IsPortrait]].Value);
                this.InternalFormats.Add(new FormatLocation()
                {
                    Left = Convert.ToInt32(xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Left]].Value),
                    Top = Convert.ToInt32(xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Top]].Value),
                    Format = KnownPaperFormats.GetFormat(str2, boolean2)
                });
            }
            this.IsLoaded = true;
        }

        public override bool Equals(object obj)
        {
            return obj is LayoutDescriptor layoutDescriptor && this.Caption == layoutDescriptor.Caption && this.InternalFormats.All<FormatLocation>(new Func<FormatLocation, bool>(layoutDescriptor.InternalFormats.Contains)) && this.InternalFormats.Count == layoutDescriptor.InternalFormats.Count && this.MainFormat.Equals((object)layoutDescriptor.MainFormat);
        }

        public override int GetHashCode()
        {
            return ((-455804880 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Caption)) * -1521134295 + EqualityComparer<List<FormatLocation>>.Default.GetHashCode(this.InternalFormats)) * -1521134295 + EqualityComparer<KnownPaperFormat>.Default.GetHashCode(this.MainFormat);
        }

        public override string ToString() => this.Caption;

        private bool IsXmlCorrect(XmlDocument xmlDocument)
        {
            if (!xmlDocument.DocumentElement.HasAttribute(LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName]) || !xmlDocument.DocumentElement.HasChildNodes)
                return false;
            XmlNodeList source = xmlDocument.DocumentElement.SelectNodes($"/{LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Layout]}/{LayoutDescriptor.xmlElementNames[LayoutDescriptor.XmlElementNames.Input]}");
            if (source.Count == 0)
                return false;
            foreach (XmlNode xmlNode in source.OfType<XmlNode>())
            {
                if (xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.FormatName]] == null || xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.IsPortrait]] == null || xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Left]] == null || xmlNode.Attributes[LayoutDescriptor.xmlAttributeNames[LayoutDescriptor.XmlAttributeNames.Top]] == null)
                    return false;
            }
            return true;
        }

        private enum XmlAttributeNames
        {
            LayoutName,
            FormatName,
            IsPortrait,
            Left,
            Top,
        }

        private enum XmlElementNames
        {
            Layout,
            Input,
        }
    }
}
