using System;
using System.Threading;

namespace NikaArtist.Service.Utils
{
	public static class CultureHelper
	{
		public static bool IsEnCulture => Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.CurrentCultureIgnoreCase);
	}
}
