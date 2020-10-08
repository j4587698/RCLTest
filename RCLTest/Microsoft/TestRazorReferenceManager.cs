using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RCLTest;

namespace Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
{
    public class TestRazorReferenceManager
    {
        public static void Test(string path, ApplicationPartManager partManager)
        {
            var a = Startup.Service.BuildServiceProvider().GetService<IViewCompilerProvider>() as MyViewCompilerProvider;
            a.Modify();
            // var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.StartsWith("Microsoft.AspNetCore.Mvc.Razor,"));
            // if (assembly == null)
            // {
            //     return;
            // }
            //
            // var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Razor.Compilation.DefaultViewCompiler");
            // if (type == null)
            // {
            //     return;
            // }
            //
            // var field = type.GetField("_compiledViews", BindingFlags.NonPublic | BindingFlags.Instance);
            // var a = Startup.Service.BuildServiceProvider().GetService<IViewCompilerProvider>();
            // var b = field.GetValue(a.GetCompiler()) as Dictionary<string, Task<CompiledViewDescriptor>>;
            //RazorReferenceManager
            //var views = new ViewsFeature();
            // foreach (var provider in partManager.ApplicationParts.OfType<IRazorCompiledItemProvider>())
            // {
            //     foreach (var item in provider.CompiledItems)
            //     {
            //         var descriptor = new CompiledViewDescriptor(item);
            //         views.ViewDescriptors.Add(descriptor);
            //     }
            // }
            // partManager.PopulateFeature(views);
        }
    }
}