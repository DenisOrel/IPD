namespace Intermech.Interfaces.Sales;

/// <summary>объект описывающий продукт</summary>
public class ProductObject : CustomProductClass
{
  public string CodeAndVersion;
  public string Name;
  public string FullName;
  public int AlgorithmNumber;
  public int LicenceType;

  public void ProductDataInit(
    long objectId,
    string caption,
    string description,
    string codeAndVersion,
    string name,
    string fullName,
    int algorithmNumber,
    int licenceType)
  {
    this.ObjectId = objectId;
    this.Caption = caption;
    this.Description = description;
    this.CodeAndVersion = codeAndVersion;
    this.Name = name;
    this.FullName = fullName;
    this.AlgorithmNumber = algorithmNumber;
    this.LicenceType = licenceType;
  }
}
