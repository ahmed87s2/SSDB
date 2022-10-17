using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Application.Requests;
using SSDB.Client.Extensions;
using SSDB.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using SSDB.Client.Infrastructure.Managers.Catalog.University;
using SSDB.Client.Infrastructure.Managers.Catalog.Student;

namespace SSDB.Client.Pages.Catalog
{
    public partial class AddEditStudentModal
    {
        [Inject] private IStudentManager StudentManager { get; set; }
        [Inject] private IUniversityManager UniversityManager { get; set; }

        [Parameter] public AddEditStudentCommand AddEditStudentModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllUniversitiesResponse> _Universities = new();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await StudentManager.SaveAsync(AddEditStudentModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await LoadImageAsync();
            await LoadUniversitiesAsync();
        }

        private async Task LoadUniversitiesAsync()
        {
            var data = await UniversityManager.GetAllAsync();
            if (data.Succeeded)
            {
                _Universities = data.Data;
            }
        }

        private async Task LoadImageAsync()
        {
            var data = await StudentManager.GetStudentImageAsync(AddEditStudentModel.Id);
            if (data.Succeeded)
            {
                var imageData = data.Data;
                if (!string.IsNullOrEmpty(imageData))
                {
                    AddEditStudentModel.ImageDataURL = imageData;
                }
            }
        }

        private void DeleteAsync()
        {
            AddEditStudentModel.ImageDataURL = null;
            AddEditStudentModel.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                AddEditStudentModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditStudentModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Student, Extension = extension };
            }
        }

        private async Task<IEnumerable<int>> SearchUniversities(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _Universities.Select(x => x.Id);

            return _Universities.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}