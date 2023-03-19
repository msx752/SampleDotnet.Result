using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace SampleDotnet.Result.Tests.Cases
{
    public class MainControllerTests
    {
        internal readonly JsonSerializerSettings jsonSerializerSettings;

        public MainControllerTests()
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
            };
        }

        internal T Response<T>(HttpResponseMessage response) where T : class
        {
            using (var sr = new StreamReader(response.Content.ReadAsStream()))
            {
                try
                {
                    var resp = sr.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<T>(resp, jsonSerializerSettings);
                    return data;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        internal HttpClient CreateMockApiServer(Action<IServiceCollection> servicesAction, Action<IApplicationBuilder> appAction)
        {
            WebApplicationFactory<Program> app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.UseContentRoot(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MockApiServer"));
                builder.ConfigureTestServices(services =>
                {
                    servicesAction?.Invoke(services);

                    services.AddEndpointsApiExplorer();
                    services.AddControllers().AddNewtonsoftJson();
                });

                builder.Configure((builderContext, app) =>
                {
                    appAction?.Invoke(app);

                    app.UseRouting();
                    app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                    app.UseAuthentication();
                    app.UseAuthorization();

                    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
                });
            });

            return app.CreateClient();
        }
    }
}