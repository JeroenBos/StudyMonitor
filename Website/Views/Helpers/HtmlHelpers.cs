using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Views.Helpers
{
	public class HtmlHelpers
	{
		public static HtmlString CreateTask(int taskId, string taskName, bool taskIsOpen, bool anyOtherTaskIsOpen)
		{
			return CreateTask(taskId, taskName, hideButton: !taskIsOpen && anyOtherTaskIsOpen, buttonCaption: taskIsOpen ? "Stop" : "Start");
		}
		public static HtmlString CreateTask(int taskId, string taskName, bool hideButton, string buttonCaption)
		{
			var htmlResult =
				$@"<p class='body-content'>
						{taskId}, {taskName}
						<button type='button' id='{taskId}-button' onclick='myFunction({taskId})'{(hideButton ? " hidden = true" : "")}>{buttonCaption}</button>
				  </p>";
			return new HtmlString(htmlResult.Replace("\r\n", "").Replace("\n", ""));
		}
	}
}