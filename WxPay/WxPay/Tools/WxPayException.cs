using System;


namespace WxPay.Tools
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg)
            : base(msg)
        {

        }
    }
}
