namespace Mobile.Bff.Shopping.Extensions;

public static class ApiMappingExtensions
{
    internal static WebApplication MapCatalogEndpoints(this WebApplication app)
    {
        return app.MapEndpoints(
            "/api/v1/catalog/",
            new[]
            {
                "items", 
                "items/by", 
                "items/{id}", 
                "items/by/{name}", 
                "items/withsemanticrelevance/{text}",
                
                "items/type/{typeId}/brand/{brandId?}", 
                "items/type/all/brand/{brandId?}", 
                
                "catalogTypes",
                "catalogBrands", 
                
                "items/{id}/pic",
            },
            "/catalog-api",
            "http://catalog-api");
    }

    internal static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        return app.MapEndpoints(
            "/api/v1/orders",
            new []
            {
                "",
                "{orderId}",
                "cancel",
            },
            "/order-api",
            "http://order-api");
    }
    
    internal static WebApplication MapIdentityEndpoints(this WebApplication app)
    {
        return app.MapEndpoints(
            "/",
            new []
            {
                ".well-known/openid-configuration",
                ".well-known/openid-configuration/jwks",
                "connect/authorize",
                "connect/token",
                "connect/userinfo",
                "connect/endsession",
                "connect/checksession",
                "connect/revocation",
                "connect/introspect",
                "connect/deviceauthorization",
                "connect/ciba",
                "connect/par",
            },
            "/identity-api",
            "http://identity-api");
    }
    
    private static WebApplication MapEndpoints(
        this WebApplication app, 
        string apiPrefix, IEnumerable<string> apiRoutesToForward, string mappedApiPath, string destinationPrefix)
    {
        foreach (var apiRoute in apiRoutesToForward)
        {
            var forwardedUrl = Path.Combine(apiPrefix, apiRoute);
            
            var mapFromPattern = mappedApiPath + forwardedUrl;
            var mapToTargetPath = forwardedUrl;

            app.MapForwarder(mapFromPattern, destinationPrefix, mapToTargetPath);
        }

        return app;
    }
}
