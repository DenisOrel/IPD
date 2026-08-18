
// Type: Intermech.PdfPrintCenter.Utils.RenamedLayout





namespace Intermech.PdfPrintCenter.Utils
{
    internal class RenamedLayout
    {
      public RenamedLayout(string oldName, string newName)
      {
        this.OldName = oldName;
        this.NewName = newName;
      }

      public string OldName { get; set; }

      public string NewName { get; set; }
    }
}
