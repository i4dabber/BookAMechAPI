﻿namespace BookAMech.Contracts.V1
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

            public const string Create = Base + "/reservations";
        }
    }
}
