using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _100autotjek.Helpers;
using _100autotjek.OCR.OCRResult;

namespace _100autotjek.OCR
{
    public static class TextRecognizer
    {
        private const string LINK = "https://api.ocr.space/Parse/Image";
        private const int LIMIT = 1024 * 1024; // 1 MB
        private const string APIKEY = "c0c1ac8dbe88957";
        private const string LANGUAGE = "eng";

        public static async Task<string> GetCPRNumber(Stream stream, string template)
        {
            if (stream.Length > LIMIT)
            {
                return string.Empty;
            }

            var buffer = await ImageConverter.ToBytesAsync(stream); ;

            var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, 30) };

            var form = new MultipartFormDataContent
            {
                {new StringContent(APIKEY), "apikey"},
                {new StringContent(LANGUAGE), "language"},
            };

            if (buffer != null)
            {
                form.Add(new ByteArrayContent(buffer, 0, buffer.Length), "image", "image.jpg");
            }

            var response = await httpClient.PostAsync(LINK, form);

            var strContent = await response.Content.ReadAsStringAsync();

            var ocrResult = JsonConvert.DeserializeObject<RootObject>(strContent);

            var cprNumber = string.Empty;

            if (ocrResult.OCRExitCode == 1)
            {
                var resultBulder = new StringBuilder();

                foreach (var text in ocrResult.ParsedResults)
                {
                    resultBulder.Append(text.ParsedText);
                }

                var section = resultBulder.ToString();

                if (!string.IsNullOrEmpty(section))
                {
                    cprNumber = Regex.Match(section, template).Value;
                }
            }

            return cprNumber;
        }
    }
}