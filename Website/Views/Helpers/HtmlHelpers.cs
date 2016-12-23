using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Views.Helpers
{
	public static class HtmlHelpers
	{
		public static string ToStringInSeconds(this TimeSpan totalTime)
		{
			return ((int)Math.Round(totalTime.TotalSeconds)).ToString();
		}
		public static HtmlString CreateTask(string taskId, string taskName, string totalTime, string estimate, bool taskIsOpen, bool anyOtherTaskIsOpen, bool taskHasTimeSpent)
		{
			return CreateTask(taskId, taskName, totalTime, estimate, hideButton: !taskIsOpen && anyOtherTaskIsOpen, buttonCaption: taskIsOpen ? "Stop" : taskHasTimeSpent ? "Continue" : "Start");
		}
		public static HtmlString CreateTask(string taskId, string taskName, string totalTime, string estimate, bool hideButton, string buttonCaption)
		{
			var htmlResult =
				$@" <tr  id='{taskId}-tr'>
						<p class='body-content'>
							<td align='right'>
								{taskName}:
							</td>
							<td width='350'>
								 {totalTime} seconds spent out of {estimate}.
							</td>
							<td width='120'>
								<button type='button' id='{taskId}-button' onclick='onClickTaskButton({taskId})'{(hideButton ? " hidden = true" : "")}>{buttonCaption}</button>
							</td>
							<td>
								<button type='button' onclick='removeTask({taskId})'>Remove</button>
							</td>
						</p>
					</tr>";
			return new HtmlString(htmlResult.Replace("\r\n", "").Replace("\n", ""));
		}
	}
}