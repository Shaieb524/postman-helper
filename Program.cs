// See https://aka.ms/new-console-template for more information
using postman_helper.Helpers;

var inputFile = "C:\\Users\\robin\\source\\repos\\postman-helper\\Data\\Input\\PortalBackend.postman_collection.json";
var ouputFile = "C:\\Users\\robin\\source\\repos\\postman-helper\\Data\\Output\\PortalBackend.postman_collection.pdf";

JsonToPdfConverter.ConvertPostmanCollectionToPdf(inputFile, ouputFile);

Console.WriteLine("Hello, World!");
