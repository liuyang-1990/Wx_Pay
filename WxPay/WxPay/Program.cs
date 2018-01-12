using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using WxPay.Tools;

namespace WxPay
{
    class Program
    {
        static void Main(string[] args)
        {
            WxPayData data = new WxPayData();
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            var outTradeNo = WxPayApi.GenerateOutTradeNo();
            data.SetValue("mch_billno", outTradeNo); //商户订单号
            data.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            data.SetValue("wxappid", WxPayConfig.APPID);//公众账号ID
            data.SetValue("sender_name", "张三");    //商户名称
            data.SetValue("sender_header_media_id", "1G6nrLmr5EC3MMb_-zK1dDdzmd0p7cNliYu9V5w7o8K0"); //发送者头像，此id为微信默认的头像
            string openid = WxPayTools.ConvertToOpenidByUserId(WxPayTools.GetAccessoken(), "1234567890");
            var openid_JObj = (JObject)JsonConvert.DeserializeObject(openid);
            data.SetValue("re_openid", openid_JObj["openid"].ToString());  //用户openid   
            data.SetValue("total_amount", 100);     //付款金额，单位分
            data.SetValue("wishing", "七夕情人节快乐！");       //红包祝福语
            data.SetValue("act_name", "XX活动");      //活动名称
            data.SetValue("remark", "快来抢");  //备注
            data.SetValue("scene_id", "PRODUCT_4");           //场景(金额大于200元时必填)
            data.SetValue("workwx_sign", data.MakeWorkWxSign("redPacket"));  //企业微信签名
            data.SetValue("sign", data.MakeSign());                   //微信支付签名
            string xml = data.ToXml();
            const string postUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendworkwxredpack";  //发送企业红包接口地址
            string response = WxPayTools.PostWebRequest(postUrl, xml, Encoding.UTF8, true);//调用HTTP通信接口提交数据到API
            WxPayData result = new WxPayData();
            result.FromXml(response);

        }
    }
}
