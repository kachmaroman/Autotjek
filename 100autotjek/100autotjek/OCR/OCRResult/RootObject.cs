using System;
using System.Collections.Generic;
using System.Text;

namespace _100autotjek.OCR.OCRResult
{
    public class RootObject
    {
        public ParsedResult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}
