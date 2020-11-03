namespace CoreMentoringApp.WebSite.Logging
{
    public class LogEvents
    {
        public const int ListItems = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;


        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;

        public const int UnhandledException = 5000;
        public const int HandledException = 5001;

        public const int SaveItemToCache = 6001;
        public const int GetItemFromCache = 6002;
        public const int RemoveItemFromCache = 6003;
    }
}
