namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class LayoutAsItIs : IPdfPageProducer
    {
        public LayoutAsItIs() => this.Caption = "Как есть";

        public string Caption { get; set; }

        public override string ToString() => this.Caption;
    }
}
