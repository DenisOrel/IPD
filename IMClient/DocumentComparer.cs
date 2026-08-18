
// Type: IMClient.DocumentComparer




using System.Collections;
using System.Windows.Forms;


namespace IMClient
{
    internal class DocumentComparer : IComparer
    {
      internal string RemoveSign(string str)
      {
        if (str.StartsWith(MainForm.ProxyPrefix))
          str = str.Substring(2);
        return str;
      }

      public int Compare(object x, object y)
      {
        return string.Compare(this.RemoveSign(((Control) x).Text), this.RemoveSign(((Control) y).Text));
      }
    }
}
