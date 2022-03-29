namespace BookAMech.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const  string Base = Root + "/" + Version;

        public static class Reservations
        {
            public const string GetAll = Base + "/reservations";

            public const string Get = Base + "/reservations/{reservationId}";

            public const string Update = Base + "/reservations/{reservationId}";

            public const string Delete = Base + "/reservations/{reservationId}";

            public const string Create = Base + "/reservations";
        }

        public static class Users
        {
            public const string Login = Base + "/user/login";

            public const string Register = Base + "/user/register";
        }
    }
}
