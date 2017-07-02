using System.Collections.Generic;
using System.Linq;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Utils
{
    public class AuthorizationCache
    {
        private readonly object _instanceLock = new object();
        private bool _isInitialized;

        private Dictionary<string, UserAuthorizationModel> _authorizationModels;

        private AuthorizationCache()
        {
            _authorizationModels = new Dictionary<string, UserAuthorizationModel>();
        }

        public bool Initialized
        {
            get
            {
                lock (_instanceLock)
                {
                    return _isInitialized;
                }
            }
            private set
            {
                _isInitialized = value;
            }
        }

        public static AuthorizationCache Instance
        {
            get
            {
                return Nested.Instance;
            }
        }

        private static class Nested
        {
            static Nested() { }

            internal static readonly AuthorizationCache Instance = new AuthorizationCache();
        }

        #region Get

        public UserAuthorizationModel GetByToken(string token)
        {
            lock (_instanceLock)
            {
                if (!string.IsNullOrWhiteSpace(token) && _authorizationModels != null && _authorizationModels.Count > 0)
                {
                    return _authorizationModels[token];
                }
            }

            return null;
        }

        #endregion

        #region AddUpdate

        public void AddOrUpdateAuthorization(UserAuthorizationModel authorization)
        {
            if (authorization != null && !string.IsNullOrWhiteSpace(authorization.token))
            {
                lock (_instanceLock)
                {
                    if (!_authorizationModels.ContainsKey(authorization.token))
                    {
                        _authorizationModels.Add(authorization.token, authorization);
                    }
                    else
                    {
                        _authorizationModels[authorization.token] = authorization;
                    }
                }
            }
        }

        #endregion

        #region Log Out

        public void DeleteAuthorization(string token)
        {
            lock (_instanceLock)
            {
                if (_authorizationModels.ContainsKey(token))
                {
                    _authorizationModels.Remove(token);
                }
            }
        }

        #endregion

        public class AuthorizationCacheInitializer
        {
            public void Initialize(AuthorizationCache cache)
            {
                lock (cache._instanceLock)
                {
                    cache._authorizationModels = new Dictionary<string, UserAuthorizationModel>();

                    cache.Initialized = true;
                }
            }
        }
    }
}