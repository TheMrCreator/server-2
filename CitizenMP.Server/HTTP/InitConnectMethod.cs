﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uhttpsharp.Headers;
using Newtonsoft.Json.Linq;

namespace CitizenMP.Server.HTTP
{
    static class InitConnectMethod
    {
        public static Func<IHttpHeaders, JObject> Get()
        {
            return (headers) =>
            {
                var result = new JObject();

                var name = headers.GetByName("name");
                var guid = headers.GetByName("guid");

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(guid))
                {
                    result["err"] = "fields missing";

                    return result;
                }

                var client = new Client();
                client.Token = TokenGenerator.GenerateToken();
                client.Name = name;
                client.Guid = guid;
                client.Touch();

                ClientInstances.AddClient(client);

                result["token"] = client.Token;

                return result;
            };
        }
    }
}