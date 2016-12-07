using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class StudyTasks : IStudyTasks
	{
		private readonly List<StudyTask> tasks = new List<StudyTask>();

		public void Add(StudyTask task)
		{
			tasks.Add(task);
		}
	}
}
