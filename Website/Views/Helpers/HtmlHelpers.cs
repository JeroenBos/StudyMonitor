using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Views.Helpers
{
	public class HtmlHelpers
	{
		public static HtmlString Note(string content)
		{
			var result = $@"<div class='note' style='border: 1px solid black; width: 90%; padding: 5px; margin-left: 15px;'>	
								<p>	<strong> Note </strong> 
										&nbsp; &nbsp; {content}
								</p>
							</div> ";

			return new HtmlString(result);
		}
	}
}