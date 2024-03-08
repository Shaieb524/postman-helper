using System;
using System.IO;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities;
using System.Xml.Linq;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;

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
                var req = item["request"];
                var reqName = item["name"].ToString();
                ProcessRequestFormat(req, reqName, document, indent);
            }
        }

        private static void ProcessRequestFormat(JToken requestItem, string reqName, Document document, string indent)
        {
            document.Add(new Paragraph($"{indent}Request: {requestItem["name"]?.ToString()}"));
            indent += "  ";

            document.Add(new Paragraph($"{indent}Name: {reqName}"));

            if (requestItem["description"] != null)
            {
                document.Add(new Paragraph($"{indent}Description: {requestItem["description"]}"));
            }
            else
            {
                document.Add(new Paragraph($"{indent}Description: -----------"));
            }

            if (requestItem["auth"] is null)
            {
                document.Add(new Paragraph($"{indent}Auth: Inherited from parent"));
            }
            else
            {
                // TODO fix switch cases for auth types
                document.Add(new Paragraph($"{indent}Auth: {requestItem["auth"]["bearer"]}"));
            }

            document.Add(new Paragraph($"{indent}Method: {requestItem["method"]}"));
            document.Add(new Paragraph($"{indent}URL: {requestItem["url"]["raw"]}"));
            document.Add(new Paragraph($"-------------------"));
        }
    }
}

// TODO find url and enhance request formatting
// TODO get format from postman print collection pdf