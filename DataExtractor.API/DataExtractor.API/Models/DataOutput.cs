using CsvHelper.Configuration.Attributes;

public class DataOutput
{
    [Name("ISIN")]
    public string ISIN { get; set; }
    [Name("CFICode")]
    public string CFICode { get; set; }
    [Name("Venue")]
    public string Venue { get; set; }
    private string _contractSize;
    [Index(35)]
    public string ContractSize
    {
        get
        {
            return _contractSize;
        }
        set
        {
            _contractSize = StringParserUtility.GetKeyValueFromString(value, ColumnConstants.PriceMultiplier);
        }
    }
}