using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace GoodM8s.TheCup {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Priority = 9,
                    Route = new Route(
                        "GoodM8s.TheCup/ScoreAdmin/Create/{cupId}",
                        new RouteValueDictionary {
                            {"area", "GoodM8s.TheCup"},
                            {"controller", "ScoreAdmin"},
                            {"action", "Create"},
                            {"cupId", 0}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "GoodM8s.TheCup"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Priority = 9,
                    Route = new Route(
                        "TheCup/Results/{cupId}",
                        new RouteValueDictionary {
                            {"area", "GoodM8s.TheCup"},
                            {"controller", "Home"},
                            {"action", "Results"},
                            {"cupId", UrlParameter.Optional}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "GoodM8s.TheCup"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}