// <copyright file="EmptyCustomerIdException.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Domain.Customers.ValueObjects
{
    /// <summary>
    /// EmptyCustomerIdException.
    /// </summary>
    internal sealed class EmptyCustomerIdException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCustomerIdException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        internal EmptyCustomerIdException(string message)
            : base(message)
        {
        }
    }
}
