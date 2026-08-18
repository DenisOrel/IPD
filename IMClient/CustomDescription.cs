
// Type: IMClient.CustomDescription




using System.ComponentModel;


namespace IMClient
{
    internal class CustomDescription : DescriptionAttribute
    {
      public CustomDescription(string description)
      {
        if (LocalizationHolder.rma.GetString(description) != null)
          this.DescriptionValue = LocalizationHolder.rma.GetString(description);
        else
          this.DescriptionValue = string.Empty;
      }
    }
}
