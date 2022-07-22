using System.Globalization;
using CsvHelper;

public class CsvProcessor : ICsvProcessor
{
    private IEnumerable<DataOutput> dataOutputs = Enumerable.Empty<DataOutput>();
    public async Task<bool> Process(string fileName)
    {
        var result = false;
        try
        {
            using (var reader = new StreamReader($"DataFiles/Input/{fileName}"))
            {
                await reader.ReadLineAsync();
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    dataOutputs = csv.GetRecords<DataOutput>().ToList();
                }
            }
            result = true;
        }
        catch (System.Exception)
        {

        }
        return result;
    }

    public async Task<bool> Save(string fileName)
    {
        var result = false;
        try
        {
            using (var writer = new StreamWriter($"DataFiles/Output/{fileName}"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(dataOutputs);
            }
            result = true;
        }
        catch (System.Exception)
        {

        }
        return result;
    }
}