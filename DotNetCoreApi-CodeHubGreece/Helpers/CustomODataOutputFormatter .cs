using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreApi_CodeHubGreece.Helpers
{
    public class CustomODataOutputFormatter : ODataOutputFormatter
    {

        private readonly JsonSerializer serializer;

        public CustomODataOutputFormatter() : base(new[] { ODataPayloadKind.Error })
        {
            this.serializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            this.SupportedMediaTypes.Add("application/json");
            this.SupportedEncodings.Add(new UTF8Encoding());
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (!(context.Object is SerializableError serializableError))
            {
                return base.WriteResponseBodyAsync(context, selectedEncoding);
            }

            var error = serializableError.CreateODataError();
            using (var writer = new StreamWriter(context.HttpContext.Response.Body))
            {
                this.serializer.Serialize(writer, error);
                return writer.FlushAsync();
            }
        }

    }

    internal static class FormatterExtensions
    {

        public const string DefaultErrorCode = "500";
        public const string DefaultErrorMessage = "Internal Server Error";

        public static ODataError CreateODataError(this SerializableError serializableError)
        {
            var convertedError = SerializableErrorExtensions.CreateODataError(serializableError);
            var error = new ODataError
            {
                ErrorCode = DefaultErrorCode,
                Message = DefaultErrorMessage,
                Details = new[] { new ODataErrorDetail { Message = convertedError.Message } }
            };

            return error;
        }

        public static ODataError CreateODataError(this Exception ex)
        {
            var error = new ODataError
            {
                ErrorCode = DefaultErrorCode,
                Message = DefaultErrorMessage,
                Details = new[] { new ODataErrorDetail { Message = ex.Message } }
            };

            return error;
        }
    }

}
