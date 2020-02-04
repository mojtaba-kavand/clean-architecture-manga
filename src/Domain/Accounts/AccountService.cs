﻿// <copyright file="AccountService.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Domain.Accounts
{
    using System.Threading.Tasks;
    using Domain.Accounts.Credits;
    using Domain.Accounts.Debits;
    using Domain.Accounts.ValueObjects;
    using Domain.Customers.ValueObjects;

    /// <summary>
    /// Account <see href="https://github.com/ivanpaulovich/clean-architecture-manga/wiki/Domain-Driven-Design-Patterns#domain-service">Domain Service Domain-Driven Design Pattern</see>.
    /// </summary>
    public class AccountService
    {
        private readonly IAccountFactory accountFactory;
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="accountFactory">Account Factory.</param>
        /// <param name="accountRepository">Account Repository.</param>
        public AccountService(
            IAccountFactory accountFactory,
            IAccountRepository accountRepository)
        {
            this.accountFactory = accountFactory;
            this.accountRepository = accountRepository;
        }

        /// <summary>
        /// Open Checking Account.
        /// </summary>
        /// <param name="customerId">Customer Id.</param>
        /// <param name="amount">Amount.</param>
        /// <returns>IAccount created.</returns>
        public async Task<IAccount> OpenCheckingAccount(CustomerId customerId, PositiveMoney amount)
        {
            var account = this.accountFactory.NewAccount(customerId);
            var credit = account.Deposit(this.accountFactory, amount);
            await this.accountRepository.Add(account, credit);

            return account;
        }

        /// <summary>
        /// Withdrawls from Account.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <param name="amount">Amount.</param>
        /// <returns>Debit Transaction.</returns>
        public async Task<IDebit> Withdraw(IAccount account, PositiveMoney amount)
        {
            var debit = account.Withdraw(this.accountFactory, amount);
            await this.accountRepository.Update(account, debit);

            return debit;
        }

        /// <summary>
        /// Deposits into Account.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <param name="amount">Amount.</param>
        /// <returns>Credit Transaction.</returns>
        public async Task<ICredit> Deposit(IAccount account, PositiveMoney amount)
        {
            var credit = account.Deposit(this.accountFactory, amount);
            await this.accountRepository.Update(account, credit);

            return credit;
        }
    }
}
