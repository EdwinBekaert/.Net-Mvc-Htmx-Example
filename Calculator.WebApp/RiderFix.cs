/*
    This file exists to resolve some files in Rider IDE.
 */

using JetBrains.Annotations;

[assembly: AspMvcViewLocationFormat("~/Features/Shared/{0}.cshtml")]
[assembly: AspMvcPartialViewLocationFormat("~/Features/Shared/{0}.cshtml")]
[assembly: AspMvcViewLocationFormat("~/Features/{1}/{0}.cshtml")]
[assembly: AspMvcPartialViewLocationFormat("~/Features/{1}/{0}.cshtml")]
[assembly: AspMvcMasterLocationFormat("~/Features/{1}/{0}.cshtml")]

namespace Calculator.WebApp;

public class RiderFix
{
    
}
