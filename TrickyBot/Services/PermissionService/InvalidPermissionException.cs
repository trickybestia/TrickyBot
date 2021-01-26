using System;

namespace TrickyBot.Services.PermissionService
{
    public class InvalidPermissionException : Exception
    {
        public string Permission { get; }

        public InvalidPermissionException(string permission) : base($"\"{permission}\" is invalid permission!")
        {
            Permission = permission;
        }
    }
}
