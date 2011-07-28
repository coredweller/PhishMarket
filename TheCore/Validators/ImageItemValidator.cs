using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Validators
{
    public class ImageItemValidator
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ImageItemValidator(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
