using System;
using System.Collections.Generic;
using System.Text;

namespace _100autotjek.OCR.OCRResult
{
    public class ParsedResult
    {
        public object FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}
