namespace Medium.Domain.Routes
{
    public static class MediumApiRoutes
    {
        /// <summary>
        /// The route to the GetToken Api method
        /// </summary>
        public const string Token = "https://api.medium.com/v1/tokens";
            

        /// <summary>
        /// The route to the Authorize method
        /// </summary>
        public const string Authorize = "https://medium.com/m/oauth/authorize";


        /// <summary>
        /// The route to the GetUserProfile Method
        /// </summary>
        public const string UserProfile = "https://api.medium.com/v1/me";
    }
}
