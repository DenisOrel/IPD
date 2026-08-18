using System.ComponentModel;


namespace IMClient
{
    internal class CustomDisplayName : DisplayNameAttribute
    {
      public CustomDisplayName(string displayName)
      {
        this.DisplayNameValue = LocalizationHolder.rma.GetString(displayName);
      }
    }
}
