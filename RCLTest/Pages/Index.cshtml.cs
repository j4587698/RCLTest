using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

namespace RCLTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationPartManager _partManager;
        public IndexModel(ILogger<IndexModel> logger, ApplicationPartManager partManager)
        {
            _logger = logger;
            _partManager = partManager;
        }

        public void OnGet()
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "RazorClassLibrary1.dll"));
            var assemblyView = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "RazorClassLibrary1.Views.dll"));
            var viewAssemblyPart = new CompiledRazorAssemblyPart(assemblyView);
            
            var controllerAssemblyPart = new AssemblyPart(assembly);
            _partManager.ApplicationParts.Add(controllerAssemblyPart);
            _partManager.ApplicationParts.Add(viewAssemblyPart);
            //TestRazorReferenceManager.Test(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "RazorClassLibrary1.Views.dll"), _partManager);
            var a = HttpContext.RequestServices.GetService(typeof(IViewCompilerProvider)) as MyViewCompilerProvider;
            a.Modify();
            MyActionDescriptorChangeProvider.Instance.HasChanged = true;
            MyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            
        }
    }
}
