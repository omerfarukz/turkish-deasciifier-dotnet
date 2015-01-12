namespace OMR.Deasciifier
{
    public static class DeasciifyHelper
    {
        public static string Deasciify(this string asciiString)
        {
            var da = new Deasciifier(asciiString);
            return da.ConvertToTurkish();
        }
    }
}
