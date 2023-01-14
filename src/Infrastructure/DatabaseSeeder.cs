using SSDB.Application.Interfaces.Services;
using SSDB.Infrastructure.Contexts;
using SSDB.Infrastructure.Helpers;
using SSDB.Infrastructure.Models.Identity;
using SSDB.Shared.Constants.Permission;
using SSDB.Shared.Constants.Role;
using SSDB.Shared.Constants.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSDB.Infrastructure
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IStringLocalizer<DatabaseSeeder> _localizer;
        private readonly BlazorHeroContext _db;
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly RoleManager<BlazorHeroRole> _roleManager;

        public DatabaseSeeder(
            UserManager<BlazorHeroUser> userManager,
            RoleManager<BlazorHeroRole> roleManager,
            BlazorHeroContext db,
            ILogger<DatabaseSeeder> logger,
            IStringLocalizer<DatabaseSeeder> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
            _localizer = localizer;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddOtherUsersAndRoles();
            _db.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new BlazorHeroRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    _logger.LogInformation(_localizer["Seeded Administrator Role."]);
                }
                //Check if User Exists
                var superUser = new BlazorHeroUser
                {
                    FirstName = "Emad",
                    LastName = "Elhaj",
                    Email = "emadfrind2all@gmail.com",
                    UserName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddOtherUsersAndRoles()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists

                var rolesToBeCreated = new List<BlazorHeroRole>();
                rolesToBeCreated.Add(new BlazorHeroRole(RoleConstants.InhouseRole, _localizer["Inhouse university role."]));
                rolesToBeCreated.Add(new BlazorHeroRole(RoleConstants.OutSourceRole, _localizer["OutSource university role."]));

                foreach (var role in rolesToBeCreated)
                {
                    var basicRoleInDb = await _roleManager.FindByNameAsync(role.Name);
                    if (basicRoleInDb == null)
                    {
                        await _roleManager.CreateAsync(role);
                        await GetRolePermissionsAsync(role);
                        _logger.LogInformation(_localizer[$"Seeded {role.Name} Role."]);
                    }
                }



                //Seed Users
                var usersToBeCreated = new List<BlazorHeroUser>();
                usersToBeCreated.Add(new BlazorHeroUser
                {
                    FirstName = RoleConstants.InhouseRole,
                    LastName = "User",
                    Email = "inhouse@gmail.com",
                    UserName = "InhouseUser",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                });

                usersToBeCreated.Add(new BlazorHeroUser
                {
                    FirstName = RoleConstants.OutSourceRole,
                    LastName = "User",
                    Email = "outsource@gmail.com",
                    UserName = "OutsourceUser",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                });

                foreach(var user in usersToBeCreated)
                {
                    var basicUserInDb = await _userManager.FindByNameAsync(user.UserName);
                    if (basicUserInDb == null)
                    {
                        await _userManager.CreateAsync(user, UserConstants.DefaultPassword);
                        await _userManager.AddToRoleAsync(user, user.FirstName);
                        _logger.LogInformation(_localizer[$"Seeded User with {user.FirstName} Role."]);
                    }
                }
                
            }).GetAwaiter().GetResult();
        }


        private async Task GetRolePermissionsAsync(BlazorHeroRole role)
        {
            var allowedPermissions = new List<string>();
            if (role.Name == RoleConstants.InhouseRole)
            {                
                allowedPermissions.Add(Permissions.Students.View);
                allowedPermissions.Add(Permissions.Students.Create);
                allowedPermissions.Add(Permissions.Students.Edit);
                allowedPermissions.Add(Permissions.Students.Delete);
                allowedPermissions.Add(Permissions.Students.Export);
                allowedPermissions.Add(Permissions.Students.Search);
                allowedPermissions.Add(Permissions.Registrations.View);
                allowedPermissions.Add(Permissions.Registrations.Search);
                allowedPermissions.Add(Permissions.Registrations.Export);
                allowedPermissions.Add(Permissions.Payments.View);
                allowedPermissions.Add(Permissions.Payments.Search);
                allowedPermissions.Add(Permissions.Payments.Export);
            }
            if (role.Name == RoleConstants.OutSourceRole)
            {
                allowedPermissions.Add(Permissions.Registrations.UpdateInfo);
                allowedPermissions.Add(Permissions.Payments.View);
                allowedPermissions.Add(Permissions.Payments.Search);
                allowedPermissions.Add(Permissions.Payments.Export);
            }

            foreach (var permission in allowedPermissions)
            {
                await _roleManager.AddPermissionClaim(role, permission);
            }
        }
    }
}