using NUnit.Framework;
using DataExtractor.API;
using DataExtractor.API.Controllers;
using Moq;

namespace DataExtractor.Tests;

[TestFixture]
public class DataProcessorControllerTest
{
    private readonly DataProcessorController _dataProcessorController;
    private readonly Mock<ICsvProcessor> _csvProcessor = new Mock<ICsvProcessor>();

    public DataProcessorControllerTest()
    {
        _dataProcessorController = new DataProcessorController(_csvProcessor.Object);
    }

    [TestCase("DataExtractor_Example_Input.csv", "DataExtractor_Example_Output.csv", true)]
    [TestCase("Incorrect.csv", "DataExtractor_Example_Output.csv", false)]
    public void ProcessCsv(string inputFileName, string outputFileName, bool expectedOutput)
    {
        _csvProcessor.Setup(service => service.Process(inputFileName)).ReturnsAsync(expectedOutput);
        _csvProcessor.Setup(service => service.Save(outputFileName)).ReturnsAsync(expectedOutput);
        var result = _dataProcessorController.ProcessCsv().Result;
        Assert.AreEqual(result, expectedOutput);
    }
}
