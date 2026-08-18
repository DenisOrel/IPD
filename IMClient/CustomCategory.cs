using System.ComponentModel;


namespace IMClient
{
    internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
    {
      protected override string GetLocalizedString(string value)
      {
        return LocalizationHolder.rma.GetString(value);
      }
    }
}
