using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace WebApiContrib.Core.Formatter.PlainText
{
    public class PlainTextFormatterOptions
    {
        public Encoding[] SupportedEncodings { get; set; } = { Encoding.UTF8 };

        public string[] SupportedMediaTypes { get; set; } = { "text/plain" };
    }

    public class PlainTextInputFormatter : TextInputFormatter
    {
        public PlainTextInputFormatter(PlainTextFormatterOptions opts)
        {
            foreach (var enc in opts.SupportedEncodings)
            {
                SupportedEncodings.Add(enc);
            }

            foreach (var mediaType in opts.SupportedMediaTypes)
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(mediaType));
            }
        }

        public PlainTextInputFormatter() : this(new PlainTextFormatterOptions())
        {
        }

        protected override bool CanReadType(Type type)
        {
            return type.IsAssignableFrom(typeof(string));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            string value;
            using (var reader = new StreamReader(request.Body))
            {
                value = await reader.ReadToEndAsync();
            }

            return InputFormatterResult.Success(value);
        }
    }
}