﻿using CompanyName.ProjectName.DataDictionaryManagement;
using CompanyName.ProjectName.ElasticsearchRepository;
using CompanyName.ProjectName.FreeSqlRepository;
using CompanyName.ProjectName.NotificationManagement;
using EasyAbp.Abp.SettingUi;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace CompanyName.ProjectName
{
    [DependsOn(
        typeof(ProjectNameDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(ProjectNameApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(AbpSettingUiApplicationModule),
        typeof(FreeSqlRepositoryModule),
        typeof(ProjectNameElasticSearchModule)
        )]
    public class ProjectNameApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ProjectNameApplicationModule>();
            });
        }
    }
}
