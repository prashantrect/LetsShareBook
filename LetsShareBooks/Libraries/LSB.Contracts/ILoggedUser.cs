//-----------------------------------------------------------------------
// <copyright file="IUser.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace LSB.Contracts
{
    public interface ILoggedUser
    {
        string Email { get; set; }

        string Name { get; set; }
    }
}
