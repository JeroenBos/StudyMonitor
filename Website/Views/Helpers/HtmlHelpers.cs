using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Views.Helpers
{
	public class HtmlHelpers
	{
		public static HtmlString CreateTask(int taskId, bool taskIsOpen, bool anyOtherTaskIsOpen)
		{
			return CreateTask(taskId, hideButton: !taskIsOpen && anyOtherTaskIsOpen, buttonCaption: taskIsOpen ? "Stop" : "Start");
		}
		public static HtmlString CreateTask(int taskId, bool hideButton, string buttonCaption)
		{
			var htmlResult =
				$@"<p class='body - content'>
						taskId, taskName +
						<button type='button' id='{taskId}'-button' onclick='myFunction({taskId})' hidden='{hideButton}'>{buttonCaption}</button>
				  </p>";
			return new HtmlString(htmlResult);
		}
	}
}