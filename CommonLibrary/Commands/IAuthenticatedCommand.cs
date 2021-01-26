using System;

namespace Library.Common.Commands
{
    interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
