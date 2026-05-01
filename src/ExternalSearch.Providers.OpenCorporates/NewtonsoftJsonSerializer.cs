// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewtonsoftJsonSerializer.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the newtonsoft JSON serializer class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Serializers;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public class NewtonsoftJsonSerializer : IRestSerializer, ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public ContentType ContentType { get; set; } = ContentType.Json;

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public ISerializer Serializer => this;
        public IDeserializer Deserializer => this;

        public string[] AcceptedContentTypes => ContentType.JsonAccept;
        public SupportsContentType SupportsContentType => contentType => contentType.Value.EndsWith("json", System.StringComparison.InvariantCultureIgnoreCase);
        public DataFormat DataFormat => DataFormat.Json;

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    this.serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public string Serialize(Parameter parameter) => Serialize(parameter.Value);

        public T Deserialize<T>(RestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return this.serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer() {
                                                                                             NullValueHandling = NullValueHandling.Ignore,
                                                                                         });
            }
        }
    }
}
