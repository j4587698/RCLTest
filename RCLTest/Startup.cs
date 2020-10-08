using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;

namespace RCLTest
{
    public class Startup
    {
        public static IServiceCollection Service;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Service = services;
            //services.TryAddSingleton<IViewCompilerProvider, MyViewCompilerProvider>();
            services.AddRazorPages().AddRazorRuntimeCompilation(setupAction =>
            {
                setupAction.FileProviders.Add(
                    new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test")));
            });
            // }).ConfigureApplicationPartManager(setupAction =>
            // {
            //     var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "RazorClassLibrary1.dll"));
            //     var assemblyView = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "RazorClassLibrary1.Views.dll"));
            //     var viewAssemblyPart = new CompiledRazorAssemblyPart(assemblyView);
            //
            //     var controllerAssemblyPart = new AssemblyPart(assembly);
            //     setupAction.ApplicationParts.Add(controllerAssemblyPart);
            //     setupAction.ApplicationParts.Add(viewAssemblyPart);
            //     
            // });
            services.AddSingleton<IActionDescriptorChangeProvider>(MyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(MyActionDescriptorChangeProvider.Instance);
            services.Replace<IViewCompilerProvider, MyViewCompilerProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
