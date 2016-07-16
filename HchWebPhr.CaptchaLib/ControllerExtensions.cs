using System.Web.Mvc;

namespace HchWebPhr.CaptchaLib
{
	public static class ControllerExtensions
	{
		public static CaptchaResult Captcha(this Controller controller)
		{
			return Captcha(controller, new CaptchaImage());
		}

		public static CaptchaResult Captcha(this Controller controller, ICaptchaImage captchaImage, int quality = 50, int width = 150, int height = 45)
		{
			var captchaValue = captchaImage as ICaptchaValue;
			if (captchaValue != null)
				controller.Session[CaptchaConstants.CaptchaUniqueIdPrefix + captchaImage.CaptchaUniqueId] = captchaValue.RenderedValue;
			return new CaptchaResult(captchaImage, quality, width, height);
		}
	}
}