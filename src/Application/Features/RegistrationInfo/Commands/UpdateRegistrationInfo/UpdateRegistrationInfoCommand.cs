using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Interfaces.Services;
using SSDB.Application.Requests;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using static SSDB.Shared.Constants.Permission.Permissions;
using SSDB.Application.Interfaces.Services.Identity;
using SSDB.Domain.Enums;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace SSDB.Application.Features.Registrations.Commands
{
    public partial class UpdateRegistrationInfoCommand : IRequest<Result<string>>
    {
        public bool IsForAll { get; set; }
        public string StudentId { get; set; }
        public int UniversityId { get; set; }
    }

    internal class UpdateRegistrationInfoCommandHandler : IRequestHandler<UpdateRegistrationInfoCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<UpdateRegistrationInfoCommandHandler> _localizer;

        public UpdateRegistrationInfoCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<UpdateRegistrationInfoCommandHandler> localizer,
            ICurrentUserService currentUser, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<Result<string>> Handle(UpdateRegistrationInfoCommand command, CancellationToken cancellationToken)
        {
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                command.UniversityId = int.Parse(_currentUser.UniversityId);
            }
            var university = await _unitOfWork.Repository<University>()
                .Entities.Include(x => x.Configs)
                .FirstOrDefaultAsync(x => x.Id == command.UniversityId);
            try
            {
                var result = (university.Type == UniversityType.Inhouse.ToString()) ?
                    await GetInhouseReegistrationInfo(command, university) :
                    GetOutSourseRegistrationInfo(command, university);

                if (result != null && result.Count > 0)
                {
                    if (command.IsForAll)
                    {
                        var toBeRemoved = await _unitOfWork.Repository<StudentsRegistrationInfo>().Entities
                            .Where(x => x.UniversityId == command.UniversityId).ToListAsync();
                        await _unitOfWork.Repository<StudentsRegistrationInfo>().DeleteAsync(toBeRemoved);
                    }
                    else
                    {
                        var toBeRemoved = await _unitOfWork.Repository<StudentsRegistrationInfo>().Entities
                            .Where(x => x.UniversityId == command.UniversityId && x.StudentNumber == command.StudentId)
                            .ToListAsync();
                        await _unitOfWork.Repository<StudentsRegistrationInfo>().DeleteAsync(toBeRemoved);
                    }
                }
                result.ForEach(x => x.UniversityId = command.UniversityId);
                await _unitOfWork.Repository<StudentsRegistrationInfo>().AddAsync(result);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<string>.SuccessAsync("Imported: " + result.Count);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<string>.FailAsync(_localizer[$"Received Data is incorect, the expected format is\n {JsonConvert.SerializeObject(ex.Message)}"]);
            }
        }

        private async Task<List<StudentsRegistrationInfo>> GetInhouseReegistrationInfo(UpdateRegistrationInfoCommand command, University university)
        {
            var registrations = _unitOfWork.Repository<Registration>().Entities
                                .Where(x => x.UniversityId == university.Id)
                                .Include(x => x.Student)
                                .ThenInclude(x => x.Fuculty)
                                .Include(x => x.Currency);

            if (command.IsForAll)
            {
                var registrationInfo = _mapper.Map<List<StudentsRegistrationInfo>>(await registrations.ToListAsync());
                return registrationInfo;
            }

            if (string.IsNullOrEmpty(command.StudentId))
            {
                throw new Exception(_localizer["Student Number is required"]);
            }
            var filtered = await registrations.Where(x => x.StudentId == command.StudentId).ToListAsync();
            var result = _mapper.Map<List<StudentsRegistrationInfo>>(filtered);
            return result;
        }

        private List<StudentsRegistrationInfo> GetOutSourseRegistrationInfo(UpdateRegistrationInfoCommand command, University university)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(university.Configs.Token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", university.Configs.Token);
                }
                HttpResponseMessage response = client.GetAsync(university.Configs.Url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resp = response.Content.ReadAsStringAsync().Result;
                    var result = new List<StudentsRegistrationInfo>();

                    try
                    {
                        result = JsonConvert.DeserializeObject<List<StudentsRegistrationInfo>>(resp);
                    }
                    catch (Exception)
                    {
                        throw new Exception(_localizer[$"Received Data is incorect, the expected format is\n {JsonConvert.SerializeObject(result)}"]);
                    }

                    if (!command.IsForAll)
                    {
                        if (string.IsNullOrEmpty(command.StudentId))
                        {
                            throw new Exception(_localizer["Student Number is required"]);
                        }
                        result = result.Where(x => x.StudentNumber == command.StudentId).ToList();
                    }
                    return result;
                }

                throw new Exception(_localizer[$"Host server error, please check endpoint administrator!"]);
            }
        }
    }

}