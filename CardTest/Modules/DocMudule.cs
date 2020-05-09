using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Routing;

namespace CardService.Modules
{
   public class DocMudule: NancyModule
    {
        private IRouteCacheProvider _routeCacheProvider;
        public DocMudule(IRouteCacheProvider routeCacheProvider) : base("/docs")
        {
            this._routeCacheProvider = routeCacheProvider;

            Get["/"] = _ =>
            {
                var routeDescriptionList = _routeCacheProvider
                    .GetCache()
                    .SelectMany(x => x.Value)
                    .Select(x => x.Item2)
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                    .ToList();

                return Response.AsJson(routeDescriptionList);
            };
        }
    }
}
