namespace RegisterSystem.Application.Common.Utils
{
    public static class LogUtils
    {
        public static string MaskSensitiveData(this string data, int unmaskedLength = 3)
        {
            if (string.IsNullOrWhiteSpace(data) || data.Length <= unmaskedLength)
            {
                return data;
            }

            return data.Substring(0, unmaskedLength) + new string('*', data.Length - unmaskedLength);
        }
    }
}
