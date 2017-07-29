using MagicUpdater.DL.Common;
using MagicUpdaterCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Models
{
	public class ViewTaskModel
	{
		private Dictionary<ShedulerTaskModes, string> ShedulerTaskModesTranslate = new Dictionary<ShedulerTaskModes, string>
		{
			{ShedulerTaskModes.Daily, "Ежедневно"},
			{ShedulerTaskModes.Hours, "Каждый интервал (часы)"},
			{ShedulerTaskModes.Minutes, "Каждый интервал (минуты)"},
			{ShedulerTaskModes.Monthly, "Каждый месяц"},
			{ShedulerTaskModes.Once, "Один раз"},
			{ShedulerTaskModes.Weekly, "Каждую неделю"},
		};

		private Dictionary<ShedulerStatuses, string> ShedulerStatusesTranslate = new Dictionary<ShedulerStatuses, string>
		{
			{ShedulerStatuses.Competed, "Завершен"},
			{ShedulerStatuses.CompletedWithErrors, "Завершен с ошибками"},
			{ShedulerStatuses.New, "Новый"},
			{ShedulerStatuses.Running, "Выполняется"},
			{ShedulerStatuses.Stoped, "Остановлен"},
		};

		[NotMapRefreshingGridView]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		[NotMapRefreshingGridView]
		public int FirstStepId { get; set; }
		public string ModeVis => ShedulerTaskModesTranslate.ContainsKey(Mode) ? ShedulerTaskModesTranslate[Mode] : Mode.ToString();
		[NotMapRefreshingGridView]
		public int? RepeatValue { get; set; }
		public int? RepeatValueVis => Mode == ShedulerTaskModes.Hours || Mode == ShedulerTaskModes.Minutes ? RepeatValue : null;
		[NotMapRefreshingGridView]
		public ShedulerTaskModes Mode { get; set; }
		[NotMapRefreshingGridView]
		public DateTime StartTime { get; set; }
		[NotMapRefreshingGridView]
		public DateTime NextStartTime { get; set; }
		[SetCurrentTimeZome]
		public DateTime? NextStartTimeVis => NextStartTime == DateTime.Parse("1889-01-01 00:00:00.000") ? null : (DateTime?)NextStartTime;
		[SetCurrentTimeZome]
		public DateTime? LastStartTime { get; set; }
		[SetCurrentTimeZome]
		public DateTime? LastEndTime { get; set; }
		[NotMapRefreshingGridView]
		public ShedulerStatuses Status { get; set; }
		[NotMapRefreshingGridView]
		public string StatusVis => ShedulerStatusesTranslate.ContainsKey(Status) ? ShedulerStatusesTranslate[Status] : Status.ToString();
	}
}
