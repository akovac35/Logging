# Logging
This library contains helpful resources for .NET Core and ASP.NET Core logging using various logger frameworks.

![this](Resources/.NET_Core_Logo_small.png)

* [com.github.akovac35.Logging](https://www.nuget.org/packages/com.github.akovac35.Logging/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.Logging.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.Logging/) [![NuGet Downloads](https://img.shields.io/nuget/dt/com.github.akovac35.Logging.svg)](https://www.nuget.org/packages/com.github.akovac35.Logging/) 


* [com.github.akovac35.Logging.NLog](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.Logging.NLog.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog/) [![NuGet Downloads](https://img.shields.io/nuget/dt/com.github.akovac35.Logging.NLog.svg)](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog/) 

* [com.github.akovac35.Logging.NLog.AspNetCore](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog.AspNetCore/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.Logging.NLog.AspNetCore.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog.AspNetCore/) [![NuGet Downloads](https://img.shields.io/nuget/dt/com.github.akovac35.Logging.NLog.AspNetCore.svg)](https://www.nuget.org/packages/com.github.akovac35.Logging.NLog.AspNetCore/) 

* [com.github.akovac35.Logging.Serilog](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.Logging.Serilog.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog/) [![NuGet Downloads](https://img.shields.io/nuget/dt/com.github.akovac35.Logging.Serilog.svg)](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog/) 

* [com.github.akovac35.Logging.Serilog.AspNetCore](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog.AspNetCore/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.Logging.Serilog.AspNetCore.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog.AspNetCore/) [![NuGet Downloads](https://img.shields.io/nuget/dt/com.github.akovac35.Logging.Serilog.AspNetCore.svg)](https://www.nuget.org/packages/com.github.akovac35.Logging.Serilog.AspNetCore/) 

## Contents

The following functionality is provided for Microsoft.Extensions.Logging:

* ```ILogger``` extensions for method entry / exit logging:

	```cs
    public IEnumerable<Blog> Find(string term)
    {
        _logger.Entering(term);

        var tmp = Context.Blogs
            .Where(b => b.Url.Contains(term))
            .OrderBy(b => b.Url)
            .ToList();

        _logger.Exiting(tmp);
        return tmp;
    }
	```

* ```LoggerFactoryProvider``` for straightforward access to an instance of ```ILoggerFactory```,

* ```LoggerHelper<T>``` using which it is possible to log application startup events:

    ```cs
    using com.github.akovac35.Logging;
    using Microsoft.Extensions.Logging;
    using System;
    using static com.github.akovac35.Logging.LoggerHelper<ConsoleApp.Program>;

    namespace ConsoleApp
    {
        public class Program
        {
            public static async Task Main(string[] args)
            {
                // Configure logger framework here

                Logger.Entering(args);

                try
                {
                    // Perform work here

                    Logger.Exiting(args);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    // Dispose logger framework here
                }
            }
        }
    }
    ```

* invocation context functions for ```ILogger``` and ```LoggerHelper<T>```. The following call will contain **compile time** log scope state using which it is possible to log method name and source code line number: 
  
    ```cs
    _logger.Here(l => l.LogDebug("{@disposedValue}", disposedValue));
    ```

* ```CorrelationProvider```, ```CorrelationIdMiddleware``` and other code for correlating log entries and requests, or log scopes,
* logger framework specifics:
  * Serilog enricher and NLog layout renderer utilizing ```CorrelationProvider```,
  * Serilog configuration monitor for settings file updates. 

## Samples

Advanced samples utilizing the listed functionality are provided here: [Logging.Samples](https://github.com/akovac35/Logging.Samples)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[Apache-2.0](LICENSE)