namespace BusinessControlApp;
public static class Routes
{
    public static void ConfigureRoutes(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllerRoute(
            name: "test",
            pattern: "{controller=Test}/{action=Get}");
    }
}




