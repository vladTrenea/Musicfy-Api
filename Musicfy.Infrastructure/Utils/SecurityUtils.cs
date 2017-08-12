using System;

namespace Musicfy.Infrastructure.Utils
{
    public static class SecurityUtils
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateEntityId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}