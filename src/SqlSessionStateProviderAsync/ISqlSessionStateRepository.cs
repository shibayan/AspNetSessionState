﻿namespace Microsoft.AspNet.SessionState
{
    using System;
    using System.Threading.Tasks;
    using System.Web.SessionState;

    interface ISqlSessionStateRepository
    {
        void CreateSessionStateTable();

        void DeleteExpiredSessions();

        Task<SessionItem> GetStateIteAsync(string id, bool exclusive);

        Task CreateUpdateStateItemAsync(bool newItem, string id, byte[] buf, int length, int timeout, int lockCookie, int orginalStreamLen);

        Task ResetSessionItemTimeoutAsync(string id);

        Task RemoveSessionItemAsync(string id, object lockId);

        Task ReleaseSessionItemAsync(string id, object lockId);

        Task CreateUninitializedSessionItemAsync(string id, int length, byte[] buf, int timeout);
    }

    class SessionItem
    {
        public SessionItem(byte[] item, bool locked, TimeSpan lockAge, object lockId,
            SessionStateActions actions)
        {
            Item = item;
            Locked = locked;
            LockAge = lockAge;
            LockId = lockId;
            Actions = actions;
        }
        
        public byte[] Item { get; private set; }

        public bool Locked { get; private set; }
        
        public TimeSpan LockAge { get; private set; }
        
        public object LockId { get; private set; }

        public SessionStateActions Actions { get; private set; }
    }
}