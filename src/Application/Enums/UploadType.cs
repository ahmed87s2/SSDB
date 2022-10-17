using System.ComponentModel;

namespace SSDB.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\Students")]
        Student,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}