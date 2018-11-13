// -----------------------------------------------------------------------
//  <copyright file="RoleStore.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Data;
using ESoftor.Framework.Infrastructure;
using ESoftor.Permission.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Core.Permission.Identity
{
    public class RoleStore
          : IQueryableRoleStore<Role>,
            IRoleClaimStore<Role>
    {

        private bool _disposed;

        #region ctor
        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = _unitOfWork.Repository<Role, Guid>();
            _roleClaimRepository = _unitOfWork.Repository<RoleClaim, Guid>();

        }
        #endregion

        #region fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<RoleClaim, Guid> _roleClaimRepository;
        #endregion

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _disposed = true;
        }

        #endregion

        #region Implementation of IQueryableRoleStore<Role>

        /// <summary>
        /// Returns an <see cref="T:System.Linq.IQueryable`1" /> collection of roles.
        /// </summary>
        /// <value>An <see cref="T:System.Linq.IQueryable`1" /> collection of roles.</value>
        public IQueryable<Role> Roles => _roleRepository.Query();

        #endregion

        #region Implementation of IRoleStore<Role>

        /// <summary>
        /// Creates a new role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to create in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.</returns>
        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            if (role.IsDefault)
            {
                string defaultRole = _roleRepository.Query(m => m.IsDefault).Select(m => m.Name).FirstOrDefault();
                if (defaultRole != null)
                {
                    return new IdentityResult().Failed($"系统中已存在默认角色“{defaultRole}”，不能重复添加");
                }
            }
            await _roleRepository.InsertAsync(role);
            await _unitOfWork.CommitAsync();
            return IdentityResult.Success;
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.</returns>
        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            if (role.IsDefault)
            {
                var defaultRole = _roleRepository.Query(m => m.IsDefault).Select(m => new { m.ID, m.Name }).FirstOrDefault();
                if (defaultRole != null && !defaultRole.ID.Equals(role.ID))
                {
                    return new IdentityResult().Failed($"系统中已存在默认角色“{defaultRole.Name}”，不能重复添加");
                }
            }
            _roleRepository.Update(role);
            await _unitOfWork.CommitAsync();
            return IdentityResult.Success;
        }

        /// <summary>
        /// Deletes a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to delete from the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.</returns>
        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            _roleRepository.Remove(role);
            await _unitOfWork.CommitAsync();
            return IdentityResult.Success;
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose ID should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the ID of the role.</returns>
        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            return Task.FromResult(ConvertIdToString(role.ID));
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.</returns>
        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            role.Name = roleName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.</returns>
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be set.</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            role.Name = normalizedName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Finds the role who has the specified ID as an asynchronous operation.
        /// </summary>
        /// <param name="roleId">The role ID to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.</returns>
        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            Guid id = ConvertIdFromString(roleId);
            return Task.FromResult(_roleRepository.Query().FirstOrDefault(m => m.ID.Equals(id)));
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="normalizedRoleName">The normalized role name to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.</returns>
        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Task.FromResult(_roleRepository.Query().FirstOrDefault(m => m.Name == normalizedRoleName));
        }

        #endregion

        #region Implementation of IRoleClaimStore<Role>

        /// <summary>
        ///  Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the specified <paramref name="role" /> as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose claims to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
        /// </returns>
        public Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));

            IList<Claim> list = _roleClaimRepository.Query(m => m.RoleId.Equals(role.ID)).Select(n => new Claim(n.ClaimType, n.ClaimValue)).ToList();
            return Task.FromResult(list);
        }

        /// <summary>
        /// Add a new claim to a role as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to add a claim to.</param>
        /// <param name="claim">The <see cref="T:System.Security.Claims.Claim" /> to add.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));
            Check.NotNull(claim, nameof(claim));

            RoleClaim roleClaim = new RoleClaim() { RoleId = role.ID, ClaimType = claim.Type, ClaimValue = claim.Value };
            await _roleClaimRepository.InsertAsync(roleClaim);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Remove a claim from a role as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to remove the claim from.</param>
        /// <param name="claim">The <see cref="T:System.Security.Claims.Claim" /> to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            Check.NotNull(role, nameof(role));
            Check.NotNull(claim, nameof(claim));

            _roleClaimRepository.Remove(m => m.RoleId.Equals(role.ID) && m.ClaimValue == claim.Type && m.ClaimValue == claim.Value);
            await _unitOfWork.CommitAsync();
        }

        #endregion

        /// <summary>
        /// Converts the provided <paramref name="id"/> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An instance of <typeparamref name="TRoleKey"/> representing the provided <paramref name="id"/>.</returns>
        public virtual Guid ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(Guid);
            }
            return (Guid)TypeDescriptor.GetConverter(typeof(Guid)).ConvertFromInvariantString(id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An <see cref="string"/> representation of the provided <paramref name="id"/>.</returns>
        public virtual string ConvertIdToString(Guid id)
        {
            if (id.Equals(default(Guid)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// 如果已释放，则抛出异常
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
