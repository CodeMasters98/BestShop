namespace ProductService.Api.General
{
    public static class General
    {
        public static string FormatCellphone(this string str)
        {
            return str.Replace("+98","0").ToString();
        }
    }
}
