using Autofac;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.FeatureParser;
using GherkinSyncTool.Interfaces;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Content;
using TestRail;

namespace GherkinSyncTool.DI
{
    public class GherkinSyncToolModule : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            var config = ConfigurationManager.GetConfiguration();
            builder.RegisterType<FeatureFilesGrabber>().As<IFeatureFilesGrabber>().SingleInstance();
            builder.RegisterType<TestRailSynchronizer>().As<ISynchronizer>().SingleInstance();
            builder.Register(c => new TestRailClient(config.TestRailBaseUrl, config.TestRailUserName, config.TestRailPassword)).SingleInstance();
            builder.RegisterType<FeatureParser.FeatureParser>().SingleInstance();
            builder.RegisterType<TestRailClientWrapper>().SingleInstance();
            builder.RegisterType<SectionSynchronizer>().SingleInstance();
            builder.RegisterType<CaseContentBuilder>().SingleInstance();
        }
    }
}