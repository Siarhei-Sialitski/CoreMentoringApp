using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using CoreMentoringApp.WebSite.Logging;
using CoreMentoringApp.WebSite.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Filters.CustomActionLogger
{
    public class CustomActionLogger : ICustomActionLogger
    {

        private readonly ILogger<CustomActionLogger> _logger;

        private readonly ActionsLoggingOptions _options;

        private Stopwatch _stopwatch;

        public CustomActionLogger(ILogger<CustomActionLogger> logger, IOptionsSnapshot<ActionsLoggingOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void ActionStartLog(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            if (_options.LogActionParameters)
            {
                if (context.ActionArguments.Any())
                {
                    _logger.LogDebug(LogEvents.ActionExecuting, "Executing action {actionName} with following parameters", actionName);
                    foreach (var keyValue in context.ActionArguments)
                    {
                        var name = keyValue.Key;
                        var type = context.ActionDescriptor.Parameters
                            .First(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ParameterType;
                        var value = type.IsValueType ? context.ActionArguments[name] : JsonSerializer.Serialize(context.ActionArguments[name]);
                        var typeName = type.FullName;

                        _logger.LogDebug(LogEvents.ActionExecuting, "Parameter: name '{parameterName}', value '{value}', of type '{typeName}'", name, value, typeName);
                    }
                }
                else
                {
                    _logger.LogDebug(LogEvents.ActionExecuting, "Executing action {actionName} without parameters.", actionName);
                }
            }
            else
            {
                _logger.LogDebug(LogEvents.ActionExecuting, "Executing action {actionName}.", actionName);
            }
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void ActionEndLog(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            long elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogDebug(LogEvents.ActionExecuted, "Executed action {actionName} in {elapsedMilliseconds}ms.", actionName, elapsedMilliseconds);
        }

    }
}
