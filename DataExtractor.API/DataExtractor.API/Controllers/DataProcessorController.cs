using Microsoft.AspNetCore.Mvc;

namespace DataExtractor.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DataProcessorController : ControllerBase
{
    private const string InputFileName = "DataExtractor_Example_Input.csv";
    private const string OutputFileName = "DataExtractor_Example_Output.csv";
    private readonly ICsvProcessor _csvProcessor;
    public DataProcessorController(ICsvProcessor csvProcessor)
    {
        _csvProcessor = csvProcessor;
    }

    [HttpGet("ProcessCsv")]
    public async Task<bool> ProcessCsv()
    {
        var isProcessed = await _csvProcessor.Process(InputFileName);
        var isSaved = isProcessed ? await _csvProcessor.Save(OutputFileName) : false;
        return isProcessed && isSaved;
    }
}
