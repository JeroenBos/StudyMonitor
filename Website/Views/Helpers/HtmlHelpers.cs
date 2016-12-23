using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Views.Helpers
{
	public class HtmlHelpers
	{
		public static string FormatTotalTime(TimeSpan totalTime)
		{
			string formatSpecifier = totalTime.Days == 0 ? @"hh\:mm\:ss" : @"dd\.hh\:mm\:ss";
			return totalTime.ToString(formatSpecifier);
		}
		public static HtmlString CreateTask(string taskId, string taskName, string totalTime, string estimate, bool taskIsOpen, bool anyOtherTaskIsOpen)
		{
			return CreateTask(taskId, taskName, totalTime, estimate, hideButton: !taskIsOpen && anyOtherTaskIsOpen, buttonCaption: taskIsOpen ? "Stop" : "Start");
		}
		public static HtmlString CreateTask(string taskId, string taskName, string totalTime, string estimate, bool hideButton, string buttonCaption)
		{
			var htmlResult =
				$@"<p class='body-content'>
						{taskId}, {taskName}, {totalTime}, {estimate}
						<button type='button' id='{taskId}-button' onclick='onClickTaskButton({taskId})'{(hideButton ? " hidden = true" : "")}>{buttonCaption}</button>
				  </p>";
			return new HtmlString(htmlResult.Replace("\r\n", "").Replace("\n", ""));
		}
	}
}