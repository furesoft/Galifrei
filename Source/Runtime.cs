﻿using Avalonia;
using Avalonia.Logging.Serilog;
using Galifrei.Core;
using Galifrei.Core.Interfaces;
using Galifrei.Core.Platforming;
using System;

namespace Galifrei
{
    internal static class Runtime
    {
        public static SetupContext Context = new SetupContext();

        public static void Run(string[] args)
        {
            var cli = new CommandlineArguments(args);

            InstallMode mode = InstallMode.Install;

            var runtimeInfo = Platform.New<IRuntimeInfo>();

            if (cli.GetValue<string>("mode") != null)
            {
                mode = (InstallMode)Enum.Parse(typeof(InstallMode), cli.GetValue<string>("mode"), true);
            }
            else
            {
                mode = runtimeInfo.IsApplicationInstalled(Context) ? InstallMode.Uninstall : InstallMode.Install;
            }

            Context.Properties.SetProperty("mode", mode);
            Context.Properties.SetProperty("silent", cli.HasOption("silent"));

            if (!cli.HasOption("silent"))
            {
                //run ui mode
                AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .LogToDebug()
                        .StartWithClassicDesktopLifetime(args);
            }
            else
            {
                // run cmd mode

                if (mode == InstallMode.Install)
                {
                    Context.Pipeline.Install(Context);
                }
                else if (mode == InstallMode.Uninstall)
                {
                    Context.Pipeline.Uninstall(Context);
                }
                else if (mode == InstallMode.Upgrade)
                {
                    Context.Pipeline.Upgrade(Context);
                }
            }
        }
    }
}