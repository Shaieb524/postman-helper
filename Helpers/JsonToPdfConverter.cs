﻿using System;
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
                ProcessRequestFormat(req, document, indent);
            }
        }

        private static void ProcessRequestFormat(JToken requestItem, Document document, string indent)
        {
            document.Add(new Paragraph($"{indent}Request: {requestItem["name"]?.ToString()}"));
            indent += "  ";
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
                document.Add(new Paragraph($"{indent}Auth: {requestItem["auth"]}"));
            }

            document.Add(new Paragraph($"{indent}Method: {requestItem["method"]}"));
        }
    }
}

// TODO find url and enhance request formatting