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

        private static void ProcessJsonItem(JToken item, Document document)
        {
            var type = item.Type.ToString();
            var tt = item.Children();
            var t3 = item.Values();
            var t4 = item.Count();

            int? itemChildsCount = 0;

            if (item is JObject)
            {
                itemChildsCount = item["item"]?.Count();
            }

            switch (type)
            {
                //case "Property":
                //    if (item["item"] != null)
                //    {
                //        document.Add(new Paragraph($"Request: {item["name"]?.ToString()}"));
                //    }
                //    break;

                case "Array":
                    if (item.Next is null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            var ttt = item[i];
                            if (ttt != null)
                            {
                                ProcessJsonItem(item[i], document);
                            }
                        }
                    }
                    break;

                case "Object":
                    if (item is JObject jsonObject && jsonObject.ContainsKey("request"))
                    {
                        document.Add(new Paragraph($"       Request: {item["name"]?.ToString()}"));
                    }
                    else
                    {
                        document.Add(new Paragraph($"Folder: {item["name"]?.ToString()}"));
                        ProcessJsonItem(item["item"], document);
                    }
                    break;

                default:
                    Console.WriteLine("DDDDDd");
                    break;
            }

            /*
			if (!(item is JArray))
			{
                if (item["item"] != null)
                {
                    document.Add(new Paragraph($"Folder: {item["name"]?.ToString()}"));
                    ProcessJsonItem(item["item"], document);
                }
            }
			else
			{
                // Item name
                string itemName = item[0]["name"]?.ToString();
                if (!string.IsNullOrEmpty(itemName))
                {
                    document.Add(new Paragraph($"Request: {itemName}"));
                }
            }
			*/
        }
    }
}