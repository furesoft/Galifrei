﻿using Galifrei.Core.Interfaces;
using Galifrei.Core.Platforming;
using Galifrei.Core.SetupContextStorages;
using System.IO;

namespace Galifrei.Core
{
    public class SetupContext
    {
        public PropertyStore Properties = new PropertyStore();
        public ComponentStore Components = new ComponentStore();
        public ResourceLoaderStore ResourceLoaders = new ResourceLoaderStore();
        public EventStore Events = new EventStore();

        public InstallActionPipeline Pipeline = new InstallActionPipeline();

        public Terminal Shell = new Terminal();

        public Progress CurrentProgress = new Progress();

        public dynamic PropertyBinder;

        public bool IsSilent => (bool)Properties[NamingConstants.Silent];

        public IDefaultPaths Paths;
        private string _tmpDir;

        public string TempDirectory => _tmpDir;

        public DirectoryInfo TmpDir => new DirectoryInfo(_tmpDir);

        public SetupContext()
        {
            Paths = Platform.New<IDefaultPaths>();

            _tmpDir = Utils.GetTempPath();

            PropertyBinder = new DynamicPropertyObject(this);
        }

        public void ExecuteComponentInstall(ISetupComponent component)
        {
            // if component is IVisualComponent and not in silent mode then add ui
            component.OnInstall(this);
        }

        public void ExecuteComponentUninstall(ISetupComponent component)
        {
            // if component is IVisualComponent and not in silent mode then add ui
            component.OnUninstall(this);
        }

        public void ExecuteComponentUpgrade(ISetupComponent component)
        {
            // if component is IVisualComponent and not in silent mode then add ui
            component.OnUninstall(this);
        }
    }
}