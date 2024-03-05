using System;
using System.IO;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities;
using System.Xml.Linq;

namespace postman_helper.Helpers
{
    public class JsonToPdfConverter
    {
        public JsonToPdfConverter()
        {
        }

        public static void ConvertPostmanCollectionToPdf(string jsonFilePath, string pdfFilePath)
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var jsonObject = JObject.Parse(jsonContent);

            using (Document document = new Document())
            {
                PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
                document.Open();

                if (jsonObject["info"]?["name"] != null)
                {
                    document.Add(new Paragraph($"Collection {jsonObject["info"]?["name"]}"));
                    document.Add(new Paragraph(""));
                }
                var postmantCollection = jsonObject["item"];

                if (postmantCollection is JArray items)
                {
                    foreach (var item in items)
                    {
                        ProcessJsonItem(item, document);
                    }
                }

                document.Close();
            }
        }

        private static void ProcessJsonItem(JToken item, Document document, string indent = "")
        {
            // item is a folder
            if (item["item"] != null)
            {
                document.Add(new Paragraph($"{indent}Folder: {item["name"]?.ToString()}"));
                indent += "  "; 

                foreach (var subItem in item["item"])
                {
                    ProcessJsonItem(subItem, document, indent);
                }
            }
            // item is a request
            else if (item["request"] != null)
            {
                document.Add(new Paragraph($"{indent}Request: {item["name"]?.ToString()}"));
            }
        }
    }
}