﻿using System;
using System.Threading.Tasks;
using CompanyName.ProjectName.Permissions;
using CompanyName.ProjectName.Users;
using CompanyName.ProjectName.Users.Dtos;
using Lion.Abp.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("Users")]
    [Authorize(Policy = IdentityPermissions.Users.Default)]
    public class UserContoller:ProjectNameController,IUserAppService
    {
        private readonly IUserAppService _userAppService;

        public UserContoller(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取用户信息", Tags = new[] { "Users" })]
        public Task<PagedResultDto<IdentityUserDto>> ListAsync(PagingUserListInput input)
        {
            return _userAppService.ListAsync(input);
        }
        
        [HttpPost("create")]
        [Authorize(IdentityPermissions.Users.Create)]
        [SwaggerOperation(summary: "创建用户", Tags = new[] { "Users" })]
        public Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return _userAppService.CreateAsync(input);
        }
        
        [HttpPost("update")]
        [Authorize(IdentityPermissions.Users.Update)]
        [SwaggerOperation(summary: "编辑用户", Tags = new[] { "Users" })]
        public Task<IdentityUserDto> UpdateAsync(UpdateUserInput input)
        {
            return _userAppService.UpdateAsync(input);
        }
        
        [HttpPost("delete")]
        [Authorize(IdentityPermissions.Users.Update)]
        [SwaggerOperation(summary: "删除用户", Tags = new[] { "Users" })]
        public Task DeleteAsync(IdInput input)
        {
            return _userAppService.DeleteAsync(input);
        }


        [HttpPost("role")]
        [SwaggerOperation(summary: "获取用户角色信息", Tags = new[] { "Users" })]
        public Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(IdInput input)
        {
            return _userAppService.GetRoleByUserId(input);
        }

        [HttpPost("changePassword")]
        [SwaggerOperation(summary: "修改当前用户密码", Tags = new[] { "Users" })]
        public Task<bool> ChangePasswordAsync(ChangePasswordInput input)
        {
            return _userAppService.ChangePasswordAsync(input);
        }

        [HttpPost("lock")]
        [Authorize(ProjectNamePermissions.SystemManagement.UserEnable)]
        [SwaggerOperation(summary: "锁定用户", Tags = new[] { "Users" })]
        public Task LockAsync(LockUserInput input)
        {
            return _userAppService.LockAsync(input);
        }
    }
}