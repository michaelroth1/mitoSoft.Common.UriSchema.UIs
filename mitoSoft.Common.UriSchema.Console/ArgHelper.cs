namespace mitoSoft.Common.UriSchema.Console
{
    internal class ArgHelper
    {
        private readonly string[] _args;

        public ArgHelper(string[] args)
        {
            this._args = args;
        }

        public string? TryGetValue(params string[] argNames)
        {
            try
            {
                return this.GetValue(argNames);
            }
            catch
            {
                return null;
            }
        }

        public string GetValue(params string[] argNames)
        {
            if (argNames == null || argNames.Length == 0)
            {
                throw new Exception($"No params given.");
            }

            int idx = -1;
            foreach (var argName in argNames)
            {
                var i = this._args?.ToList().IndexOf(argName);
                if (i.HasValue)
                {
                    idx = i.Value;
                    break;
                }
            }

            if (idx == -1)
            {
                throw new Exception($"Argument '{argNames[0]}' not found.");
            }

            if (idx >= _args?.Length - 1)
            {
                throw new Exception($"No value found for Argument '{argNames[0]}'.");
            }

            var value = this._args?.ToList()[idx + 1]!;
            return value;
        }

        public bool IsAvailable(params string[] argNames)
        {
            if (argNames == null || argNames.Length == 0)
            {
                throw new Exception($"No params given.");
            }

            int idx = -1;
            foreach (var argName in argNames)
            {
                var i = this._args?.ToList().IndexOf(argName);
                if (i.HasValue)
                {
                    idx = i.Value;
                    break;
                }
            }

            if (idx == -1)
            {
                return false;
            }

            return true;
        }
    }
}