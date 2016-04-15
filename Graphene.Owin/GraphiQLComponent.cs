using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Graphene.Core;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Owin.Spike;
using Graphene.Spike;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Graphene.Owin
{
    public class GraphiQLComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _appFunc;

        public GraphiQLComponent(Func<IDictionary<string, object>, Task> appFunc)
        {
            _appFunc = appFunc;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (owinContext.Request.Path.Value == "/")
            {
                var html = @"<!DOCTYPE html><html><head><meta charset=""utf-8"" /><meta name=""viewport"" content=""width=device-width"" /><title>GraphiQL</title><link rel=""stylesheet"" href=""/public/codemirror.css"" /><link rel=""stylesheet"" href=""/public/foldgutter.css"" /><link rel=""stylesheet"" href=""/public/lint.css"" /><link rel=""stylesheet"" href=""/public/show-hint.css"" /><link rel=""stylesheet"" href=""/public/app.css"" /></head><body><div class=""container body-content""><div id=""app""></div></div><script src=""/public/bundle.js"" type=""text/javascript""></script></body></html>";
                await owinContext.Response.WriteAsync(html);
            }
            else
            {
                await _appFunc(environment);
            }
        }
    }
}