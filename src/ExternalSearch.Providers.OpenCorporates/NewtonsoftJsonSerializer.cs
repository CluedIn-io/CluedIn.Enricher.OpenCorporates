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
    // RestSharp's serializer interfaces changed shape between the 106.x line (CluedIn 4.7/4.8,
    // net6.0) and the 114.x line (CluedIn 5.0, net10.0) - ContentType went from a string to a
    // struct, IDeserializer/IRestSerializer moved namespaces, and IRestResponse<T> was replaced by
    // a concrete RestResponse<T>. Two full implementations rather than #if-riddling individual
    // members, since almost every member signature differs.
#if CLUEDIN_V50
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
#else
    public class NewtonsoftJsonSerializer : ISerializer, RestSharp.Deserializers.IDeserializer, RestSharp.Serialization.IRestSerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType { get; set; } = "application/json";

        public string[] SupportedContentTypes => new[] { "application/json", "text/json", "text/x-json", "text/javascript", "*+json" };

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

        public T Deserialize<T>(IRestResponse response)
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
#endif
}
