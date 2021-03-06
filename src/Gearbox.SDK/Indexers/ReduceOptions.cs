﻿namespace Gearbox.Sdk.Indexers
{
    public class ReduceOptions
    {
        public bool ReduceByName = true;
        public bool ReduceBySize = true;
        public bool ReduceBySimilarName = true;
        public bool ReduceBySimilarPath = true;
        public bool GuessLikelySourceArchive = true;
        public string PreferredArchiveName;
    }
}
